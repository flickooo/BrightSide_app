using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace BrightSide_appWpf
{
    class VelicinaDAL
    {
        public static List<Velicina> vratiVelicine()
        {
            string upit = "SELECT * FROM Velicina";

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnBrightSide))
            {
                try
                {
                    IEnumerable<Velicina> listaVelicina = konekcija.Query<Velicina>(upit);
                    return listaVelicina.ToList();
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
    }
}
