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
using Microsoft.Win32;
using System.Data.SqlClient;

namespace avto
{
    /// <summary>
    /// Логика взаимодействия для Avtorizaciya.xaml
    /// </summary>
    public partial class Avtorizaciya : Window
    {
        public Avtorizaciya()
        {
            InitializeComponent();
        }

        void button2_Click(object sender, RoutedEventArgs e)
        {
            switch (Pass_box.Password == ConfPass_Box.Password)
            {
                case (true):
                    string NewUserCkeck;
                    ConnectionClass ConCheck = new ConnectionClass();
                    RegistryKey DataBase_Connection = Registry.CurrentConfig;
                    RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                    ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                                                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                                                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                                                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
                    SqlConnection connectionNewUser = new SqlConnection(ConCheck.ConnectString);
                    SqlCommand Select_USID = new SqlCommand("select [dbo].[Login].[login]" +
                    " from [dbo].[Login] inner join[dbo].[roli] on " +
                    "[dbo].[Login].[roli_id] =[dbo].[roli].[ID_roli]" +
                    "where login='" + Login_text.Text + "' and pass='" + Pass_box.Password + "'", connectionNewUser);
                    try
                    {
                        connectionNewUser.Open();
                        NewUserCkeck = Select_USID.ExecuteScalar().ToString();
                        connectionNewUser.Close();
                        MessageBox.Show("Пользователь с именем " + NameClient_textbox.Text + ", уже есть!");
                    }
                    catch
                    {
                        string GuestRole; 
                        //int Tel_Value;
                        SqlConnection connectionNewUserInsert = new SqlConnection(ConCheck.ConnectString);
                        SqlCommand SelectGuestRole = new SqlCommand("select ID_roli from [dbo].[roli] where Role_Name = 'Гость'"
                            , connectionNewUserInsert);
                        SqlCommand SelectTelNum = new SqlCommand("select max(ID_Login) from [dbo].[Login]", connectionNewUser);
                        connectionNewUserInsert.Open();
                        //GuestRole = SelectGuestRole.ExecuteScalar().ToString();
                        //Tel_Value = Convert.ToInt32(SelectTelNum.ExecuteScalar().ToString());
                        //Tel_Value = Tel_Value + 1;
                        SqlCommand CreateNewUser = new SqlCommand("insert into [dbo].[Login]" +
                        "([IM],[FAM],[OTCH],[TEL],[Roli_id],[login],[pass])" +
                        "values ('" + NameClient_textbox.Text + "','" + FamKlient_textbox.Text + "','" + Otch_klient_Textbox.Text
                        + "','" +Tel_Value.Text+"',"
                        + "'1'" /*GuestRole*/ + ",'" + Login_text.Text + "','" + Pass_box.Password + "')"
                        , connectionNewUserInsert);
                        CreateNewUser.ExecuteNonQuery();
                        connectionNewUserInsert.Close();
                        MessageBox.Show("Вы прошли регистрацию!");

                        //MessageBoxResult.OK:
                        
                        //    Application.Current.Shutdown();






                    }
                    break;
        
                case (false):
                    MessageBox.Show("Пароли не совпадают, повторите попытку");
                    break;
            }
        }

        private void Tel_Value_KeyDown(object sender, KeyEventArgs e)
        {
            //Tel_Value.MaxLength = 11;
            //char cir = 
        }

        private void Tel_Value_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
                e.Handled = !Char.IsDigit(e.Text, 0);
            
        }
    }
}

