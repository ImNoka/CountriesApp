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
using System.Windows.Shapes;
using TestEFAsyncWPF.Model.Countries;
namespace TestEFAsyncWPF.View
{
    /// <summary>
    /// Логика взаимодействия для CountryWindow.xaml
    /// </summary>
    public partial class CountryWindow : Window
    {
        public Country Country;

        public CountryWindow(Country country)
        {
            InitializeComponent();
            Country = country;
            this.DataContext = Country;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
