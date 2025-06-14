﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using System.Windows.Media;
using Diplom.Classes;
using Diplom.Classes.Calculator;
using Diplom.Classes.Validator;
using ValidationResult = Diplom.Classes.Validator.ValidationResult;

namespace Diplom.Pages
{
    public partial class AddPolicyPage : Page
    {
        private bool isEditMode = false;
        private Policies editablePolicy;

        private readonly VSK_DBEntities _context;

        private readonly ClientValidator _clientValidator;
        private readonly DriverValidator _driverValidator;
        private readonly VehicleValidator _vehicleValidator;
        private readonly PolicyValidator _policyValidator;

        private ObservableCollection<Drivers> SelectedDrivers { get; set; }
        private List<Clients> _selectedClients;
        private readonly InsuranceCalculator _calculator;
        private Dictionary<int, bool> DriverAccidentMap = new Dictionary<int, bool>();
        private Dictionary<int, decimal> DriverKBMMap = new Dictionary<int, decimal>();

        public AddPolicyPage()
        {
            InitializeComponent();

            _context = ClassFrame.ConnectDB;
            _calculator = new InsuranceCalculator(_context);
            SelectedDrivers = new ObservableCollection<Drivers>();
            _selectedClients = new List<Clients>();

            _clientValidator = new ClientValidator(_context);
            _driverValidator = new DriverValidator(_context);
            _vehicleValidator = new VehicleValidator(_context);
            _policyValidator = new PolicyValidator();

            LoadData();
            InitializePlaceholders();
        }

        public AddPolicyPage(Policies policyToEdit) : this()
        {
            isEditMode = true;
            editablePolicy = policyToEdit;
            LoadPolicyData(policyToEdit);
        }

        private void LoadData()
        {
            var clientTypes = _context.ClientTypes.ToList();
            ClientTypeComboBox.ItemsSource = clientTypes;

            var vehicleMakes = _context.VehicleMakes.ToList();
            VehicleMakeComboBox.ItemsSource = vehicleMakes;

            var policyTypes = _context.PolicyTypes
        .Where(pt => pt.TypeName == "ОСАГО" || pt.TypeName == "КАСКО")
        .ToList();
            PolicyTypeComboBox.ItemsSource = policyTypes;
            PolicyTypeComboBox.DisplayMemberPath = "TypeName";
            PolicyTypeComboBox.SelectedValuePath = "PolicyTypeID";

            var policyStatuses = _context.PolicyStatuses.ToList();
            StatusComboBox.ItemsSource = policyStatuses;

            ClientsListBox.ItemsSource = _selectedClients;
            SelectedDriversListBox.ItemsSource = SelectedDrivers;
        }

        private void RegionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComboBoxPlaceholder(RegionComboBox, RegionPlaceholder);
        }

        private void InitializePlaceholders()
        {
            UpdateComboBoxPlaceholder(PolicyTypeComboBox, PolicyTypePlaceholder);
            UpdateComboBoxPlaceholder(StatusComboBox, StatusPlaceholder);
            UpdateComboBoxPlaceholder(VehicleMakeComboBox, VehicleMakePlaceholder);
            UpdateComboBoxPlaceholder(VehicleModelComboBox, VehicleModelPlaceholder);
            UpdateComboBoxPlaceholder(ClientTypeComboBox, ClientTypePlaceholder);
            UpdateComboBoxPlaceholder(RegionComboBox, RegionPlaceholder);

            UpdateTextBoxPlaceholder(VinTextBox, VinPlaceholder);
            UpdateTextBoxPlaceholder(YearTextBox, YearPlaceholder);
            UpdateTextBoxPlaceholder(LicensePlateTextBox, LicensePlatePlaceholder);
            UpdateTextBoxPlaceholder(DriverLastNameTextBox, DriverLastNamePlaceholder);
            UpdateTextBoxPlaceholder(DriverFirstNameTextBox, DriverFirstNamePlaceholder);
            UpdateTextBoxPlaceholder(DriverMiddleNameTextBox, DriverMiddleNamePlaceholder);
            UpdateTextBoxPlaceholder(DriverLicenseNumberTextBox, DriverLicenseNumberPlaceholder);
            UpdateTextBoxPlaceholder(DriverExperienceTextBox, DriverExperiencePlaceholder);
            UpdateTextBoxPlaceholder(DriverPhoneTextBox, DriverPhonePlaceholder);
            UpdateTextBoxPlaceholder(DriverEmailTextBox, DriverEmailPlaceholder);
            UpdateTextBoxPlaceholder(LastNameTextBox, LastNamePlaceholder);
            UpdateTextBoxPlaceholder(FirstNameTextBox, FirstNamePlaceholder);
            UpdateTextBoxPlaceholder(MiddleNameTextBox, MiddleNamePlaceholder);
            UpdateTextBoxPlaceholder(CompanyNameTextBox, CompanyNamePlaceholder);
            UpdateTextBoxPlaceholder(PassportNumberTextBox, PassportNumberPlaceholder);
            UpdateTextBoxPlaceholder(INNTextBox, INNPlaceholder);
            UpdateTextBoxPlaceholder(PhoneTextBox, PhonePlaceholder);
            UpdateTextBoxPlaceholder(EmailTextBox, EmailPlaceholder);
            UpdateTextBoxPlaceholder(DriverPassportNumberTextBox, DriverPassportNumberPlaceholder);
            UpdateTextBoxPlaceholder(DriverINNTextBox, DriverINNPlaceholder);

            VinTextBox.TextChanged += (s, e) => {
                if (VinTextBox.Text != VinTextBox.Text.ToUpperInvariant())
                {
                    int caretIndex = VinTextBox.CaretIndex;
                    VinTextBox.Text = VinTextBox.Text.ToUpperInvariant();
                    VinTextBox.CaretIndex = caretIndex;
                }
            };

            LicensePlateTextBox.TextChanged += (s, e) => {
                if (LicensePlateTextBox.Text != LicensePlateTextBox.Text.ToUpperInvariant())
                {
                    int caretIndex = LicensePlateTextBox.CaretIndex;
                    LicensePlateTextBox.Text = LicensePlateTextBox.Text.ToUpperInvariant();
                    LicensePlateTextBox.CaretIndex = caretIndex;
                }
            };
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
                ValidateTextBox(textBox);
            }


        }

        private void ValidateTextBox(TextBox textBox)
        {
            string text = textBox.Text;
            Classes.Validator.ValidationResult result = null;
            Brush originalBorderBrush = textBox.BorderBrush;

            textBox.BorderBrush = originalBorderBrush;
            textBox.ToolTip = null;

            try
            {
                switch (textBox.Name)
                {
                    case "LastNameTextBox":
                        result = _clientValidator.ValidateLastName(text);
                        break;
                    case "FirstNameTextBox":
                        result = _clientValidator.ValidateFirstName(text);
                        break;
                    case "MiddleNameTextBox":
                        result = _clientValidator.ValidateMiddleName(text);
                        break;
                    case "CompanyNameTextBox":
                        result = _clientValidator.ValidateCompanyName(text);
                        break;
                    case "PassportNumberTextBox":
                        result = _clientValidator.ValidatePassportNumber(text);
                        break;
                    case "INNTextBox":
                        if (ClientTypeComboBox.SelectedItem is ClientTypes clientType)
                            result = _clientValidator.ValidateINN(text, clientType.ClientTypeID);
                        else
                            result = new Classes.Validator.ValidationResult(false, "Выберите тип клиента.");
                        break;
                    case "PhoneTextBox":
                        result = _clientValidator.ValidatePhone(text, _selectedClients.FirstOrDefault()?.ClientID);
                        break;
                    case "EmailTextBox":
                        result = _clientValidator.ValidateEmail(text, _selectedClients.FirstOrDefault()?.ClientID);
                        break;
                    case "DriverLastNameTextBox":
                        result = _driverValidator.ValidateLastName(text);
                        break;
                    case "DriverFirstNameTextBox":
                        result = _driverValidator.ValidateFirstName(text);
                        break;
                    case "DriverMiddleNameTextBox":
                        result = _driverValidator.ValidateMiddleName(text);
                        break;
                    case "DriverPassportNumberTextBox":
                        result = _driverValidator.ValidatePassportNumber(text);
                        break;
                    case "DriverINNTextBox":
                        result = _driverValidator.ValidateINN(text);
                        break;
                    case "DriverPhoneTextBox":
                        result = _driverValidator.ValidatePhone(text);
                        break;
                    case "DriverEmailTextBox":
                        result = _driverValidator.ValidateEmail(text);
                        break;
                    case "VinTextBox":
                        if (isEditMode && editablePolicy != null)
                        {
                            var vehicle = _context.Vehicles.FirstOrDefault(v => v.PolicyID == editablePolicy.PolicyID);
                            if (vehicle != null)
                                result = _vehicleValidator.ValidateVIN(text, vehicle.VehicleID);
                            else
                                result = _vehicleValidator.ValidateVIN(text);
                        }
                        else
                        {
                            result = _vehicleValidator.ValidateVIN(text);
                        }
                        break;
                    case "YearTextBox":
                        if (int.TryParse(text, out int year))
                            result = _vehicleValidator.ValidateYear(year);
                        else
                            result = new Classes.Validator.ValidationResult(false, "Год выпуска: числовое значение.");
                        break;
                    case "LicensePlateTextBox":
                        result = _vehicleValidator.ValidateLicensePlate(text);
                        break;
                    case "DriverLicenseNumberTextBox":
                        result = _driverValidator.ValidateLicenseNumber(text);
                        break;
                    case "DriverExperienceTextBox":
                        if (int.TryParse(text, out int experience))
                            result = _driverValidator.ValidateDrivingExperience(experience, DriverDateOfBirthPicker.SelectedDate.Value);
                        else
                            result = new Classes.Validator.ValidationResult(false, "Стаж: числовое значение.");
                        break;
                    case "EnginePowerTextBox":
                        if (int.TryParse(text, out int power))
                            result = _vehicleValidator.ValidateEnginePower(power);
                        else
                            result = new Classes.Validator.ValidationResult(false, "Мощность: числовое значение.");
                        break;
                }

                if (result != null && !result.IsValid)
                {
                    textBox.BorderBrush = Brushes.Red;
                    textBox.ToolTip = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                textBox.BorderBrush = Brushes.Red;
                textBox.ToolTip = $"Ошибка валидации: {ex.Message}";
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
            if (sender is ComboBox comboBox)
            {
                UpdateComboBoxPlaceholder(comboBox, comboBox.Tag as TextBlock);
                ValidateComboBox(comboBox);
            }

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
        }

        private void ValidateComboBox(ComboBox comboBox)
        {
            ValidationResult result = null;
            Brush originalBorderBrush = comboBox.BorderBrush;

            comboBox.BorderBrush = originalBorderBrush;
            comboBox.ToolTip = null;

            switch (comboBox.Name)
            {
                case "ClientTypeComboBox":
                    result = _clientValidator.ValidateClientType(comboBox.SelectedItem as ClientTypes);
                    break;
                case "PolicyTypeComboBox":
                    result = _policyValidator.ValidatePolicyType(comboBox.SelectedItem as PolicyTypes);
                    break;
                case "StatusComboBox":
                    result = _policyValidator.ValidateStatus(comboBox.SelectedItem as PolicyStatuses);
                    break;
                case "VehicleMakeComboBox":
                    result = _vehicleValidator.ValidateVehicleMake(comboBox.SelectedItem as VehicleMakes);
                    break;
                case "VehicleModelComboBox":
                    result = _vehicleValidator.ValidateVehicleModel(comboBox.SelectedItem as VehicleModels);
                    break;
                case "RegionComboBox":
                    result = _vehicleValidator.ValidateRegion(comboBox.SelectedItem);
                    break;
            }

            if (result != null && !result.IsValid)
            {
                comboBox.BorderBrush = Brushes.Red;
                comboBox.ToolTip = result.ErrorMessage;
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DatePicker datePicker)
            {
                ValidateDatePicker(datePicker);
            }
        }

        private void ValidateDatePicker(DatePicker datePicker)
        {
            ValidationResult result = null;
            Brush originalBorderBrush = datePicker.BorderBrush;

            datePicker.BorderBrush = originalBorderBrush;
            datePicker.ToolTip = null;

            if (datePicker.Name == "DriverDateOfBirthPicker")
            {
                result = _driverValidator.ValidateDateOfBirth(datePicker.SelectedDate);
            }
            else if (datePicker.Name == "StartDatePicker")
            {
                result = _policyValidator.ValidateStartDate(datePicker.SelectedDate);
            }
            else if (datePicker.Name == "EndDatePicker")
            {
                result = _policyValidator.ValidateEndDate(datePicker.SelectedDate, StartDatePicker.SelectedDate);
            }

            if (result != null && !result.IsValid)
            {
                datePicker.BorderBrush = Brushes.Red;
                datePicker.ToolTip = result.ErrorMessage;
            }
        }

        private void UpdateComboBoxPlaceholder(ComboBox comboBox, TextBlock placeholder)
        {
            if (placeholder != null)
            {
                placeholder.Visibility = comboBox.SelectedItem == null ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void VehicleMakeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VehicleMakeComboBox.SelectedItem is VehicleMakes selectedMake)
            {
                var models = _context.VehicleModels
                    .Where(vm => vm.MakeID == selectedMake.MakeID)
                    .ToList();
                VehicleModelComboBox.ItemsSource = models;
            }
            UpdateComboBoxPlaceholder(VehicleMakeComboBox, VehicleMakePlaceholder);
        }

        private void CalculateCostButton_Click(object sender, RoutedEventArgs e)
        {
            if (PolicyTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип полиса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (isEditMode && editablePolicy != null)
            {
                var vehicle = _context.Vehicles.FirstOrDefault(v => v.PolicyID == editablePolicy.PolicyID);
                if (vehicle != null)
                {
                    var vinResult = _vehicleValidator.ValidateVIN(VinTextBox.Text.ToUpperInvariant(), vehicle.VehicleID);
                    if (!vinResult.IsValid)
                    {
                        MessageBox.Show(vinResult.ErrorMessage, "Ошибка VIN", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
            }
            else
            {
                var vinResult = _vehicleValidator.ValidateVIN(VinTextBox.Text.ToUpperInvariant());
                if (!vinResult.IsValid)
                {
                    MessageBox.Show(vinResult.ErrorMessage, "Ошибка VIN", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            if (!int.TryParse(YearTextBox.Text, out int year))
            {
                MessageBox.Show("Год выпуска: введите числовое значение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var yearResult = _vehicleValidator.ValidateYear(year);
            if (!yearResult.IsValid)
            {
                MessageBox.Show(yearResult.ErrorMessage, "Ошибка года выпуска", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var plateResult = _vehicleValidator.ValidateLicensePlate(LicensePlateTextBox.Text.ToUpperInvariant());
            if (!plateResult.IsValid)
            {
                MessageBox.Show(plateResult.ErrorMessage, "Ошибка номера", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (VehicleMakeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите марку автомобиля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (VehicleModelComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите модель автомобиля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (RegionComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите регион регистрации.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string enginePowerText = EnginePowerTextBox.Text.Replace("л.с.", "").Trim();
            if (!int.TryParse(enginePowerText, out int enginePower))
            {
                MessageBox.Show("Мощность: введите числовое значение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var powerResult = _vehicleValidator.ValidateEnginePower(enginePower);
            if (!powerResult.IsValid)
            {
                MessageBox.Show(powerResult.ErrorMessage, "Ошибка мощности", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var startDateResult = _policyValidator.ValidateStartDate(StartDatePicker.SelectedDate);
            if (!startDateResult.IsValid)
            {
                MessageBox.Show(startDateResult.ErrorMessage, "Ошибка даты начала", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var endDateResult = _policyValidator.ValidateEndDate(EndDatePicker.SelectedDate, StartDatePicker.SelectedDate);
            if (!endDateResult.IsValid)
            {
                MessageBox.Show(endDateResult.ErrorMessage, "Ошибка даты окончания", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!_selectedClients.Any())
            {
                MessageBox.Show("Добавьте хотя бы одного клиента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var driversResult = _policyValidator.ValidateDrivers(SelectedDrivers);
            if (!driversResult.IsValid)
            {
                MessageBox.Show(driversResult.ErrorMessage, "Ошибка водителей", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                double kt = double.Parse((RegionComboBox.SelectedItem as ComboBoxItem).Tag.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                foreach (var driver in SelectedDrivers)
                {
                    decimal kbmDecimal = (decimal)_calculator.CalculateKBM(driver);
                    DriverKBMMap[driver.DriverID] = kbmDecimal;
                }
                double cost = _calculator.CalculateInsuranceCost(
                    StartDatePicker.SelectedDate.Value,
                    EndDatePicker.SelectedDate.Value,
                    enginePower,
                    SelectedDrivers.ToList(),
                    kt.ToString(System.Globalization.CultureInfo.InvariantCulture)
                );
                if (cost == 0.0)
                {
                    MessageBox.Show("Не удалось рассчитать стоимость. Проверьте введенные данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                CalculatedCostTextBlock.Text = $"{cost:F2} руб.";
                CalculatedCostTextBlock.Visibility = Visibility.Visible;
                SaveButton.Visibility = Visibility.Visible;
                MessageBox.Show($"Стоимость: {cost:F2} руб.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void RemoveDriverButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDriversListBox.SelectedItem is Drivers selectedDriver)
            {
                SelectedDrivers.Remove(selectedDriver);
                if (!SelectedDrivers.Any())
                {
                    RemoveDriverButton.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите водителя для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ShowNewDriverPanel_Click(object sender, RoutedEventArgs e)
        {
            NewDriverPanel.Visibility = Visibility.Visible;
        }

        private void AddNewDriverButton_Click(object sender, RoutedEventArgs e)
        {
            int? experience = null;
            if (int.TryParse(DriverExperienceTextBox.Text, out int exp))
            {
                experience = exp;
            }
            var experienceResult = _driverValidator.ValidateDrivingExperience(experience, DriverDateOfBirthPicker.SelectedDate ?? DateTime.Today);
            if (!experienceResult.IsValid)
            {
                MessageBox.Show(experienceResult.ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var validationResult = _driverValidator.ValidateDriver(
                DriverLastNameTextBox.Text,
                DriverFirstNameTextBox.Text,
                DriverMiddleNameTextBox.Text,
                DriverDateOfBirthPicker.SelectedDate,
                DriverLicenseNumberTextBox.Text,
                experience,
                DriverPassportNumberTextBox.Text,
                DriverINNTextBox.Text,
                DriverPhoneTextBox.Text,
                DriverEmailTextBox.Text);

            if (!validationResult.IsValid)
            {
                MessageBox.Show(validationResult.ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string lastName = DriverLastNameTextBox.Text;
            string firstName = DriverFirstNameTextBox.Text;
            string middleName = DriverMiddleNameTextBox.Text;
            DateTime dateOfBirth = DriverDateOfBirthPicker.SelectedDate.Value;
            string licenseNumber = DriverLicenseNumberTextBox.Text;
            string phone = DriverPhoneTextBox.Text;
            string email = DriverEmailTextBox.Text;
            string passportNumber = DriverPassportNumberTextBox.Text;
            string inn = DriverINNTextBox.Text;
            bool hadAccident = DriverHadAccidentCheckBox.IsChecked == true;

            try
            {
                var existingClient = _context.Clients.FirstOrDefault(c => c.Phone == phone || c.Email == email);
                Clients client;

                if (existingClient != null)
                {
                    client = existingClient;
                    if (string.IsNullOrWhiteSpace(client.PassportNumber)) client.PassportNumber = passportNumber;
                    if (string.IsNullOrWhiteSpace(client.INN)) client.INN = inn;
                }
                else
                {
                    client = new Clients
                    {
                        LastName = lastName,
                        FirstName = firstName,
                        MiddleName = middleName,
                        ClientTypeID = 1,
                        Phone = phone,
                        Email = email,
                        PassportNumber = passportNumber,
                        INN = inn,
                        AgentID = CurrentUser.EmployeeID
                    };
                    _context.Clients.Add(client);
                    _context.SaveChanges();
                    LogAction("Clients", "Добавление", $"Добавлен клиент: {client.LastName} {client.FirstName} {client.MiddleName}");
                }

                var newDriver = new Drivers
                {
                    LastName = lastName,
                    FirstName = firstName,
                    MiddleName = middleName,
                    DateOfBirth = dateOfBirth,
                    LicenseNumber = licenseNumber,
                    DrivingExperience = experience.Value,
                    ClientID = client.ClientID
                };

                _context.Drivers.Add(newDriver);
                _context.SaveChanges();

                LogAction("Drivers", "Добавление", $"Добавлен водитель: {newDriver.LastName} {newDriver.FirstName} {newDriver.MiddleName}");

                SelectedDrivers.Add(newDriver);
                DriverAccidentMap[newDriver.DriverID] = hadAccident;
                ClientsListBox.ItemsSource = null;
                ClientsListBox.ItemsSource = _selectedClients;

                ClearDriverFields();
                NewDriverPanel.Visibility = Visibility.Collapsed;
                RemoveDriverButton.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении водителя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearDriverFields()
        {
            DriverLastNameTextBox.Text = string.Empty;
            DriverFirstNameTextBox.Text = string.Empty;
            DriverMiddleNameTextBox.Text = string.Empty;
            DriverDateOfBirthPicker.SelectedDate = null;
            DriverLicenseNumberTextBox.Text = string.Empty;
            DriverExperienceTextBox.Text = string.Empty;
            DriverPhoneTextBox.Text = string.Empty;
            DriverEmailTextBox.Text = string.Empty;
            DriverPassportNumberTextBox.Text = string.Empty;
            DriverINNTextBox.Text = string.Empty;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedClients.Any() &&
                PolicyTypeComboBox.SelectedItem is PolicyTypes selectedPolicyType &&
                StatusComboBox.SelectedItem is PolicyStatuses selectedStatus &&
                double.TryParse(CalculatedCostTextBlock.Text.Replace(" руб.", ""), out double insuranceAmount) &&
                StartDatePicker.SelectedDate.HasValue &&
                EndDatePicker.SelectedDate.HasValue &&
                VehicleModelComboBox.SelectedItem is VehicleModels selectedModel &&
                !string.IsNullOrWhiteSpace(VinTextBox.Text) &&
                int.TryParse(YearTextBox.Text, out int year) &&
                !string.IsNullOrWhiteSpace(LicensePlateTextBox.Text))
            {
                try
                {
                    Policies policy;
                    if (isEditMode)
                    {
                        policy = editablePolicy;
                        policy.PolicyTypeID = selectedPolicyType.PolicyTypeID;
                        policy.StatusID = selectedStatus.StatusID;
                        policy.InsuranceAmount = (decimal)insuranceAmount;
                        policy.StartDate = StartDatePicker.SelectedDate.Value;
                        policy.EndDate = EndDatePicker.SelectedDate.Value;

                        policy.Clients.Clear();
                        foreach (var client in _selectedClients)
                            policy.Clients.Add(client);

                        policy.Drivers.Clear();
                        foreach (var driver in SelectedDrivers)
                            policy.Drivers.Add(driver);

                        var existingVehicle = _context.Vehicles.FirstOrDefault(v => v.PolicyID == policy.PolicyID);
                        if (existingVehicle != null)
                        {
                            existingVehicle.ModelID = selectedModel.ModelID;
                            existingVehicle.VIN = VinTextBox.Text;
                            existingVehicle.Year = year;
                            existingVehicle.LicensePlate = LicensePlateTextBox.Text;
                            existingVehicle.RegistrationRegion = (RegionComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                            
                            if (!string.IsNullOrWhiteSpace(EnginePowerTextBox.Text))
                            {
                                string powerText = EnginePowerTextBox.Text.Replace("л.с.", "").Trim();
                                if (int.TryParse(powerText, out int power))
                                {
                                    existingVehicle.EnginePower = power;
                                }
                            }
                        }
                        else
                        {
                            var newVehicle = new Vehicles
                            {
                                PolicyID = policy.PolicyID,
                                ModelID = selectedModel.ModelID,
                                VIN = VinTextBox.Text,
                                Year = year,
                                LicensePlate = LicensePlateTextBox.Text,
                                RegistrationRegion = (RegionComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()
                            };

                            if (!string.IsNullOrWhiteSpace(EnginePowerTextBox.Text))
                            {
                                string powerText = EnginePowerTextBox.Text.Replace("л.с.", "").Trim();
                                if (int.TryParse(powerText, out int power))
                                {
                                    newVehicle.EnginePower = power;
                                }
                            }

                            _context.Vehicles.Add(newVehicle);
                        }

                        LogAction("Policies", "Редактирование", $"Полис {policy.PolicyID} обновлён.");
                        LogAction("Vehicles", "Редактирование", $"Обновлены данные ТС для полиса {policy.PolicyID}");
                    }
                    else
                    {
                        policy = new Policies
                        {
                            PolicyTypeID = selectedPolicyType.PolicyTypeID,
                            StatusID = selectedStatus.StatusID,
                            InsuranceAmount = (decimal)insuranceAmount,
                    StartDate = StartDatePicker.SelectedDate.Value,
                    EndDate = EndDatePicker.SelectedDate.Value
                };
                        foreach (var client in _selectedClients)
                            policy.Clients.Add(client);
                        foreach (var driver in SelectedDrivers)
                            policy.Drivers.Add(driver);

                _context.Policies.Add(policy);
                _context.SaveChanges();

                        var newVehicle = new Vehicles
                        {
                            PolicyID = policy.PolicyID,
                            ModelID = selectedModel.ModelID,
                            VIN = VinTextBox.Text,
                            Year = year,
                            LicensePlate = LicensePlateTextBox.Text,
                            RegistrationRegion = (RegionComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()
                        };

                        if (!string.IsNullOrWhiteSpace(EnginePowerTextBox.Text))
                        {
                            string powerText = EnginePowerTextBox.Text.Replace("л.с.", "").Trim();
                            if (int.TryParse(powerText, out int power))
                            {
                                newVehicle.EnginePower = power;
                            }
                        }

                        _context.Vehicles.Add(newVehicle);

                        LogAction("Policies", "Добавление", $"Добавлен полис: {policy.PolicyID}");
                        LogAction("Vehicles", "Добавление", $"Добавлен автомобиль для полиса {policy.PolicyID}");
                }

                _context.SaveChanges();

                    MessageBox.Show("Полис успешно сохранён.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClassFrame.frmObj.Navigate(new MainPage.MainPage());
            }
            catch (Exception ex)
            {
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearForm()
        {
            _selectedClients.Clear();
            ClientsListBox.ItemsSource = null;
            PolicyTypeComboBox.SelectedItem = null;
            StatusComboBox.SelectedItem = null;
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
            VehicleMakeComboBox.SelectedItem = null;
            VehicleModelComboBox.SelectedItem = null;
            VinTextBox.Text = string.Empty;
            YearTextBox.Text = string.Empty;
            LicensePlateTextBox.Text = string.Empty;
            DriverLastNameTextBox.Text = string.Empty;
            DriverFirstNameTextBox.Text = string.Empty;
            DriverMiddleNameTextBox.Text = string.Empty;
            DriverLicenseNumberTextBox.Text = string.Empty;
            DriverExperienceTextBox.Text = string.Empty;
            DriverPhoneTextBox.Text = string.Empty;
            DriverEmailTextBox.Text = string.Empty;
            DriverPassportNumberTextBox.Text = string.Empty;
            DriverINNTextBox.Text = string.Empty;
            DriverDateOfBirthPicker.Text= string.Empty;
            SelectedDrivers.Clear();
            NewDriverPanel.Visibility = Visibility.Collapsed;

            ClearClientForm();
            NewClientPanel.Visibility = Visibility.Collapsed;

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

        private void HideNewClientPanelButton_Click(object sender, RoutedEventArgs e)
        {
            ClearClientForm();
            NewClientPanel.Visibility = Visibility.Collapsed;
        }

        private void ShowDriverListPanel_Click(object sender, RoutedEventArgs e)
        {
            var drivers = _context.Drivers.ToList();

            var selectDriversWindow = new Window
            {
                Title = "Выберите водителей",
                Width = 400,
                Height = 500,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize
            };

            var stackPanel = new StackPanel { Margin = new Thickness(10) };

            var searchTextBox = new TextBox
            {
                Width = 200,
                Margin = new Thickness(0, 0, 0, 10),
                ToolTip = "Введите имя для поиска"
            };

            var driversListBox = new ListBox
            {
                DisplayMemberPath = "FullName",
                SelectionMode = SelectionMode.Multiple,
                ItemsSource = drivers,
                Margin = new Thickness(0, 0, 0, 10),
                Height = 300
            };

            searchTextBox.TextChanged += (s, args) =>
            {
                var filter = searchTextBox.Text.ToLower();
                var filteredDrivers = drivers.Where(d => d.FullName.ToLower().Contains(filter)).ToList();
                driversListBox.ItemsSource = filteredDrivers;
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
                var selectedDrivers = driversListBox.SelectedItems.Cast<Drivers>().ToList();
                foreach (var driver in selectedDrivers)
                {
                    if (!SelectedDrivers.Contains(driver))
                    {
                        SelectedDrivers.Add(driver);
                    }
                }
                if (SelectedDrivers.Any())
                {
                    RemoveDriverButton.Visibility = Visibility.Visible;
                }
                selectDriversWindow.Close();
            };

            var cancelButton = new Button
            {
                Content = "Отмена",
                Width = 100,
                Height = 30,
                Margin = new Thickness(0, 10, 0, 0)
            };

            cancelButton.Click += (s, args) => selectDriversWindow.Close();

            stackPanel.Children.Add(searchTextBox);
            stackPanel.Children.Add(driversListBox);
            stackPanel.Children.Add(addButton);
            stackPanel.Children.Add(cancelButton);

            selectDriversWindow.Content = stackPanel;
            selectDriversWindow.ShowDialog();
        }

        private void HideNewDriverPanelButton_Click(object sender, RoutedEventArgs e)
        {
            ClearDriverFields();
            NewDriverPanel.Visibility = Visibility.Collapsed;
        }

        private void LoadPolicyData(Policies policy)
        {
            try
            {
                var policyType = _context.PolicyTypes.FirstOrDefault(pt => pt.PolicyTypeID == policy.PolicyTypeID);
                PolicyTypeComboBox.SelectedItem = policyType;
                UpdateComboBoxPlaceholder(PolicyTypeComboBox, PolicyTypePlaceholder);

                var status = _context.PolicyStatuses.FirstOrDefault(ps => ps.StatusID == policy.StatusID);
                StatusComboBox.SelectedItem = status;
                UpdateComboBoxPlaceholder(StatusComboBox, StatusPlaceholder);

                StartDatePicker.SelectedDate = policy.StartDate;
                EndDatePicker.SelectedDate = policy.EndDate;

                CalculatedCostTextBlock.Text = $"{policy.InsuranceAmount:F2} руб.";
                SaveButton.Visibility = Visibility.Collapsed;

                _selectedClients.Clear();
                foreach (var client in policy.Clients)
                {
                    _selectedClients.Add(client);
                }
                ClientsListBox.ItemsSource = null;
                ClientsListBox.ItemsSource = _selectedClients;

                SelectedDrivers.Clear();
                foreach (var driver in policy.Drivers)
                {
                    SelectedDrivers.Add(driver);
                }
                SelectedDriversListBox.ItemsSource = null;
                SelectedDriversListBox.ItemsSource = SelectedDrivers;

                var vehicle = _context.Vehicles
                    .Include(v => v.VehicleModels)
                    .Include(v => v.VehicleModels.VehicleMakes)
                    .FirstOrDefault(v => v.PolicyID == policy.PolicyID);

                if (vehicle != null)
                {
                    VehicleMakeComboBox.IsEnabled = false;
                    VehicleModelComboBox.IsEnabled = false;
                    VinTextBox.IsReadOnly = true;
                    YearTextBox.IsReadOnly = true;
                    LicensePlateTextBox.IsReadOnly = true;
                    EnginePowerTextBox.IsReadOnly = true;

                    VehicleMakeComboBox.SelectedItem = vehicle.VehicleModels.VehicleMakes;
                    UpdateComboBoxPlaceholder(VehicleMakeComboBox, VehicleMakePlaceholder);

                    VehicleModelComboBox.SelectedItem = vehicle.VehicleModels;
                    UpdateComboBoxPlaceholder(VehicleModelComboBox, VehicleModelPlaceholder);

                    VinTextBox.Text = vehicle.VIN;
                    UpdateTextBoxPlaceholder(VinTextBox, VinPlaceholder);

                    YearTextBox.Text = vehicle.Year.ToString();
                    UpdateTextBoxPlaceholder(YearTextBox, YearPlaceholder);

                    LicensePlateTextBox.Text = vehicle.LicensePlate;
                    UpdateTextBoxPlaceholder(LicensePlateTextBox, LicensePlatePlaceholder);

                    if (!string.IsNullOrEmpty(vehicle.RegistrationRegion))
                    {
                        string regionName = vehicle.RegistrationRegion;
                        if (regionName.Contains(":"))
                        {
                            regionName = regionName.Split(':').Last().Trim();
                        }
                        var regionItem = RegionComboBox.Items.Cast<ComboBoxItem>()
                            .FirstOrDefault(item => item.Content.ToString() == regionName);
                        if (regionItem != null)
                        {
                            RegionComboBox.SelectedItem = regionItem;
                            UpdateComboBoxPlaceholder(RegionComboBox, RegionPlaceholder);
                        }
                    }

                    if (vehicle.EnginePower.HasValue)
                    {
                        EnginePowerTextBox.Text = vehicle.EnginePower.Value.ToString();
                        UpdateTextBoxPlaceholder(EnginePowerTextBox, EnginePowerPlaceholder);
                    }
                }

                if (SelectedDrivers.Any())
                {
                    RemoveDriverButton.Visibility = Visibility.Visible;
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