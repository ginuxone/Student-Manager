using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WpfApplication1
{
    /// <summary>
    /// Logica di interazione per StudenteManager.xaml
    /// </summary>
    public partial class StudenteManager : Window
    {
        public string idStudente = "";
        public Studente s;
        public StudenteManager(string id)
        {
            idStudente = id;
            s = new Studente();
            InitializeComponent();
        }
        private void loadDatiStudente()
        {
            SqlConnection sc = new SqlConnection(ConfigurationManager.ConnectionStrings["SchoolConnectionString"].ToString());
            SqlDataReader dr;
            try 
            {
                sc.Open();
                using (sc)
                {
                    SqlCommand cmd = new SqlCommand("select top 1 * from Studente where id = "+idStudente, sc);
                    dr = cmd.ExecuteReader();
                    if(dr.HasRows)
                    {
                        while(dr.Read())
                        {
                            s.Nome=dr.GetString(1);
                            s.Cognome = dr.GetString(2);
                            s.data_di_nascita = dr.GetDateTime(3);
                            s.data_di_iscrizione = dr.GetDateTime(4);
                            s.classe = dr.GetInt32(5);
                            s.sezione = dr.GetString(6);
                        }
                        UserInfo.DataContext = s;
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                sc.Close();
            }
        }
        private void loadOrari()
        {
            SqlConnection sc = new SqlConnection(ConfigurationManager.ConnectionStrings["SchoolConnectionString"].ToString());
            SqlDataAdapter da = new SqlDataAdapter();
            sc.Open();
            using (sc)
            {
                SqlCommand cmd = new SqlCommand("select * from orari", sc);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Orari");
                sda.Fill(dt);
                Orari.ItemsSource = dt.DefaultView;
            }
            sc.Close();
        }
        private void loadDocs()
        {
            SqlConnection sc = new SqlConnection(ConfigurationManager.ConnectionStrings["SchoolConnectionString"].ToString());
            SqlDataAdapter da = new SqlDataAdapter();
            sc.Open();
            using (sc)
            {
                SqlCommand cmd = new SqlCommand("select * from Documenti", sc);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Documenti");
                sda.Fill(dt);
                Docs.ItemsSource = dt.DefaultView;
            }
            sc.Close();
        }
        private void loadVoti()
        {
            SqlConnection sc = new SqlConnection(ConfigurationManager.ConnectionStrings["SchoolConnectionString"].ToString());
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd;
            DataTable dt;
            sc.Open();
            using (sc)
            {
                //Carico Media Voti
                cmd = new SqlCommand("select Materia,avg(voto) as Voto from Voti where ID_Studente="+idStudente+" group by Materia", sc);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable("VotiMedi");
                da.Fill(dt);
                VotiMedi.ItemsSource = dt.DefaultView;
                //Carico Voti complessivi
                cmd = new SqlCommand("select * from voti where ID_Studente="+idStudente+" order by data asc",sc);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable("VotiComplessivi");
                da.Fill(dt);
                VotiComplessivi.ItemsSource = dt.DefaultView;
            }
            sc.Close();
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl t=(TabControl)sender;
            TabItem tmp = (TabItem)t.SelectedItem;
            switch (tmp.Header.ToString())
            {
                case "Home":
                    loadDatiStudente();
                    break;
                case "Orari":
                    loadOrari();
                    break;
                case "Voti":
                    loadVoti();
                    break;
                case "Documents":
                    loadDocs();
                    break;
            }
        } 
    }
}
