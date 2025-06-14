using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Diplom.Classes;
using Diplom.Classes.Validator;
using System.Data.Entity;

namespace Diplom.Pages.AddPrintPage
{
    public partial class AddLifeInsurancePage : Page
    {
        private readonly VSK_DBEntities _context;
        private List<Clients> _selectedClients; 
        private readonly LifeInsuranceValidator _lifeInsuranceValidator;
        private readonly ClientValidator _clientValidator;
        private bool isEditMode = false;
        private Policies editablePolicy;

        public AddLifeInsurancePage()
        {
            InitializeComponent();

            _context = ClassFrame.ConnectDB;
            _selectedClients = new List<Clients>(); 
            _lifeInsuranceValidator = new LifeInsuranceValidator(_context);
            _clientValidator = new ClientValidator(_context);

            LoadData();
            InitializePlaceholders();
            SetPolicyType("Страхование жизни");
        }

        public AddLifeInsurancePage(Policies policyToEdit) : this()
        {
            isEditMode = true;
            editablePolicy = policyToEdit;
            LoadPolicyData(policyToEdit);
        }

        private void SetPolicyType(string typeName)
        {
            foreach (var item in PolicyTypeComboBox.Items)
            {
                var policytype = item as PolicyTypes;
                if(policytype != null && policytype.TypeName == typeName)
                {
                    PolicyTypeComboBox.SelectedItem = item;
                    break;
                }
            }
        }

        private void LoadData()
        {
            var policyTypes = _context.PolicyTypes.ToList();
            PolicyTypeComboBox.ItemsSource = policyTypes;

            var policyStatuses = _context.PolicyStatuses.ToList();
            StatusComboBox.ItemsSource = policyStatuses;

            var healthConditions = _context.HealthConditions.ToList();
            HealthConditionComboBox.ItemsSource = healthConditions;

            var clientTypes = _context.ClientTypes.ToList();
            ClientTypeComboBox.ItemsSource = clientTypes;
        }

        private void InitializePlaceholders()
        {
            UpdateComboBoxPlaceholder(StatusComboBox, StatusPlaceholder);
            UpdateComboBoxPlaceholder(GenderComboBox, GenderPlaceholder);
            UpdateComboBoxPlaceholder(HealthConditionComboBox, HealthConditionPlaceholder);
            UpdateComboBoxPlaceholder(ClientTypeComboBox, ClientTypePlaceholder);

            UpdateTextBoxPlaceholder(InsuranceAmountTextBox, InsuranceAmountPlaceholder);
            UpdateTextBoxPlaceholder(AgeTextBox, AgePlaceholder);
            UpdateTextBoxPlaceholder(OccupationTextBox, OccupationPlaceholder);
            UpdateTextBoxPlaceholder(LastNameTextBox, LastNamePlaceholder);
            UpdateTextBoxPlaceholder(FirstNameTextBox, FirstNamePlaceholder);
            UpdateTextBoxPlaceholder(MiddleNameTextBox, MiddleNamePlaceholder);
            UpdateTextBoxPlaceholder(CompanyNameTextBox, CompanyNamePlaceholder);
            UpdateTextBoxPlaceholder(PassportNumberTextBox, PassportNumberPlaceholder);
            UpdateTextBoxPlaceholder(INNTextBox, INNPlaceholder);
            UpdateTextBoxPlaceholder(PhoneTextBox, PhonePlaceholder);
            UpdateTextBoxPlaceholder(EmailTextBox, EmailPlaceholder);
        }

        private void HideNewClientPanelButton_Click(object sender, RoutedEventArgs e)
        {
            ClearClientForm();
            NewClientPanel.Visibility = Visibility.Collapsed;
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == ClientTypeComboBox)
            {
                if (ClientTypeComboBox.SelectedItem is ClientTypes selectedType)
                {
                    if (selectedType.ClientTypeID == 1) 
                    {
                        GrdLastName.Visibility = Visibility.Visible;
                        GrdFirstName.Visibility = Visibility.Visible;
                        GrdMiddleName.Visibility = Visibility.Visible;
                        GrdCompany.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedType.ClientTypeID == 2) 
                    {
                        GrdLastName.Visibility = Visibility.Collapsed;
                        GrdFirstName.Visibility = Visibility.Collapsed;
                        GrdMiddleName.Visibility = Visibility.Collapsed;
                        GrdCompany.Visibility = Visibility.Visible;
                    }
                }
            }
            UpdateComboBoxPlaceholder(ClientTypeComboBox, ClientTypePlaceholder);
           
            UpdateComboBoxPlaceholder(StatusComboBox, StatusPlaceholder);
            UpdateComboBoxPlaceholder(GenderComboBox, GenderPlaceholder);
            UpdateComboBoxPlaceholder(HealthConditionComboBox, HealthConditionPlaceholder);
        }

        private void UpdateComboBoxPlaceholder(ComboBox comboBox, TextBlock placeholder)
        {
            if (placeholder != null)
            {
                placeholder.Visibility = comboBox.SelectedItem == null ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DatePicker datePicker)
            {
                UpdateDatePickerPlaceholder(datePicker, datePicker.Tag as TextBlock);
            }
        }

        private void UpdateDatePickerPlaceholder(DatePicker datePicker, TextBlock placeholder)
        {
            if (placeholder != null)
            {
                placeholder.Visibility = datePicker.SelectedDate == null ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedClients.Any())
            {
                MessageBox.Show("Можно добавить только одного клиента.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var clients = _context.Clients.ToList();
            var selectClientWindow = new Window
            {
                Title = "Выберите клиента",
                Width = 300,
                Height = 400,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize
            };

            var stackPanel = new StackPanel { Margin = new Thickness(10) };
            var clientComboBox = new ComboBox
            {
                DisplayMemberPath = "DisplayName",
                SelectedValuePath = "ClientID",
                ItemsSource = clients,
                Margin = new Thickness(0, 0, 0, 10)
            };
            var addButton = new Button
            {
                Content = "Добавить",
                Width = 100,
                Height = 30,
                Margin = new Thickness(0, 10, 0, 0)
            };
            addButton.Click += (s, args) =>
            {
                if (clientComboBox.SelectedItem is Clients selectedClient)
                {
                    if (!_selectedClients.Any(c => c.ClientID == selectedClient.ClientID))
                    {
                        _selectedClients.Add(selectedClient);
                        ClientsListBox.ItemsSource = null;
                        ClientsListBox.ItemsSource = _selectedClients;
                        RemoveClientButton.Visibility = Visibility.Visible;
                        AddClientButton.Visibility = Visibility.Collapsed;
                    }
                    selectClientWindow.Close();
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите клиента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            };

            stackPanel.Children.Add(clientComboBox);
            stackPanel.Children.Add(addButton);
            selectClientWindow.Content = stackPanel;
            selectClientWindow.ShowDialog();
        }

        private void RemoveClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsListBox.SelectedItem is Clients selectedClient)
            {
                _selectedClients.Remove(selectedClient);
                ClientsListBox.ItemsSource = null;
                ClientsListBox.ItemsSource = _selectedClients;
                RemoveClientButton.Visibility = Visibility.Collapsed;
                AddClientButton.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите клиента для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void ShowNewClientPanelButton_Click(object sender, RoutedEventArgs e)
        {
            NewClientPanel.Visibility = Visibility.Visible;

            GrdLastName.Visibility = Visibility.Visible;
            GrdFirstName.Visibility = Visibility.Visible;
            GrdMiddleName.Visibility = Visibility.Visible;
            GrdCompany.Visibility = Visibility.Collapsed;

            ClientTypeComboBox.SelectedIndex = -1; 
        }
        private void CreateClientButton_Click(object sender, RoutedEventArgs e)
        {
            var validationResult = _clientValidator.ValidateClient(
                ClientTypeComboBox.SelectedItem as ClientTypes,
                LastNameTextBox.Text,
                FirstNameTextBox.Text,
                MiddleNameTextBox.Text,
                CompanyNameTextBox.Text,
                PassportNumberTextBox.Text,
                INNTextBox.Text,
                PhoneTextBox.Text,
                EmailTextBox.Text);

            if (!validationResult.IsValid)
            {
                MessageBox.Show(validationResult.ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var newClient = new Clients
                {
                    ClientTypeID = (ClientTypeComboBox.SelectedItem as ClientTypes).ClientTypeID,
                    AgentID = CurrentUser.EmployeeID,
                    LastName = LastNameTextBox.Text,
                    FirstName = FirstNameTextBox.Text,
                    MiddleName = MiddleNameTextBox.Text,
                    CompanyName = CompanyNameTextBox.Text,
                    PassportNumber = PassportNumberTextBox.Text,
                    INN = INNTextBox.Text,
                    Phone = PhoneTextBox.Text,
                    Email = EmailTextBox.Text
                };

                _context.Clients.Add(newClient);
                _context.SaveChanges();

                LogAction("Clients", "Добавление", $"Добавлен клиент: {newClient.ClientID}");

                if (!_selectedClients.Any())
                {
                    _selectedClients.Add(newClient);
                    ClientsListBox.ItemsSource = null;
                    ClientsListBox.ItemsSource = _selectedClients;
                    AddClientButton.Visibility = Visibility.Collapsed;
                    RemoveClientButton.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Клиент успешно создан, но в полис можно добавить только одного клиента.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                ClearClientForm();
                NewClientPanel.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearClientForm()
        {
            ClientTypeComboBox.SelectedItem = null;
            LastNameTextBox.Text = string.Empty;
            FirstNameTextBox.Text = string.Empty;
            MiddleNameTextBox.Text = string.Empty;
            CompanyNameTextBox.Text = string.Empty;
            PassportNumberTextBox.Text = string.Empty;
            INNTextBox.Text = string.Empty;
            PhoneTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;

            UpdateComboBoxPlaceholder(ClientTypeComboBox, ClientTypePlaceholder);
            UpdateTextBoxPlaceholder(LastNameTextBox, LastNamePlaceholder);
            UpdateTextBoxPlaceholder(FirstNameTextBox, FirstNamePlaceholder);
            UpdateTextBoxPlaceholder(MiddleNameTextBox, MiddleNamePlaceholder);
            UpdateTextBoxPlaceholder(CompanyNameTextBox, CompanyNamePlaceholder);
            UpdateTextBoxPlaceholder(PassportNumberTextBox, PassportNumberPlaceholder);
            UpdateTextBoxPlaceholder(INNTextBox, INNPlaceholder);
            UpdateTextBoxPlaceholder(PhoneTextBox, PhonePlaceholder);
            UpdateTextBoxPlaceholder(EmailTextBox, EmailPlaceholder);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int? age = null;
            if (int.TryParse(AgeTextBox.Text, out int parsedAge))
            {
                age = parsedAge;
            }
            string gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            HealthConditions healthCondition = HealthConditionComboBox.SelectedItem as HealthConditions;
            string occupation = OccupationTextBox.Text;
            decimal? insuranceAmount = null;
            if (decimal.TryParse(InsuranceAmountTextBox.Text, out decimal parsedAmount))
            {
                insuranceAmount = parsedAmount;
            }
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;
            PolicyTypes policyType = PolicyTypeComboBox.SelectedItem as PolicyTypes;
            PolicyStatuses status = StatusComboBox.SelectedItem as PolicyStatuses;

            var validationResult = _lifeInsuranceValidator.ValidateLifeInsurance(
                age,
                gender,
                healthCondition,
                occupation,
                insuranceAmount,
                startDate,
                endDate,
                policyType,
                status,
                _selectedClients);

            if (!validationResult.IsValid)
            {
                MessageBox.Show(validationResult.ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Policies policy;
                if (isEditMode)
                {
                    policy = editablePolicy;
                    policy.PolicyTypeID = policyType.PolicyTypeID;
                    policy.StatusID = status.StatusID;
                    policy.InsuranceAmount = insuranceAmount.Value;
                    policy.StartDate = startDate.Value;
                    policy.EndDate = endDate.Value;

                    policy.Clients.Clear();
                    foreach (var client in _selectedClients)
                    {
                        policy.Clients.Add(client);
                    }

                    var existingLifeAndHealth = _context.LifeAndHealth.FirstOrDefault(lh => lh.PolicyID == policy.PolicyID);
                    if (existingLifeAndHealth != null)
                    {
                        existingLifeAndHealth.Age = age.Value;
                        existingLifeAndHealth.Gender = gender;
                        existingLifeAndHealth.HealthConditionID = healthCondition.HealthConditionID;
                        existingLifeAndHealth.Occupation = occupation;
                    }

                    LogAction("Policies", "Редактирование", $"Полис {policy.PolicyID} обновлён");
                    LogAction("LifeAndHealth", "Редактирование", $"Обновлена запись для полиса: {policy.PolicyID}");
                }
                else
                {
                    policy = new Policies
                    {
                        PolicyTypeID = policyType.PolicyTypeID,
                        StatusID = status.StatusID,
                        InsuranceAmount = insuranceAmount.Value,
                        StartDate = startDate.Value,
                        EndDate = endDate.Value
                    };

                    foreach (var client in _selectedClients)
                    {
                        policy.Clients.Add(client);
                    }

                    _context.Policies.Add(policy);
                    _context.SaveChanges();

                    var lifeAndHealth = new LifeAndHealth
                    {
                        PolicyID = policy.PolicyID,
                        Age = age.Value,
                        Gender = gender,
                        HealthConditionID = healthCondition.HealthConditionID,
                        Occupation = occupation
                    };

                    _context.LifeAndHealth.Add(lifeAndHealth);
                    LogAction("Policies", "Добавление", $"Добавлен полис: {policy.PolicyID}");
                    LogAction("LifeAndHealth", "Добавление", $"Добавлена запись для полиса: {policy.PolicyID}");
                }

                _context.SaveChanges();
                MessageBox.Show("Полис успешно сохранён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearForm();
                Classes.ClassFrame.frmObj.Navigate(new Pages.MainPage.MainPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearForm()
        {
            _selectedClients.Clear();
            ClientsListBox.ItemsSource = null;
            PolicyTypeComboBox.SelectedItem = null;
            StatusComboBox.SelectedItem = null;
            InsuranceAmountTextBox.Text = string.Empty;
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
            AgeTextBox.Text = string.Empty;
            GenderComboBox.SelectedItem = null;
            HealthConditionComboBox.SelectedItem = null;
            OccupationTextBox.Text = string.Empty;

            ClearClientForm();
            InitializePlaceholders();
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            NavigationService.GoBack();
        }

        private void LoadPolicyData(Policies policy)
        {
            try
            {
                var policyType = _context.PolicyTypes.FirstOrDefault(pt => pt.PolicyTypeID == policy.PolicyTypeID);
                PolicyTypeComboBox.SelectedItem = policyType;

                var status = _context.PolicyStatuses.FirstOrDefault(ps => ps.StatusID == policy.StatusID);
                StatusComboBox.SelectedItem = status;
                UpdateComboBoxPlaceholder(StatusComboBox, StatusPlaceholder);

                StartDatePicker.SelectedDate = policy.StartDate;
                EndDatePicker.SelectedDate = policy.EndDate;

                InsuranceAmountTextBox.Text = policy.InsuranceAmount.ToString();
                UpdateTextBoxPlaceholder(InsuranceAmountTextBox, InsuranceAmountPlaceholder);

                _selectedClients.Clear();
                foreach (var client in policy.Clients)
                {
                    _selectedClients.Add(client);
                }
                ClientsListBox.ItemsSource = null;
                ClientsListBox.ItemsSource = _selectedClients;

                var lifeAndHealth = _context.LifeAndHealth
                    .Include(lh => lh.HealthConditions)
                    .FirstOrDefault(lh => lh.PolicyID == policy.PolicyID);

                if (lifeAndHealth != null)
                {
                    AgeTextBox.Text = lifeAndHealth.Age.ToString();
                    UpdateTextBoxPlaceholder(AgeTextBox, AgePlaceholder);

                    var genderItem = GenderComboBox.Items.Cast<ComboBoxItem>()
                        .FirstOrDefault(item => item.Content.ToString() == lifeAndHealth.Gender);
                    if (genderItem != null)
                    {
                        GenderComboBox.SelectedItem = genderItem;
                        UpdateComboBoxPlaceholder(GenderComboBox, GenderPlaceholder);
                    }

                    HealthConditionComboBox.SelectedItem = lifeAndHealth.HealthConditions;
                    UpdateComboBoxPlaceholder(HealthConditionComboBox, HealthConditionPlaceholder);

                    OccupationTextBox.Text = lifeAndHealth.Occupation;
                    UpdateTextBoxPlaceholder(OccupationTextBox, OccupationPlaceholder);
                }

                if (_selectedClients.Any())
                {
                    RemoveClientButton.Visibility = Visibility.Visible;
                    AddClientButton.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных полиса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}