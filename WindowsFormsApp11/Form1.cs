using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp11
{
    public partial class Form1 : Form
    {
         private readonly HttpClient _client;
        public Form1()
        {
            InitializeComponent();
            random = new Random();
            button6.Visible = false;
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            _client = new HttpClient();
        }
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        private async void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = Properties.Settings.Default.Kullanici;
            button1.Font= new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            button2.Font= new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           // button3.Font= new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            button4.Font= new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            button5.Font= new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


            try
            {
                string username = "5526818379";
                string password = "av123.123";

                var smsBalance = await GetBalance(username, password);

                // SMS bakiye bilgisini gösterme
                // MessageBox.Show("SMS Bakiye Bilgisi: " + smsBalance, "SMS Bakiye Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                label3.Text = smsBalance;

            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajını gösterme
               
            }

        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    //currentButton = (Button)btnSender;
                    //currentButton.BackColor = color;
                    //currentButton.ForeColor = Color.White;
                    //currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panel1.BackColor = color;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    ThemeColor.PrimaryColor = color;
                    ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    button6.Visible = true;
                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    //previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    //previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(childForm);
            this.panel2.Tag= childForm;
            childForm.BringToFront();
            childForm.Show();
            //label1.Text = childForm.Text;
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





        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form3(), sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Olustur(), sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Guncelle(), sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Gonder(), sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            Reset();
        }
        private void Reset()
        {
            DisableButton();
            //label1.Text = "HOŞGELDİNİZ";
            //panel1.BackColor = Color.FromArgb(0, 150, 136);
            //panelLogo.BackColor = Color.FromArgb(39, 39, 58);
            currentButton = null;
            button6.Visible = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new MailFiltre(), sender);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OpenChildForm(new LogoE(), sender);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmLogo(), sender);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Baglanti(), sender);
        }
    }
}
