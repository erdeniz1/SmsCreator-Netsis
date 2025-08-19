using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp11.Models;

namespace WindowsFormsApp11
{
    public partial class Mail : Form
    {
        public Mail()
        {
            InitializeComponent();
        }

        public class GelenCariler
        {
            public string CariKod { get; set; }
            public string TelNo { get; set; }
        }

        public class Parameter
        {
            public string Name { get; set; }
            public string Value { get; set; }

            public override string ToString()
            {
                return Name;

            }


        }

        Olustur fm = new Olustur();
        public string cari { get; set; }
        public string tel { get; set; }
        string gonderilecek;
        List<GelenCariler> carilers = new List<GelenCariler>();
        public class CariKodlar
        {
            public string CariKod { get; set; }
            public string TelNo { get; set; }


        }

        private List<Parameter> parameters;
        public List<CariKodlar> cariKodlars { get; set; } = new List<CariKodlar>();
        public List<CariKodlar> cariKodlar2 { get; set; } = new List<CariKodlar>();
        public List<CariKodlar> cariKodlar3 { get; set; } = new List<CariKodlar>();
        public List<CariKodlar> OnizleList { get; set; } = new List<CariKodlar>();
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
        public string icerik;
        //public string cari;
        public string telno;
        public string yazi { get; set; }
        public string yazi1 { get; set; }
        public string SelectedValue { get; set; }
        public string SelectedValue1 { get; set; }
        SmsApiService smsApi = new SmsApiService();

       

        private void TemplateGetir()
        {

            //SqlConnection connection = new SqlConnection(Compaments.ConnectionString3);
            //SqlCommand command = new SqlCommand();
            //command.Connection = connection;
            //try
            //{
            //    connection.Open();
            //    command.CommandText = "TEMPLATE_GETİR";
            //    SqlDataReader reader = command.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        checkedComboBoxEdit1.Properties.Items.Add(reader["templatead"].ToString());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}
        }
        private string GetComparisonOperator(string comparison)
        {
            switch (comparison)
            {
                case "Eşit":
                    return "=";
                case "Büyük":
                    return ">";
                case "Küçük":
                    return "<";
                default:
                    return "=";
            }
        }
        public void CokluSms(string kosul)
        {

            string connectionString = Compaments.ConnectionString3;
            string sql = "SECİLEN_BİLGİ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CariKod", kosul);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            carikod = reader["CARI_KOD"].ToString();
                            cariisim = reader["CARI_ISIM"].ToString();
                            caritel = reader["CARI_TEL"].ToString();
                            cariil = reader["CARI_IL"].ToString();
                            caritip = reader["CARI_TIP"].ToString();
                            cariadres = reader["CARI_ADRES"].ToString();
                            subekodu = reader["SUBE_KODU"].ToString();
                            isletmekodu = reader["ISLETME_KODU"].ToString();
                            ulkekodu = reader["ULKE_KODU"].ToString();
                            grupkodu = reader["GRUP_KODU"].ToString();
                            vergidairesi = reader["VERGI_DAIRESI"].ToString();
                            verginumarasi = reader["VERGI_NUMARASI"].ToString();
                            teslimatgunu = reader["TESLIMAT_GUNU"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı bağlantısında veya sorgu yaparken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            parameters = new List<Parameter>();
            parameters.Add(new Parameter { Name = "@CARI_KOD", Value = carikod });
            parameters.Add(new Parameter { Name = "@CARI_ISIM", Value = cariisim });
            parameters.Add(new Parameter { Name = "@CARI_TEL", Value = caritel });
            parameters.Add(new Parameter { Name = "@CARI_IL", Value = cariil });
            parameters.Add(new Parameter { Name = "@CARI_TIP", Value = caritip });
            parameters.Add(new Parameter { Name = "@CARI_ADRES", Value = cariadres });
            parameters.Add(new Parameter { Name = "@SUBE_KODU", Value = subekodu });
            parameters.Add(new Parameter { Name = "@ISLETME_KODU", Value = isletmekodu });
            parameters.Add(new Parameter { Name = "@ULKE_KODU", Value = ulkekodu });
            parameters.Add(new Parameter { Name = "@GRUP_KODU", Value = grupkodu });
            parameters.Add(new Parameter { Name = "@VERGI_DAIRESI", Value = vergidairesi });
            parameters.Add(new Parameter { Name = "@VERGİ_NUMARASI", Value = verginumarasi });
            parameters.Add(new Parameter { Name = "@TESLIMAT_GUNU", Value = teslimatgunu });
            string sqlSorgu = "MESAJ_GETİR";
            string sorgu = SelectedValue;
            string metin = "";

            using (SqlConnection connection = new SqlConnection(Compaments.ConnectionString3))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlSorgu, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        if (!string.IsNullOrEmpty(sorgu))
                        {

                            command.Parameters.AddWithValue("@Templatead", sorgu);
                        }
                        else
                        {
                            // stur null veya boş ise, kullanıcıya uyarı verin veya işlemi durdurun.
                            MessageBox.Show("Şablon adı boş olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        // Veriyi alın ve metin olarak okuyun
                        metin = command.ExecuteScalar()?.ToString();

                        if (string.IsNullOrEmpty(metin))
                        {
                            MessageBox.Show("Mesaj bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanına bağlanırken veya sorgu yaparken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string parametreDeseni = @"@\w+";

            MatchCollection parametrelerEşleşme = Regex.Matches(metin, parametreDeseni, RegexOptions.IgnoreCase);

            foreach (Match parametre in parametrelerEşleşme)
            {
                Parameter bulunanParametre = parameters.Find(p => p.Name == parametre.Value);
                if (bulunanParametre != null)
                {
                    metin = metin.Replace(parametre.Value, bulunanParametre.Value.ToString());
                    yazi1 = metin;
                }
            }




        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {

//            foreach (CheckedListBoxItem items in checkedComboBoxEdit1.Properties.Items)
//            {
//                if (items.CheckState == CheckState.Checked)

//                {


//                    using (SqlConnection conn = new SqlConnection(Compaments.ConnectionString3))
//                    {
//                        conn.Open();
//                        SqlCommand cmd = new SqlCommand("SELECT CARI_KOD,CARI_TEL FROM Cari WHERE Template=@Template ", conn);
//                        cmd.Parameters.AddWithValue("@Template", items.Value.ToString());
//                        SqlDataReader reader = cmd.ExecuteReader();

//                        if (reader.HasRows)
//                        {
//                            while (reader.Read())
//                            {
//                                OnizleList.Add(new CariKodlar()
//                                {
//                                    CariKod = reader["CARI_KOD"].ToString(),
//                                    TelNo = reader["CARI_TEL"].ToString()
//                                });

//                            }

//                        }
//                        else
//                        {
//                            // Veri bulunamadı
//                            MessageBox.Show("Veri bulunamadı.");
//                        }
//                        var SelectedItems = items.Value.ToString();
//                        SelectedValue = SelectedItems;
//                        foreach (var caritel in OnizleList)
//                        {
//                            cari = caritel.CariKod;
//                            tel = caritel.TelNo;
//                            CokluSms(cari);
//                            gonderilecek = yazi1;
//                            MessageBox.Show(gonderilecek);
//                            string debugPath = AppDomain.CurrentDomain.BaseDirectory;
//                            string destinationPath = Path.Combine(debugPath, "selectedImage.jpg");


//                            //CokluSms(item.CariKod);
//                            //MessageBox.Show(yazi1);
//                            //SmsApiService smsApi = new SmsApiService();
//                            //smsApi.SmsSender(item.TelNo, yazi1,kadi,pass);
//                            try
//                            {
//                                string fromAddress = "tupculala123@gmail.com";
//                                string password = "vaxd uncy okjm fuuo";
//                                string toAddress = "guzelerdeniz2@gmail.com";
//                                string subject = "SMS Gönderilmiştir";
//                                string body = 
//$@"<!DOCTYPE html>
//                <html lang=""en"">
//                <head>
//                    <meta charset=""UTF-8"">
//                    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
//                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
//                    <title>Kurumsal E-Posta</title>
//                </head>
//                <body>
//                    <div style=""text-align: center;"">
//                        <img src=""file:///{destinationPath}"" alt=""Şirket Logosu"" width=""200"">
//                        <h2>Şirket Adı</h2>
//                        <p>Şirketinizin açıklaması veya diğer bilgileri buraya gelebilir.</p>
//                    </div>
//                    <hr>
//                    <div>
//                        <h3>Mesaj Başlığı</h3>
//                        <p>Mesaj içeriği buraya gelecek.</p>
//                    </div>
//                </body>
//                </html>";

//                                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
//                                {
//                                    Port = 587,
//                                    Credentials = new NetworkCredential(fromAddress, password),
//                                    EnableSsl = true,
//                                };

//                                MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body);
//                                mailMessage.IsBodyHtml = true;

//                                smtpClient.Send(mailMessage);

//                                MessageBox.Show("E-posta başarıyla gönderildi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                            }
//                            catch (Exception ex)
//                            {
//                                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            }
//                            //smsApi.SmsSender(tel,gonderilecek,kadi,pass);
//                            //try
//                            //{
//                            //    string fromAddress = "tupculala123@gmail.com";
//                            //    string password = "vaxd uncy okjm fuuo";
//                            //    string toAddress = "guzelerdeniz2@gmail.com";
//                            //    string subject = "SMS Gönderilmiştir";
//                            //    string body = yazi1;

//                            //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
//                            //    {
//                            //        Port = 587,
//                            //        Credentials = new NetworkCredential(fromAddress, password),
//                            //        EnableSsl = true,
//                            //    };
//                            //    MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body);
//                            //    smtpClient.Send(mailMessage);
//                            //    MessageBox.Show("E-posta başarıyla gönderildi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                            //}
//                            //catch (Exception ex)
//                            //{
//                            //    MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            //}

//                        }

//                        break;

//                    }


//                }







//            }
        }

        string filtrekosul;
        private void button3_Click(object sender, EventArgs e)
        {

          
        }

        private void Mail_Load(object sender, EventArgs e)
        {

           



            TemplateGetir();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedComboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
