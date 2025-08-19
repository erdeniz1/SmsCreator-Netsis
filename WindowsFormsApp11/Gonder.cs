using DevExpress.XtraEditors.Controls;
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
    public partial class Gonder : Form
    {
        public Gonder()
        {
            InitializeComponent();
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

        public class Filtrelenenler
        {
            public string Cari { get; set; }
        }

        public class Cari
        {
            public string CariKod { get; set; }
            public string CariTel { get; set; }
        }

        public List<Filtrelenenler> filtre;
        public List<Cari> cariler;
        private List<Parameter> parameters;

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
        string alacak;
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
        public string SelectedValue { get; set; }
        public string yazi1 { get; set; }


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
                            carikod = reader["CARI_KOD"].ToString();
                            tarih = reader["TARIH"].ToString();
                            vadetarih = reader["VADE_TARIHI"].ToString();
                            belgeno = reader["BELGE_NO"].ToString();
                            aciklama = reader["ACIKLAMA"].ToString();
                            hka = reader["HKA"].ToString();
                            borc = reader["BORC"].ToString();
                            alacak = reader["ALACAK"].ToString();
                            bakiye = (int.Parse(alacak) - int.Parse(borc)).ToString();
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
            parameters = new List<Parameter>();

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

        private void Gonder_Load(object sender, EventArgs e)
        {
            TemplateGetir();
        }
        

        private void checkedComboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            List<Filtrelenenler> filtre = new List<Filtrelenenler>();

            string cari;
            var checkedItems = checkedComboBoxEdit1.Properties.Items.GetCheckedValues();
            string checkedItemsString = string.Join(",", checkedItems);
            using (SqlConnection conn = new SqlConnection(Compaments.ConnectionString3))
            {

                using (SqlCommand cmd = new SqlCommand("FILTRELENENLER", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Template", checkedItemsString);


                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("CARI_KOD")))
                            {
                                cari = reader["CARI_KOD"].ToString();
                                filtre.Add(new Filtrelenenler { Cari = cari });
                            }

                        }
                    }

                }
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("CARI_KOD", typeof(string));
                dataTable.Columns.Add("CARI_TEL", typeof(string));
                dataTable.Columns.Add("CARI_ISIM", typeof(string));
                dataTable.Columns.Add("CARI_IL", typeof(string));
                dataTable.Columns.Add("CM_BORCT", typeof(string));
                dataTable.Columns.Add("CM_ALACT", typeof(string));
                foreach (Filtrelenenler filtrelenen in filtre)
                {
                    // Cari değerini al
                    string carikod = filtrelenen.Cari;

                    // TBLCASABIT tablosundan ilgili bilgileri al
                    using (SqlConnection con = new SqlConnection(Compaments.ConnectionString3))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT  CARI_KOD,CARI_ISIM,CARI_TEL,CARI_IL,CM_BORCT,CM_ALACT FROM TBLCASABIT WHERE CARI_KOD = @CariKod", con))
                        {
                            cmd.Parameters.AddWithValue("@CariKod", carikod);
                            con.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                // CariAdi değerini al
                                string cariTel = reader["CARI_TEL"].ToString();
                                string cariKod = reader["CARI_KOD"].ToString();
                                string cariIsim = reader["CARI_ISIM"].ToString();
                                string cariIl = reader["CARI_IL"].ToString();
                                string cariBorc = reader["CM_BORCT"].ToString();
                                string cariAlacak = reader["CM_ALACT"].ToString();


                                // Veri kaynağına ekle
                                dataTable.Rows.Add(carikod, cariTel, cariIsim, cariIl, cariBorc, cariAlacak);
                            }
                            reader.Close();
                        }
                    }
                }
                gridControl1.DataSource = dataTable;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    gridView1.SelectRow(i);
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var checkedItems = checkedComboBoxEdit1.Properties.Items.GetCheckedValues();
            string checkedItemsString = string.Join(",", checkedItems);
            string gonderilecek;
            SelectedValue = checkedItemsString;
            List<Cari> cariler = new List<Cari>();

            foreach (int rowHandle in gridView1.GetSelectedRows())
            {
                
                string cariKod = gridView1.GetRowCellValue(rowHandle, "CARI_KOD").ToString();
                string cariTel = gridView1.GetRowCellValue(rowHandle, "CARI_TEL").ToString();


                cariler.Add(new Cari { CariKod = cariKod, CariTel=cariTel });

                foreach (var item in cariler)
                {
                    string cari = item.CariKod;
                    string tel = item.CariTel;
                    CokluSms(cari);
                    gonderilecek = yazi1;
                    
                    MessageBox.Show(gonderilecek);
                }


            }






            }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
    }

