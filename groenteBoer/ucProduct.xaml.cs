using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace groenteBoer
{
    public partial class ucProduct : UserControl
    {
        // Dependency Properties
        public static readonly DependencyProperty ProductNameProperty =
            DependencyProperty.Register("ProductName", typeof(string), typeof(ucProduct), new PropertyMetadata(string.Empty, OnProductNameChanged));

        public static readonly DependencyProperty ProductPriceProperty =
            DependencyProperty.Register("ProductPrice", typeof(string), typeof(ucProduct), new PropertyMetadata(string.Empty, OnProductPriceChanged));

        public static readonly DependencyProperty ProductImageProperty =
            DependencyProperty.Register("ProductImage", typeof(ImageSource), typeof(ucProduct), new PropertyMetadata(null, OnProductImageChanged));

        public static readonly DependencyProperty ProductIDProperty =
            DependencyProperty.Register("ProductID", typeof(string), typeof(ucProduct), new PropertyMetadata(string.Empty, OnProductIDChanged));

        public static readonly DependencyProperty ProductCategoryProperty =
            DependencyProperty.Register("ProductCategory", typeof(string), typeof(ucProduct), new PropertyMetadata(string.Empty, OnProductCategoryChanged));

        // Event to send data to parent
        public event EventHandler<ProductClickEventArgs> ProductClicked;

        public ucProduct()
        {
            InitializeComponent();
        }

        // Properties
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

        public ImageSource ProductImage
        {
            get => (ImageSource)GetValue(ProductImageProperty);
            set => SetValue(ProductImageProperty, value);
        }

        public string ProductID
        {
            get => (string)GetValue(ProductIDProperty);
            set => SetValue(ProductIDProperty, value);
        }

        public string ProductCategory
        {
            get => (string)GetValue(ProductCategoryProperty);
            set => SetValue(ProductCategoryProperty, value);
        }

        // DependencyProperty Changed Handlers
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
            control.utProductImage.Source = e.NewValue as ImageSource;
        }

        private static void OnProductIDChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ucProduct;
            control.utProductID.Content = e.NewValue as string;
        }

        private static void OnProductCategoryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ucProduct;
            control.utProductCategory.Content = e.NewValue as string;
        }

        // Event handler for click
        private void OnProductClick(object sender, RoutedEventArgs e)
        {
            // Raise the ProductClicked event and pass product details
            ProductClicked?.Invoke(this, new ProductClickEventArgs(ProductName, ProductPrice, ProductID, ProductImage, ProductCategory));
        }
    }

    public class ProductClickEventArgs : EventArgs
    {
        public string ProductName { get; }
        public string ProductPrice { get; }
        public string ProductID { get; }
        public ImageSource ProductImage { get; }
        public string ProductCategory { get; }

        public ProductClickEventArgs(string productName, string productPrice, string productID, ImageSource productImage, string productCategory)
        {
            ProductName = productName;
            ProductPrice = productPrice;
            ProductID = productID;
            ProductImage = productImage;
            ProductCategory = productCategory;
        }
    }
}
