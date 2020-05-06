using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightSide_appWpf
{
    class Porudzbina
    {
        public int PorudzbinaId { get; set; }
        public int ProizvodId { get; set; }
        public int KupacId { get; set; }
        public int Boja { get; set; }
        public int Velicina { get; set; }
        public string DatumPorudzbine { get; set; }
        public string DatumSlanja { get; set; }
        public string Dizajn { get; set; }
        public int Obostrano { get; set; }
        public string Napomena { get; set; }
    }
}
