using System.Collections.Generic;

namespace TestEFAsyncWPF.Model.Countries
{
    public class EconomicUnion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Country> Countries { get; set; } = new();
    }
}