using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
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
using DevExpress.XtraReports.UI;

namespace WindowsFormsApp11
{
    public partial class Form2 : Form
    {
        private string carikod;
        private string reportFilePath;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
    
        }
        string smsConnectionString = Properties.Settings.Default.ConnectionString;

        private void RaporOlustur()
        {
          
        }

        private DataTable GetReportDataByCarikod(string carikod)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(smsConnectionString))
            {
                // SQL sorgusu
                string query = "SELECT * FROM TBLCAHAR WHERE CARI_KOD = @carikod";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Parametre ekle
                    command.Parameters.AddWithValue("@carikod", carikod);

                    try
                    {
                        connection.Open();
                        // Verileri almak için SqlDataAdapter kullan
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataTable);
                    }
                    catch (Exception ex)
                    {
                        // Hata işleme
                        MessageBox.Show("Veritabanı hatası: " + ex.Message);
                    }
                }
            }

            return dataTable;
        }

    }
}
