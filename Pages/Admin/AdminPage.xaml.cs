using Diplom.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Diplom.Controls;
using System;
using System.Diagnostics;
using Diplom.Classes.Helper;

namespace Diplom.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {

        public AdminPage()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            EmployeesGrid.ItemsSource =  ClassFrame.ConnectDB.Employees.ToList();
            ClientsGrid.ItemsSource = ClassFrame.ConnectDB.Clients.ToList();
            PoliciesGrid.ItemsSource =  ClassFrame.ConnectDB.Policies.ToList();
            LogsGrid.ItemsSource =  ClassFrame.ConnectDB.Logs.ToList();
            BrandsGrid.ItemsSource =  ClassFrame.ConnectDB.VehicleMakes.ToList();
            ModelsGrid.ItemsSource =  ClassFrame.ConnectDB.VehicleModels.ToList();
            PropertyTypesGrid.ItemsSource =  ClassFrame.ConnectDB.PropertyTypes.ToList();
            HealthConditionsGrid.ItemsSource =  ClassFrame.ConnectDB.HealthConditions.ToList();
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
            ClassFrame.ConnectDB.Logs.Add(log);
            ClassFrame.ConnectDB.SaveChanges();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainTabControl.SelectedItem == EmployeesTab)
            {
                var control = new AddEmployeeControl();
                control.EmployeeAdded += (s, args) =>
                {
                    EmployeesGrid.ItemsSource = ClassFrame.ConnectDB.Employees.ToList();
                    RightPanel.Content = null; // Можно очистить панель после добавления
                    string fullName = DisplayHelper.GetFullName(args.Employee.LastName, args.Employee.FirstName, args.Employee.MiddleName);
                    LogAction("Employees", "Добавление", $"Добавлен сотрудник: {fullName}");
                    Load();
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
                        BrandsGrid.ItemsSource = ClassFrame.ConnectDB.VehicleMakes.ToList();
                        RightPanel.Content = null; // Можно очистить панель после добавления
                        LogAction("VehicleMakes", "Добавление", $"Добавлен бренд: {args.Brand.MakeName}");
                        Load();
                    };
                    RightPanel.Content = control;

                }
                else if (InfoTabControl.SelectedItem == ModelsTab)
                {
                    var control = new AddModelControl();
                    control.ModelAdded += (s, args) =>
                    {
                        ModelsGrid.ItemsSource = ClassFrame.ConnectDB.VehicleModels.ToList();
                        RightPanel.Content = null;

                        // Находим бренд по MakeID
                        var brand = ClassFrame.ConnectDB.VehicleMakes
                            .FirstOrDefault(b => b.MakeID == args.Model.MakeID);
                        string brandName = brand != null ? brand.MakeName : "Неизвестный бренд";

                        // Обновляем строку лога
                        LogAction("VehicleModels", "Добавление",
                            $"Добавлена модель: {args.Model.ModelName} бренда {brandName}");
                        Load();
                    };
                    RightPanel.Content = control;
                }

                else if (InfoTabControl.SelectedItem == PropertyTabs)
                {
                    var control = new AddPropertyTypeControl();
                    control.PropertyTypeAdded += (s, args) =>
                    {
                        PropertyTypesGrid.ItemsSource = ClassFrame.ConnectDB.PropertyTypes.ToList();
                        RightPanel.Content = null; // Можно очистить панель после добавления
                        LogAction("PropertyTypes", "Добавление", $"Добавлен тип имущества: {args.PropertyType.TypeName}");
                        Load();
                    };
                    RightPanel.Content = control;
                }
                else if (InfoTabControl.SelectedItem == HelthTab)
                {
                    var control = new AddHealthConditionControl();
                    control.HealthConditionAdded += (s, args) =>
                    {
                        HealthConditionsGrid.ItemsSource = ClassFrame.ConnectDB.HealthConditions.ToList();
                        RightPanel.Content = null; // Можно очистить панель после добавления
                        LogAction("HealthConditions", "Добавление", $"Добавлено состояние здоровья: {args.HealthCondition.ConditionName}");
                        Load();
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

            if (selectedUser.Count == 0)
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
                    foreach (var delete in selectedUser)
                    {
                        var del = ClassFrame.ConnectDB.Employees.FirstOrDefault(d => d.EmployeeID == delete.EmployeeID);
                        if (del != null)
                        {
                            ClassFrame.ConnectDB.Employees.Remove(del);
                            // Логируем удаление каждого сотрудника
                            LogAction("Employees", "Удаление", $"Удалён сотрудник с ID: {del.EmployeeID}, ФИО: {del.LastName} {del.FirstName} {del.MiddleName}");
                        }
                    }

                    ClassFrame.ConnectDB.SaveChanges();
                    // Логируем успешное завершение операции
                    LogAction("Employees", "Удаление (успешно)", $"Удалено {selectedUser.Count} сотрудника(ов) в {DateTime.Now}");

                    ClassFrame.ConnectDB.ChangeTracker.Entries().ToList().ForEach(ent => ent.Reload());

                    EmployeesGrid.ItemsSource = ClassFrame.ConnectDB.Employees.ToList();

                    MessageBox.Show("Данные удалены");
                    Load();
                }
                catch (Exception ex)
                {
                    // Логируем ошибку
                    LogAction("Employees", "Ошибка удаления", $"Ошибка при удалении сотрудников: {ex.Message}");
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Определяем текущую вкладку
                var selectedTab = InfoTabControl.SelectedItem as TabItem;
                if (selectedTab == null) return;

                if (selectedTab == BrandsTab)
                {
                    // Удаление брендов авто
                    var selectedBrands = BrandsGrid.SelectedItems.Cast<VehicleMakes>().ToList();
                    if (selectedBrands.Count == 0)
                    {
                        MessageBox.Show("Пожалуйста, выберите бренды для удаления!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Проверка зависимостей
                    foreach (var brand in selectedBrands)
                    {
                        var dependentModels = ClassFrame.ConnectDB.VehicleModels
                            .Any(m => m.MakeID == brand.MakeID);
                        if (dependentModels)
                        {
                            MessageBox.Show($"Нельзя удалить бренд '{brand.MakeName}', так как он используется в моделях автомобилей.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    if (MessageBox.Show($"Удалить {selectedBrands.Count} бренда(ов)?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;

                    foreach (var brand in selectedBrands)
                    {
                        var entity = ClassFrame.ConnectDB.VehicleMakes.FirstOrDefault(b => b.MakeID == brand.MakeID);
                        if (entity != null)
                        {
                            ClassFrame.ConnectDB.VehicleMakes.Remove(entity);
                            LogAction("VehicleMakes", "Удаление", $"Удалён бренд: ID={entity.MakeID}, Название={entity.MakeName}");
                        }
                    }
                    ClassFrame.ConnectDB.SaveChanges();
                    LogAction("VehicleMakes", "Удаление (успешно)", $"Удалено {selectedBrands.Count} бренда(ов) в {DateTime.Now}");
                    Load();
                }
                else if (selectedTab == ModelsTab)
                {
                    // Удаление моделей авто
                    var selectedModels = ModelsGrid.SelectedItems.Cast<VehicleModels>().ToList();
                    if (selectedModels.Count == 0)
                    {
                        MessageBox.Show("Пожалуйста, выберите модели для удаления!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Проверка зависимостей
                    foreach (var model in selectedModels)
                    {
                        var dependentVehicles = ClassFrame.ConnectDB.Vehicles
                            .Any(v => v.ModelID == model.ModelID);
                        if (dependentVehicles)
                        {
                            MessageBox.Show($"Нельзя удалить модель '{model.ModelName}', так как она используется в записях об автомобилях.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    if (MessageBox.Show($"Удалить {selectedModels.Count} модели(ей)?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;

                    foreach (var model in selectedModels)
                    {
                        var entity = ClassFrame.ConnectDB.VehicleModels.FirstOrDefault(m => m.ModelID == model.ModelID);
                        if (entity != null)
                        {
                            ClassFrame.ConnectDB.VehicleModels.Remove(entity);
                            LogAction("VehicleModels", "Удаление", $"Удалена модель: ID={entity.ModelID}, Название={entity.ModelName}, Бренд={entity.VehicleMakes?.MakeName}");
                        }
                    }
                    ClassFrame.ConnectDB.SaveChanges();
                    LogAction("VehicleModels", "Удаление (успешно)", $"Удалено {selectedModels.Count} модели(ей) в {DateTime.Now}");
                    Load();
                }
                else if (selectedTab == PropertyTabs)
                {
                    // Удаление типов недвижимости
                    var selectedPropertyTypes = PropertyTypesGrid.SelectedItems.Cast<PropertyTypes>().ToList();
                    if (selectedPropertyTypes.Count == 0)
                    {
                        MessageBox.Show("Пожалуйста, выберите типы недвижимости для удаления!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Проверка зависимостей
                    foreach (var propertyType in selectedPropertyTypes)
                    {
                        var dependentProperties = ClassFrame.ConnectDB.Properties
                            .Any(p => p.PropertyTypeID == propertyType.PropertyTypeID);
                        if (dependentProperties)
                        {
                            MessageBox.Show($"Нельзя удалить тип недвижимости '{propertyType.TypeName}', так как он используется в записях о недвижимости.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    if (MessageBox.Show($"Удалить {selectedPropertyTypes.Count} типа(ов) недвижимости?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;

                    foreach (var propertyType in selectedPropertyTypes)
                    {
                        var entity = ClassFrame.ConnectDB.PropertyTypes.FirstOrDefault(p => p.PropertyTypeID == propertyType.PropertyTypeID);
                        if (entity != null)
                        {
                            ClassFrame.ConnectDB.PropertyTypes.Remove(entity);
                            LogAction("PropertyTypes", "Удаление", $"Удалён тип недвижимости: ID={entity.PropertyTypeID}, Название={entity.TypeName}");
                        }
                    }
                    ClassFrame.ConnectDB.SaveChanges();
                    LogAction("PropertyTypes", "Удаление (успешно)", $"Удалено {selectedPropertyTypes.Count} типа(ов) недвижимости в {DateTime.Now}");
                    Load();
                }
                else if (selectedTab == HelthTab)
                {
                    // Удаление состояний здоровья
                    var selectedHealthConditions = HealthConditionsGrid.SelectedItems.Cast<HealthConditions>().ToList();
                    if (selectedHealthConditions.Count == 0)
                    {
                        MessageBox.Show("Пожалуйста, выберите состояния здоровья для удаления!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Проверка зависимостей
                    foreach (var healthCondition in selectedHealthConditions)
                    {
                        var dependentRecords = ClassFrame.ConnectDB.LifeAndHealth
                            .Any(lh => lh.HealthConditionID == healthCondition.HealthConditionID);
                        if (dependentRecords)
                        {
                            MessageBox.Show($"Нельзя удалить состояние здоровья '{healthCondition.ConditionName}', так как оно используется в записях о страховании жизни и здоровья.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    if (MessageBox.Show($"Удалить {selectedHealthConditions.Count} состояния(ий) здоровья?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;

                    foreach (var healthCondition in selectedHealthConditions)
                    {
                        var entity = ClassFrame.ConnectDB.HealthConditions.FirstOrDefault(h => h.HealthConditionID == healthCondition.HealthConditionID);
                        if (entity != null)
                        {
                            ClassFrame.ConnectDB.HealthConditions.Remove(entity);
                            LogAction("HealthConditions", "Удаление", $"Удалено состояние здоровья: ID={entity.HealthConditionID}, Название={entity.ConditionName}");
                        }
                    }
                    ClassFrame.ConnectDB.SaveChanges();
                    LogAction("HealthConditions", "Удаление (успешно)", $"Удалено {selectedHealthConditions.Count} состояния(ий) здоровья в {DateTime.Now}");
                    Load();
                }

                MessageBox.Show("Данные удалены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                var selectedTab = InfoTabControl.SelectedItem as TabItem;
                string tableName = selectedTab == BrandsTab ? "VehicleMakes" :
                                   selectedTab == ModelsTab ? "VehicleModels" :
                                   selectedTab == PropertyTabs ? "PropertyTypes" : "HealthConditions";
                LogAction(tableName, "Ошибка удаления", $"Ошибка при удалении: {ex.Message}");
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
