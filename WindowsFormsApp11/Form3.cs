using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WindowsFormsApp11.Models;

namespace WindowsFormsApp11
{
    public partial class Form3 : Form
    {
       
        public Form3()
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
        public class CariKodlar
        {
            public string CariKod { get; set; }
            public string TelNo { get; set; }


        }

        public class ParametreList
        {
            public string ParametreAdi { get; set; }
        }

        private List<Parameter> parameters;
       
        public List<CariKodlar> cariKodlars { get; set; } = new List<CariKodlar>();
        public List<CariKodlar> cariKodlar2 { get; set; } = new List<CariKodlar>();
        public List<CariKodlar> cariKodlar3 { get; set; } = new List<CariKodlar>();
        public List<CariKodlar> OnizleList { get; set; } = new List<CariKodlar>();
        string kadi = Properties.Settings.Default.kadi;
        string pass = Properties.Settings.Default.pass;
        public string secilenparametre;
        string carikod = "";
        string tarih = "";
        string vadetarih = "";
        string belgeno = "";
        string aciklama = ""; 
        string hka = "";
        string borc = "";
        string bakiye = "";
        string dovizturu = "";
        string doviztutar = "";
        string raporkodu = "";
        string f9sc = "";
        string hareketturu = "";
        string miktar = "";
        string ilaveraporkodu = "";
        string updatekodu = "";
        string kapatilmistuar = "";
        string odemegunu = "";
        string firmadoviztipi = "";
        string firmadoviztutari = "";
        string plasiyerkodu = "";
        string raporkodu2 = "";
        string duzeltmetarihi = "";
        string syedek1 = "";
        string syedek2 = "";
        string fyedek1 = "";
        string fyedek2 = "";
        string cyedek1 = "";
        string cyedek2 = "";
        string byedek1 = "";
        string iyedek1 = "";
        string lyedek1 = "";
        string dyedek1 = "";
        string projekodu = "";
        string onaytipi = "";
        string onaynum = "";
        string baglantino = "";
        private string sablonismi;
        public string icerik;
        //public string cari;
        public string filtresorgu;
        public string telno;
        public string yazi { get; set; }
        public string yazi1 { get; set; }
        public string SelectedValue { get; set; }
        public string SelectedValue1 { get; set; }
        SmsApiService smsApi = new SmsApiService();
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
                    checkedComboBoxEdit1.Properties.Items.Add(reader["templatead"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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
                            carikod = reader["CH.CARI_KOD"].ToString();
                            tarih = reader["CH.TARIH"].ToString();
                            vadetarih = reader["CH.VADE_TARIHI"].ToString();
                            belgeno = reader["CH.BELGE_NO"].ToString();
                            aciklama = reader["CH.ACIKLAMA"].ToString();
                            hka = reader["CH.HKA"].ToString();
                            borc = reader["CH.BORC"].ToString();
                            bakiye = reader["CH.BAKIYE"].ToString();
                            dovizturu = reader["CH.DOVIZ_TURU"].ToString();
                            doviztutar = reader["CH.DOVIZ_TUTAR"].ToString();
                            raporkodu = reader["CH.RAPOR_KODU"].ToString();
                            f9sc = reader["CH.F9SC"].ToString();
                            hareketturu = reader["CH.HAREKET_TURU"].ToString();
                            miktar = reader["CH.MIKTAR"].ToString();
                            ilaveraporkodu = reader["CH.ILAVE_RAPOR_KODU"].ToString();
                            updatekodu = reader["CH.UPDATE_KODU"].ToString();
                            kapatilmistuar = reader["CH.KAPATILMIS_TUTAR"].ToString();
                            odemegunu = reader["CH.ODEME_GUNU"].ToString();
                            firmadoviztipi = reader["CH.FIRMA_DOVIZ_TIPI"].ToString();
                            firmadoviztutari = reader["CH.FIRMA_DOVIZ_TUTARI"].ToString();
                            plasiyerkodu = reader["CH.PLASIYER_KODU"].ToString();
                            raporkodu2 = reader["CH.RAPOR_KODU2"].ToString();
                            duzeltmetarihi = reader["CH.DUZELTMETARIHI"].ToString();
                            syedek1 = reader["CH.S_YEDEK1"].ToString();
                            syedek2 = reader["CH.S_YEDEK2"].ToString();
                            fyedek1 = reader["CH.F_YEDEK1"].ToString();
                            fyedek2 = reader["CH.F_YEDEK2"].ToString();
                            cyedek1 = reader["CH.C_YEDEK1"].ToString();
                            cyedek2 = reader["CH.C_YEDEK2"].ToString();
                            byedek1 = reader["CH.B_YEDEK1"].ToString();
                            iyedek1 = reader["CH.I_YEDEK1"].ToString();
                            lyedek1 = reader["CH.L_YEDEK1"].ToString();
                            dyedek1 = reader["CH.D_YEDEK1"].ToString();
                            projekodu = reader["CH.PROJE_KODU"].ToString();
                            onaytipi = reader["CH.ONAYTIPI"].ToString();
                            onaynum = reader["CH.ONAYNUM"].ToString();
                            baglantino = reader["CH.BAGLANTI_NO"].ToString();
                            
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
            parameters.Add(new Parameter { Name = "@TARIH", Value = tarih });
            parameters.Add(new Parameter { Name = "@VADE_TARIHI", Value = vadetarih });
            parameters.Add(new Parameter { Name = "@BELGE_NO", Value = belgeno });
            parameters.Add(new Parameter { Name = "@ACIKLAMA", Value = aciklama });
            parameters.Add(new Parameter { Name = "@HKA", Value = hka });
            parameters.Add(new Parameter { Name = "@BORC", Value = borc });
            parameters.Add(new Parameter { Name = "@BAKIYE", Value = bakiye });
            parameters.Add(new Parameter { Name = "@DOVIZ_TURU", Value = dovizturu });
            parameters.Add(new Parameter { Name = "@DOVIZ_TUTAR", Value = doviztutar });
            parameters.Add(new Parameter { Name = "@RAPOR_KODU", Value = raporkodu });
            parameters.Add(new Parameter { Name = "@F9SC", Value = f9sc });
            parameters.Add(new Parameter { Name = "@HAREKET_TURU", Value = hareketturu });
            parameters.Add(new Parameter { Name = "@MIKTAR", Value = miktar });
            parameters.Add(new Parameter { Name = "@ILAVE_RAPOR_KODU", Value = ilaveraporkodu });
            parameters.Add(new Parameter { Name = "@UPDATE_KODU", Value = updatekodu });
            parameters.Add(new Parameter { Name = "@KAPATILMIS_TUTAR", Value = kapatilmistuar });
            parameters.Add(new Parameter { Name = "@ODEME_GUNU", Value = odemegunu });
            parameters.Add(new Parameter { Name = "@FIRMA_DOVIZ_TIPI", Value = firmadoviztipi });
            parameters.Add(new Parameter { Name = "@FIRMA_DOVIZ_TUTARI", Value = firmadoviztutari });
            parameters.Add(new Parameter { Name = "@PLASIYER_KODU", Value = plasiyerkodu });
            parameters.Add(new Parameter { Name = "@RAPOR_KODU2", Value = raporkodu2 });
            parameters.Add(new Parameter { Name = "@DUZELTMETARIHI", Value = duzeltmetarihi });
            parameters.Add(new Parameter { Name = "@S_YEDEK1", Value = syedek1 });
            parameters.Add(new Parameter { Name = "@S_YEDEK2", Value = syedek2 });
            parameters.Add(new Parameter { Name = "@F_YEDEK1", Value = fyedek1 });
            parameters.Add(new Parameter { Name = "@F_YEDEK2", Value = fyedek2 });
            parameters.Add(new Parameter { Name = "@C_YEDEK1", Value = cyedek1 });
            parameters.Add(new Parameter { Name = "@C_YEDEK2", Value = cyedek2 });
            parameters.Add(new Parameter { Name = "@B_YEDEK1", Value = byedek1 });
            parameters.Add(new Parameter { Name = "@I_YEDEK1", Value = iyedek1 });
            parameters.Add(new Parameter { Name = "@L_YEDEK1", Value = lyedek1 });
            parameters.Add(new Parameter { Name = "@D_YEDEK1", Value = dyedek1 });
            parameters.Add(new Parameter { Name = "@PROJE_KODU", Value = projekodu });
            parameters.Add(new Parameter { Name = "@ONAYTIPI", Value = onaytipi });
            parameters.Add(new Parameter { Name = "@ONAYNUM", Value = onaynum });
            parameters.Add(new Parameter { Name = "@BAGLANTI_NO", Value = baglantino });


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
                            
                            MessageBox.Show("Şablon adı boş olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
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
        private async void Form3_Load(object sender, EventArgs e)
        {
            string connectionString = Compaments.ConnectionString3;
            string sql = "SELECT * FROM TBLCAHAR";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            carikod = reader["CARI_KOD"].ToString();
                            tarih = reader["TARIH"].ToString();
                            vadetarih = reader["VADE_TARIHI"].ToString();
                            belgeno = reader["BELGE_NO"].ToString();
                            aciklama = reader["ACIKLAMA"].ToString();
                            hka = reader["HKA"].ToString();
                            borc = reader["BORC"].ToString();
                            bakiye = reader["BAKIYE"].ToString();
                            dovizturu = reader["DOVIZ_TURU"].ToString();
                            doviztutar = reader["DOVIZ_TUTAR"].ToString();
                            raporkodu = reader["RAPOR_KODU"].ToString();
                            f9sc = reader["F9SC"].ToString();
                            hareketturu = reader["HAREKET_TURU"].ToString();
                            miktar = reader["MIKTAR"].ToString();
                            ilaveraporkodu = reader["ILAVE_RAPOR_KODU"].ToString();
                            updatekodu = reader["UPDATE_KODU"].ToString();
                            kapatilmistuar = reader["KAPATILMIS_TUTAR"].ToString();
                            odemegunu = reader["ODEME_GUNU"].ToString();
                            firmadoviztipi = reader["FIRMA_DOVIZ_TIPI"].ToString();
                            firmadoviztutari = reader["FIRMA_DOVIZ_TUTARI"].ToString();
                            plasiyerkodu = reader["PLASIYER_KODU"].ToString();
                            raporkodu2 = reader["RAPOR_KODU2"].ToString();
                            duzeltmetarihi = reader["DUZELTMETARIHI"].ToString();
                            syedek1 = reader["S_YEDEK1"].ToString();
                            syedek2 = reader["S_YEDEK2"].ToString();
                            fyedek1 = reader["F_YEDEK1"].ToString();
                            fyedek2 = reader["F_YEDEK2"].ToString();
                            cyedek1 = reader["C_YEDEK1"].ToString();
                            cyedek2 = reader["C_YEDEK2"].ToString();
                            byedek1 = reader["B_YEDEK1"].ToString();
                            iyedek1 = reader["I_YEDEK1"].ToString();
                            lyedek1 = reader["L_YEDEK1"].ToString();
                            dyedek1 = reader["D_YEDEK1"].ToString();
                            projekodu = reader["PROJE_KODU"].ToString();
                            onaytipi = reader["ONAYTIPI"].ToString();
                            onaynum = reader["ONAYNUM"].ToString();
                            baglantino = reader["BAGLANTI_NO"].ToString();

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

                            listBox2.Items.Add(parametreAdi);
                        }
                    }
                }
            }


           
            TemplateGetir();

           


        }






        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            foreach (CheckedListBoxItem items in checkedComboBoxEdit1.Properties.Items)
            {
                if (items.CheckState == CheckState.Checked)

                {

                    
                            using (SqlConnection conn = new SqlConnection(Compaments.ConnectionString3))
                            {
                                conn.Open();
                                SqlCommand cmd = new SqlCommand("SELECT CARI_KOD,CARI_TEL FROM Cari WHERE Template=@Template ", conn);
                                cmd.Parameters.AddWithValue("@Template",items.Value.ToString());
                                SqlDataReader reader = cmd.ExecuteReader();

                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        OnizleList.Add(new CariKodlar()
                                        {
                                            CariKod = reader["CARI_KOD"].ToString(),
                                            TelNo = reader["CARI_TEL"].ToString()
                                        });

                                    }

                                }
                                else
                                {
                                    // Veri bulunamadı
                                    MessageBox.Show("Veri bulunamadı.");
                                }
                                var SelectedItems = items.Value.ToString();
                                SelectedValue = SelectedItems;
                                foreach (var caritel in OnizleList)
                                {
                                    cari = caritel.CariKod;
                                    tel = caritel.TelNo;
                                    CokluSms(cari);
                                    gonderilecek = yazi1;
                                    MessageBox.Show(gonderilecek);
                                    //smsApi.SmsSender(tel,gonderilecek,kadi,pass);
                                    //try
                                    //{
                                    //    string fromAddress = "tupculala123@gmail.com";
                                    //    string password = "vaxd uncy okjm fuuo";
                                    //    string toAddress = "guzelerdeniz2@gmail.com";
                                    //    string subject = "SMS Gönderilmiştir";
                                    //    string body = yazi1;

                                    //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                                    //    {
                                    //        Port = 587,
                                    //        Credentials = new NetworkCredential(fromAddress, password),
                                    //        EnableSsl = true,
                                    //    };
                                    //    MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body);
                                    //    smtpClient.Send(mailMessage);
                                    //    MessageBox.Show("E-posta başarıyla gönderildi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //}
                                    //catch (Exception ex)
                                    //{
                                    //    MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //}

                                }

                                break;
                             
                            }


                        }


                       
                    

                    
                
            }
        }
        public void SetData(string data)
        {
            textBox3.Text = data;
        }

        string filtrekosul;
        string alacak;

        private void button3_Click(object sender, EventArgs e)
        {
            string text = textBox3.Text;

            // Eğer metin içinde virgül varsa, onu noktaya çevir
            if (text.Contains(","))
            {
                text = text.Replace(",", ".");
                // Düzeltilmiş metni TextBox3'e yaz
                textBox3.Text = text;
            }

            parameters = new List<Parameter>();
            parameters.Add(new Parameter { Name = "@CARI_KOD", Value = carikod });
            parameters.Add(new Parameter { Name = "@TARIH", Value = tarih });
            parameters.Add(new Parameter { Name = "@VADE_TARIHI", Value = vadetarih });
            parameters.Add(new Parameter { Name = "@BELGE_NO", Value = belgeno });
            parameters.Add(new Parameter { Name = "@ACIKLAMA", Value = aciklama });
            parameters.Add(new Parameter { Name = "@HKA", Value = hka });
            parameters.Add(new Parameter { Name = "@BORC", Value = borc });
            parameters.Add(new Parameter { Name = "@ALACAK", Value = alacak });
            parameters.Add(new Parameter { Name = "@BAKIYE", Value = bakiye });
            parameters.Add(new Parameter { Name = "@DOVIZ_TURU", Value = dovizturu });
            parameters.Add(new Parameter { Name = "@DOVIZ_TUTAR", Value = doviztutar });
            parameters.Add(new Parameter { Name = "@RAPOR_KODU", Value = raporkodu });
            parameters.Add(new Parameter { Name = "@F9SC", Value = f9sc });
            parameters.Add(new Parameter { Name = "@HAREKET_TURU", Value = hareketturu });
            parameters.Add(new Parameter { Name = "@MIKTAR", Value = miktar });
            parameters.Add(new Parameter { Name = "@ILAVE_RAPOR_KODU", Value = ilaveraporkodu });
            parameters.Add(new Parameter { Name = "@UPDATE_KODU", Value = updatekodu });
            parameters.Add(new Parameter { Name = "@KAPATILMIS_TUTAR", Value = kapatilmistuar });
            parameters.Add(new Parameter { Name = "@ODEME_GUNU", Value = odemegunu });
            parameters.Add(new Parameter { Name = "@FIRMA_DOVIZ_TIPI", Value = firmadoviztipi });
            parameters.Add(new Parameter { Name = "@FIRMA_DOVIZ_TUTARI", Value = firmadoviztutari });
            parameters.Add(new Parameter { Name = "@PLASIYER_KODU", Value = plasiyerkodu });
            parameters.Add(new Parameter { Name = "@RAPOR_KODU2", Value = raporkodu2 });
            parameters.Add(new Parameter { Name = "@DUZELTMETARIHI", Value = duzeltmetarihi });
            parameters.Add(new Parameter { Name = "@S_YEDEK1", Value = syedek1 });
            parameters.Add(new Parameter { Name = "@S_YEDEK2", Value = syedek2 });
            parameters.Add(new Parameter { Name = "@F_YEDEK1", Value = fyedek1 });
            parameters.Add(new Parameter { Name = "@F_YEDEK2", Value = fyedek2 });
            parameters.Add(new Parameter { Name = "@C_YEDEK1", Value = cyedek1 });
            parameters.Add(new Parameter { Name = "@C_YEDEK2", Value = cyedek2 });
            parameters.Add(new Parameter { Name = "@B_YEDEK1", Value = byedek1 });
            parameters.Add(new Parameter { Name = "@I_YEDEK1", Value = iyedek1 });
            parameters.Add(new Parameter { Name = "@L_YEDEK1", Value = lyedek1 });
            parameters.Add(new Parameter { Name = "@D_YEDEK1", Value = dyedek1 });
            parameters.Add(new Parameter { Name = "@PROJE_KODU", Value = projekodu });
            parameters.Add(new Parameter { Name = "@ONAYTIPI", Value = onaytipi });
            parameters.Add(new Parameter { Name = "@ONAYNUM", Value = onaynum });
            parameters.Add(new Parameter { Name = "@BAGLANTI_NO", Value = baglantino });


            using (SqlConnection connection = new SqlConnection(Compaments.ConnectionString3))
            {
                string selectQuery = "SELECT COUNT(*) FROM Sablon_Filtre WHERE TemplateAd = @Templatead AND Parametre = @Parametre";
                string updateQuery = "UPDATE Sablon_Filtre SET Kisit = @Kisit, Kosul = @Kosul WHERE TemplateAd = @Templatead AND Parametre = @Parametre";
                string insertQuery = "INSERT INTO Sablon_Filtre (TemplateAd, Parametre, Kisit, Kosul) VALUES (@Templatead, @Parametre, @Kisit, @Kosul)";

                var checkedItems = checkedComboBoxEdit1.Properties.Items.GetCheckedValues();
                string checkedItemsString = string.Join(",", checkedItems);

                using (SqlCommand selectCmd = new SqlCommand(selectQuery, connection))
                {
                    selectCmd.Parameters.AddWithValue("@Templatead", checkedItemsString);
                    selectCmd.Parameters.AddWithValue("@Parametre", listBox3.SelectedItem.ToString());

                    connection.Open();
                    int existingCount = (int)selectCmd.ExecuteScalar();

                    if (existingCount > 0)
                    {
                     
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@Templatead", checkedItemsString);
                            updateCmd.Parameters.AddWithValue("@Parametre", listBox3.SelectedItem.ToString());
                            updateCmd.Parameters.AddWithValue("@Kisit", comboBox1.SelectedItem.ToString());
                            updateCmd.Parameters.AddWithValue("@Kosul", textBox3.Text);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                     
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                        {
                            insertCmd.Parameters.AddWithValue("@Templatead", checkedItemsString);
                            insertCmd.Parameters.AddWithValue("@Parametre", listBox3.SelectedItem.ToString());
                            insertCmd.Parameters.AddWithValue("@Kisit", comboBox1.SelectedItem.ToString());
                            insertCmd.Parameters.AddWithValue("@Kosul", textBox3.Text);

                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            string secilenSecenek = comboBox1.SelectedItem.ToString();

            string textboxDegeri = textBox3.Text;

            string secilenAlan = listBox3.SelectedItem.ToString();

            string sorgu = "";



            string parametreDeseni = @"@\w+";

            MatchCollection parametrelerEşleşme = Regex.Matches(secilenAlan, parametreDeseni, RegexOptions.IgnoreCase);

            foreach (Match parametre in parametrelerEşleşme)
            {
                Parameter bulunanParametre = parameters.Find(p => p.Name == parametre.Value);
                if (bulunanParametre != null)
                {
                    if (secilenAlan.StartsWith("@"))
                    {
                        secilenAlan = secilenAlan.Substring(1); // Başındaki "@" işaretini kaldırır
                    }
                    filtrekosul = secilenAlan;
                }
            }

            switch (secilenSecenek)
            {

                case "Büyük":
                    sorgu = $"SELECT CH.CARI_KOD AS CARI_KOD,CS.CARI_TEL AS CARI_TEL FROM TBLCAHAR AS CH JOIN TBLCASABIT AS CS ON CH.CARI_KOD = CS.CARI_KOD WHERE {filtrekosul} > '{textboxDegeri}'";
                    break;
                case "Eşit":
                    sorgu = $"SELECT CH.CARI_KOD AS CARI_KOD,CS.CARI_TEL AS CARI_TEL FROM TBLCAHAR AS CH JOIN TBLCASABIT AS CS ON CH.CARI_KOD = CS.CARI_KOD WHERE {filtrekosul} = '{textboxDegeri}'";
                    break;
                case "Küçük":
                    sorgu = $"SELECT CH.CARI_KOD AS CARI_KOD,CS.CARI_TEL AS CARI_TEL FROM TBLCAHAR AS CH JOIN TBLCASABIT AS CS ON CH.CARI_KOD = CS.CARI_KOD WHERE {filtrekosul} < '{textboxDegeri}'";
                    break;
                case "İle Başlayan":
                    sorgu = $"SELECT CH.CARI_KOD AS CARI_KOD,CS.CARI_TEL AS CARI_TEL FROM TBLCAHAR AS CH JOIN TBLCASABIT AS CS ON CH.CARI_KOD = CS.CARI_KOD WHERE {filtrekosul} LIKE '{textboxDegeri}%'";
                    break;
                case "İle Biten":
                    sorgu = $"SELECT CH.CARI_KOD AS CARI_KOD,CS.CARI_TEL AS CARI_TEL FROM TBLCAHAR AS CH JOIN TBLCASABIT AS CS ON CH.CARI_KOD = CS.CARI_KOD WHERE {filtrekosul} LIKE '%{textboxDegeri}'";
                    break;
                case "İçinde Geçen":
                    sorgu = $"SELECT CH.CARI_KOD ,CS.CARI_TEL  FROM TBLCAHAR AS CH JOIN TBLCASABIT AS CS ON CH.CARI_KOD = CS.CARI_KOD WHERE {filtrekosul} LIKE '%{textboxDegeri}%'";
                    break;
                default:
                    MessageBox.Show("Geçersiz seçenek.");
                    return;
            }



            using (SqlConnection con = new SqlConnection(Compaments.ConnectionString3))
            {
                using (SqlCommand cmd = new SqlCommand(sorgu, con))
                {

               con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();


                    if (cariKodlars.Count == 0)
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {

                                cariKodlars.Add(new CariKodlar()
                                {
                                    CariKod = reader["CARI_KOD"].ToString(),
                                    TelNo = reader["CARI_TEL"].ToString()
                                });






                            }

                        }
                        else
                        {
                        
                        }
                    }
                    else
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {




                                cariKodlar2.Add(new CariKodlar()
                                {
                                    CariKod = reader["CARI_KOD"].ToString(),
                                    TelNo = reader["CARI_TEL"].ToString()
                                }


                                    );



                            }

                        }
                        else
                        {
                            
                        }
                    }


                }

            }




            foreach (var item in cariKodlars)
            {

                foreach (var item2 in cariKodlar2)
                {
                    if (item.CariKod == item2.CariKod)
                    {
                        cariKodlar3.Add(new CariKodlar

                        {
                            CariKod = item.CariKod.ToString(),
                            TelNo = item.TelNo.ToString()
                        }
                            );
                    }
                }

            }

            if (cariKodlar3.Count != 0)
            {
                string procedureName = "FILTRE_GUNCELLE";
                string connectionString = Compaments.ConnectionString3;

                foreach (CheckedListBoxItem items in checkedComboBoxEdit1.Properties.Items)
                {
                    if (items.CheckState == CheckState.Checked)
                    {
                        foreach (var item in cariKodlar3)
                        {
                            string girilenDeger = items.Value.ToString();

                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                SqlCommand command = new SqlCommand(procedureName, connection);
                                command.CommandType = System.Data.CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@caritel", item.TelNo);
                                command.Parameters.AddWithValue("@Templatead", girilenDeger);
                                command.Parameters.AddWithValue("@carikod", item.CariKod);
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
                



                //CokluSms(item.CariKod);
                //MessageBox.Show(yazi1);
                //SmsApiService smsApi = new SmsApiService();
                //smsApi.SmsSender(item.TelNo, yazi1,kadi,pass);
                //try
                //{
                //    string fromAddress = "tupculala123@gmail.com";
                //    string password = "vaxd uncy okjm fuuo";
                //    string toAddress = "guzelerdeniz2@gmail.com";
                //    string subject = "SMS Gönderilmiştir";
                //    string body = yazi1;

                //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                //    {
                //        Port = 587,
                //        Credentials = new NetworkCredential(fromAddress, password),
                //        EnableSsl = true,
                //    };

                //    MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body);

                //    smtpClient.Send(mailMessage);

                //    MessageBox.Show("E-posta başarıyla gönderildi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}



                    MessageBox.Show("Filtreleme Başarılı");

            }



            if (cariKodlars.Count != 0 && cariKodlar3.Count==0)
            {
                foreach (CheckedListBoxItem items in checkedComboBoxEdit1.Properties.Items)
                {
                    if (items.CheckState == CheckState.Checked)
                    {
                        foreach (var item in cariKodlars)
                        {
                            string connectionString = Compaments.ConnectionString3;
                            string procedureName = "CARI_KAYIT";

                            string girilenDeger = items.Value.ToString();

                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                SqlCommand command = new SqlCommand(procedureName, connection);
                                command.CommandType = System.Data.CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@caritel", item.TelNo);
                                command.Parameters.AddWithValue("@Templatead", girilenDeger);
                                command.Parameters.AddWithValue("@carikod", item.CariKod);
                                command.ExecuteNonQuery();


                            }
                            //CokluSms(item.CariKod);
                            //MessageBox.Show(yazi1);
                            //SmsApiService smsApi = new SmsApiService();
                            //smsApi.SmsSender(item.TelNo, yazi1,kadi,pass);
                            //try
                            //{
                            //    string fromAddress = "tupculala123@gmail.com";
                            //    string password = "vaxd uncy okjm fuuo";
                            //    string toAddress = "guzelerdeniz2@gmail.com";
                            //    string subject = "SMS Gönderilmiştir";
                            //    string body = yazi1;

                            //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                            //    {
                            //        Port = 587,
                            //        Credentials = new NetworkCredential(fromAddress, password),
                            //        EnableSsl = true,
                            //    };

                            //    MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body);

                            //    smtpClient.Send(mailMessage);

                            //    MessageBox.Show("E-posta başarıyla gönderildi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                            //catch (Exception ex)
                            //{
                            //    MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //}
                        }
                    }








                }
                

            }
            else if(cariKodlars.Count!=0 && cariKodlar3.Count!=0 )
            {
                foreach (CheckedListBoxItem items in checkedComboBoxEdit1.Properties.Items)
                {
                    if (items.CheckState == CheckState.Checked)
                    {
                        foreach (var item in cariKodlars)
                        {
                            string connectionString = Compaments.ConnectionString3;
                            string procedureName = "CARI_KAYIT";

                            string girilenDeger = items.Value.ToString();

                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                SqlCommand command = new SqlCommand(procedureName, connection);
                                command.CommandType = System.Data.CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@caritel", item.TelNo);
                                command.Parameters.AddWithValue("@Templatead", girilenDeger);
                                command.Parameters.AddWithValue("@carikod", item.CariKod);
                                command.ExecuteNonQuery();


                            }
                            //CokluSms(item.CariKod);
                            //MessageBox.Show(yazi1);
                            //SmsApiService smsApi = new SmsApiService();
                            //smsApi.SmsSender(item.TelNo, yazi1,kadi,pass);
                            //try
                            //{
                            //    string fromAddress = "tupculala123@gmail.com";
                            //    string password = "vaxd uncy okjm fuuo";
                            //    string toAddress = "guzelerdeniz2@gmail.com";
                            //    string subject = "SMS Gönderilmiştir";
                            //    string body = yazi1;

                            //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                            //    {
                            //        Port = 587,
                            //        Credentials = new NetworkCredential(fromAddress, password),
                            //        EnableSsl = true,
                            //    };

                            //    MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body);

                            //    smtpClient.Send(mailMessage);

                            //    MessageBox.Show("E-posta başarıyla gönderildi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                            //catch (Exception ex)
                            //{
                            //    MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //}
                        }
                    }

                    else
                    {
                        MessageBox.Show("Şablon Seçiniz");
                    }






                }

            }














            //CokluSms(item.CariKod);
            //MessageBox.Show(yazi1);
            //SmsApiService smsApi = new SmsApiService();
            //smsApi.SmsSender(item.TelNo, yazi1,kadi,pass);
            //try
            //{
            //    string fromAddress = "tupculala123@gmail.com";
            //    string password = "vaxd uncy okjm fuuo";
            //    string toAddress = "guzelerdeniz2@gmail.com";
            //    string subject = "SMS Gönderilmiştir";
            //    string body = yazi1;

            //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            //    {
            //        Port = 587,
            //        Credentials = new NetworkCredential(fromAddress, password),
            //        EnableSsl = true,
            //    };

            //    MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body);

            //    smtpClient.Send(mailMessage);

            //    MessageBox.Show("E-posta başarıyla gönderildi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var selectedItem in listBox2.SelectedItems)
            {
                listBox3.Items.Add(selectedItem);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (var selectedItem in listBox3.SelectedItems.OfType<string>().ToList())
            {
                listBox3.Items.Remove(selectedItem);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)

        {
          
            parameters = new List<Parameter>();

            parameters.Add(new Parameter { Name = "@CARI_KOD", Value = carikod });
            parameters.Add(new Parameter { Name = "@TARIH", Value = tarih });
            parameters.Add(new Parameter { Name = "@VADE_TARIHI", Value = vadetarih });
            parameters.Add(new Parameter { Name = "@BELGE_NO", Value = belgeno });
            parameters.Add(new Parameter { Name = "@ACIKLAMA", Value = aciklama });
            parameters.Add(new Parameter { Name = "@HKA", Value = hka });
            parameters.Add(new Parameter { Name = "@BORC", Value = borc });
            parameters.Add(new Parameter { Name = "@BAKIYE", Value = bakiye });
            parameters.Add(new Parameter { Name = "@DOVIZ_TURU", Value = dovizturu });
            parameters.Add(new Parameter { Name = "@DOVIZ_TUTAR", Value = doviztutar });
            parameters.Add(new Parameter { Name = "@RAPOR_KODU", Value = raporkodu });
            parameters.Add(new Parameter { Name = "@F9SC", Value = f9sc });
            parameters.Add(new Parameter { Name = "@HAREKET_TURU", Value = hareketturu });
            parameters.Add(new Parameter { Name = "@MIKTAR", Value = miktar });
            parameters.Add(new Parameter { Name = "@ILAVE_RAPOR_KODU", Value = ilaveraporkodu });
            parameters.Add(new Parameter { Name = "@UPDATE_KODU", Value = updatekodu });
            parameters.Add(new Parameter { Name = "@KAPATILMIS_TUTAR", Value = kapatilmistuar });
            parameters.Add(new Parameter { Name = "@ODEME_GUNU", Value = odemegunu });
            parameters.Add(new Parameter { Name = "@FIRMA_DOVIZ_TIPI", Value = firmadoviztipi });
            parameters.Add(new Parameter { Name = "@FIRMA_DOVIZ_TUTARI", Value = firmadoviztutari });
            parameters.Add(new Parameter { Name = "@PLASIYER_KODU", Value = plasiyerkodu });
            parameters.Add(new Parameter { Name = "@RAPOR_KODU2", Value = raporkodu2 });
            parameters.Add(new Parameter { Name = "@DUZELTMETARIHI", Value = duzeltmetarihi });
            parameters.Add(new Parameter { Name = "@S_YEDEK1", Value = syedek1 });
            parameters.Add(new Parameter { Name = "@S_YEDEK2", Value = syedek2 });
            parameters.Add(new Parameter { Name = "@F_YEDEK1", Value = fyedek1 });
            parameters.Add(new Parameter { Name = "@F_YEDEK2", Value = fyedek2 });
            parameters.Add(new Parameter { Name = "@C_YEDEK1", Value = cyedek1 });
            parameters.Add(new Parameter { Name = "@C_YEDEK2", Value = cyedek2 });
            parameters.Add(new Parameter { Name = "@B_YEDEK1", Value = byedek1 });
            parameters.Add(new Parameter { Name = "@I_YEDEK1", Value = iyedek1 });
            parameters.Add(new Parameter { Name = "@L_YEDEK1", Value = lyedek1 });
            parameters.Add(new Parameter { Name = "@D_YEDEK1", Value = dyedek1 });
            parameters.Add(new Parameter { Name = "@PROJE_KODU", Value = projekodu });
            parameters.Add(new Parameter { Name = "@ONAYTIPI", Value = onaytipi });
            parameters.Add(new Parameter { Name = "@ONAYNUM", Value = onaynum });
            parameters.Add(new Parameter { Name = "@BAGLANTI_NO", Value = baglantino });


            string sorgu ;
            Bilgiler bilgiler = new Bilgiler(this);
            string kosul = listBox3.SelectedItem.ToString();


            string parametreDeseni = @"@\w+";

            MatchCollection parametrelerEşleşme = Regex.Matches(kosul, parametreDeseni, RegexOptions.IgnoreCase);

            foreach (Match parametre in parametrelerEşleşme)
            {
                Parameter bulunanParametre = parameters.Find(p => p.Name == parametre.Value);
                if (bulunanParametre != null)
                {
                    if (kosul.StartsWith("@"))
                    {
                        kosul = kosul.Substring(1); // Başındaki "@" işaretini kaldırır
                    }
                    sorgu = kosul;
                    string query = $"SELECT {sorgu} FROM TBLCAHAR";
                    bilgiler.sorgu = query;
                }

            }

            bilgiler.Show();
        }

        private void checkedComboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            comboBox1.SelectedIndex = -1;
            textBox3.Text = "";
            string param, kisit, kosul;
            var checkedItems = checkedComboBoxEdit1.Properties.Items.GetCheckedValues();
            string checkedItemsString = string.Join(",", checkedItems);
            using (SqlConnection conn = new SqlConnection(Compaments.ConnectionString3))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT Parametre,Kisit,Kosul FROM Sablon_Filtre WHERE Templatead=@Templatead", conn))
                {

                    cmd.Parameters.AddWithValue("@Templatead", checkedItemsString);


                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            param = reader["Parametre"].ToString();

                            kisit = reader["Kisit"].ToString();
                            kosul = reader["Kosul"].ToString();

                            listBox3.Items.Add(param);
                            

                        }
                    }
                    
                }

            }

           
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex != -1) 
            {
               
                string secilenOge = listBox3.SelectedItem.ToString();

                string param, kisit, kosul;
                var checkedItems = checkedComboBoxEdit1.Properties.Items.GetCheckedValues();
                string checkedItemsString = string.Join(",", checkedItems);
                using (SqlConnection conn = new SqlConnection(Compaments.ConnectionString3))
                {

                    using (SqlCommand cmd = new SqlCommand("SELECT Kisit,Kosul FROM Sablon_Filtre WHERE Templatead=@Templatead AND Parametre=@param", conn))
                    {

                        cmd.Parameters.AddWithValue("@Templatead", checkedItemsString);
                        cmd.Parameters.AddWithValue("@param", secilenOge);


                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                

                                kisit = reader["Kisit"].ToString();
                                kosul = reader["Kosul"].ToString();

                               
                                textBox3.Text = kosul;
                                comboBox1.SelectedItem = kisit;

                            }
                        }

                    }

                }

            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }

