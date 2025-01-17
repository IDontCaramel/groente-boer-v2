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
using groenteBoerDatabase;

namespace groenteBoer
{
    /// <summary>
    /// Interaction logic for pageBetalen.xaml
    /// </summary>
    public partial class pageBetalen : Page
    {
        public pageBetalen()
        {
            InitializeComponent();

            PopulateWrapPanel(wpItems, databaseProduct.GetProducts());
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new pageAdmin());
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

        private int currentID;
        private ImageSource currentImage;
        private string currentName;
        private string currentPrice;

        private void OnProductClicked(object sender, ProductClickEventArgs e)
        {
            currentID = int.Parse(e.ProductID);
            currentImage = e.ProductImage;
            currentName = e.ProductName;
            currentPrice = e.ProductPrice;

            tbAmount.Text = String.Empty;
            gridAmountPannel.Visibility = Visibility.Visible;
        }


        private int totalAmount;


        private void createBonItem()
        {
            var bonControl = new ucBon();

            bonControl.Height = 80;

            bonControl.ucProductAmount.Content = tbAmount.Text;
            bonControl.ucProductName.Content = currentName;
            bonControl.ucProductImg.Source = currentImage;
            bonControl.ucProductPrice.Content = "€ "+currentPrice;

            totalAmount += int.Parse(currentPrice)*int.Parse(tbAmount.Text);

            bonControl.UserControlClick += BonControl_UserControlClick;

            spBon.Children.Add(bonControl);

        }

        private void BonControl_UserControlClick(object sender, ProductClickEventArgs e)
        {
            totalAmount -= int.Parse(e.ProductPrice.Trim("€")) * int.Parse(e.);
        }


        private void BonControl_UserControlClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnAmount_Click(object sender, RoutedEventArgs e)
        {
            string number = (e.Source as Button).Content.ToString();
            tbAmount.Text += number;
        }

        private void btnAmountEnter_Click(object sender, RoutedEventArgs e)
        {
            gridAmountPannel.Visibility = Visibility.Hidden;

            if (tbAmount.Text != String.Empty && int.Parse(tbAmount.Text) >= 1) 
            {
                createBonItem();
            }
            else
            {
                gridAmountPannel.Visibility = Visibility.Hidden;
            }

        }

        private void btnAmountBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (tbAmount.Text.Length > 0)
            {
                tbAmount.Text = tbAmount.Text.Remove(tbAmount.Text.Length - 1);
            }
            else
            {
                gridAmountPannel.Visibility = Visibility.Hidden;
            }
        }
    }
}
