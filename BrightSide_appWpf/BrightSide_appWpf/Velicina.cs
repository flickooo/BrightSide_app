using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightSide_appWpf
{
    class Velicina
    {
        public int VelicinaId { get; set; }
        public string VelicinaNaziv { get; set; }
        public override string ToString() => VelicinaNaziv;
    }
}
