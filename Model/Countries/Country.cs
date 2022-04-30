using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEFAsyncWPF.Model.Countries
{
    public class Country
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public int Id { get; set; }
        public DateTime? FoundationDate { get; set; }
        public GDP GDP { get; set; }
        public Continent Continent { get; set; }
        public EconomicUnion? EconomicUnions { get; set; } = new();
        public MilitaryUnion? MilitaryUnions { get; set; } = new();
        public List<Country>? OpenCountries { get; set; } = new();
        public List<Country>? Conflicts { get; set; } = new();
        
    }
}
