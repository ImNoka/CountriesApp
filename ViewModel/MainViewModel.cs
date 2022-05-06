using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEFAsyncWPF.Model.Countries;
using TestEFAsyncWPF.View;

namespace TestEFAsyncWPF.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        CountryContext db;

        private IEnumerable<Country> countries;
        private IEnumerable<Continent> continents;
        private IEnumerable<EconomicUnion> economicUnions;

        private RelayCommand addCountryCommand;
        private RelayCommand removeCountryCommand;
        private RelayCommand editCountryCommand;
        private RelayCommand getCountriesCommand;
        private RelayCommand getContinentsCommand;
        private RelayCommand getEconomicUnionsCommand;
        

        #region GettersSetters
        public IEnumerable<Country> Countries
        {
            get { return countries; }
            set { countries = value;
                OnPropertyChanged("Countries");
            }
        }

        public IEnumerable<Continent> Continents
        {
            get { return continents; }
            set
            {
                continents = value;
                OnPropertyChanged("Continents");
            }
        }

        public IEnumerable<EconomicUnion> EconomicUnions
        {
            get { return economicUnions; }
            set
            {
                economicUnions = value;
                OnPropertyChanged("EconomicUnions");
            }
        }
        #endregion


        public MainViewModel()
        {
            db = new CountryContext();
            db.Countries.Load();
            db.Continents.Load();
            db.EconomicInteractions.Load();
            Countries = db.Countries.Local.ToBindingList();
            Continents = db.Continents.Local.ToBindingList();
            EconomicUnions = db.EconomicUnions.Local.ToBindingList();

        }


        public RelayCommand AddCountryCommand
        {
            get
            {
                return addCountryCommand ??
                    (addCountryCommand = new RelayCommand(obj =>
                    {
                        CountryWindow countryWindow = new CountryWindow(new Country(), Continents);
                        if (countryWindow.ShowDialog() == true)
                        {
                            Country country = countryWindow.Country;
                            System.Diagnostics.Debug.WriteLine("VM Continent: "+country.Continent);
                            System.Diagnostics.Debug.WriteLine("VM GDP: "+country.GDP);
                            db.Countries.Add(country);
                            //db.SaveChanges();
                        }
                    }));
            }
        }

        public RelayCommand EditCountryCommand
        {
            get
            {
                return editCountryCommand ??
                    (editCountryCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Country country = selectedItem as Country;
                        Country countryVM = new Country()
                        {
                            Id = country.Id,
                            Name = country.Name,
                            Capital = country.Capital,
                            GDP = country.GDP,
                            Continent = country.Continent,
                            EconomicUnions = country.EconomicUnions
                        };
                        //System.Diagnostics.Debug.WriteLine(countryVM.Continent.Name);
                        //System.Diagnostics.Debug.WriteLine(countryVM.GDP);
                        CountryWindow countryWindow = new CountryWindow(countryVM, Continents);
                        if (countryWindow.ShowDialog() == true)
                        {
                            country = db.Countries.Find(countryWindow.Country.Id);
                            if (country != null)
                            {
                                country.Name = countryWindow.Country.Name;
                                country.Continent = countryWindow.Country.Continent;
                                country.EconomicUnions = countryWindow.Country.EconomicUnions;
                                country.GDP.Value = countryWindow.Country.GDP.Value;
                                country.Capital = countryWindow.Country.Capital;
                                db.Entry(country).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }));
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string prop ="")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
