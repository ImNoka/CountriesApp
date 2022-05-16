using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEFAsyncWPF.Model.Countries
{
    public class Interaction
    {
        public int Id { get; set; }
        [Column(TypeName="date")]
        public DateTime Date { get; set; }
        
    }
}
