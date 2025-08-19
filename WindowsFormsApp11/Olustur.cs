using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp11
{
    public partial class Olustur : Form
    {
        public class Parameter
        {
            public string Name { get; set; }
            public string Value { get; set; }

            public override string ToString()
            {
                return Name;

            }


        }
        private List<Parameter> parameters;

        public class Veriler
        {
            public string Name { get; set; }
            public string Value { get; set; }



        }

        public class ParametreList
        {
            public string ParametreAdi { get; set; }
        }

        public class CariKodlar
        {
            public string CariKod { get; set; }
            public string TelNo { get; set; }


        }

        public List<Veriler> verilers;
        public List<CariKodlar> cariKodlars { get; set; } = new List<CariKodlar>();
        string kadi = Properties.Settings.Default.kadi;
        string pass = Properties.Settings.Default.pass;
        public string secilenparametre;
        private string carikod;
        private string cariisim;
        private string caritel;
        private string cariil;
        private string caritip;
        private string cariadres;
        private string subekodu;
        private string isletmekodu;
        private string ulkekodu;
        private string grupkodu;
        private string vergidairesi;
        private string verginumarasi;
        private string teslimatgunu;
        private string sablonismi;
        public string servisbaglantisi;
        public string icerik;
        //public string cari;
        public string telno;
        public string yazi { get; set; }
        public string yazi1 { get; set; }
        public string SelectedValue { get; set; }
        public string SelectedValue1 { get; set; }
        public Olustur()
        {
            InitializeComponent();

           

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            TemplateGetir();
            LoadTheme();

            List<ParametreList> parametre = new List<ParametreList>();

            using (SqlConnection connection = new SqlConnection(Compaments.ConnectionString3))
            using (SqlCommand command = new SqlCommand("Parametreler", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Okunan değeri uygun türde al
                        object parametreAdiObject = reader.GetValue(0);

                        // Eğer değer null değilse ve string ise, ParametreList nesnesine ekle
                        if (parametreAdiObject != null && parametreAdiObject != DBNull.Value)
                        {
                            string parametreAdi = parametreAdiObject.ToString();

                            listBox1.Items.Add(parametreAdi);
                        }
                    }
                }
            }
        }

        private void textBox2_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(typeof(Parameter)) || e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {


            if (e.Data.GetDataPresent(typeof(Parameter)))
            {
                Parameter parametre = (Parameter)e.Data.GetData(typeof(Parameter));
                textBox2.Text += parametre.Name + " ";
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string label = (string)e.Data.GetData(DataFormats.Text);
                textBox2.Text += label + " ";
            }

            //string textBoxIcerik = textBox1.Text;


            //string textBoxText = textBox1.Text;
            //Regex regex = new Regex(@"@\w+");
            //using (SqlConnection baglan = new SqlConnection(Compaments.ConnectionString3))
            //{
            //    baglan.Open();

            //    using (SqlCommand komut = new SqlCommand("Kaydet2", baglan))
            //    {
            //        komut.CommandType = System.Data.CommandType.StoredProcedure;


            //        foreach (Match match in regex.Matches(textBoxText))
            //        {
            //            string parameterName = match.Value; 

            //        }

            //        try
            //        {
            //            komut.ExecuteNonQuery();
            //            MessageBox.Show("Veri başarıyla kaydedildi.");
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Hata: " + ex.Message);
            //        }
            //    }



            //}

        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox1.DoDragDrop(listBox1.SelectedItem, DragDropEffects.Copy);
            }
        }

        private void TemplateGetir()
        {

            SqlConnection connection = new SqlConnection(Compaments.ConnectionString3);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            try
            {
                connection.Open();
                command.CommandText = "TEMPLATE_GETİR";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["templatead"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private List<string> VeritabanindanTemplateAdlariGetir()
        {
            List<string> templateListesi = new List<string>();

            using (SqlConnection con = new SqlConnection(Compaments.ConnectionString3))
            {
                con.Open();
                string query = "SELECT Templatead FROM Sablon";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string templateAd = reader["Templatead"].ToString();
                            templateListesi.Add(templateAd);
                        }

                        // Döngü bittikten sonra combobox'ı güncelle
                        // comboBox1.DataSource = templateListesi;
                    }
                }
            }

            return templateListesi;

        }

        private void LoadTheme()
        {
            button1.BackColor = ThemeColor.PrimaryColor;
            button1.ForeColor = Color.White;
            button1.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
        }

        private bool DegerVarMi(string deger)
        {
            using (SqlConnection con = new SqlConnection(Compaments.ConnectionString3))
            {
                con.Open();
                string query = "SABLON_KONTROL";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Templatead", deger);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        private string GetTemplateMessage(string templateAd)
        {
            SqlConnection connection = new SqlConnection(Compaments.ConnectionString3);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            string message = string.Empty;
            try
            {
                connection.Open();
                command.CommandText = "MESAJ_GETİR";
                command.Parameters.Clear();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Templatead", templateAd);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    message = result.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return message;
        }
      

       

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                servisbaglantisi = "E";
            }
            else
            {
                servisbaglantisi = "H";
            }




            icerik = textBox2.Text;
            string connectionString1 = Compaments.ConnectionString3;
            string girilenDeger1 = comboBox1.Text.Trim();
            string messageText1 = textBox2.Text;
            string procedureName1 = "SABLON_KAYIT";





            if (girilenDeger1 != "")
            {
                if (DegerVarMi(girilenDeger1))
                {
                  
                    DialogResult result = MessageBox.Show("Bu İsime Sahip Şablon Bulunmaktadır. Güncellemek İster Misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection conn = new SqlConnection(Compaments.ConnectionString3))
                        {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("SABLON_GUNCELLE", conn);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Templatead", girilenDeger1);
                            cmd.Parameters.AddWithValue("@mesaj", messageText1);
                            cmd.Parameters.AddWithValue("@Servis", servisbaglantisi);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Başarıyla Güncellenmiştir");
                        }

                    }
                    else
                    {

                    }

                }
                else
                {


                    using (SqlConnection connection = new SqlConnection(connectionString1))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(procedureName1, connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@mesaj", messageText1);
                        command.Parameters.AddWithValue("@Templatead", girilenDeger1);
                        command.Parameters.AddWithValue("@Servis", servisbaglantisi);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Mesaj başarıyla kaydedildi.");
                        VeritabanindanTemplateAdlariGetir();
                    }
                    //foreach (var item in cariKodlars)
                    //{
                    //    string connectionString = Compaments.ConnectionString3;
                    //    string procedureName = "CARI_KAYIT";

                    //    string girilenDeger = textBox2.Text.Trim();

                    //    using (SqlConnection connection = new SqlConnection(connectionString))
                    //    {
                    //        connection.Open();
                    //        SqlCommand command = new SqlCommand(procedureName, connection);
                    //        command.CommandType = System.Data.CommandType.StoredProcedure;
                    //        command.Parameters.AddWithValue("@caritel", item.TelNo);
                    //        command.Parameters.AddWithValue("@Templatead", girilenDeger);
                    //        command.Parameters.AddWithValue("@carikod", item.CariKod);
                    //        command.ExecuteNonQuery();
                    //        MessageBox.Show("Mesaj başarıyla kaydedildi.");
                    //        VeritabanindanTemplateAdlariGetir();
                    //    }
                    //    //CokluSms(item.CariKod);
                    //    //MessageBox.Show(yazi1);
                    //    //SmsApiService smsApi = new SmsApiService();
                    //    //smsApi.SmsSender(item.TelNo, yazi1,kadi,pass);
                    //    //try
                    //    //{
                    //    //    string fromAddress = "tupculala123@gmail.com";
                    //    //    string password = "vaxd uncy okjm fuuo";
                    //    //    string toAddress = "guzelerdeniz2@gmail.com";
                    //    //    string subject = "SMS Gönderilmiştir";
                    //    //    string body = yazi1;

                    //    //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                    //    //    {
                    //    //        Port = 587,
                    //    //        Credentials = new NetworkCredential(fromAddress, password),
                    //    //        EnableSsl = true,
                    //    //    };

                    //    //    MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body);

                    //    //    smtpClient.Send(mailMessage);

                    //    //    MessageBox.Show("E-posta başarıyla gönderildi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    //}
                    //    //catch (Exception ex)
                    //    //{
                    //    //    MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    //}
                    //}

                }
            }
            else { }




            textBox2.Text = "";
            comboBox1.Text = "";
            TemplateGetir();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Clear();
            string query = "SELECT mesaj FROM Sablon WHERE TemplateAd = @templateAd";
            using (SqlConnection connection = new SqlConnection(Compaments.ConnectionString3))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@templateAd", comboBox1.Text);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        textBox2.Text = result.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
    }
}
