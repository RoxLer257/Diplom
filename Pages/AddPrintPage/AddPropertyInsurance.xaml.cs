﻿using Diplom.Classes;
using Diplom.Classes.Validator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using System.Windows.Navigation;

namespace Diplom.Pages.AddPrintPage
{
    /// <summary>
    /// Логика взаимодействия для AddPropertyInsurance.xaml
    /// </summary>
    public partial class AddPropertyInsurance : Page
    {
        private readonly VSK_DBEntities _context;
        private ObservableCollection<Diplom.Classes.Properties> SelectedProperties { get; set; }
        private List<Clients> _selectedClients;
        private readonly PropertyInsuranceValidator _propertyInsuranceValidator;
        private readonly ClientValidator _clientValidator;
        private bool isEditMode = false;
        private Policies editablePolicy;

        public AddPropertyInsurance()
        {
            InitializeComponent();
            _context = ClassFrame.ConnectDB;
            SelectedProperties = new ObservableCollection<Diplom.Classes.Properties>();
            _selectedClients = new List<Clients>();
            _propertyInsuranceValidator = new PropertyInsuranceValidator(_context);
            _clientValidator = new ClientValidator(_context);

            LoadData();
            InitializePlaceholders();
            SetPolicyType("Страхование имущества");
        }

        public AddPropertyInsurance(Policies policyToEdit) : this()
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
                if (policytype != null && policytype.TypeName == typeName)
                {
                    PolicyTypeComboBox.SelectedItem = item;
                    break;
                }
            }
        }

        private void LoadData()
        {
            var clientTypes = _context.ClientTypes.ToList();
            ClientTypeComboBox.ItemsSource = clientTypes;

            var propertyTypes = _context.PropertyTypes.ToList();
            PropertyTypeComboBox.ItemsSource = propertyTypes;

            var policyTypes = _context.PolicyTypes.ToList();
            PolicyTypeComboBox.ItemsSource = policyTypes;

            var policyStatuses = _context.PolicyStatuses.ToList();
            StatusComboBox.ItemsSource = policyStatuses;

            ClientsListBox.ItemsSource = _selectedClients;

            PropertiesListBox.ItemsSource = SelectedProperties;
        }

        private void InitializePlaceholders()
        {
            UpdateComboBoxPlaceholder(StatusComboBox, StatusPlaceholder);
            UpdateComboBoxPlaceholder(PropertyTypeComboBox, PropertyTypePlaceholder);
            UpdateComboBoxPlaceholder(ClientTypeComboBox, ClientTypePlaceholder);

            UpdateTextBoxPlaceholder(InsuranceAmountTextBox, InsuranceAmountPlaceholder);
            UpdateTextBoxPlaceholder(AddressTextBox, AddressPlaceholder);
            UpdateTextBoxPlaceholder(AreaTextBox, AreaPlaceholder);
            UpdateTextBoxPlaceholder(ValueTextBox, ValuePlaceholder);
            UpdateTextBoxPlaceholder(LastNameTextBox, LastNamePlaceholder);
            UpdateTextBoxPlaceholder(FirstNameTextBox, FirstNamePlaceholder);
            UpdateTextBoxPlaceholder(MiddleNameTextBox, MiddleNamePlaceholder);
            UpdateTextBoxPlaceholder(CompanyNameTextBox, CompanyNamePlaceholder);
            UpdateTextBoxPlaceholder(PassportNumberTextBox, PassportNumberPlaceholder);
            UpdateTextBoxPlaceholder(INNTextBox, INNPlaceholder);
            UpdateTextBoxPlaceholder(PhoneTextBox, PhonePlaceholder);
            UpdateTextBoxPlaceholder(EmailTextBox, EmailPlaceholder);
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
            UpdateComboBoxPlaceholder(PropertyTypeComboBox, PropertyTypePlaceholder);
        }

        private void UpdateComboBoxPlaceholder(ComboBox comboBox, TextBlock placeholder)
        {
            if (placeholder != null)
            {
                placeholder.Visibility = comboBox.SelectedItem == null ? Visibility.Visible : Visibility.Collapsed;
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

        private void HideNewClientPanelButton_Click(object sender, RoutedEventArgs e)
        {
            ClearClientForm();
            NewClientPanel.Visibility = Visibility.Collapsed;
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

        private void AddNewPropertyButton_Click(object sender, RoutedEventArgs e)
        {
            if (PropertyTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип имущества", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            decimal? area = null;
            decimal? value = null;
            if (decimal.TryParse(AreaTextBox.Text, out decimal parsedArea))
            {
                area = parsedArea;
            }
            if (decimal.TryParse(ValueTextBox.Text, out decimal parsedValue))
            {
                value = parsedValue;
            }

            var propertyTypeResult = _propertyInsuranceValidator.ValidatePropertyType(PropertyTypeComboBox.SelectedItem as PropertyTypes);
            if (!propertyTypeResult.IsValid)
            {
                MessageBox.Show(propertyTypeResult.ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var addressResult = _propertyInsuranceValidator.ValidateAddress(AddressTextBox.Text);
            if (!addressResult.IsValid)
            {
                MessageBox.Show(addressResult.ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var areaResult = _propertyInsuranceValidator.ValidateArea(area);
            if (!areaResult.IsValid)
            {
                MessageBox.Show(areaResult.ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var valueResult = _propertyInsuranceValidator.ValidateValue(value);
            if (!valueResult.IsValid)
            {
                MessageBox.Show(valueResult.ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var newProperty = new Diplom.Classes.Properties
                {
                    PropertyTypeID = (PropertyTypeComboBox.SelectedItem as PropertyTypes).PropertyTypeID,
                    Address = AddressTextBox.Text,
                    Area = area.Value,
                    Value = value.Value
                };

                SelectedProperties.Add(newProperty);
                PropertiesListBox.ItemsSource = null;
                PropertiesListBox.ItemsSource = SelectedProperties;

                AddPropertyButton.Visibility = Visibility.Collapsed;

                PropertyTypeComboBox.SelectedItem = null;
                AddressTextBox.Text = string.Empty;
                AreaTextBox.Text = string.Empty;
                ValueTextBox.Text = string.Empty;

                UpdateComboBoxPlaceholder(PropertyTypeComboBox, PropertyTypePlaceholder);
                UpdateTextBoxPlaceholder(AddressTextBox, AddressPlaceholder);
                UpdateTextBoxPlaceholder(AreaTextBox, AreaPlaceholder);
                UpdateTextBoxPlaceholder(ValueTextBox, ValuePlaceholder);

                NewPropertyPanel.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении имущества: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PropertiesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemovePropertyButton.Visibility = PropertiesListBox.SelectedItem != null ? Visibility.Visible : Visibility.Collapsed;
        }

        private void RemovePropertyButton_Click(object sender, RoutedEventArgs e)
        {
            if (PropertiesListBox.SelectedItem is Diplom.Classes.Properties selectedProperty)
            {
                SelectedProperties.Remove(selectedProperty);
                PropertiesListBox.ItemsSource = null;
                PropertiesListBox.ItemsSource = SelectedProperties;
                RemovePropertyButton.Visibility = Visibility.Collapsed;
                
                if (!SelectedProperties.Any())
                {
                    AddPropertyButton.Visibility = Visibility.Visible;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите имущество для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ShowNewPropertyPanelButton_Click(object sender, RoutedEventArgs e)
        {
            NewPropertyPanel.Visibility = Visibility.Visible;
            
            PropertyTypeComboBox.SelectedItem = null;
            AddressTextBox.Text = string.Empty;
            AreaTextBox.Text = string.Empty;
            ValueTextBox.Text = string.Empty;

            UpdateComboBoxPlaceholder(PropertyTypeComboBox, PropertyTypePlaceholder);
            UpdateTextBoxPlaceholder(AddressTextBox, AddressPlaceholder);
            UpdateTextBoxPlaceholder(AreaTextBox, AreaPlaceholder);
            UpdateTextBoxPlaceholder(ValueTextBox, ValuePlaceholder);
        }

        private void HideNewPropertyPanelButton_Click(object sender, RoutedEventArgs e)
        {
            PropertyTypeComboBox.SelectedItem = null;
            AddressTextBox.Text = string.Empty;
            AreaTextBox.Text = string.Empty;
            ValueTextBox.Text = string.Empty;

            UpdateComboBoxPlaceholder(PropertyTypeComboBox, PropertyTypePlaceholder);
            UpdateTextBoxPlaceholder(AddressTextBox, AddressPlaceholder);
            UpdateTextBoxPlaceholder(AreaTextBox, AreaPlaceholder);
            UpdateTextBoxPlaceholder(ValueTextBox, ValuePlaceholder);

            NewPropertyPanel.Visibility = Visibility.Collapsed;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            decimal? insuranceAmount = null;
            if (decimal.TryParse(InsuranceAmountTextBox.Text, out decimal parsedAmount))
            {
                insuranceAmount = parsedAmount;
            }
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;
            PolicyTypes policyType = PolicyTypeComboBox.SelectedItem as PolicyTypes;
            PolicyStatuses status = StatusComboBox.SelectedItem as PolicyStatuses;

            var validationResult = _propertyInsuranceValidator.ValidatePolicyData(
                startDate,
                endDate,
                insuranceAmount,
                policyType,
                status,
                _selectedClients,
                SelectedProperties
            );

            if (!validationResult.IsValid)
            {
                MessageBox.Show(validationResult.ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (isEditMode)
                {
                    editablePolicy.PolicyTypeID = policyType.PolicyTypeID;
                    editablePolicy.StatusID = status.StatusID;
                    editablePolicy.InsuranceAmount = insuranceAmount.Value;
                    editablePolicy.StartDate = startDate.Value;
                    editablePolicy.EndDate = endDate.Value;

                    editablePolicy.Clients.Clear();
                    foreach (var client in _selectedClients)
                    {
                        editablePolicy.Clients.Add(client);
                    }

                    var existingProperties = _context.Properties.Where(p => p.PolicyID == editablePolicy.PolicyID).ToList();
                    _context.Properties.RemoveRange(existingProperties);

                    foreach (var property in SelectedProperties)
                    {
                        var newProperty = new Diplom.Classes.Properties
                        {
                            PolicyID = editablePolicy.PolicyID,
                            PropertyTypeID = property.PropertyTypeID,
                            Address = property.Address,
                            Area = property.Area,
                            Value = property.Value
                        };
                        _context.Properties.Add(newProperty);
                    }

                    _context.SaveChanges();

                    LogAction("Policies", "Редактирование", $"Отредактирован полис: {editablePolicy.PolicyID}");
                    LogAction("Properties", "Редактирование", $"Обновлены записи имущества для полиса: {editablePolicy.PolicyID}");

                    MessageBox.Show("Полис успешно обновлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var policy = new Policies
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

                    foreach (var property in SelectedProperties)
                    {
                        var newProperty = new Diplom.Classes.Properties
                        {
                            PolicyID = policy.PolicyID,
                            PropertyTypeID = property.PropertyTypeID,
                            Address = property.Address,
                            Area = property.Area,
                            Value = property.Value
                        };
                        _context.Properties.Add(newProperty);
                    }

                    _context.SaveChanges();

                    LogAction("Policies", "Добавление", $"Добавлен полис: {policy.PolicyID} с {policy.Clients.Count} клиентами и {SelectedProperties.Count} объектами имущества");
                    LogAction("Properties", "Добавление", $"Добавлена запись для полиса: {policy.PolicyID} с {SelectedProperties.Count} объектами имущества");

                    MessageBox.Show("Полис успешно сохранён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

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
            SelectedProperties.Clear();
            PropertiesListBox.ItemsSource = null;

            PropertyTypeComboBox.SelectedItem = null;
            AddressTextBox.Text = string.Empty;
            AreaTextBox.Text = string.Empty;
            ValueTextBox.Text = string.Empty;

            ClearClientForm();
            NewClientPanel.Visibility = Visibility.Collapsed;
            NewPropertyPanel.Visibility = Visibility.Collapsed;
            RemovePropertyButton.Visibility = Visibility.Collapsed;

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

                SelectedProperties.Clear();
                var properties = _context.Properties
                    .Include(p => p.PropertyTypes)
                    .Where(p => p.PolicyID == policy.PolicyID)
                    .ToList();

                foreach (var property in properties)
                {
                    SelectedProperties.Add(property);
                }
                PropertiesListBox.ItemsSource = null;
                PropertiesListBox.ItemsSource = SelectedProperties;

                if (_selectedClients.Any())
                {
                    RemoveClientButton.Visibility = Visibility.Visible;
                    AddClientButton.Visibility = Visibility.Collapsed;
                }
                if (SelectedProperties.Any())
                {
                    RemovePropertyButton.Visibility = Visibility.Visible;
                    AddPropertyButton.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных полиса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
