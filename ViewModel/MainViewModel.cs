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
        public CountryContext db;
        public InteractionMethods InteractionMethods;

        private IEnumerable<Country> countries;
        private IEnumerable<Continent> continents;
        private IEnumerable<EconomicUnion> economicUnions;
        private IEnumerable<EconomicInteraction> economicInteractions;


        #region Incapsulated commands
        private RelayCommand addCountryCommand;
        private RelayCommand removeCountryCommand;
        private RelayCommand editCountryCommand;

        private RelayCommand addContinentCommand;
        private RelayCommand removeContinentCommand;
        private RelayCommand editContinentCommand;

        private RelayCommand addEconomicUnionCommand;
        private RelayCommand editEconomicUnionCommand;
        private RelayCommand removeEconomicUnionCommand;

        private RelayCommand addEconomicInteractionCommand;
        private RelayCommand removeEconomicInteractionCommand;
        private RelayCommand editEconomicInteractionCommand;

        #endregion

        #region Commands
        public RelayCommand AddEconomicInteractionCommand
        {
            get
            {
                return addEconomicInteractionCommand
                    ?? (addEconomicInteractionCommand = new RelayCommand((obj) =>
                    {
                        EconomicInteraction economicInteraction = InteractionMethods.AddEconomicInteraction();
                        if(economicInteraction!=null)
                        {
                            db.EconomicInteractions.Add(economicInteraction);
                            db.Entry(economicInteraction.CountryBuyer).State = EntityState.Modified;
                            db.Entry(economicInteraction.CountrySeller).State = EntityState.Modified;
                            db.SaveChanges();
                            Countries = db.Countries.ToList();
                        }
                    }));
            }
        }
        public RelayCommand EditEconomicInteractionCommand
        {
            get
            {
                return editEconomicInteractionCommand
                    ?? (editEconomicInteractionCommand = new RelayCommand((seletedItem) =>
                    {
                        if (seletedItem == null)
                            return;
                        
                        EconomicInteraction economicInteraction = InteractionMethods.EditEconomicInteraction(seletedItem as EconomicInteraction);
                        var economicInteractionToDb =  db.EconomicInteractions.Find(economicInteraction.Id);
                        if (economicInteractionToDb != null)
                        {
                            economicInteractionToDb.Date = economicInteraction.Date;
                            economicInteractionToDb.Price = economicInteraction.Price;
                            economicInteractionToDb.CountryBuyer = economicInteraction.CountryBuyer;
                            economicInteractionToDb.CountrySeller = economicInteraction.CountrySeller;
                            economicInteractionToDb.Product=economicInteraction.Product;
                            db.Entry(economicInteractionToDb).State = EntityState.Modified;
                            db.SaveChanges();
                            Countries = db.Countries.ToList();
                            EconomicInteractions = db.EconomicInteractions.ToList();
                        }
                    }));
            }
        }

        public RelayCommand RemoveEconomicInteractionCommand
        {
            get
            {
                return removeEconomicInteractionCommand
                    ?? (removeEconomicInteractionCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null)
                            return;
                        EconomicInteraction economicInteraction = selectedItem as EconomicInteraction;
                        Country seller = db.Countries.Find(economicInteraction.CountrySeller.Id);
                        Country buyer = db.Countries.Find(economicInteraction.CountryBuyer.Id);
                        if (seller.GDP.Value >= economicInteraction.Price)
                            seller.GDP.Value -= economicInteraction.Price;
                        else
                            seller.GDP.Value = 0;
                        buyer.GDP.Value+=economicInteraction.Price;
                        db.Entry(buyer).State = EntityState.Modified;
                        db.Entry(seller).State = EntityState.Modified;
                        db.EconomicInteractions.Remove(economicInteraction);
                        db.SaveChanges();
                        Countries=db.Countries.ToList();
                        EconomicInteractions = db.EconomicInteractions.ToList();
                    }));
            }
        }

        #endregion


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

        public IEnumerable<EconomicInteraction> EconomicInteractions
        {
            get { return economicInteractions; }
            set
            {
                economicInteractions = value;
                OnPropertyChanged("EconomicInteractions");
            }
        }

        #endregion


        public MainViewModel()
        {
            db = new CountryContext();
            db.Continents.Include(c => c.Countries).ThenInclude(c=>c.EconomicUnions).Load();
            db.EconomicInteractions.Load();
            //db.Countries.Include(c => c.EconomicUnions).Include(c=>c.Continent).Load();
            Countries = db.Countries.Local.ToBindingList();
            Continents = db.Continents.Local.ToBindingList();
            EconomicUnions = db.EconomicUnions.Local.ToBindingList();
            EconomicInteractions = db.EconomicInteractions.Local.ToBindingList();

            InteractionMethods = new InteractionMethods(this);

            //System.Diagnostics.Debug.WriteLine("From Country: "+Countries.FirstOrDefault().EconomicUnions.FirstOrDefault().Name);
            //System.Diagnostics.Debug.WriteLine("From Unions: " + economicUnions.FirstOrDefault().Name);
            foreach (Continent continent in Continents)
                System.Diagnostics.Debug.WriteLine(continent.Name);
        }


        public RelayCommand AddCountryCommand
        {
            get
            {
                return addCountryCommand ??
                    (addCountryCommand = new RelayCommand(obj =>
                    {
                        CountryWindow countryWindow = new CountryWindow(new Country(), this);
                        if (countryWindow.ShowDialog() == true)
                        {
                            Country country = countryWindow.Country;
                            System.Diagnostics.Debug.WriteLine("VM Continent: "+country.Continent);
                            System.Diagnostics.Debug.WriteLine("VM GDP: "+country.GDP);
                            db.Countries.Add(country);
                            db.SaveChanges();
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
                        CountryWindow countryWindow = new CountryWindow(countryVM, this);
                        if (countryWindow.ShowDialog() == true)
                        {
                            //System.Diagnostics.Debug.WriteLine("From CW Continent: " + countryWindow.Country.Continent.Name);
                            country = db.Countries.Find(countryWindow.Country.Id);
                            
                            if (country != null)
                            {
                                country.Name = countryWindow.Country.Name;
                                if(countryWindow.Country.Continent!=null)
                                    country.Continent = db.Continents.Find(countryWindow.Country.Continent.Id);
                                country.EconomicUnions = countryWindow.Country.EconomicUnions;
                                country.GDP.Value = countryWindow.Country.GDP.Value;
                                country.Capital = countryWindow.Country.Capital;
                                System.Diagnostics.Debug.WriteLine("Before Write Continent: " + country.Continent.Name);
                                foreach(var c in Continents)
                                {
                                    System.Diagnostics.Debug.WriteLine("Before Write Continent: " + c.Name);
                                    foreach (var ctry in c.Countries)
                                    System.Diagnostics.Debug.WriteLine("Before Write Continent: " + ctry.Name);
                                }
                                db.Entry(country).State = EntityState.Modified;
                                db.SaveChanges();
                                Continents = db.Continents.ToList();
                                Countries = db.Countries.ToList();
                            }

                        }
                    }));
            }
        }
        public RelayCommand RemoveCountryCommand
        {
            get
            {
                return removeCountryCommand ??
                    (removeCountryCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null)
                            return;
                        Country country = (Country)selectedItem;
                        db.Countries.Remove(country);
                        db.SaveChanges();
                    }));
            }
        }

        public RelayCommand AddEconomicUnionCommand
        {
            get
            {
                return addEconomicUnionCommand ??
                    (addEconomicUnionCommand = new RelayCommand(obj =>
                    {
                        EconomicUnionWindow economicUnionWindow = new EconomicUnionWindow(new EconomicUnion(), this);
                        if (economicUnionWindow.ShowDialog() == true)
                        {
                            EconomicUnion economicUnion = economicUnionWindow.EconomicUnion;
                            System.Diagnostics.Debug.WriteLine("VM EU Name: " + economicUnion.Name);
                            db.EconomicUnions.Add(economicUnion);
                            db.SaveChanges();
                        }
                    }));
            }
        }
        public RelayCommand EditEconomicUnionCommand
        {
            get
            {
                return editEconomicUnionCommand ??
                    (editEconomicUnionCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null)
                            return;
                        var economicUnion = selectedItem as EconomicUnion;
                        System.Diagnostics.Debug.WriteLine("Catched: "+economicUnion.Name);
                        EconomicUnion economicUnionVM = new EconomicUnion()
                        {
                            Id = economicUnion.Id,
                            Countries = economicUnion.Countries,
                            Name = economicUnion.Name
                        };

                        EconomicUnionWindow economicUnionWindow = new EconomicUnionWindow(economicUnionVM,this);
                        if(economicUnionWindow.ShowDialog()==true)
                        {
                            System.Diagnostics.Debug.WriteLine("Dialog true. "+economicUnionWindow.EconomicUnion.Name+" "+economicUnionWindow.EconomicUnion.Id);
                            foreach(EconomicUnion economicUnion1 in db.EconomicUnions.ToList())
                            {
                                System.Diagnostics.Debug.WriteLine("Name: "+economicUnion1.Name+"Id: "+economicUnion1.Id);
                            }
                            economicUnion = db.EconomicUnions.Find(economicUnionWindow.EconomicUnion.Id);
                            if(economicUnion != null)
                            {
                                System.Diagnostics.Debug.WriteLine("Adding");
                                economicUnion.Countries = economicUnionWindow.EconomicUnion.Countries;
                                db.Entry(economicUnion).State = EntityState.Modified;
                                db.SaveChanges();
                                economicUnions = db.EconomicUnions.ToList();
                                Countries = db.Countries.ToList();
                            }
                        }

                    }));
            }
        }
        public RelayCommand RemoveEconomicUnionCommand
        {
            get
            {
                return removeEconomicUnionCommand ??
                    (removeEconomicUnionCommand = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null)
                            return;
                        EconomicUnion economicUnion = (EconomicUnion)selectedItem;
                        db.EconomicUnions.Remove(economicUnion);
                        db.SaveChanges();
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
