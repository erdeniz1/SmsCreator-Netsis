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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void GirisYap()
        {
            string kullaniciAdi = textBox1.Text;
            string sifre = textBox2.Text;

            string query = "SELECT COUNT(*) FROM Kullanici WHERE KullaniciAdi=@KullaniciAdi AND Sifre=@Sifre";

            using (SqlConnection connection = new SqlConnection(Compaments.ConnectionString3))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@KullaniciAdi", SqlDbType.NVarChar).Value = kullaniciAdi;
                command.Parameters.Add("@Sifre", SqlDbType.NVarChar).Value = sifre;

                try
                {
                    connection.Open();
                    int kullaniciSayisi = (int)command.ExecuteScalar();

                    if (kullaniciSayisi > 0)
                    { 
                        Properties.Settings.Default.Kullanici = kullaniciAdi;
                        Properties.Settings.Default.Save();
                        Form1 mainForm = new Form1();
                        mainForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre yanlış!", "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Veritabanı bağlantı hatası: " + ex.Message, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GirisYap();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
       
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GirisYap(); // Enter tuşuna basıldığında PerformLogin işlemini çağırıyoruz
            }
        }
    }
}
