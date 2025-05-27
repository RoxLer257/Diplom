using Diplom.Classes;
using Diplom.Pages.AddPrintPage;
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
using System.Data.Entity;
using System.IO;

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
                    if (string.IsNullOrWhiteSpace(search))
                    {
                        DtgClients.ItemsSource = ClassFrame.ConnectDB.Clients.ToList(); // Полный список клиентов
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
                        DtgContracts.ItemsSource = ClassFrame.ConnectDB.Policies.ToList(); // Полный список договоров
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
                FIOSearchTxtBox.Text = string.Empty; // Очищаем поле поиска по ФИО
                string search = textBox.Text;
                if (string.IsNullOrWhiteSpace(search))
                {
                    DtgContracts.ItemsSource = ClassFrame.ConnectDB.Policies.ToList(); // Полный список при пустом поиске
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
            DtgClients.ItemsSource = ClassFrame.ConnectDB.Clients.ToList();
            DtgContracts.ItemsSource = ClassFrame.ConnectDB.Policies.ToList();
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
                DtgClients.ItemsSource = ClassFrame.ConnectDB.Clients.ToList(); // Сбрасываем фильтр клиентов при переключении
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
            DtgContracts.ItemsSource = ClassFrame.ConnectDB.Policies.ToList(); // Сбрасываем фильтр договоров
            DtgClients.ItemsSource = ClassFrame.ConnectDB.Clients.ToList(); // Сбрасываем фильтр клиентов
        }

        private void SupportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AutoInsurance_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new Pages.AddPolicyPage(/*null*/));
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
            ClassFrame.frmObj.Navigate(new Pages.AddPolicyPage(/*null*/));
        }

        private void BtnKasko_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frmObj.Navigate(new Pages.AddPolicyPage(/*null*/));
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
            //if (DtgContracts.SelectedItem is Policies selectedPolicy)
            //{
            //    var fullPolicy = ClassFrame.ConnectDB.Policies
            //        .Include(p => p.Clients)
            //        .Include(p => p.Drivers)
            //        .Include(p => p.Vehicles)
            //        .FirstOrDefault(p => p.PolicyID == selectedPolicy.PolicyID);

            //    if (fullPolicy == null)
            //    {
            //        MessageBox.Show("Не удалось найти полис для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //        return;
            //    }

            //    switch (fullPolicy.PolicyTypeID)
            //    {
            //        case 1: // автострахование
            //            ClassFrame.frmObj.Navigate(new Pages.AddPolicyPage(fullPolicy));
            //            break;
            //        case 2: // страхование жизни
            //            ClassFrame.frmObj.Navigate(new Pages.AddPrintPage.AddLifeInsurancePage(fullPolicy));
            //            break;
            //        case 3: // страхование имущества
            //            ClassFrame.frmObj.Navigate(new Pages.AddPrintPage.AddPropertyInsurance(fullPolicy));
            //            break;
            //        default:
            //            MessageBox.Show("Редактирование данного типа полиса пока не поддерживается.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            //            break;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Выберите полис для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }

    }
}
