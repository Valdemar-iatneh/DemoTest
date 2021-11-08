using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for employees_panel.xaml
    /// </summary>
    public partial class employees_panel : Page
    {
        public static ObservableCollection<Employee> employee { get; set; }
        public static ObservableCollection<Arrival> arrival { get; set; }
        //public static employeeArrival EmploAriv { get; set; }
        private static IEnumerable<employeeArrival> EmploAriv { get; set; }
        // public static FilmInSession selectedFilmInSession { get; set; }
        public employees_panel()
        {
            InitializeComponent();
            
            //employee = new ObservableCollection<Employee>(bd_connection.connection.Employee.ToList());
            arrival = new ObservableCollection<Arrival>(bd_connection.connection.Arrival.ToList());
            employee = new ObservableCollection<Employee>(bd_connection.connection.Employee.Where(i => i.Came == false).ToList());
            
            this.DataContext = this;
        }

        private void film_select_event(object sender, RoutedEventArgs e)
        {
            //selectedFilmInSession_FilmID = (list_of_films.SelectedItem as Film).Film_ID;
            //Переход на страницу с фильмом
            //NavigationService.Navigate(new film_page_in_films(selectedFilmInSession_FilmID));
            
            //Удаление
            
        }

        private void selected_item(object sender, SelectionChangedEventArgs e)
        {
            var info = (sender as ListView).SelectedItem as Employee;
            if (MessageBox.Show($"Отметить {info.First_Name} {info.Last_Name}", "Отметить?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var save = new Arrival();
                save.Employee_ID = info.Employee_ID;
                DateTime dateNow = DateTime.Now;
                DateTime time8 = new DateTime(2021, 11, 8, 8, 0, 0);
                int lateTime = (dateNow - time8).Minutes;
                if (lateTime < 10)
                {
                    save.BeingLate_ID = 101;
                    info.PenaltyBalance = info.PenaltyBalance - 0;
                }
                else if (lateTime > 10 && lateTime < 30)
                {
                    save.BeingLate_ID = 102;
                    info.PenaltyBalance = info.PenaltyBalance - 500;
                }
                else if (lateTime == 30)
                {
                    save.BeingLate_ID = 103;
                    info.PenaltyBalance = info.PenaltyBalance - 1000;
                }
                else if (lateTime > 30)
                {
                    save.BeingLate_ID = 104;
                    info.PenaltyBalance = info.PenaltyBalance - 2000;
                }
                save.ArrivalTime = DateTime.Now;
                save.Came = true;
                Task.Delay(86400 * 1000).ContinueWith(_ =>
                {
                    save.Came = false;
                });
                bd_connection.connection.Arrival.Add(save);
                list_of_emp.Items.Refresh();
                info.Came = true;
                bd_connection.connection.SaveChanges();
            }
            NavigationService.Navigate(new employees_panel());
        }
    }
}
