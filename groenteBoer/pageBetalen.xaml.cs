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
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new pageAdmin());
        }
    }
}
