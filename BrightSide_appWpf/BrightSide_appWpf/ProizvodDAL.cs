using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace BrightSide_appWpf
{
    class ProizvodDAL
    {
        public static int ubaciProizvod(Proizvod p)
        {
            string upit = @"INSERT INTO Proizvod VALUES(@ImeProizvoda, @NetoCena, @StamparijaCena)
                            SELECT CAST(SCOPE_IDENTITY() AS int)";

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnBrightSide))
            {
                try
                {
                    int id = konekcija.QuerySingleOrDefault<int>(upit, p);
                    return id;
                }
                catch (Exception)
                {

                    return -1;
                }
            }
        }
        public static List<Proizvod> vratiProizvod()
        {
            string upit = "SELECT * FROM Proizvod";

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnBrightSide))
            {
                try
                {
                    IEnumerable<Proizvod> listaProizvoda = konekcija.Query<Proizvod>(upit);
                    return listaProizvoda.ToList();
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
    }
}
