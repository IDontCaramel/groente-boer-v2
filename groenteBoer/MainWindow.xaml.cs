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

namespace groenteBoer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new pageBetalen());
        }




        public void PopulateWrapPanel(WrapPanel wrapPanel, DataTable dataTable)
        {
            // Clear existing controls from the WrapPanel
            wrapPanel.Children.Clear();

            // Iterate through each row in the DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Create a new instance of your custom UserControl
                var itemControl = new ucProduct();

                // Set the properties of the UserControl using DataTable values
                itemControl.ProductName = row["name"].ToString();
                itemControl.ProductPrice = Convert.ToDecimal(row["price"]);
                itemControl.ProductID = Convert.ToInt32(row["categoryId"]);

                // Convert BLOB to ImageSource
                if (row["Image"] != DBNull.Value)
                {
                    byte[] imageBytes = (byte[])row["image"];
                    itemControl.ProductImage = ByteArrayToImageSource(imageBytes);
                }

                // Add the UserControl to the WrapPanel
                wrapPanel.Children.Add(itemControl);
            }
        }

        // Helper method to convert byte array to ImageSource
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
    }
}
