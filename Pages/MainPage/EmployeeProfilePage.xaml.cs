using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Diplom.Classes;

namespace Diplom.Pages.MainPage
{
    public partial class EmployeeProfilePage : Page
    {
        private readonly VSK_DBEntities _context;
        private Employees _currentEmployee;

        public EmployeeProfilePage()
        {
            InitializeComponent();

            _context = ClassFrame.ConnectDB;
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            try
            {
                // Загружаем данные сотрудника из базы данных по EmployeeID из CurrentUser
                _currentEmployee = _context.Employees
                    .FirstOrDefault(emp => emp.EmployeeID == CurrentUser.EmployeeID);

                // Заполняем поля данными сотрудника
                FullNameTextBox.Text = _currentEmployee.FullName;
                EmailTextBox.Text = _currentEmployee.Email;
                RoleTextBox.Text = _context.Roles
                    .FirstOrDefault(r => r.RoleID == _currentEmployee.RoleID)?.RoleName ?? "Не указана";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            // Показываем панель для ввода нового пароля
            PasswordChangePanel.Visibility = Visibility.Visible;
            ChangePasswordButton.Visibility = Visibility.Collapsed; // Скрываем кнопку "Поменять пароль"
        }

        private void SavePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем, введён ли новый пароль
                string newPassword = NewPasswordBox.Password.Trim();
                string confirmPassword = ConfirmPasswordBox.Password.Trim();

                if (string.IsNullOrEmpty(newPassword))
                {
                    MessageBox.Show("Пожалуйста, введите новый пароль.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверяем длину пароля
                if (newPassword.Length < 6)
                {
                    MessageBox.Show("Новый пароль должен содержать не менее 6 символов.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверяем совпадение паролей
                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("Пароли не совпадают. Пожалуйста, проверьте введённые данные.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Обновляем пароль сотрудника
                _currentEmployee.Password = newPassword;
                CurrentUser.Password = newPassword; // Обновляем пароль в CurrentUser

                // Логирование изменения пароля
                LogAction("Employees", "Изменение", $"Сотрудник {CurrentUser.FullName} изменил пароль.");

                // Сохраняем изменения в базе данных
                _context.SaveChanges();

                MessageBox.Show("Пароль успешно изменён!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Скрываем панель и очищаем поля
                PasswordChangePanel.Visibility = Visibility.Collapsed;
                ChangePasswordButton.Visibility = Visibility.Visible;
                NewPasswordBox.Clear();
                ConfirmPasswordBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении нового пароля: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LogAction(string tableName, string action, string changedData)
        {
            var log = new Logs
            {
                TableName = tableName,
                Action = action,
                ChangedData = changedData,
                ChangeDate = DateTime.Now,
                UserName = CurrentUser.Email ?? "System",
                EmployeeID = CurrentUser.EmployeeID
            };
            _context.Logs.Add(log);
            _context.SaveChanges();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Возвращаемся на главную страницу
            NavigationService.GoBack();
        }
    }
}