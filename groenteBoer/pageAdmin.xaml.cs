using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Xml.Linq;
using groenteBoerDatabase;
using Microsoft.Win32;

namespace groenteBoer
{
    /// <summary>
    /// Interaction logic for pageAdmin.xaml
    /// </summary>
    /// 

    public partial class pageAdmin : Page
    {

        public pageAdmin()
        {
            InitializeComponent();
            cbProductCategory.Items.Add("select category");
            fillComboBox();


            PopulateWrapPanel(wpItems, databaseProduct.GetProducts());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new pageBetalen());
        }


        private void fillComboBox()
        {
            DataTable data = databaseCategory.GetCategories();
            foreach (DataRow row in data.Rows)
            {
                cbProductCategory.Items.Add(row["category_name"].ToString());
            }
        }

        private void btnProductNew_Click(object sender, RoutedEventArgs e)
        {
            databaseProduct.AddProduct(
                tbProductName.Text,
                Math.Round(Convert.ToDecimal(tbProductPrice.Text), 2),
                ConvertImageSourceToByteArray(imgProduct.Source),
                cbProductCategory.SelectedIndex
            );
            PopulateWrapPanel(wpItems, databaseProduct.GetProducts());
            clearFields();
        }



        public void SetImageFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png|All Files|*.*",
                Title = "Select an Image"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                // Load the image and set it to the Image control's Source
                BitmapImage bitmapImage = new BitmapImage(new Uri(filePath));
                imgProduct.Source = bitmapImage;
            }
        }

        private void imgProduct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetImageFromFile();
        }


        public byte[] ConvertImageSourceToByteArray(ImageSource imageSource)
        {
            if (imageSource is BitmapImage bitmapImage)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            return null;
        }

        private void tbProductPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string input = textBox.Text;

                // Remove alphabetic characters, keeping only numbers and dots
                input = System.Text.RegularExpressions.Regex.Replace(input, @"[^0-9.]", string.Empty);

                // Replace commas with dots
                input = input.Replace(',', '.');

                // Ensure only one dot is present after the first occurrence
                int firstDotIndex = input.IndexOf('.');
                if (firstDotIndex != -1)
                {
                    input = input.Substring(0, firstDotIndex + 1) + input.Substring(firstDotIndex + 1).Replace(".", string.Empty);
                }

                // Try to parse the cleaned input as a decimal
                if (decimal.TryParse(input, out decimal number))
                {
                    textBox.Text = number.ToString("G");
                }
                else
                {
                    textBox.Text = string.Empty;
                }
            }
        }


        private void clearFields()
        {

            tbProductName.Text = string.Empty;
            tbProductPrice.Text = string.Empty;
            imgProduct.Source = new BitmapImage(new Uri("pack://application:,,,/groenteBoer;component/assets/localhost-file-not-found.jpg"));
            cbProductCategory.SelectedIndex = 0;
        }


        public void PopulateWrapPanel(WrapPanel wrapPanel, DataTable dataTable)
        {
            wrapPanel.Children.Clear();

            foreach (DataRow row in dataTable.Rows)
            {
                var itemControl = new ucProduct();

                itemControl.Height = 200;
                itemControl.Width = 200;
                itemControl.Margin = new Thickness(1);

                itemControl.ProductName = row["name"].ToString();
                itemControl.ProductPrice = row["price"].ToString();
                itemControl.ProductID = row["ID"].ToString();
                itemControl.ProductCategory = row["category_id"].ToString();

                if (row["Image"] != DBNull.Value)
                {
                    byte[] imageBytes = (byte[])row["image"];
                    itemControl.ProductImage = ByteArrayToImageSource(imageBytes);
                }


                itemControl.ProductClicked += OnProductClicked;

                wrapPanel.Children.Add(itemControl);
            }

        }

        private int currentID;

        private void OnProductClicked(object sender, ProductClickEventArgs e)
        {
            tbProductName.Text = e.ProductName;
            tbProductPrice.Text = e.ProductPrice;
            imgProduct.Source = e.ProductImage;
            currentID = int.Parse(e.ProductID);
            cbProductCategory.SelectedIndex = int.Parse(e.ProductCategory);

        }


        private ImageSource ByteArrayToImageSource(byte[] imageBytes)
        {
            using (var stream = new MemoryStream(imageBytes))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                return bitmap;
            }
        }

        private void btnProductSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                databaseProduct.UpdateProduct(
                    currentID,
                    tbProductName.Text,
                    Math.Round(Convert.ToDecimal(tbProductPrice.Text), 2),
                    ConvertImageSourceToByteArray(imgProduct.Source),
                    cbProductCategory.SelectedIndex
                );
                PopulateWrapPanel(wpItems, databaseProduct.GetProducts());
            }
            catch
            {
                MessageBox.Show("Please select a product to update");
            }
            
        }

        private void btnProductDelete_Click(object sender, RoutedEventArgs e)
        {
            databaseProduct.DeleteProduct(currentID);
            clearFields();
            PopulateWrapPanel(wpItems, databaseProduct.GetProducts());
        }
    }
}
