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
using TestEFAsyncWPF.Model.Countries;
using Microsoft.EntityFrameworkCore;
using TestEFAsyncWPF.ViewModel;

namespace TestEFAsyncWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new CountriesViewModel();

            using (CountryContext countryContext = new CountryContext())
            {
                countryContext.Database.EnsureDeleted();
                countryContext.Database.EnsureCreated();

                Continent continentAsia = new Continent { Name = "Asia" };
                Continent continentEurope = new Continent { Name = "Europe" };
                countryContext.Continents.AddRange(continentAsia,continentEurope);
                EconomicUnion APEC = new EconomicUnion { Name = "Asia-Pacific Economic Cooperation" };
                EconomicUnion EU = new EconomicUnion { Name = "European Union" };
                countryContext.EconomicUnions.AddRange(APEC, EU);

                GDP gDP = new GDP { Value = 688000000 };
                Country country1 = new Country
                {
                    Name = "Russian Federation",
                    Capital = "Moscow",
                    FoundationDate = new DateTime(1991, 12, 25),
                    Continent = continentAsia,
                    GDP = gDP
                };
                GDP gDP1 = new GDP { Value = 25000000000 };
                Country country2 = new Country
                {
                    Name = "People's Republic Of China",
                    Capital = "Beijing",
                    FoundationDate = new DateTime(1949, 10, 1),
                    Continent = continentAsia,
                    GDP = gDP1
                };
                Country country3 = new Country
                {
                    Name = "Germany",
                    Capital = "Berlin",
                    FoundationDate = new DateTime(1990, 10, 3),
                    Continent = continentEurope,
                    GDP = new GDP { Value = 3300000000 }               
                };

                countryContext.AddRange(country1, country2, country3);

                country1.EconomicUnions.Add(APEC);
                country2.EconomicUnions.Add(APEC);
                country3.EconomicUnions.Add(EU);

                System.Diagnostics.Debug.WriteLine(country1.EconomicUnions.FirstOrDefault().Name);

                countryContext.SaveChanges();

            }

            using (CountryContext db = new CountryContext())
            {
                //Continent? continent;
                Continent? continent = db.Continents.FirstOrDefault();

                if (continent != null)
                {
                    List<Country> countries = db.Countries.Where(c => c.Continent.Name == "Asia").Include(u => u.EconomicUnions).ToList();
                    //db.Continents.Include(c => c.Countries).ThenInclude(c => c.EconomicUnions).ToList();
                    
                    System.Diagnostics.Debug.WriteLine($"Continent: {continent.Name}");
                    foreach(Country country in countries)
                    {
                        System.Diagnostics.Debug.WriteLine( $"Country: {country.Name}\n" +
                                                            $"Capital: {country.Capital}\n" +
                                                            $"EconomicUnions: {country.EconomicUnions.FirstOrDefault().Name}");
                        
                    }
                }
            }
        }
    }
}
