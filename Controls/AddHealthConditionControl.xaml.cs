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
    /// Логика взаимодействия для AddHealthConditionControl.xaml
    /// </summary>
    public partial class AddHealthConditionControl : UserControl
    {
        public event EventHandler<HealthConditionEventArgs> HealthConditionAdded;
        public AddHealthConditionControl()
        {
            InitializeComponent();
            InitializePlaceholders();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string ConditionName = AddConditionNameBox.Text.Trim();

            if (!string.IsNullOrWhiteSpace(ConditionName))
            {
                var db = VSK_DBEntities.GetContext();
                var newHealthCondition = new HealthConditions { ConditionName = ConditionName };
                db.HealthConditions.Add(newHealthCondition);
                db.SaveChanges();
                // Передаем объект через событие
                HealthConditionAdded?.Invoke(this, new HealthConditionEventArgs(newHealthCondition));
            }
        }

        //private void SaveButton_Click(object sender, RoutedEventArgs e)
        //{
        //    string ConditionName = AddConditionNameBox.Text.Trim();

        //    if (!string.IsNullOrWhiteSpace(ConditionName))
        //    {
        //        var db = VSK_DBEntities.GetContext();

        //        db.HealthConditions.Add(new HealthConditions
        //        {
        //            ConditionName = ConditionName
        //        });

        //        db.SaveChanges();

        //        // Оповестим, что бренд добавлен
        //        HealthConditionAdded?.Invoke(this, EventArgs.Empty);

        //        //LogAction("VehicleMakes", "Добавление", $"Добавлен автомобильный бренд {brandName}");
        //    }


        //}

        private void InitializePlaceholders()
        {
            UpdateTextBoxPlaceholder(AddConditionNameBox, AddConditionNameBoxPlaceholder);
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

    }
}
