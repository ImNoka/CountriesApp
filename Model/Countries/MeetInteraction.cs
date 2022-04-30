using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEFAsyncWPF.Model.Countries
{
    internal class MeetInteraction
    {
        public string Name { get; set; }
        public Country MeetCountry { get; set; }
        public List<Country> Countries { get; set; }

    }
}
