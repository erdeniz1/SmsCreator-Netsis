using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp11
{
    public partial class Guncelle : Form
    {
        private readonly HttpClient _client;
        
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


        public Guncelle()
        {
            InitializeComponent();
            _client = new HttpClient();
        }

        private void Guncelle_Load(object sender, EventArgs e)
        {

           

            LoadTheme();
            parameters = new List<Parameter>();
            parameters.Add(new Parameter { Name = "@CARI_KOD", Value = "ornek" });
            parameters.Add(new Parameter { Name = "@CARI_ISIM", Value = "ornek" });
            parameters.Add(new Parameter { Name = "@CARI_TEL", Value = "ornek" });
            parameters.Add(new Parameter { Name = "@CARI_IL", Value = "ornek" });
            parameters.Add(new Parameter { Name = "@CARI_TIP", Value = "1221" });
            parameters.Add(new Parameter { Name = "@CARI_ADRES", Value = "122" });
            parameters.Add(new Parameter { Name = "@SUBE_KODU", Value = "122" });
            parameters.Add(new Parameter { Name = "@ISLETME_KODU", Value = "122" });
            parameters.Add(new Parameter { Name = "@ULKE_KODU", Value = "122" });
            parameters.Add(new Parameter { Name = "@GRUP_KODU", Value = "122" });
            parameters.Add(new Parameter { Name = "@VERGI_DAIRESI", Value = "122" });
            parameters.Add(new Parameter { Name = "@VERGİ_NUMARASI", Value = "122" });
            parameters.Add(new Parameter { Name = "@TESLIMAT_GUNU", Value = "122" });

            listBoxControl1.DataSource = parameters;
        }

        public async Task<string> GetBalance(string username, string password)
        {
            try
            {
                string apiUrl = "https://api.iletimerkezi.com/v1/get-balance";

                string xmlData = $"<request><authentication><username>{username}</username><password>{password}</password></authentication></request>";

                var response = await XMLPOST(apiUrl, xmlData);

                // XML yanıtını parse etme
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response);

                // <sms> elementini al
                XmlNode smsNode = doc.SelectSingleNode("//sms");
                if (smsNode != null)
                {
                    return smsNode.InnerText;
                }
                else
                {
                    return "SMS bilgisi bulunamadı.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "-1";
            }
        }


        private void LoadTheme()
        {


            button1.BackColor = ThemeColor.PrimaryColor;
            button1.ForeColor = Color.White;
            button1.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;

            button2.BackColor = ThemeColor.PrimaryColor;
            button2.ForeColor = Color.White;
            button2.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;

            button3.BackColor = ThemeColor.PrimaryColor;
            button3.ForeColor = Color.White;
            button3.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;

            button4.BackColor = ThemeColor.PrimaryColor;
            button4.ForeColor = Color.White;
            button4.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;



        }

        public async Task<string> XMLPOST(string PostAdress, string xmlData)
        {
            try
            {
                var res = " ";
                byte[] bytes = Encoding.UTF8.GetBytes(xmlData);
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(PostAdress),
                    Content = new ByteArrayContent(bytes)
                };
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");

                using (var response = await _client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    res = await response.Content.ReadAsStringAsync();
                }
                return res;
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing && _client != null)
            {
                _client.Dispose();
            }
            base.Dispose(disposing);
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string username = "5526818379";
                string password = "av123.123";

                var smsBalance = await GetBalance(username, password);

                // SMS bakiye bilgisini gösterme
                MessageBox.Show("SMS Bakiye Bilgisi: " + smsBalance, "SMS Bakiye Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajını gösterme
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
