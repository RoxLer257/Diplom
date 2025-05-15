using Diplom.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Diplom.Pages.AddPrintPage
{
    /// <summary>
    /// Логика взаимодействия для AddPropertyInsurance.xaml
    /// </summary>
    public partial class AddPropertyInsurance : Page
    {
        private readonly VSK_DBEntities _context;
        private ObservableCollection<Diplom.Classes.Properties> SelectedProperties { get; set; }
        private List<Clients> _selectedClients; // Список выбранных клиентов
        public AddPropertyInsurance()
        {
            InitializeComponent();
            _context = VSK_DBEntities.GetContext();
            SelectedProperties = new ObservableCollection<Diplom.Classes.Properties>();
            _selectedClients = new List<Clients>(); // Инициализируем список клиентов

            LoadData();
            InitializePlaceholders();
            SetPolicyType("Страхование имущества");
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
            // Загрузка типов клиентов
            var clientTypes = _context.ClientTypes.ToList();
            ClientTypeComboBox.ItemsSource = clientTypes;

            // Загрузка типов имущества
            var propertyTypes = _context.PropertyTypes.ToList();
            PropertyTypeComboBox.ItemsSource = propertyTypes;

            // Загрузка типов полисов
            var policyTypes = _context.PolicyTypes.ToList();
            PolicyTypeComboBox.ItemsSource = policyTypes;

            // Загрузка статусов полисов
            var policyStatuses = _context.PolicyStatuses.ToList();
            StatusComboBox.ItemsSource = policyStatuses;

            // Привязка выбранных клиентов
            ClientsListBox.ItemsSource = _selectedClients;

            // Привязка добавленного имущества
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
        //ВОЗМОЖНО ОН ВЛИЯЕТ НА УСТАНОВКУ КОМБОБОКСА - ПРОВЕРИТЬ!!!
        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (sender is ComboBox comboBox)
        //    {
        //        UpdateComboBoxPlaceholder(comboBox, comboBox.Tag as TextBlock);
        //    }
        //}
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == ClientTypeComboBox)
            {
                if (ClientTypeComboBox.SelectedItem is ClientTypes selectedType)
                {
                    if (selectedType.ClientTypeID == 1) // Физическое лицо
                    {
                        GrdLastName.Visibility = Visibility.Visible;
                        GrdFirstName.Visibility = Visibility.Visible;
                        GrdMiddleName.Visibility = Visibility.Visible;
                        GrdCompany.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedType.ClientTypeID == 2) // Юридическое лицо
                    {
                        GrdLastName.Visibility = Visibility.Collapsed;
                        GrdFirstName.Visibility = Visibility.Collapsed;
                        GrdMiddleName.Visibility = Visibility.Collapsed;
                        GrdCompany.Visibility = Visibility.Visible;
                    }
                }
            }
            UpdateComboBoxPlaceholder(ClientTypeComboBox, ClientTypePlaceholder);
            //UpdateComboBoxPlaceholder(PolicyTypeComboBox, PolicyTypePlaceholder);
            UpdateComboBoxPlaceholder(StatusComboBox, StatusPlaceholder);
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

            // Показываем поля ФИО, скрываем "Название компании"
            GrdLastName.Visibility = Visibility.Visible;
            GrdFirstName.Visibility = Visibility.Visible;
            GrdMiddleName.Visibility = Visibility.Visible;
            GrdCompany.Visibility = Visibility.Collapsed;

            // Сбрасываем выбор или задаём тип клиента по умолчанию
            ClientTypeComboBox.SelectedIndex = -1;
        }
        private void CreateClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientTypeComboBox.SelectedItem is ClientTypes selectedClientType &&
                !string.IsNullOrWhiteSpace(PhoneTextBox.Text) &&
                !string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                try
                {
                    var newClient = new Clients
                    {
                        ClientTypeID = selectedClientType.ClientTypeID,
                        AgentID = CurrentUser.EmployeeID, // Используем EmployeeID из CurrentUser
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

                    // Логирование добавления клиента
                    LogAction("Clients", "Добавление", $"Добавлен клиент: {newClient.ClientID}");

                    // Добавляем нового клиента в список выбранных
                    if (!_selectedClients.Any(c => c.ClientID == newClient.ClientID))
                    {
                        _selectedClients.Add(newClient);
                        ClientsListBox.ItemsSource = null;
                        ClientsListBox.ItemsSource = _selectedClients;
                    }

                    // Очищаем поля для создания клиента и скрываем панель
                    ClearClientForm();
                    NewClientPanel.Visibility = Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля (тип клиента, телефон, email).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            if (PropertyTypeComboBox.SelectedItem is PropertyTypes selectedPropertyType &&
                !string.IsNullOrWhiteSpace(AddressTextBox.Text) &&
                decimal.TryParse(AreaTextBox.Text, out decimal area) &&
                decimal.TryParse(ValueTextBox.Text, out decimal value))
            {
                if (area <= 0 || value <= 0)
                {
                    MessageBox.Show("Площадь и стоимость должны быть больше 0.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    var newProperty = new Diplom.Classes.Properties
                    {
                        PropertyTypeID = selectedPropertyType.PropertyTypeID,
                        Address = AddressTextBox.Text,
                        Area = area,
                        Value = value
                    };

                    SelectedProperties.Add(newProperty);

                    // Очищаем поля
                    PropertyTypeComboBox.SelectedItem = null;
                    AddressTextBox.Text = string.Empty;
                    AreaTextBox.Text = string.Empty;
                    ValueTextBox.Text = string.Empty;

                    UpdateComboBoxPlaceholder(PropertyTypeComboBox, PropertyTypePlaceholder);
                    UpdateTextBoxPlaceholder(AddressTextBox, AddressPlaceholder);
                    UpdateTextBoxPlaceholder(AreaTextBox, AreaPlaceholder);
                    UpdateTextBoxPlaceholder(ValueTextBox, ValuePlaceholder);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении имущества: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля: тип имущества, адрес, площадь, стоимость.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedClients.Any() &&
                PolicyTypeComboBox.SelectedItem is PolicyTypes selectedPolicyType &&
                StatusComboBox.SelectedItem is PolicyStatuses selectedStatus &&
                decimal.TryParse(InsuranceAmountTextBox.Text, out decimal insuranceAmount) &&
                StartDatePicker.SelectedDate.HasValue &&
                EndDatePicker.SelectedDate.HasValue)
            {
                // Дополнительные проверки
                if (insuranceAmount <= 0)
                {
                    MessageBox.Show("Сумма страхования должна быть больше 0.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (EndDatePicker.SelectedDate.Value <= StartDatePicker.SelectedDate.Value)
                {
                    MessageBox.Show("Дата окончания должна быть позже даты начала.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!SelectedProperties.Any())
                {
                    MessageBox.Show("Добавьте хотя бы одно имущество.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    // Создаём новый полис
                    var policy = new Policies
                    {
                        PolicyTypeID = selectedPolicyType.PolicyTypeID,
                        StatusID = selectedStatus.StatusID,
                        InsuranceAmount = insuranceAmount,
                        StartDate = StartDatePicker.SelectedDate.Value,
                        EndDate = EndDatePicker.SelectedDate.Value
                    };

                    // Добавляем клиентов в коллекцию навигационного свойства
                    foreach (var client in _selectedClients)
                    {
                        policy.Clients.Add(client);
                    }

                    // Добавляем имущество
                    foreach (var property in SelectedProperties)
                    {
                        property.Policies = policy; // Связываем имущество с полисом
                        _context.Properties.Add(property);
                    }

                    _context.Policies.Add(policy);
                    _context.SaveChanges();

                    // Логирование добавления полиса
                    LogAction("Policies", "Добавление", $"Добавлен полис: {policy.PolicyID} с {policy.Clients.Count} клиентами и {SelectedProperties.Count} объектами имущества");

                    MessageBox.Show("Полис успешно сохранён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Очищаем форму после сохранения
                    ClearForm();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    var errorMessages = dbEx.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                    var fullErrorMessage = string.Join("; ", errorMessages);
                    var exceptionMessage = $"Ошибка валидации: {fullErrorMessage}";
                    MessageBox.Show(exceptionMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, проверьте все обязательные поля и добавьте хотя бы одного клиента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            // Очищаем поля имущества
            PropertyTypeComboBox.SelectedItem = null;
            AddressTextBox.Text = string.Empty;
            AreaTextBox.Text = string.Empty;
            ValueTextBox.Text = string.Empty;

            // Очищаем поля клиента и скрываем панель
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
                UserName = CurrentUser.Email ?? "System", // Используем Email из CurrentUser
                EmployeeID = CurrentUser.EmployeeID // Используем EmployeeID из CurrentUser
            };
            _context.Logs.Add(log);
            _context.SaveChanges();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            NavigationService.GoBack();
        }
    }
}
