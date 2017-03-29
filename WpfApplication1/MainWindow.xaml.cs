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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;

namespace WpfApplication1
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void logIn(object sender, RoutedEventArgs e)
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["SchoolConnectionString"].ToString());
            SqlCommand command;
            SqlDataReader dr;
            bool correct=false, isAdmin=false;
            string id="";
            if(!string.IsNullOrEmpty(usernameTxt.Text) && !string.IsNullOrEmpty(passwordTxt.Password))
            {
                command = new SqlCommand("SELECT * FROM Users WHERE Username LIKE '" + usernameTxt.Text.ToString() + "'",cnn);
                try
                {
                    cnn.Open(); 
                    dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            correct = (dr.GetValue(2).ToString() == passwordTxt.Password ? true : false);
                            isAdmin = (dr.GetValue(4).ToString() != "" ? true : false);
                            id=(!isAdmin ? dr.GetValue(3).ToString():dr.GetValue(4).ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("WrongUsername", "Wrong Parameters", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    cnn.Close();
                }catch(SqlException ex)
                {
                    MessageBox.Show("Error while connecting to the DB \r\n Try again later"+ex.ToString(), "Wrong DB", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                //Launch the new window or throw "Error"
                if (!correct)
                    MessageBox.Show("WrongPassword", "Wrong Parameters", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else if(isAdmin)
                { 
                    InsegnanteManager im = new InsegnanteManager();
                    im.Show();
                    this.Close();
                }
                else if (!isAdmin)
                {
                    StudenteManager sm = new StudenteManager(id);
                    sm.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Fill the missing fields", "Wrong Parameters", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
