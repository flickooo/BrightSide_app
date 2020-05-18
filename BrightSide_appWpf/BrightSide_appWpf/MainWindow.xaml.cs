using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace BrightSide_appWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<Proizvod> listaComboBoxProzivodi = ProizvodDAL.vratiProizvod();
        List<Velicina> listaComboBoxVelicina = VelicinaDAL.vratiVelicine();
        List<Boja> listaComboBoxBoja = BojaDAL.vratiBoje();


        readonly List<string> listaComboBoxAtributi = new List<string> { "Ime", "Prezime", "Instagram" };
        readonly List<string> listaComboBoxPorudzbinaAtrubuti = new List<string> { "Ime", "Prezime", "DatumPorudzbine", "Instagram" };





        private void Resetuj()
        {
            DataGridLista.SelectedIndex = -1;
            TextBoxIme.Clear();
            TextBoxPrezime.Clear();
            TextBoxAdresa.Clear();
            TextBoxGrad.Clear();
            TextBoxPosta.Clear();
            TextBoxTelefon.Clear();
            TextBoxInstagram.Clear();
        }

        private bool validacijaKupca()
        {
            if (string.IsNullOrWhiteSpace(TextBoxIme.Text))
            {
                MessageBox.Show("Ime je obavezno polje");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxPrezime.Text))
            {
                MessageBox.Show("Prezime je obavezno polje");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxAdresa.Text))
            {
                MessageBox.Show("Adresa je obavezno polje");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxGrad.Text))
            {
                MessageBox.Show("Grad je obavezno polje");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxTelefon.Text))
            {
                MessageBox.Show("Telefon je obavezno polje");
                return false;
            }
            return true;
        }

        private bool validacijaPorudzbine()
        {
            if(ListBoxTest.SelectedIndex < 0)
            {
                MessageBox.Show("Odaberite kupca");
                return false;
            }
            if (ComboBoxProizvod.SelectedIndex < 0)
            {
                MessageBox.Show("Odaberi neki proizvod");
                return false;
            }
            if (ComboBoxVelicina.SelectedIndex < 0)
            {
                MessageBox.Show("Odaberi velicinu");
                return false;
            }
            if (ComboBoxBoja.SelectedIndex < 0)
            {
                MessageBox.Show("Odaberi boju");
                return false;
            }
            if (!DatePickerPordzbine.SelectedDate.HasValue)
            {
                MessageBox.Show("Odaberi dan");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxDizajn.Text))
            {
                MessageBox.Show("Dizajn je obavezno polje");
                return false;
            }
            return true;
        }
        private int obostranoStampanje()
        {
            if (CheckBoxObostrano.IsChecked == true)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        private bool obostranoStampanjeIzBaze(Porudzbina p)
        {
            if (p.Obostrano == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void PopupBox_Opened(object sender, RoutedEventArgs e)
        {

        }

        private void PopupBox_Closed(object sender, RoutedEventArgs e)
        {

        }



        private void ButtonOcistiKolone_Click(object sender, RoutedEventArgs e)
        {
            Resetuj();
        }

        private void DataGridLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridLista.SelectedIndex > -1)
            {
                Kupac k = DataGridLista.SelectedItem as Kupac;

                TextBoxIme.Text = k.Ime;
                TextBoxPrezime.Text = k.Prezime;
                TextBoxPosta.Text = k.Posta;
                TextBoxAdresa.Text = k.Adresa;
                TextBoxGrad.Text = k.Grad;
                TextBoxTelefon.Text = k.Telefon;
                TextBoxInstagram.Text = k.Instagram;
            }
        }

        //Ovo ce biti novi prikaz kupca

        private void ListBoxTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxTest.SelectedIndex > -1)
            {
                Kupac k = ListBoxTest.SelectedItem as Kupac;

                TextBoxIme.Text = k.Ime;
                TextBoxPrezime.Text = k.Prezime;
                TextBoxPosta.Text = k.Posta;
                TextBoxAdresa.Text = k.Adresa;
                TextBoxGrad.Text = k.Grad;
                TextBoxTelefon.Text = k.Telefon;
                TextBoxInstagram.Text = k.Instagram;
            }
        }

        private void ButtonPrikaziKupce_Click(object sender, RoutedEventArgs e)
        {
            
            DataGridLista.ItemsSource = null;
            DataGridLista.ItemsSource = KupacDAL.vratiKupca();
            
        }

        private void ButtonUbaciKupca_Click(object sender, RoutedEventArgs e)
        {
            if (validacijaKupca())
            {
                Kupac k = new Kupac
                {
                    Ime = TextBoxIme.Text,
                    Prezime = TextBoxPrezime.Text,
                    Posta = TextBoxPosta.Text,
                    Adresa = TextBoxAdresa.Text,
                    Grad = TextBoxGrad.Text,
                    Telefon = TextBoxTelefon.Text,
                    Instagram = TextBoxInstagram.Text
                };

                int ubacen = KupacDAL.ubaciKupca(k);
                if (ubacen == -1)
                {
                    MessageBox.Show("Greska pri ubacivanju kupca");
                }
                else
                {
                    SnackBarPoruka.MessageQueue.Enqueue("Uspeno ubacen kupac");
                    //MessageBox.Show("Uspesno ubacen kupac");
                    ListBoxTest.ItemsSource = null;
                    ListBoxTest.ItemsSource = KupacDAL.vratiKupca();
                }
            }
        }

        private void ButtonPromeni_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxTest.SelectedIndex > -1)
            {
                if (validacijaKupca())
                {
                    Kupac k = ListBoxTest.SelectedItem as Kupac;

                    k.Ime = TextBoxIme.Text;
                    k.Prezime = TextBoxPrezime.Text;
                    k.Posta = TextBoxPosta.Text;
                    k.Adresa = TextBoxAdresa.Text;
                    k.Grad = TextBoxGrad.Text;
                    k.Telefon = TextBoxTelefon.Text;
                    k.Instagram = TextBoxInstagram.Text;

                    int rezultat = KupacDAL.promeniKupca(k);

                    if (rezultat == -1)
                    {
                        MessageBox.Show("Greska pri promeni proizvoda");
                    }
                    else
                    {
                        DataGridLista.Focus();
                        DataGridLista.ScrollIntoView(k);
                        SnackBarPoruka.MessageQueue.Enqueue("Uspeno promenjen kupac");
                        ListBoxTest.ItemsSource = null;
                        ListBoxTest.ItemsSource = KupacDAL.vratiKupca();
                    }

                }
            }
        }

        private void ButtonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxTest.SelectedIndex > -1)
            {
                Kupac k = ListBoxTest.SelectedItem as Kupac;

                MessageBoxResult mbr = MessageBox.Show(k.Ime, "Brisanje kupca", MessageBoxButton.YesNo);

                if (mbr == MessageBoxResult.No)
                {
                    return;
                }
                int rezultat = KupacDAL.obrisiKupca(k.KupacId);
                //MessageBox.Show(k.KupacId.ToString()); AKO U DATA GRIDU NEMA KUPACID NECE DA GA OBRISE, TO MORAS DA PROVERIS GDE JE GRESKA

                if (rezultat == -1)
                {
                    MessageBox.Show("Greska pri brisanju kupca");
                }
                else
                {
                    Resetuj();
                    SnackBarPoruka.MessageQueue.Enqueue("Uspeno obrisan kupac");
                    ListBoxTest.ItemsSource = null;
                    ListBoxTest.ItemsSource = KupacDAL.vratiKupca();
                }
            }
        }




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Proizvod item in listaComboBoxProzivodi)
            {
                ComboBoxProizvod.Items.Add(item);
            }

            foreach (Velicina item in listaComboBoxVelicina)
            {
                ComboBoxVelicina.Items.Add(item);
            }

            foreach (Boja item in listaComboBoxBoja)
            {
                ComboBoxBoja.Items.Add(item);
            }

            foreach (string item in listaComboBoxAtributi)
            {
                ComboboxAtributi.Items.Add(item);
            }

            
            foreach (string item in listaComboBoxPorudzbinaAtrubuti)
            {
                ComboBoxPorudzbinaPretraga.Items.Add(item);
            }

            DataGridLista.ItemsSource = KupacDAL.vratiKupca();

            ListBoxTest.ItemsSource = KupacDAL.vratiKupca();

            DataGridPorudzbina.ItemsSource = PorudzbinaDAL.VratiPorudzbinu();
            
        }




        private void ButtonNapraviPorudzbinu_Click(object sender, RoutedEventArgs e)
        {
            if (validacijaPorudzbine())
            {
                // Hvata CheckBoxeve i uzima selektovane iteme i vraca selektovani objekat
                Kupac k = ListBoxTest.SelectedItem as Kupac;
                Proizvod p = ComboBoxProizvod.SelectedItem as Proizvod;
                Boja b = ComboBoxBoja.SelectedItem as Boja;
                Velicina v = ComboBoxVelicina.SelectedItem as Velicina;
                string datumPorudzbine = DatePickerPordzbine.SelectedDate.Value.ToString("MM.dd.yyyy");
                string datum = null;
                if (!DatePickerSlanje.SelectedDate.HasValue)
                {
                    datum = null;
                }
                else
                {
                    datum = DatePickerSlanje.SelectedDate.Value.ToString("MM.dd.yyyy");
                }
                // Punim porudzbinu dohvacenim objektima, textboxovima itd
                Porudzbina por = new Porudzbina
                {
                    KupacId = k.KupacId,
                    ProizvodId = p.ProizvodId,
                    Boja = b.BojaId,
                    Velicina = v.VelicinaId,
                    DatumPorudzbine = datumPorudzbine,
                    DatumSlanja = datum,
                    Dizajn = TextBoxDizajn.Text,
                    Obostrano = obostranoStampanje(),
                    Napomena = TextBoxNapomena.Text
                };
                //Pozivam metodu napraviPorudzbini iz klase porudzbinaDAL
                int rezultat = PorudzbinaDAL.NapraviPorudzbinu(por);
                if (rezultat != -1)
                {
                    MessageBox.Show("Uspesno napravljena porudzbina");
                    DataGridPorudzbina.ItemsSource = null;
                    DataGridPorudzbina.ItemsSource = PorudzbinaDAL.VratiPorudzbinu();
                }
                else
                {
                    MessageBox.Show("Greska pri pravljenju porudzbine");
                }
            }
        }

        

        private void ButtonPrikaziPorudzbine_Click(object sender, RoutedEventArgs e)
        {
            DataGridPorudzbina.ItemsSource = null;
            DataGridPorudzbina.ItemsSource = PorudzbinaDAL.VratiPorudzbinu();
        }

        private void ButtonExcel_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void TextBoxPretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListBoxTest.ItemsSource = KupacDAL.flitritanjeKupca(ComboboxAtributi.SelectedItem.ToString(), TextBoxPretraga.Text);
        }


        //Porudzbina pretraga ne radi
        private void TextBoxPoruzbinaPretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ComboBoxPorudzbinaPretraga.SelectedIndex > -1)
            {
                DataGridPorudzbina.ItemsSource = PorudzbinaDAL.flitritanjePorudzbine(ComboBoxPorudzbinaPretraga.SelectedItem.ToString(), TextBoxPoruzbinaPretraga.Text);
            }
            
            
        }

        private void DataGridPorudzbina_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridPorudzbina.SelectedIndex > -1)
            {
                PorudzbinaView p = DataGridPorudzbina.SelectedItem as PorudzbinaView;
                
                //mora da se sredi u databazi indexi za kupce

                ListBoxTest.SelectedIndex = p.KupacId-1;
                
                ComboBoxProizvod.SelectedIndex = p.ProizvodId -1;
                ComboBoxBoja.SelectedIndex = p.Boja -1;
                ComboBoxVelicina.SelectedIndex = p.Velicina -1;
            }
        }

        private void DataGridPorudzbina_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            if (headername == "KupacId")
            {
                e.Cancel = true;
            }
            if (headername == "ProizvodId")
            {
                e.Cancel = true;
            }
            if (headername == "Boja")
            {
                e.Cancel = true;
            }
            if (headername == "Velicina")
            {
                e.Cancel = true;
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
