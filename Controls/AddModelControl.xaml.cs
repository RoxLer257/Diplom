using Diplom.Classes;
using Diplom.Classes.ControlEventArgs;
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
    /// Логика взаимодействия для AddModelControl.xaml
    /// </summary>
    public partial class AddModelControl : UserControl
    {
        public event EventHandler<ModelEventArgs> ModelAdded;
        public AddModelControl()
        {
            InitializeComponent();
            LoadCmb();
            InitializePlaceholders();
        }

        private void LoadCmb()
        {
            var brandbox = ClassFrame.ConnectDB.VehicleMakes.ToList();
            BrandIdBox.ItemsSource = brandbox;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string Modelname = ModelNameBox.Text.Trim();

            if (!string.IsNullOrWhiteSpace(Modelname) && BrandIdBox.SelectedItem is VehicleMakes selectedBrandId)
            {
                var db = ClassFrame.ConnectDB;
                var newModel = new VehicleModels
                {
                    ModelName = Modelname,
                    MakeID = selectedBrandId.MakeID
                };
                db.VehicleModels.Add(newModel);
                db.SaveChanges();
                // Передаем объект через событие
                ModelAdded?.Invoke(this, new ModelEventArgs(newModel));
            }
        }

        //private void SaveButton_Click(object sender, RoutedEventArgs e)
        //{
        //    string Modelname = ModelNameBox.Text.Trim();
        //    string brandId = BrandIdBox.Text.Trim();

        //    if (!string.IsNullOrWhiteSpace(Modelname) && BrandIdBox.SelectedItem is VehicleMakes selectedBrandId)
        //    {
        //        var db = VSK_DBEntities.GetContext();

        //        db.VehicleModels.Add(new VehicleModels
        //        {
        //            ModelName = Modelname,
        //            MakeID = selectedBrandId.MakeID
        //        });

        //        db.SaveChanges();

        //        // Оповестим, что бренд добавлен
        //        ModelAdded?.Invoke(this, EventArgs.Empty);

        //        //LogAction("VehicleMakes", "Добавление", $"Добавлен автомобильный бренд {brandName}");
        //    }
        //}

        private void InitializePlaceholders()
        {
            UpdateComboBoxPlaceholder(BrandIdBox, BrandIdPlaceholder);
            UpdateTextBoxPlaceholder(ModelNameBox, ModelNamePlaceholder);
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
        private void BrandIdBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

    }
}
