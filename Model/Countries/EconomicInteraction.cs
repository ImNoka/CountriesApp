using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEFAsyncWPF.Model.Countries
{
    public class EconomicInteraction : Interaction
    {
        public string Product { get; set; }
        public double Price { get; set; }
        public Country CountrySeller { get; set; }
        public Country CountryBuyer { get; set; }

    }
}
