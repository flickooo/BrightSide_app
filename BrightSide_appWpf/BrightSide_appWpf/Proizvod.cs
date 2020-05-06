using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightSide_appWpf
{
    class Proizvod
    {
        public int ProizvodId { get; set; }
        public string ImeProizvoda { get; set; }
        public decimal NetoCena { get; set; }
        public decimal StamparijaCena { get; set; }
        public override string ToString() => ImeProizvoda;
    }
}
