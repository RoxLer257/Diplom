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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                // Если каталог уже виден, возвращаем кнопки и сбрасываем цвет
                ResetContent();
                CatalogButton.Background = Brushes.Transparent;
            }
            else
            {
                // Показываем каталог и окрашиваем кнопку
                PolicyButtonsStackPanel.Visibility = Visibility.Collapsed;
                DtgContracts.Visibility = Visibility.Collapsed;
                DtgClients.Visibility = Visibility.Collapsed;
                CatalogPanel.Visibility = Visibility.Visible;
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
                // Если таблица договоров уже видна, возвращаем кнопки и сбрасываем цвет
                ResetContent();
                ContractsButton.Background = Brushes.Transparent;
            }
            else
            {
                // Показываем таблицу договоров и окрашиваем кнопку
                PolicyButtonsStackPanel.Visibility = Visibility.Collapsed;
                DtgContracts.Visibility = Visibility.Visible;
                DtgClients.Visibility = Visibility.Collapsed;
                CatalogPanel.Visibility = Visibility.Collapsed;
                ContractsButton.Background = new SolidColorBrush(Color.FromRgb(190, 230, 255));
                CatalogButton.Background = Brushes.Transparent;
                ClientsButton.Background = Brushes.Transparent;
                isContractsVisible = true;
                isClientsVisible = false;
                isCatalogVisible = false;
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
                // Показываем таблицу клиентов и окрашиваем кнопку
                PolicyButtonsStackPanel.Visibility = Visibility.Collapsed;
                DtgContracts.Visibility = Visibility.Collapsed;
                DtgClients.Visibility = Visibility.Visible;
                CatalogPanel.Visibility = Visibility.Collapsed;
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
            isContractsVisible = false;
            isClientsVisible = false;
            isCatalogVisible = false;
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
    }
}
