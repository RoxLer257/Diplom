using Diplom.Classes;
using System;
using System.ComponentModel;
using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using System.Linq;

namespace Diplom.Pages.Login
{
    public partial class ForgotPass : Page, INotifyPropertyChanged
    {
        private string _generatedCode;
        private string _employeeEmail;
        private Visibility _emailStepVisibility = Visibility.Visible;
        private Visibility _codeStepVisibility = Visibility.Collapsed;
        private Visibility _passwordStepVisibility = Visibility.Collapsed;

        public Visibility EmailStepVisibility
        {
            get => _emailStepVisibility;
            set { _emailStepVisibility = value; OnPropertyChanged(nameof(EmailStepVisibility)); }
        }

        public Visibility CodeStepVisibility
        {
            get => _codeStepVisibility;
            set { _codeStepVisibility = value; OnPropertyChanged(nameof(CodeStepVisibility)); }
        }

        public Visibility PasswordStepVisibility
        {
            get => _passwordStepVisibility;
            set { _passwordStepVisibility = value; OnPropertyChanged(nameof(PasswordStepVisibility)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ForgotPass()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            InitializeComponent();
            DataContext = this; // Для привязки Visibility
        }

        private async void BtnSendCode_Click(object sender, RoutedEventArgs e)
        {
            string email = TxtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Введите email!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new VSK_DBEntities())
            {
                var employee = await context.Employees.FirstOrDefaultAsync(emp => emp.Email == email);

                if (employee == null)
                {
                    MessageBox.Show("Сотрудник с таким email не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _employeeEmail = email;

                // Генерируем код
                _generatedCode = new Random().Next(100000, 999999).ToString();

                // Отправляем код на email
                try
                {
                    SendCodeToEmail(email, _generatedCode);
                    MessageBox.Show("Код отправлен на ваш email!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при отправке кода: {ex.Message}\nПодробности: {ex.InnerException?.Message ?? "Нет дополнительных данных"}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Переключаемся на шаг ввода кода (только если отправка успешна)
                if (string.IsNullOrEmpty(TxtCode.Text)) // Проверка, чтобы избежать случайного перехода
                {
                    EmailStepVisibility = Visibility.Collapsed;
                    CodeStepVisibility = Visibility.Visible;
                    PasswordStepVisibility = Visibility.Collapsed;

                    // Логируем отправку кода
                    LogAction(context, "Employees", "Восстановление пароля",
                        $"Сотруднику {employee.FullName} (ID: {employee.EmployeeID}) отправлен код на {email}",
                        employee.EmployeeID);
                }
            }
        }

        public void SendCodeToEmail(string recipientEmail, string code)
        {
            var fromAddress = new MailAddress("diplomtestvsk@yandex.ru", "САО ВСК");
            var toAddress = new MailAddress(recipientEmail);
            const string fromPassword = "ucgejgipwdkfmyea"; // желательно использовать app-password

            const string subject = "Восстановление пароля — САО ВСК";

            string textBody = $"Здравствуйте!\n\nВы запросили восстановление пароля. Ваш код подтверждения: {code}\n\nЕсли вы не запрашивали восстановление — проигнорируйте это письмо.\n\nС уважением,\nКоманда ВСК.";
            string htmlBody = $"<p>Здравствуйте!</p><p>Вы запросили восстановление пароля. Ваш код подтверждения: <b>{code}</b></p><p>Если вы не запрашивали восстановление, просто проигнорируйте это письмо.</p><p>С уважением,<br>САО Страховой Дом ВСК.</p>";

            var smtp = new SmtpClient
            {
                Host = "smtp.yandex.ru",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage())
            {
                message.From = fromAddress;
                message.To.Add(toAddress);
                message.Subject = subject;
                message.IsBodyHtml = true;

                // Добавим как HTML, так и обычный текст
                var altViewPlain = AlternateView.CreateAlternateViewFromString(textBody, null, "text/plain");
                var altViewHtml = AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html");
                message.AlternateViews.Add(altViewPlain);
                message.AlternateViews.Add(altViewHtml);

                smtp.Send(message);
            }
        }

        private void BtnVerifyCode_Click(object sender, RoutedEventArgs e)
        {
            string enteredCode = TxtCode.Text.Trim();

            if (string.IsNullOrEmpty(enteredCode))
            {
                MessageBox.Show("Введите код!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (enteredCode == _generatedCode)
            {
                // Код верный, переходим к вводу нового пароля
                EmailStepVisibility = Visibility.Collapsed;
                CodeStepVisibility = Visibility.Collapsed;
                PasswordStepVisibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Неверный код!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void BtnSavePassword_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = TxtNewPassword.Password.Trim();
            string confirmPassword = TxtConfirmPassword.Password.Trim();

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Введите новый пароль и подтверждение!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new VSK_DBEntities())
            {
                var employee = await context.Employees.FirstOrDefaultAsync(emp => emp.Email == _employeeEmail);
                if (employee != null)
                {
                    // Обновляем пароль
                    employee.Password = newPassword;
                    await context.SaveChangesAsync();

                    // Логируем изменение пароля
                    LogAction(context, "Employees", "Изменение пароля",
                        $"Сотрудник {employee.FullName} (ID: {employee.EmployeeID}) изменил пароль",
                        employee.EmployeeID);

                    MessageBox.Show("Пароль успешно изменен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Сохраняем данные сотрудника в CurrentUser
                    CurrentUser.EmployeeID = employee.EmployeeID;
                    CurrentUser.FullName = employee.FullName;
                    CurrentUser.Email = employee.Email;
                    CurrentUser.Password = employee.Password;
                    CurrentUser.RoleName = context.Roles.FirstOrDefault(r => r.RoleID == employee.RoleID)?.RoleName;

                    // Перенаправляем на соответствующую страницу
                    if (employee.EmployeeID == 1) // Администратор
                    {
                        ClassFrame.frmObj.Navigate(new AdminPage());
                    }
                    else // Обычный пользователь
                    {
                        ClassFrame.frmObj.Navigate(new MainPage.MainPage());
                    }
                }
            }
        }

        private void LogAction(VSK_DBEntities context, string tableName, string action, string changedData, int employeeID)
        {
            var log = new Logs
            {
                TableName = tableName,
                Action = action,
                ChangedData = changedData,
                ChangeDate = DateTime.Now,
                UserName = CurrentUser.FullName ?? "System",
                EmployeeID = employeeID
            };
            context.Logs.Add(log);
            context.SaveChanges();
        }

        // Обработчики для плейсхолдеров
        private void TxtEmail_GotFocus(object sender, RoutedEventArgs e) => UpdatePlaceholder(sender as TextBox, EmailPlaceholder);
        private void TxtEmail_LostFocus(object sender, RoutedEventArgs e) => UpdatePlaceholder(sender as TextBox, EmailPlaceholder);
        private void TxtEmail_TextChanged(object sender, TextChangedEventArgs e) => UpdatePlaceholder(sender as TextBox, EmailPlaceholder);

        private void TxtCode_GotFocus(object sender, RoutedEventArgs e) => UpdatePlaceholder(sender as TextBox, CodePlaceholder);
        private void TxtCode_LostFocus(object sender, RoutedEventArgs e) => UpdatePlaceholder(sender as TextBox, CodePlaceholder);
        private void TxtCode_TextChanged(object sender, TextChangedEventArgs e) => UpdatePlaceholder(sender as TextBox, CodePlaceholder);

        private void TxtNewPassword_GotFocus(object sender, RoutedEventArgs e) => UpdatePasswordPlaceholder(sender as PasswordBox, NewPasswordPlaceholder);
        private void TxtNewPassword_LostFocus(object sender, RoutedEventArgs e) => UpdatePasswordPlaceholder(sender as PasswordBox, NewPasswordPlaceholder);
        private void TxtNewPassword_PasswordChanged(object sender, RoutedEventArgs e) => UpdatePasswordPlaceholder(sender as PasswordBox, NewPasswordPlaceholder);

        private void TxtConfirmPassword_GotFocus(object sender, RoutedEventArgs e) => UpdatePasswordPlaceholder(sender as PasswordBox, ConfirmPasswordPlaceholder);
        private void TxtConfirmPassword_LostFocus(object sender, RoutedEventArgs e) => UpdatePasswordPlaceholder(sender as PasswordBox, ConfirmPasswordPlaceholder);
        private void TxtConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e) => UpdatePasswordPlaceholder(sender as PasswordBox, ConfirmPasswordPlaceholder);

        private void UpdatePlaceholder(TextBox textBox, TextBlock placeholder)
        {
            if (placeholder != null)
            {
                placeholder.Visibility = string.IsNullOrWhiteSpace(textBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void UpdatePasswordPlaceholder(PasswordBox passwordBox, TextBlock placeholder)
        {
            if (placeholder != null)
            {
                placeholder.Visibility = string.IsNullOrWhiteSpace(passwordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}