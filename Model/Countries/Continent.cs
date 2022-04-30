using System.Collections.Generic;

namespace TestEFAsyncWPF.Model.Countries
{
    public class Continent
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<Country> Countries { get; set; } = new();
    }
}