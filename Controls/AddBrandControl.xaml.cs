using Diplom.Classes;
using Diplom.Classes.ControlEventArgs;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Diplom.Controls
{
    /// <summary>
    /// Логика взаимодействия для AddBrandControl.xaml
    /// </summary>
    public partial class AddBrandControl : UserControl
    {
        public event EventHandler<BrandEventArgs> BrandAdded;
        private readonly VSK_DBEntities _context;

        public AddBrandControl()
        {
            InitializeComponent();
            InitializePlaceholders();
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string brandName = AddBrand.Text.Trim();

            if (!string.IsNullOrWhiteSpace(brandName))
            {
                var db = ClassFrame.ConnectDB;
                var newBrand = new VehicleMakes { MakeName = brandName };
                db.VehicleMakes.Add(newBrand);
                db.SaveChanges();

                BrandAdded?.Invoke(this, new BrandEventArgs(newBrand));
            }
        }

        private void InitializePlaceholders()
        {
            UpdateTextBoxPlaceholder(AddBrand, AddBrandsPlaceholder);
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
