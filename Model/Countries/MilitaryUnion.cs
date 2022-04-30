using System.Collections.Generic;

namespace TestEFAsyncWPF.Model.Countries
{
    public class MilitaryUnion
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public List<Country> Countries { get; set; }
    }
}