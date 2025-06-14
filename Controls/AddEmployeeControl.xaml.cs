﻿using Diplom.Classes;
using Diplom.Classes.ControlEventArgs;
using Diplom.Classes.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Diplom.Controls
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeeControl.xaml
    /// </summary>
    public partial class AddEmployeeControl : UserControl
    {
        public event EventHandler<EmployeeEventArgs> EmployeeAdded;

        public event EventHandler Canceled;

        private Employees _editingEmployee;
        public AddEmployeeControl()
        {
            InitializeComponent();
            InitializePlaceholders();
            LoadCmb();
        }
        private void LoadCmb()
        {
            var roles = ClassFrame.ConnectDB.Roles.ToList();
            RoleIdCmb.ItemsSource = roles;
            RoleIdCmb.DisplayMemberPath = "RoleName";
            RoleIdCmb.SelectedValuePath = "RoleID";
        }

        public void SetEmployeeForEdit(Employees employee)
        {
            _editingEmployee = employee;

            LastNameBox.Text = employee.LastName;
            FirstNameBox.Text = employee.FirstName;
            MiddleNameBox.Text = employee.MiddleName;
            EmailBox.Text = employee.Email;
            PhoneBox.Text = employee.Phone;

            RoleIdCmb.SelectedValue = employee.RoleID;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string EmpLastName = LastNameBox.Text.Trim();
            string EmpFirstName = FirstNameBox.Text.Trim();
            string EmpMiddleName = MiddleNameBox.Text.Trim();
            string EmpEmail = EmailBox.Text.Trim();
            string EmpPhone = PhoneBox.Text.Trim();
            string EmpPassword = PasswordBox.Password.Trim();

            var validator = new EmployeeValidator();
            var validationResult = validator.ValidateEmployee(
                EmpLastName,
                EmpFirstName,
                EmpMiddleName,
                EmpEmail,
                EmpPhone,
                EmpPassword,
                RoleIdCmb.SelectedItem as Roles
            );

            if (!validationResult.IsValid)
            {
                MessageBox.Show(validationResult.ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var db = ClassFrame.ConnectDB;
            Employees employee;

            if (_editingEmployee == null)
            {
                employee = new Employees
                {
                    LastName = EmpLastName,
                    FirstName = EmpFirstName,
                    MiddleName = EmpMiddleName,
                    Email = EmpEmail,
                    Phone = EmpPhone,
                    Password = EmpPassword,
                    RoleID = (RoleIdCmb.SelectedItem as Roles).RoleID
                };
                db.Employees.Add(employee);
            }
            else
            {
                employee = _editingEmployee;
                employee.LastName = EmpLastName;
                employee.FirstName = EmpFirstName;
                employee.MiddleName = EmpMiddleName;
                employee.Email = EmpEmail;
                employee.Phone = EmpPhone;
                employee.RoleID = (RoleIdCmb.SelectedItem as Roles).RoleID;

                if (!string.IsNullOrEmpty(EmpPassword))
                    employee.Password = EmpPassword;
            }

            db.SaveChanges();

            EmployeeAdded?.Invoke(this, new EmployeeEventArgs(employee));
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            Canceled?.Invoke(this, EventArgs.Empty); 
        }

        private void InitializePlaceholders()
        {
            UpdateTextBoxPlaceholder(LastNameBox, LastNamePlaceholder);
            UpdateTextBoxPlaceholder(FirstNameBox, FirstNamePlaceholder);
            UpdateTextBoxPlaceholder(MiddleNameBox, MiddleNamePlaceholder);
            UpdateTextBoxPlaceholder(EmailBox, EmailPlaceholder);
            UpdateTextBoxPlaceholder(PhoneBox, PhonePlaceholder);

            UpdateComboBoxPlaceholder(RoleIdCmb, RoleIdPlaceholder);

            UpdatePasswordBoxPlaceholder(PasswordBox, PasswordBoxPlaceholder);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                var placeholder = textBox.Tag as TextBlock;
                if (placeholder != null)
                {
                    placeholder.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                UpdateTextBoxPlaceholder(textBox, textBox.Tag as TextBlock);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                UpdateTextBoxPlaceholder(textBox, textBox.Tag as TextBlock);
            }
        }
        private void UpdateTextBoxPlaceholder(TextBox textBox, TextBlock placeholder)
        {
            if (placeholder != null)
            {
                placeholder.Visibility = string.IsNullOrWhiteSpace(textBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void RoleIdCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                UpdateComboBoxPlaceholder(comboBox, comboBox.Tag as TextBlock);
            }
        }
        private void UpdateComboBoxPlaceholder(ComboBox comboBox, TextBlock placeholder)
        {
            if (placeholder != null)
            {
                placeholder.Visibility = comboBox.SelectedItem == null ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password.Length > 0)
            {
                PasswordBoxPlaceholder.Visibility = Visibility.Hidden;
            }
            else
            {
                PasswordBoxPlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                var placeholder = passwordBox.Tag as TextBlock;
                if (placeholder != null)
                {
                    placeholder.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                UpdatePasswordBoxPlaceholder(passwordBox, passwordBox.Tag as TextBlock);
            }
        }
        private void UpdatePasswordBoxPlaceholder(PasswordBox passwordBox, TextBlock placeholder)
        {
            if (placeholder != null)
            {
                placeholder.Visibility = string.IsNullOrWhiteSpace(passwordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void ClearForm()
        {
            LastNameBox.Clear();
            FirstNameBox.Clear();
            MiddleNameBox.Clear();
            EmailBox.Clear();
            PhoneBox.Clear();
            PasswordBox.Clear();
            RoleIdCmb.SelectedIndex = -1;
            _editingEmployee = null;
        }
    }
}
