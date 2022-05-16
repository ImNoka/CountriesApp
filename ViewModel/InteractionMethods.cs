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
    public class InteractionMethods
    {
        public MainViewModel MainViewModel { get; set; }
        IEnumerable<Country> countries;
        IEnumerable<EconomicInteraction> economicInteractions;
        IEnumerable<MeetInteraction> meetInteractions;


        private RelayCommand addEconomicInteraction;
        private RelayCommand removeEconomicInteraction;
        private RelayCommand editEconomicInteraction;


        public InteractionMethods(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }


        public EconomicInteraction AddEconomicInteraction()
        {
            EconomicInteractionWindow economicInteractionWindow = new EconomicInteractionWindow(
                            new EconomicInteraction() {Date=DateTime.Now }, MainViewModel.Countries);
            if (economicInteractionWindow.ShowDialog() == true)
            {
                EconomicInteraction economicInteraction = economicInteractionWindow.EconomicInteraction;
                return economicInteraction;
            }
            return null;
        }

        public EconomicInteraction EditEconomicInteraction(EconomicInteraction economicInteraction)
        {
            EconomicInteraction economicInteractionVM = new EconomicInteraction()
            {
                Id = economicInteraction.Id,
                Price = economicInteraction.Price,
                CountrySeller = economicInteraction.CountrySeller,
                CountryBuyer = economicInteraction.CountryBuyer,
                Date = economicInteraction.Date,
                Product = economicInteraction.Product
            };
            EconomicInteractionWindow economicInteractionWindow = new EconomicInteractionWindow(
                            economicInteractionVM, MainViewModel.Countries);
            if(economicInteractionWindow.ShowDialog() == true)
            {
                return economicInteractionWindow.EconomicInteraction;
            }
            return economicInteraction;
        }
    }
}
