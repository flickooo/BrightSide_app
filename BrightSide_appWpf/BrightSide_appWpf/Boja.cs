using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightSide_appWpf
{
    class Boja
    {
        public int BojaId { get; set; }
        public string BojaNaziv { get; set; }
        public override string ToString() => BojaNaziv;
    }
}
