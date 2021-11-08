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

namespace DemoTest
{
    /// <summary>
    /// Interaction logic for autothor.xaml
    /// </summary>
    public partial class autothor : Page
    {
        public autothor()
        {
            InitializeComponent();
        }

        private void authoriz_click_event(object sender, RoutedEventArgs e)
        {
            if (text_password.Password == "admin")
            {
                NavigationService.Navigate(new employees_panel());
            }
            else
            {
                MessageBox.Show("Password incorrect", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
