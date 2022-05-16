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
        public IEnumerable<EconomicUnion> EconomicUnions;
        public CountryWindow(Country country, MainViewModel mainViewModel)
        {
            InitializeComponent();
            Country = country;
            if (Country.GDP == null)
                Country.GDP = new GDP();
            Continents = mainViewModel.Continents;
            EconomicUnions= mainViewModel.EconomicUnions;
            this.DataContext = Country;
            continentsBox.DataContext = Continents;
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {   
            Country.Continent = continentsBox.SelectedItem as Continent;
            //System.Diagnostics.Debug.WriteLine("CW Continent: "+Country.Continent.Name);
            this.DialogResult = true;
        }
    }
}
