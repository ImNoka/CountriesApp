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
using TestEFAsyncWPF.ViewModel;

namespace TestEFAsyncWPF.View
{
    /// <summary>
    /// Логика взаимодействия для CountryWindow.xaml
    /// </summary>
    public partial class CountryWindow : Window
    {
        public Country Country;
        public IEnumerable<Continent> Continents;
        public string ContinentName;
        public CountryWindow(Country country, IEnumerable<Continent> continents)
        {
            InitializeComponent();
            this.DataContext = new CountryViewModel();
            Country = country;
            Continents = continents;
            if (Country.GDP == null)
                Country.GDP = new GDP();
            this.DataContext = Country;
            continentsBox.DataContext = new { Continents, ContinentName };
            System.Diagnostics.Debug.WriteLine("CW Continent: " + Country.Continent);
            System.Diagnostics.Debug.WriteLine("CW GDP: " + Country.GDP);
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
