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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace groenteBoer
{
    /// <summary>
    /// Interaction logic for ucBon.xaml
    /// </summary>
    public partial class ucBon : UserControl
    {
        public ucBon()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ProductImageProperty =
            DependencyProperty.Register(
                "ProductImage", typeof(BitmapImage), typeof(ucBon),
                new PropertyMetadata(null, OnProductImageChanged));

        public BitmapImage ProductImage
        {
            get { return (BitmapImage)GetValue(ProductImageProperty); }
            set { SetValue(ProductImageProperty, value); }
        }

        private static void OnProductImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ucBon;
            if (control != null && e.NewValue is BitmapImage newImage)
            {
                control.ucProductImg.Source = newImage;
            }
        }

        public static readonly DependencyProperty ProductNameProperty =
            DependencyProperty.Register(
                "ProductName", typeof(string), typeof(ucBon),
                new PropertyMetadata(string.Empty, OnProductNameChanged));

        public string ProductName
        {
            get { return (string)GetValue(ProductNameProperty); }
            set { SetValue(ProductNameProperty, value); }
        }

        private static void OnProductNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ucBon;
            if (control != null && e.NewValue is string newName)
            {
                control.ucProductName.Content = newName;
            }
        }

        public static readonly DependencyProperty ProductCountProperty =
            DependencyProperty.Register(
                "ProductCount", typeof(string), typeof(ucBon),
                new PropertyMetadata(string.Empty, OnProductCountChanged));

        public string ProductCount
        {
            get { return (string)GetValue(ProductCountProperty); }
            set { SetValue(ProductCountProperty, value); }
        }

        private static void OnProductCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ucBon;
            if (control != null && e.NewValue is string newCount)
            {
                control.ucProductAmount.Content = newCount;
            }
        }


        public static readonly DependencyProperty ProductPriceProperty =
            DependencyProperty.Register(
                "ProductPrice", typeof(string), typeof(ucBon),
                new PropertyMetadata(string.Empty, OnProductPriceChanged));

        public string ProductPrice
        {
            get { return (string)GetValue(ProductPriceProperty); }
            set { SetValue(ProductPriceProperty, value); }
        }

        private static void OnProductPriceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ucBon;
            if (control != null && e.NewValue is string newPrice)
            {
                control.ucProductPrice.Content = newPrice;
            }
        }

        public event RoutedEventHandler UserControlClick;

        private void OnUserControlClick(object sender, MouseButtonEventArgs e)
        {
            UserControlClick?.Invoke(this, new RoutedEventArgs());
        }
    }
}
