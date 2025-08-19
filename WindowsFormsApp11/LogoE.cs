using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp11
{
    public partial class LogoE : Form
    {
        public LogoE()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Boş Alan Bırakmayınız");
            }
            else
            {
                Properties.Settings.Default.Saglayici = comboBox1.SelectedText;
                Properties.Settings.Default.kadi = textBox1.Text;
                Properties.Settings.Default.pass = textBox2.Text;
                Properties.Settings.Default.Save();
                MessageBox.Show("İşlem Başarılı");
            }
        }
    }
}
