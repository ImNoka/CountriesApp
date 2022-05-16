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
    /// Логика взаимодействия для EconomicInteractionWindow.xaml
    /// </summary>
    public partial class EconomicInteractionWindow : Window
    {
        public EconomicInteraction EconomicInteraction;
        public IEnumerable<Country> Countries;
        public double oldPrice = 0;

        public EconomicInteractionWindow(EconomicInteraction economicInteraction, IEnumerable<Country> countries)
        {
            InitializeComponent();
            if (economicInteraction.Price != null)
                oldPrice = economicInteraction.Price;
            EconomicInteraction = economicInteraction;
            Countries = countries;
            this.DataContext = EconomicInteraction;
            sellerBox.DataContext = Countries;
            buyerBox.DataContext = Countries;
            if (EconomicInteraction.CountryBuyer != null&&EconomicInteraction.CountrySeller!=null)
            {
                //sellerBox.SelectedItem = countries.Equals(EconomicInteraction.CountrySeller);
                //buyerBox.SelectedItem = countries.Equals(EconomicInteraction.CountryBuyer);
                sellerBox.SelectedItem = Countries.Where(g => g.Id == EconomicInteraction.CountrySeller.Id).FirstOrDefault();
                buyerBox.SelectedItem = Countries.Where(g => g.Id == EconomicInteraction.CountryBuyer.Id).FirstOrDefault();
            }
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {

            if (DateBox.SelectedDate == null ||
                    ProductBox.Text == null ||
                    sellerBox.SelectedItem == null ||
                    buyerBox.SelectedItem == null ||
                    PriceBox.Text == null)
            {
                MessageBox.Show("Fill up all lines.");
                return;
            }

            double price;
            if (double.TryParse(PriceBox.Text, out price))
            {
                if (price <= 0)
                {
                    MessageBox.Show("Price must be numeric and larger than 0.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Price must be numeric and larger than 0.");
                return;
            }
            if (sellerBox.SelectedItem==buyerBox.SelectedItem)
            {
                MessageBox.Show("Countries must be different.");
                return;
            }
            
            EconomicInteraction.CountryBuyer = buyerBox.SelectedItem as Country;
            EconomicInteraction.CountrySeller = sellerBox.SelectedItem as Country;
            
            if (EconomicInteraction.CountryBuyer.GDP.Value<EconomicInteraction.Price)
            {
                MessageBox.Show("Price must be less than Price.");
                return;
            }

            System.Diagnostics.Debug.WriteLine("Not returned.");
            EconomicInteraction.Date = DateBox.SelectedDate.Value.Date;
            EconomicInteraction.Price = price;
            EconomicInteraction.CountryBuyer.GDP.Value -= EconomicInteraction.Price-oldPrice;
            EconomicInteraction.CountrySeller.GDP.Value += EconomicInteraction.Price-oldPrice;
            EconomicInteraction.Product = ProductBox.Text;  
            


            //System.Diagnostics.Debug.WriteLine("CW Continent: "+Country.Continent.Name);
            this.DialogResult = true;
        }
    }
}
