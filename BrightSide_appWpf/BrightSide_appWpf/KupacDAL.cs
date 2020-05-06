using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace BrightSide_appWpf
{
    class KupacDAL
    {
        public static int ubaciKupca(Kupac k)
        {
            string upit = @"INSERT INTO Kupac VALUES(@Ime, @Prezime, @Posta, @Adresa, @Grad, @Telefon, @Instagram)
                            SELECT CAST(SCOPE_IDENTITY() AS int)";

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnBrightSide))
            {
                try
                {
                    int id = konekcija.QuerySingleOrDefault<int>(upit, k);
                    return id;
                }
                catch (Exception)
                {

                    return -1;
                }
            }
        }
        public static List<Kupac> vratiKupca()
        {
            string upit = "SELECT * FROM Kupac";

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnBrightSide))
            {
                try
                {
                    IEnumerable<Kupac> listaKupca = konekcija.Query<Kupac>(upit);
                    return listaKupca.ToList();
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
        public static int promeniKupca(Kupac k)
        {
            string upit = @"UPDATE Kupac SET Ime=@Ime, Prezime=@Prezime, Posta=@Posta, Adresa=@Adresa, Grad=@Grad, Telefon=@Telefon,Instagram=@Instagram
                            WHERE KupacId = @KupacId";

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnBrightSide))
            {
                try
                {
                    konekcija.Execute(upit, k);
                    return 0;
                }
                catch (Exception)
                {

                    return -1;
                }
            }
        }
        public static int obrisiKupca(int id)
        {
            string upit = "DELETE Kupac WHERE KupacId = @KupacId";

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnBrightSide))
            {
                try
                {
                    konekcija.Execute(upit, new { KupacId = id });
                    return 0;
                }
                catch (Exception)
                {

                    return -1;
                }

            }
        }
        public static List<Kupac> flitritanjeKupca(string atribut, string pretraga)
        {
            string upit = $@"SELECT KupacId, Ime, Prezime, Posta, Adresa, Grad, Telefon, Instagram
                            FROM Kupac
                            WHERE {atribut} LIKE '%' + @ImeKupca + '%'";

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnBrightSide))
            {
                try
                {
                    IEnumerable<Kupac> listaKupca = konekcija.Query<Kupac>(upit, new { ImeKupca = pretraga });
                    return listaKupca.ToList();

                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
    }
}
