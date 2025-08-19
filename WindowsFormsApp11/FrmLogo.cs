using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp11
{
    public partial class FrmLogo : Form
    {
        public FrmLogo()
        {
            InitializeComponent();
           
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JPEG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|All Files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;

                    // Eski dosyanın var olup olmadığını kontrol et ve varsa sil
                    string debugPath = AppDomain.CurrentDomain.BaseDirectory;
                    string oldImagePath = Path.Combine(debugPath, "selectedImage.jpg");

                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }

                    // Yeni dosyayı debug klasörüne kopyala
                    string destinationPath = Path.Combine(debugPath, "selectedImage.jpg");
                    File.Copy(selectedFilePath, destinationPath, true);

                    Form1 form1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
                    if (form1 != null)
                    {
                        // PictureBox'a resmi yükle
                        form1.pictureBox1.Image = Image.FromFile(destinationPath);
                    }
                }
            }


        }
    }
}
