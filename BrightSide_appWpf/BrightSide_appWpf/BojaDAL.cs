using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace BrightSide_appWpf
{
    class BojaDAL
    {
        public static List<Boja> vratiBoje()
        {
            string upit = "SELECT * FROM Boja";

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnBrightSide))
            {
                try
                {
                    IEnumerable<Boja> listaBoja = konekcija.Query<Boja>(upit);
                    return listaBoja.ToList();
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
    }
}
