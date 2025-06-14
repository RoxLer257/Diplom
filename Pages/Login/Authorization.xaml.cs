using Diplom.Classes;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Diplom.Pages.Login
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void TxtLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtLogin.Text == string.Empty)
            {
                LoginPlaceholder.Visibility = Visibility.Hidden;
            }
        }

        private void TxtLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TxtLogin.Text == string.Empty)
            {
                LoginPlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void TxtLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtLogin.Text.Length > 0)
            {
                LoginPlaceholder.Visibility = Visibility.Hidden;
            }
            else
            {
                LoginPlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void TxtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtPassword.Password == string.Empty)
            {
                PassPlaceholder.Visibility = Visibility.Hidden;
            }
        }

        private void TxtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TxtPassword.Password == string.Empty)
            {
                PassPlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (TxtPassword.Password.Length > 0)
            {
                PassPlaceholder.Visibility = Visibility.Hidden;
            }
            else
            {
                PassPlaceholder.Visibility = Visibility.Visible;
            }
        }

        private async void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            TxtLogin.Background = Brushes.White;
            TxtPassword.Background = Brushes.White;
            TxtLogin.ToolTip = null;
            TxtPassword.ToolTip = null;

            string login = TxtLogin.Text.Trim(); 
            string password = TxtPassword.Password.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                if (string.IsNullOrEmpty(login))
                {
                    TxtLogin.Background = Brushes.Red;
                    TxtLogin.ToolTip = "Поле логина не может быть пустым";
                }
                if (string.IsNullOrEmpty(password))
                {
                    TxtPassword.Background = Brushes.Red;
                    TxtPassword.ToolTip = "Поле пароля не может быть пустым";
                }
                ResetFieldStyles(); 
                return;
            }

            try
            {
                using (var context = new VSK_DBEntities())
                {
                    var employee = await context.Employees
                        .FirstOrDefaultAsync(emp => emp.Email == login && emp.Password == password);

                    if (employee == null)
                    {
                        var userExists = await context.Employees.AnyAsync(emp => emp.Email == login);
                        if (userExists)
                        {
                            TxtPassword.Background = Brushes.Red;
                            TxtPassword.ToolTip = "Неверный пароль";
                            MessageBox.Show("Неправильный пароль", "Ошибка при авторизации",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            ResetFieldStyles();
                        }
                        else
                        {
                            TxtLogin.Background = Brushes.Red;
                            TxtLogin.ToolTip = "Неверный логин";
                            MessageBox.Show("Неверный логин", "Ошибка при авторизации",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            ResetFieldStyles();
                        }
                        return;
                    }

                    var log = new Logs
                    {
                        TableName = "Employees",
                        Action = "Авторизация",
                        ChangedData = $"Сотрудник {employee.FullName} (ID: {employee.EmployeeID}) вошёл в систему",
                        ChangeDate = DateTime.Now,
                        UserName = employee.Email,
                        EmployeeID = employee.EmployeeID
                    };
                    context.Logs.Add(log);
                    await context.SaveChangesAsync();

                    ClassFrame.RoleID = employee.EmployeeID; 
                    CurrentUser.EmployeeID = employee.EmployeeID; 
                    CurrentUser.FullName = employee.FullName;
                    CurrentUser.Email = employee.Email;
                    CurrentUser.Password = employee.Password; 
                    CurrentUser.RoleName = context.Roles.FirstOrDefault(r => r.RoleID == employee.RoleID)?.RoleName;

                    if (employee.EmployeeID == 1) 
                    {
                        ClassFrame.frmObj.Navigate(new AdminPage());
                    }
                    else 
                    {
                        ClassFrame.frmObj.Navigate(new MainPage.MainPage());
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при авторизации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void ResetFieldStyles()
        {
            await Task.Delay(1250); 
            TxtLogin.Background = Brushes.White;
            TxtPassword.Background = Brushes.White;
            TxtLogin.ToolTip = null;
            TxtPassword.ToolTip = null;
        }

        private void BtnForgotPass_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new Pages.Login.ForgotPass());
        }
    }
}
