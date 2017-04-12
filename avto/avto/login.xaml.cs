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
using Microsoft.Win32;
using System.Data;
using System.Drawing;


namespace avto
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     

        public string USID;
        public string ISA;
        public string ALA;
        public string EOA;
        public string MA;
        public string PRA;
        Connection cw = new Connection();

        public MainWindow()
        {
            InitializeComponent();
        }
        
       

        void button4_Click(object sender, EventArgs e)
        {

            switch (MessageBox.Show("Завершить работу прилоджения?", "Выход", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:

                    break;
            }
        }



        void button1_Click(object sender, RoutedEventArgs e)
        {
            Avtorizaciya registationWindow = new Avtorizaciya();
            registationWindow.Show();
        }
       
       
        void button2_Click(object sender, RoutedEventArgs e)
        {
            System.Data.Sql.SqlDataSourceEnumerator Server_List =
            System.Data.Sql.SqlDataSourceEnumerator.Instance;
            System.Data.DataTable Server_Table = Server_List.GetDataSources();
            foreach (DataRow row in Server_Table.Rows)
            {
                cw.ServersCB.Items.Clear();
                cw.ServersCB.Items.Add(row[0] + "\\" + row[1]);
            }
            cw.Show();
         }



        void button3_Click(object sender, RoutedEventArgs e)
        {
            
            ConnectionClass ConCheck = new ConnectionClass();
            RegistryKey DataBase_Connection = Registry.CurrentConfig;
            RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
            ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
            SqlConnection connectionUser = new SqlConnection(ConCheck.ConnectString);
            SqlCommand Select_USID = new SqlCommand("select [dbo].[Login].[ID_Login]" +
                " from [dbo].[Login] inner join[dbo].[roli] on " +
                "[dbo].[Login].[roli_id] =[dbo].[roli].[ID_roli]" +
                "where login='" + LoginTextBox.Text + "' and pass='" + PasswordBox.Password + "'", connectionUser);
            //SqlCommand Select_ISA = new SqlCommand("select [dbo].[Party_Role].[IS_Access]" +
            //  " from [dbo].[Alkogolik] inner join[dbo].[Party_Role] on " +
            //  "[dbo].[Alkogolik].[Party_Role_id] =[dbo].[Party_Role].[id_Party_Role]" +
            //  "where alkogolic_log='" + LoginTextBox.Text + "' and alkogolik_pass='" + PasswordBox.Password + "'", connectionUser);
            // SqlCommand Select_ALA = new SqlCommand("select [dbo].[Party_Role].[Alkogolik_Access]" +
            //   " from [dbo].[Alkogolik] inner join[dbo].[Party_Role] on " +
            //   "[dbo].[Alkogolik].[Party_Role_id] =[dbo].[Party_Role].[id_Party_Role]" +
            //   "where alkogolic_log='" + LoginTextBox.Text + "' and alkogolik_pass='" + PasswordBox.Password + "'", connectionUser);
            //SqlCommand Select_EOA = new SqlCommand("select [Party_Role].[Eda_Other_Access]" +
            //    " from [dbo].[Alkogolik] inner join[dbo].[Party_Role] on " +
            //    "[dbo].[Alkogolik].[Party_Role_id] =[dbo].[Party_Role].[id_Party_Role]" +
            //    "where alkogolic_log='" + LoginTextBox.Text + "' and alkogolik_pass='" + PasswordBox.Password + "'", connectionUser);
            // SqlCommand Select_MA = new SqlCommand("select [dbo].[Party_Role].[Main_Access] "+
            //   " from [dbo].[Alkogolik] inner join[dbo].[Party_Role] on " +
            //   "[dbo].[Alkogolik].[Party_Role_id] =[dbo].[Party_Role].[id_Party_Role]" +
            //   "where alkogolic_log='" + LoginTextBox.Text + "' and alkogolik_pass='" + PasswordBox.Password + "'", connectionUser);
            //SqlCommand Select_PRA = new SqlCommand("select [dbo].[Party_Role].[Alko_Price_Access]"+ 
            //    " from [dbo].[Alkogolik] inner join[dbo].[Party_Role] on " +
            //    "[dbo].[Alkogolik].[Party_Role_id] =[dbo].[Party_Role].[id_Party_Role]" +
            //    "where alkogolic_log='"+LoginTextBox.Text+"' and alkogolik_pass='"+PasswordBox.Password+"'", connectionUser);
            try
            {
                connectionUser.Open();
                USID = Select_USID.ExecuteScalar().ToString();
                //    ISA = Select_ISA.ExecuteScalar().ToString();
                //    ALA = Select_ALA.ExecuteScalar().ToString();
                //    EOA = Select_EOA.ExecuteScalar().ToString();
                //    MA = Select_MA.ExecuteScalar().ToString();
                //    PRA = Select_PRA.ExecuteScalar().ToString();
                connectionUser.Close();

            }
            catch (Exception bl)
            {
                MessageBox.Show(PasswordBox.Password);
            }
        }

        private void button4_Click(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void window1_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void window1_Loaded(object sender, RoutedEventArgs e)
        {

            ConnectionClass ConCheck = new ConnectionClass();
            RegistryKey DataBase_Connection = Registry.CurrentConfig;
            RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
            ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString())
                , Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString())
                , Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString())
                , Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
            SqlConnection connectionCheck = new SqlConnection(ConCheck.ConnectString);

            try
            {
                connectionCheck.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            switch (connectionCheck.State == ConnectionState.Open)
            {
                case (true):
                    label.Content =  " - Подключение к источнику данных есть.";
                   button2.IsEnabled = false;
                   button1.IsEnabled = true;
                   button3.IsEnabled = true;
                    break;
                case (false):
                    label.Content =  " - Отсутствует подключение к источнику данных!";
                   button2.IsEnabled = true;
                   button1.IsEnabled = false;
                   button3.IsEnabled = false;
                   
                    break;
            }
        }
    }
}
