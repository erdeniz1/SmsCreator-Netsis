using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp11
{
    public partial class Baglanti : Form
    {
        public Baglanti()
        {
            InitializeComponent();
        }

        string smsConnectionString;





        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string comboBoxValue = comboBox1.Text.Trim(); 
            string textBoxValue = textBox1.Text;


            using (SqlConnection connection = new SqlConnection(smsConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO Baglanti (BaglantiAdi, BaglantiDizesi) VALUES (@baglantiadi, @icerik)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@baglantiadi", comboBoxValue);
                    command.Parameters.AddWithValue("@icerik", textBoxValue);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Kayıt başarıyla eklendi.");
                        TemplateGetir();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bir hata oluştu: " + ex.Message);
                    }
                }
            }
            textBox1.Clear();

        }

        private void TemplateGetir()
        {

            SqlConnection connection = new SqlConnection(Compaments.ConnectionString3);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            try
            {
                connection.Open();
                command.CommandText = "SELECT BaglantiAdi FROM Baglanti";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["BaglantiAdi"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }




        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string textBoxValue = textBox1.Text;

            Properties.Settings.Default.ConnectionString = textBoxValue;
        }

        private void Baglanti_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ConnectionString=="")
            {
                smsConnectionString = Compaments.ConnectionString3;
            }
            else
            {
                smsConnectionString = Properties.Settings.Default.ConnectionString;
            }


            TemplateGetir();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string BaglantiAdi= comboBox1.SelectedItem.ToString();

            using (SqlConnection conn = new SqlConnection(smsConnectionString))
            {
                conn.Open();
                string query = "SELECT BaglantiDizesi FROM Baglanti WHERE BaglantiAdi = @BaglantiAdi";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BaglantiAdi", BaglantiAdi);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                       
                        textBox1.Text = reader["BaglantiDizesi"].ToString();
                    }
                }
            }
        }
    }
}
