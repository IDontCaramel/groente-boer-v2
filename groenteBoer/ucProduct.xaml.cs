using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace groenteBoer
{
    public partial class ucProduct : UserControl
    {
        public static readonly DependencyProperty ProductNameProperty =
            DependencyProperty.Register("ProductName", typeof(string), typeof(ucProduct), new PropertyMetadata(string.Empty, OnProductNameChanged));

        public static readonly DependencyProperty ProductPriceProperty =
            DependencyProperty.Register("ProductPrice", typeof(string), typeof(ucProduct), new PropertyMetadata(string.Empty, OnProductPriceChanged));

        public static readonly DependencyProperty ProductImageProperty =
            DependencyProperty.Register("ProductImage", typeof(string), typeof(ucProduct), new PropertyMetadata(string.Empty, OnProductImageChanged));

        public static readonly DependencyProperty ProductIDProperty =
            DependencyProperty.Register("ProductID", typeof(string), typeof(ucProduct), new PropertyMetadata(string.Empty, OnProductIDChanged));

        public ucProduct()
        {
            InitializeComponent();
        }

        public string ProductName
        {
            get => (string)GetValue(ProductNameProperty);
            set => SetValue(ProductNameProperty, value);
        }

        public string ProductPrice
        {
            get => (string)GetValue(ProductPriceProperty);
            set => SetValue(ProductPriceProperty, value);
        }

        public string ProductImage
        {
            get => (string)GetValue(ProductImageProperty);
            set => SetValue(ProductImageProperty, value);
        }

        public string ProductID
        {
            get => (string)GetValue(ProductIDProperty);
            set => SetValue(ProductIDProperty, value);
        }


        private static void OnProductNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ucProduct;
            control.utProductName.Content = e.NewValue as string;
        }

        private static void OnProductPriceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ucProduct;
            control.utProductPrice.Content = e.NewValue as string;
        }

        private static void OnProductImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ucProduct;
            control.utProductImage.Source = new BitmapImage(new Uri(e.NewValue as string, UriKind.RelativeOrAbsolute));
        }

        private static void OnProductIDChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ucProduct;
            control.utProductID.Content = e.NewValue as string;
        }

        private void OnProductClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Name: {ProductName}\nPrice: {ProductPrice}\nImage Path: {ProductImage}");
        }
    }
}
