using Diplom.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using Diplom.Controls;
using System;
using System.Diagnostics;

namespace Diplom.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {

        // РЕАЛИЗОВАТЬ ФУНКЦИЮ РЕДАКТИРОВАНИЯ, ЧТОБЫ ПО НАЖАТИЮ НА СТРОКУ ВЫВОДИЛИСЬ ДАННЫЕ В СТАК ПАНЕЛЕ!

        public AdminPage()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            EmployeesGrid.ItemsSource =  VSK_DBEntities.GetContext().Employees.ToList();
            ClientsGrid.ItemsSource =  VSK_DBEntities.GetContext().Clients.ToList();
            PoliciesGrid.ItemsSource =  VSK_DBEntities.GetContext().Policies.ToList();
            LogsGrid.ItemsSource =  VSK_DBEntities.GetContext().Logs.ToList();
            BrandsGrid.ItemsSource =  VSK_DBEntities.GetContext().VehicleMakes.ToList();
            ModelsGrid.ItemsSource =  VSK_DBEntities.GetContext().VehicleModels.ToList();
            PropertyTypesGrid.ItemsSource =  VSK_DBEntities.GetContext().PropertyTypes.ToList();
            HealthConditionsGrid.ItemsSource =  VSK_DBEntities.GetContext().HealthConditions.ToList();
        }

        // Метод для логирования действий
        private void LogAction(string tableName, string action, string changedData)
        {
            var log = new Logs
            {
                TableName = tableName,
                Action = action,
                ChangedData = changedData,
                ChangeDate = DateTime.Now,
                UserName = CurrentUser.Email ?? "System", // Имя текущего пользователя
                EmployeeID = CurrentUser.EmployeeID// ID сотрудника
            };
            VSK_DBEntities.GetContext().Logs.Add(log);
            VSK_DBEntities.GetContext().SaveChanges();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainTabControl.SelectedItem == EmployeesTab)
            {
                var control = new AddEmployeeControl();
                control.EmployeeAdded += (s, args) =>
                {
                    EmployeesGrid.ItemsSource = VSK_DBEntities.GetContext().Employees.ToList();
                    RightPanel.Content = null; // Можно очистить панель после добавления
                    LogAction("Employees", "Добавление", $"Добавлен сотрудник: {args.Employee.FullName}");
                };
                RightPanel.Content = control;
            }
            else if (MainTabControl.SelectedItem == InfoTab)
            {
                // Вложенные вкладки в разделе "Справочники"
                if (InfoTabControl.SelectedItem == BrandsTab)
                {
                    //RightPanel.Content = new AddBrandControl();
                    var control = new AddBrandControl();
                    control.BrandAdded += (s, args) =>
                    {
                        BrandsGrid.ItemsSource = VSK_DBEntities.GetContext().VehicleMakes.ToList();
                        RightPanel.Content = null; // Можно очистить панель после добавления
                        LogAction("VehicleMakes", "Добавление", $"Добавлен бренд: {args.Brand.MakeName}");
                    };
                    RightPanel.Content = control;

                }
                else if (InfoTabControl.SelectedItem == ModelsTab)
                {
                    var control = new AddModelControl();
                    control.ModelAdded += (s, args) =>
                    {
                        ModelsGrid.ItemsSource = VSK_DBEntities.GetContext().VehicleModels.ToList();
                        RightPanel.Content = null;

                        // Находим бренд по MakeID
                        var brand = VSK_DBEntities.GetContext().VehicleMakes
                            .FirstOrDefault(b => b.MakeID == args.Model.MakeID);
                        string brandName = brand != null ? brand.MakeName : "Неизвестный бренд";

                        // Обновляем строку лога
                        LogAction("VehicleModels", "Добавление",
                            $"Добавлена модель: {args.Model.ModelName} бренда {brandName}");
                    };
                    RightPanel.Content = control;
                }

                else if (InfoTabControl.SelectedItem == PropertyTabs)
                {
                    var control = new AddPropertyTypeControl();
                    control.PropertyTypeAdded += (s, args) =>
                    {
                        PropertyTypesGrid.ItemsSource = VSK_DBEntities.GetContext().PropertyTypes.ToList();
                        RightPanel.Content = null; // Можно очистить панель после добавления
                        LogAction("PropertyTypes", "Добавление", $"Добавлен тип имущества: {args.PropertyType.TypeName}");
                    };
                    RightPanel.Content = control;
                }
                else if (InfoTabControl.SelectedItem == HelthTab)
                {
                    var control = new AddHealthConditionControl();
                    control.HealthConditionAdded += (s, args) =>
                    {
                        HealthConditionsGrid.ItemsSource = VSK_DBEntities.GetContext().HealthConditions.ToList();
                        RightPanel.Content = null; // Можно очистить панель после добавления
                        LogAction("HealthConditions", "Добавление", $"Добавлено состояние здоровья: {args.HealthCondition.ConditionName}");
                    };
                    RightPanel.Content = control;
                }
            }
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Очищаем правую панель при переключении главных вкладок
            if (RightPanel != null)
            {
                RightPanel.Content = null;
            }
        }

        private void InfoTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Очищаем правую панель при переключении внутренних вкладок справочников
            if (RightPanel != null)
            {
                RightPanel.Content = null;
            }
        }

        private void EmployeesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem is Employees selectedEmployee)
            {
                Debug.WriteLine("Выбран сотрудник: " + selectedEmployee.FullName); // Проверьте, что объект существует

                var editControl = new AddEmployeeControl();
                editControl.SetEmployeeForEdit(selectedEmployee);

                editControl.EmployeeAdded += (s, args) =>
                {
                    Load(); // Обновление таблицы
                    RightPanel.Content = null;
                };

                editControl.Canceled += (s, args) =>
                {
                    RightPanel.Content = null;
                };

                RightPanel.Content = editControl;
            }
            else
            {
                Debug.WriteLine("Выбранная строка не является сотрудником!");
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

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = EmployeesGrid.SelectedItems.Cast<Employees>().ToList();

            if(selectedUser.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите сотрудников!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Удалить {selectedUser.Count()} сотрудника(ов)?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question)
                == MessageBoxResult.Yes)
            {
                try
                {
                    var deluser = selectedUser.Select(s => s.EmployeeID).Distinct().ToList();
                    foreach( var delete in selectedUser)
                    {
                        var del = VSK_DBEntities.GetContext().Employees.FirstOrDefault(d => d.EmployeeID == delete.EmployeeID);
                        if(del != null)
                        {
                            VSK_DBEntities.GetContext().Employees.Remove(del);
                        }
                    }

                    VSK_DBEntities.GetContext().SaveChanges();

                    VSK_DBEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(ent => ent.Reload());

                    EmployeesGrid.ItemsSource = VSK_DBEntities.GetContext().Employees.ToList();

                    MessageBox.Show("Данные удалены");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
    }
}
