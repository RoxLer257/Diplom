using Diplom.Classes;
using Diplom.Pages.AddPrintPage;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Data.Entity;
using System.IO;
using Xceed.Words.NET;
using System;
using System.Text;
using Xceed.Document.NET;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Diplom.Pages.MainPage
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private bool isContractsVisible = false;
        private bool isClientsVisible = false;
        private bool isCatalogVisible = false;
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;

            InitializePlaceholders();
            ResetContent();

            if (CurrentUser.RoleName == "Страховой агент")
            {
                ClientsButton.Visibility = Visibility.Collapsed;
                DtgClients.Visibility = Visibility.Collapsed;
            }
        }

        private void InitializePlaceholders()
        {
            UpdateTextBoxPlaceholder(FIOSearchTxtBox, FIOSearchTxtBlock);
            UpdateTextBoxPlaceholder(PolicySearchTxtBox, PolicySearchTxtBlock);
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
                string search = textBox.Text;

                if (isClientsVisible)
                {
                    if (CurrentUser.RoleName == "Страховой агент")
                    {
                        DtgClients.ItemsSource = null;
                    }
                    else if (string.IsNullOrWhiteSpace(search))
                    {
                        DtgClients.ItemsSource = ClassFrame.ConnectDB.Clients.ToList();
                    }
                    else
                    {
                        DtgClients.ItemsSource = ClassFrame.ConnectDB.Clients
                            .Where(c =>
                                (c.ClientTypeID == 1 && (c.LastName + " " + c.FirstName + " " + (c.MiddleName ?? "")).Contains(search)) ||
                                (c.ClientTypeID == 2 && (c.CompanyName ?? "").Contains(search))
                            )
                            .ToList();
                    }
                }
                else if (isContractsVisible)
                {
                    if (string.IsNullOrWhiteSpace(search))
                    {
                        DtgContracts.ItemsSource = ClassFrame.ConnectDB.Policies.ToList();
                    }
                    else
                    {
                        DtgContracts.ItemsSource = ClassFrame.ConnectDB.Policies
                            .Where(p => p.Clients.Any(c =>
                                (c.ClientTypeID == 1 && (c.LastName + " " + c.FirstName + " " + (c.MiddleName ?? "")).Contains(search)) ||
                                (c.ClientTypeID == 2 && (c.CompanyName ?? "").Contains(search))
                            ))
                            .ToList();
                    }
                }
            }
        }
        private void PolicyNumberSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                FIOSearchTxtBox.Text = string.Empty; 
                string search = textBox.Text;
                if (string.IsNullOrWhiteSpace(search))
                {
                    DtgContracts.ItemsSource = ClassFrame.ConnectDB.Policies.ToList();
                }
                else
                {
                    DtgContracts.ItemsSource = ClassFrame.ConnectDB.Policies
                        .AsEnumerable()
                        .Where(p => p.PolicyID.ToString().Contains(search))
                        .ToList();
                }
            }
        }

        private void UpdateTextBoxPlaceholder(TextBox textBox, TextBlock placeholder)
        {
            if (placeholder != null)
            {
                placeholder.Visibility = string.IsNullOrWhiteSpace(textBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTables();
        }

        public void UpdateTables()
        {
            if (CurrentUser.RoleName == "Страховой агент")
            {
                DtgContracts.ItemsSource = ClassFrame.ConnectDB.Policies.ToList();
                DtgClients.ItemsSource = null; 
            }
            else
            {
                DtgClients.ItemsSource = ClassFrame.ConnectDB.Clients.ToList();
                DtgContracts.ItemsSource = ClassFrame.ConnectDB.Policies.ToList();
            }
        }

        private void ExitAcc_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PersonalAcc_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new Pages.MainPage.EmployeeProfilePage());
        }

        private void CatalogButton_Click(object sender, RoutedEventArgs e)
        {
            if (isCatalogVisible)
            {
                ResetContent();
                CatalogButton.Background = Brushes.Transparent;
            }
            else
            {
                PolicyButtonsStackPanel.Visibility = Visibility.Collapsed;
                DtgContracts.Visibility = Visibility.Collapsed;
                DtgClients.Visibility = Visibility.Collapsed;
                CatalogPanel.Visibility = Visibility.Visible;
                FIOSearchGrid.Visibility = Visibility.Collapsed;
                PolicySearchGrid.Visibility = Visibility.Collapsed;
                CatalogButton.Background = new SolidColorBrush(Color.FromRgb(190, 230, 255));
                ContractsButton.Background = Brushes.Transparent;
                ClientsButton.Background = Brushes.Transparent;
                isCatalogVisible = true;
                isContractsVisible = false;
                isClientsVisible = false;
            }
        }

        private void ContractsButton_Click(object sender, RoutedEventArgs e)
        {
            if (isContractsVisible)
            {
                ResetContent();
                ContractsButton.Background = Brushes.Transparent;
            }
            else
            {
                PolicyButtonsStackPanel.Visibility = Visibility.Collapsed;
                DtgContracts.Visibility = Visibility.Visible;
                DtgClients.Visibility = Visibility.Collapsed;
                CatalogPanel.Visibility = Visibility.Collapsed;
                FIOSearchGrid.Visibility = Visibility.Visible;
                PolicySearchGrid.Visibility = Visibility.Visible;
                ContractsButton.Background = new SolidColorBrush(Color.FromRgb(190, 230, 255));
                CatalogButton.Background = Brushes.Transparent;
                ClientsButton.Background = Brushes.Transparent;
                isContractsVisible = true;
                isClientsVisible = false;
                isCatalogVisible = false;
                DtgContracts.ItemsSource = ClassFrame.ConnectDB.Policies.ToList();
            }
        }
        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            if (isClientsVisible)
            {
                ResetContent();
                ClientsButton.Background = Brushes.Transparent;
            }
            else
            {
                PolicyButtonsStackPanel.Visibility = Visibility.Collapsed;
                DtgContracts.Visibility = Visibility.Collapsed;
                DtgClients.Visibility = Visibility.Visible;
                CatalogPanel.Visibility = Visibility.Collapsed;
                FIOSearchGrid.Visibility = Visibility.Visible;
                PolicySearchGrid.Visibility = Visibility.Collapsed;
                ClientsButton.Background = new SolidColorBrush(Color.FromRgb(190, 230, 255));
                ContractsButton.Background = Brushes.Transparent;
                CatalogButton.Background = Brushes.Transparent;
                isClientsVisible = true;
                isContractsVisible = false;
                isCatalogVisible = false;
                if (CurrentUser.RoleName == "Страховой агент")
                {
                    DtgClients.ItemsSource = null;
                }
                else
                {
                    DtgClients.ItemsSource = ClassFrame.ConnectDB.Clients.ToList();
                }
            }
        }

        private void ResetContent()
        {
            PolicyButtonsStackPanel.Visibility = Visibility.Visible;
            DtgContracts.Visibility = Visibility.Collapsed;
            DtgClients.Visibility = Visibility.Collapsed;
            CatalogPanel.Visibility = Visibility.Collapsed;
            FIOSearchGrid.Visibility = Visibility.Collapsed;
            PolicySearchGrid.Visibility = Visibility.Collapsed;
            isContractsVisible = false;
            isClientsVisible = false;
            isCatalogVisible = false;
            FIOSearchTxtBox.Text = string.Empty;
            PolicySearchTxtBox.Text = string.Empty;
            InitializePlaceholders();
            if (CurrentUser.RoleName == "Страховой агент")
            {
                DtgContracts.ItemsSource = ClassFrame.ConnectDB.Policies.ToList();
                DtgClients.ItemsSource = null;
            }
            else
            {
                DtgContracts.ItemsSource = ClassFrame.ConnectDB.Policies.ToList();
                DtgClients.ItemsSource = ClassFrame.ConnectDB.Clients.ToList();
            }
        }

        private void SupportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AutoInsurance_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new Pages.AddPolicyPage());
        }

        private void LifeInsurance_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new Pages.AddPrintPage.AddLifeInsurancePage());
        }

        private void PropertyInsurance_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new AddPropertyInsurance());
        }

        private void BtnOsago_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new Pages.AddPolicyPage());
        }

        private void BtnKasko_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new Pages.AddPolicyPage());
        }

        private void BtnLifeInsurance_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new Pages.AddPrintPage.AddLifeInsurancePage());
        }

        private void BtnPropertyInsurance_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new Pages.AddPrintPage.AddPropertyInsurance());
        }
        private void Prolongpolicy_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Policies selectedPolicy)
            {
                var fullPolicy = ClassFrame.ConnectDB.Policies
                    .Include(p => p.Clients)
                    .Include(p => p.Drivers)
                    .Include(p => p.Vehicles)
                    .FirstOrDefault(p => p.PolicyID == selectedPolicy.PolicyID);

                if (fullPolicy == null)
                {
                    MessageBox.Show("Не удалось найти полис для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                switch (fullPolicy.PolicyTypeID)
                {
                    case 1:
                        ClassFrame.frmObj.Navigate(new AddPolicyPage(fullPolicy));
                        break;
                    case 2: 
                        ClassFrame.frmObj.Navigate(new AddPolicyPage(fullPolicy));
                        break;
                    case 3: 
                        ClassFrame.frmObj.Navigate(new AddPropertyInsurance(fullPolicy));
                        break;
                    case 4:
                        ClassFrame.frmObj.Navigate(new AddLifeInsurancePage(fullPolicy));
                        break;
                    default:
                        MessageBox.Show("Редактирование данного типа полиса пока не поддерживается.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Выберите полис для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DownloadPolicy_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Policies selectedPolicy)
            {
                var fullPolicy = ClassFrame.ConnectDB.Policies
                    .Include(p => p.Clients)
                    .Include(p => p.Drivers)
                    .Include(p => p.Vehicles.Select(v => v.VehicleModels.VehicleMakes))
                    .Include(p => p.Properties.Select(pr => pr.PropertyTypes))
                    .Include(p => p.PolicyTypes)
                    .Include(p => p.PolicyStatuses)
                    .FirstOrDefault(p => p.PolicyID == selectedPolicy.PolicyID);

                if (fullPolicy == null)
                {
                    MessageBox.Show("Не удалось найти полис для скачивания.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DownloadPolicyFile(fullPolicy);
            }
            else
            {
                MessageBox.Show("Выберите полис для скачивания.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void DownloadPolicyFile(Policies policy)
        {
            try
            {
                var client = policy.Clients.FirstOrDefault();
                string templatePath;
                if (policy.PolicyTypes?.TypeName == "ОСАГО" || policy.PolicyTypes?.TypeName == "КАСКО")
                {
                    templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Шаблоны полисов", "Страхование авто.docx");
                }
                else if (policy.PolicyTypes?.TypeName == "Страхование жизни")
                {
                    templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Шаблоны полисов", "Образец полиса страхования жизни и здоровья.docx");
                }
                else if (policy.PolicyTypes?.TypeName == "Страхование имущества")
                {
                    templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Шаблоны полисов", "Полис страхования имущества.docx");
                }
                else
                {
                    MessageBox.Show("Данный тип полиса не поддерживается для скачивания.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!File.Exists(templatePath))
                {
                    MessageBox.Show($"Не найден шаблон: {templatePath}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var document = Xceed.Words.NET.DocX.Load(templatePath))
                {
                    string currentUserInfo = $"{CurrentUser.FullName ?? "Не указан"} {CurrentUser.RoleName ?? "Не указана"}".Trim();
                    document.ReplaceText("{CurrentUser.FullName} {CurrentUser.RoleName}", currentUserInfo);

                    document.ReplaceText("{PolicyID}", policy.PolicyID.ToString());
                    document.ReplaceText("{StartDate}", policy.StartDate.ToString("dd.MM.yyyy"));
                    document.ReplaceText("{EndDate}", policy.EndDate.ToString("dd.MM.yyyy"));
                    document.ReplaceText("{ContractDate}", policy.StartDate.ToString("dd.MM.yyyy"));

                    if (client != null)
                    {
                        string clientType = client.ClientTypeID == 1 ? "Физическое лицо" : "Юридическое лицо";
                        string clientName = client.ClientTypeID == 1
                            ? $"{client.LastName} {client.FirstName} {client.MiddleName ?? ""}"
                            : client.CompanyName ?? "";

                        document.ReplaceText("{ClientFullName}", clientName.Trim());
                        document.ReplaceText("{CompanyName}", client.CompanyName ?? "");
                        document.ReplaceText("{TypeName}", clientType);
                        document.ReplaceText("{INN}", client.INN ?? "Не указан");
                        document.ReplaceText("{PassportNumber}", client.PassportNumber ?? "Не указан");
                        document.ReplaceText("{Phone}", client.Phone ?? "Не указан");

                        if (policy.PolicyTypes?.TypeName == "Страхование жизни")
                        {
                            var lifeAndHealth = policy.LifeAndHealth?.FirstOrDefault();
                            int? age = lifeAndHealth?.Age;
                            if (age == null || age < 1 || age > 120)
                            {
                                MessageBox.Show("Возраст не найден или некорректен в записи LifeAndHealth!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            int year = DateTime.Today.Year - age.Value;
                            Random random = new Random();
                            int month = random.Next(1, 13); 
                            int maxDay = DateTime.DaysInMonth(year, month);
                            int day = random.Next(1, maxDay + 1); 
                            DateTime birthDate = new DateTime(year, month, day);
                            string birthDateStr = birthDate.ToString("dd.MM.yyyy");
                            document.ReplaceText("{DateTime.Today – Age}", birthDateStr);
                            document.ReplaceText("{DateOfBirth}", birthDateStr);

                            string amountInWords = ConvertToWords(policy.InsuranceAmount);
                            document.ReplaceText("{InsuranceAmount}", $"{policy.InsuranceAmount:N2} ({amountInWords}) рублей");

                        }
                    }

                    string todayStr = DateTime.Today.ToString("dd.MM.yyyy");
                    document.ReplaceText("{DateTime.Today}", todayStr);

                    if (policy.PolicyTypes?.TypeName == "ОСАГО" || policy.PolicyTypes?.TypeName == "КАСКО")
                    {
                        var vehicle = policy.Vehicles.FirstOrDefault();
                        if (vehicle != null)
                        {
                            document.ReplaceText("{MakeName}", vehicle.VehicleModels?.VehicleMakes?.MakeName ?? "");
                            document.ReplaceText("{ModelName}", vehicle.VehicleModels?.ModelName ?? "");
                            document.ReplaceText("{Year}", vehicle.Year.ToString());
                            document.ReplaceText("{LicensePlate}", vehicle.LicensePlate ?? "");
                            document.ReplaceText("{VIN}", vehicle.VIN ?? "Не указан");
                        }

                        string driverAccessType = policy.Drivers.Any()
                            ? "лиц, допущенных к управлению транспортным средством"
                            : "неограниченного количества лиц, допущенных к управлению транспортным средством";
                        document.ReplaceText("{DriverAccessType}", driverAccessType);

                        if (policy.Drivers.Any())
                        {
                            Table driverTable = null;
                            foreach (var table in document.Tables)
                            {
                                foreach (var row in table.Rows)
                                {
                                    foreach (var cell in row.Cells)
                                    {
                                        if (cell.Paragraphs.Any(p => p.Text.Contains("{FullName}")))
                                        {
                                            driverTable = table;
                                            break;
                                        }
                                    }
                                    if (driverTable != null) break;
                                }
                                if (driverTable != null) break;
                            }

                            if (driverTable != null)
                            {
                                Row templateRow = null;
                                int templateRowIndex = -1;
                                for (int i = 0; i < driverTable.Rows.Count; i++)
                                {
                                    if (driverTable.Rows[i].Cells.Any(c => c.Paragraphs.Any(p => p.Text.Contains("{FullName}"))))
                                    {
                                        templateRow = driverTable.Rows[i];
                                        templateRowIndex = i;
                                        break;
                                    }
                                }

                                if (templateRow != null && templateRowIndex >= 0)
                                {
                                    driverTable.RemoveRow(templateRowIndex);
                                }
                                else
                                {
                                    templateRowIndex = driverTable.RowCount;
                                }

                                int driverNumber = 1;
                                foreach (var driver in policy.Drivers)
                                {
                                    var driverKbm = ClassFrame.ConnectDB.DriverInsuranceHistory
                                        .Where(h => h.DriverID == driver.DriverID)
                                        .OrderByDescending(h => h.LastUpdated)
                                        .FirstOrDefault()?.KBM;

                                    string kbmValue = driverKbm.HasValue ? ((double)driverKbm.Value).ToString("N2") : "Не указан";
                                    string driverFullName = $"{driver.LastName} {driver.FirstName} {driver.MiddleName ?? ""}".Trim();
                                    string licenseNumber = driver.LicenseNumber ?? "Не указан";

                                    var newRow = driverTable.InsertRow(templateRowIndex);
                                    newRow.Cells[0].Paragraphs.First().Append(driverNumber.ToString());
                                    newRow.Cells[1].Paragraphs.First().Append(driverFullName); 
                                    newRow.Cells[2].Paragraphs.First().Append(licenseNumber); 
                                    newRow.Cells[4].Paragraphs.First().Append(kbmValue);

                                    templateRowIndex++;
                                    driverNumber++;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Таблица водителей не найдена в шаблоне.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        else
                        {
                            Table driverTable = null;
                            foreach (var table in document.Tables)
                            {
                                foreach (var row in table.Rows)
                                {
                                    foreach (var cell in row.Cells)
                                    {
                                        if (cell.Paragraphs.Any(p => p.Text.Contains("{FullName}")))
                                        {
                                            driverTable = table;
                                            break;
                                        }
                                    }
                                    if (driverTable != null) break;
                                }
                                if (driverTable != null) break;
                            }

                            if (driverTable != null)
                            {
                                Row templateRow = null;
                                int templateRowIndex = -1;
                                for (int i = 0; i < driverTable.Rows.Count; i++)
                                {
                                    if (driverTable.Rows[i].Cells.Any(c => c.Paragraphs.Any(p => p.Text.Contains("{FullName}"))))
                                    {
                                        templateRow = driverTable.Rows[i];
                                        templateRowIndex = i;
                                        break;
                                    }
                                }

                                if (templateRow != null && templateRowIndex >= 0)
                                {
                                    driverTable.RemoveRow(templateRowIndex);
                                }
                                else
                                {
                                    templateRowIndex = driverTable.RowCount;
                                }

                                var newRow = driverTable.InsertRow(templateRowIndex);
                                newRow.Cells[0].Paragraphs.First().Append("1");
                                newRow.Cells[1].Paragraphs.First().Append("Не указаны");
                                newRow.Cells[2].Paragraphs.First().Append("");
                                newRow.Cells[3].Paragraphs.First().Append("");
                            }
                        }

                        var calculator = new Diplom.Classes.Calculator.InsuranceCalculator(ClassFrame.ConnectDB);
                        double tb = 0, kt = 0, kbm = 0, kvs = 0, ko = 0, ks = 0, km = 0;

                        if (vehicle != null && policy.Drivers.Any())
                        {
                            tb = (1646 + 7535) / 2.0; 
                            kt = 1.8; 
                            kvs = policy.Drivers.Min(d => calculator.CalculateKVS(d));
                            km = calculator.CalculateKM(vehicle.EnginePower ?? 100);
                            ks = calculator.CalculateKS(policy.StartDate, policy.EndDate);
                            ko = policy.Drivers.Count > 1 ? 2.32 : 1.0;

                            kbm = policy.Drivers.Max(d =>
                            {
                                var driverKbm = ClassFrame.ConnectDB.DriverInsuranceHistory
                                    .Where(h => h.DriverID == d.DriverID)
                                    .OrderByDescending(h => h.LastUpdated)
                                    .FirstOrDefault()?.KBM;
                                return driverKbm.HasValue ? (double)driverKbm.Value : 1.0;
                            });
                        }
                        else
                        {
                            kbm = 1.0; 
                        }

                        document.ReplaceText("{tb}", tb.ToString("N2"));
                        document.ReplaceText("{kt}", kt.ToString("N2"));
                        document.ReplaceText("{KBM}", kbm.ToString("N2"));
                        document.ReplaceText("{kvs}", kvs.ToString("N2"));
                        document.ReplaceText("{ko}", ko.ToString("N2"));
                        document.ReplaceText("{ks}", ks.ToString("N2"));
                        document.ReplaceText("{km}", km.ToString("N2"));
                        document.ReplaceText("{TotalAmount}", policy.InsuranceAmount.ToString("N2") + " руб.");
                        document.ReplaceText("{InsuranceAmount}", policy.InsuranceAmount.ToString("N2") + " руб.");

                    }
                    else if (policy.PolicyTypes?.TypeName == "Страхование имущества")
                    {
                        string clientType = client?.ClientTypeID == 1 ? "Физическое лицо" : "Юридическое лицо";
                        string clientName = client?.ClientTypeID == 1
                            ? $"{client.LastName} {client.FirstName} {client.MiddleName ?? ""}"
                            : client?.CompanyName ?? "";
                        document.ReplaceText("{ClientTypes.TypeName}", clientType);
                        document.ReplaceText("{ClientFullName}", clientName.Trim());
                        document.ReplaceText("{CurrentUser.RoleName} {CurrentUser.FullName}", $"{CurrentUser.RoleName} {CurrentUser.FullName}");
                        document.ReplaceText("{PolicyID}", policy.PolicyID.ToString());
                        document.ReplaceText("{StartDate}", policy.StartDate.ToString("dd.MM.yyyy"));
                        document.ReplaceText("{EndDate}", policy.EndDate.ToString("dd.MM.yyyy"));

                        var property = policy.Properties?.FirstOrDefault();
                        document.ReplaceText("{Address}", property?.Address ?? "");
                        document.ReplaceText("{PropertyTypes.TypeName}", property?.PropertyTypes?.TypeName ?? "");

                        string amountInWords = ConvertToWords(policy.InsuranceAmount);
                        document.ReplaceText("{InsuranceAmount}", $"{policy.InsuranceAmount:N2} ({amountInWords}) рублей");

                        int months = ((policy.EndDate.Year - policy.StartDate.Year) * 12 + policy.EndDate.Month - policy.StartDate.Month);
                        if (policy.EndDate.Day < policy.StartDate.Day) months--;
                        months = Math.Max(1, months); 
                        string monthsText = $"{months} ({ConvertToWords(months)}) календарный месяц";
                        document.ReplaceText("{EndDate-StartDate}", monthsText);
                    }

                    var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                    {
                        Filter = "Word Document (*.docx)|*.docx",
                        FileName = $"Полис_{policy.PolicyID}_{DateTime.Now:yyyyMMdd}.docx"
                    };

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        document.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Файл успешно сохранён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании файла: {ex.Message}\n\nПодробности: {ex.StackTrace}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string ConvertToWords(decimal number)
        {
            string[] units = { "", "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять" };
            string[] teens = { "десять", "одиннадцать", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать", "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать" };
            string[] tens = { "", "десять", "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто" };
            string[] hundreds = { "", "сто", "двести", "триста", "четыреста", "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот" };
            string[] thousands = { "", "тысяча", "тысячи", "тысяч" };
            string[] millions = { "", "миллион", "миллиона", "миллионов" };

            if (number == 0) return "ноль";

            string result = "";
            int intPart = (int)number;
            int decimalPart = (int)((number - intPart) * 100);

            if (intPart >= 1000000)
            {
                int millionsCount = intPart / 1000000;
                result += GetNumberInWords(millionsCount, hundreds, tens, units, teens) + " " + GetCorrectForm(millionsCount, millions) + " ";
                intPart %= 1000000;
            }

            if (intPart >= 1000)
            {
                int thousandsCount = intPart / 1000;
                result += GetNumberInWords(thousandsCount, hundreds, tens, units, teens) + " " + GetCorrectForm(thousandsCount, thousands) + " ";
                intPart %= 1000;
            }

            if (intPart > 0)
            {
                result += GetNumberInWords(intPart, hundreds, tens, units, teens);
            }

            if (decimalPart > 0)
            {
                result += " " + decimalPart.ToString("D2") + " копеек";
            }

            return result.Trim();
        }

        private string GetNumberInWords(int number, string[] hundreds, string[] tens, string[] units, string[] teens)
        {
            string result = "";
            if (number >= 100)
            {
                result += hundreds[number / 100] + " ";
                number %= 100;
            }
            if (number >= 10 && number <= 19)
            {
                result += teens[number - 10] + " ";
                return result;
            }
            if (number >= 20)
            {
                result += tens[number / 10] + " ";
                number %= 10;
            }
            if (number > 0)
            {
                result += units[number] + " ";
            }
            return result;
        }

        private string GetCorrectForm(int number, string[] forms)
        {
            number = number % 100;
            if (number >= 11 && number <= 19)
                return forms[3];
            int lastDigit = number % 10;
            if (lastDigit == 1)
                return forms[1];
            if (lastDigit >= 2 && lastDigit <= 4)
                return forms[2];
            return forms[3];
        }
    }
}
