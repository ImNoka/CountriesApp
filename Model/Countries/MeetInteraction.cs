using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEFAsyncWPF.Model.Countries
{
    public class MeetInteraction : Interaction
    {
        public string Name { get; set; }
        public Country MeetCountry { get; set; }
        public List<Country> Countries { get; set; }

    }
}
