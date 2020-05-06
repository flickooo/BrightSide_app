using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace BrightSide_appWpf
{
    class PorudzbinaDAL
    {
        public static int NapraviPorudzbinu(Porudzbina p)
        {
            string upit = @"INSERT INTO Porudzbina VALUES(@KupacId, @ProizvodId, @Boja, @Velicina, @DatumPorudzbine, @DatumSlanja, @Dizajn, @Obostrano, @Napomena)
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

        public static List<PorudzbinaView> VratiPorudzbinu()
        {
            string upit = @"SELECT k.Ime, k.Prezime, p.ImeProizvoda , b.BojaNaziv , v.VelicinaNaziv , DatumPorudzbine , DatumSlanja, Dizajn , o.Obostrano , Napomena
                            FROM Porudzbina as por
                            INNER JOIN Proizvod as p
                            ON por.ProizvodId = p.ProizvodId
                            INNER JOIN Kupac as k
                            ON por.KupacId = k.KupacId
                            INNER JOIN Boja as b
                            ON por.Boja = b.BojaId
                            INNER JOIN Velicina as v
                            ON por.Velicina = v.VelicinaId
                            INNER JOIN Obostrano as o
                            ON por.Obostrano = o.ObostranoId";

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnBrightSide))
            {
                try
                {
                    IEnumerable<PorudzbinaView> listaPorudzbina = konekcija.Query<PorudzbinaView>(upit);
                    return listaPorudzbina.ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }


        public static int promeniPorudzbinu(Porudzbina p)
        {
            string upit = @"UPDATE Kupac SET KupacId=@KupacId, ProizvodId=@ProizvodId, Boja=@Boja, Velicina=@Velicina, DatumPorudzbine=@DatumPorudzbine, DatumSlanja=@DatumSlanja, Dizajn=@Dizajn, Obostrano=@Obostrano, Napomena=@Napomena
                            WHERE PorudzbinaId = @PorudzbinaId";

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnBrightSide))
            {
                try
                {
                    konekcija.Execute(upit, p);
                    return 0;
                }
                catch (Exception)
                {

                    return -1;
                }
            }
        }

        public static List<PorudzbinaView> flitritanjePorudzbine(string atribut, string pretraga)
        {
            string upit = $@"SELECT k.Ime, k.Prezime, p.ImeProizvoda , b.BojaNaziv , v.VelicinaNaziv , DatumPorudzbine , DatumSlanja, Dizajn , o.Obostrano , Napomena
                            FROM Porudzbina as por
                            INNER JOIN Proizvod as p
                            ON por.ProizvodId = p.ProizvodId
                            INNER JOIN Kupac as k
                            ON por.KupacId = k.KupacId
                            INNER JOIN Boja as b
                            ON por.Boja = b.BojaId
                            INNER JOIN Velicina as v
                            ON por.Velicina = v.VelicinaId
                            INNER JOIN Obostrano as o
                            ON por.Obostrano = o.ObostranoId
                            WHERE {atribut} LIKE '%' + @Pretraga + '%'";

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnBrightSide))
            {
                try
                {
                    IEnumerable<PorudzbinaView> listaPorudzbina = konekcija.Query<PorudzbinaView>(upit, new { Pretraga = pretraga });
                    return listaPorudzbina.ToList();

                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
    }
}
