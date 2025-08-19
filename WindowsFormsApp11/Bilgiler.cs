using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp11
{
    public partial class Bilgiler : Form
    {
        private Form parentForm;

        public Bilgiler(Form parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        public string sorgu;
        private void Bilgiler_Load(object sender, EventArgs e)
        {
            LoadData();
            
        }
        Form3 Form3 = new Form3();
        private void LoadData()
        {
            
            try
            {
                using (SqlConnection connection = new SqlConnection(Compaments.ConnectionString3))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sorgu, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    gridControl1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yükleme hatası: " + ex.Message);
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

            

        }
       
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
           

        }
       

        private void gridControl1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            int columnIndex = gridView1.FocusedColumn.VisibleIndex;

            if (rowHandle >= 0 && columnIndex >= 0)
            {
                object cellValue = gridView1.GetRowCellValue(rowHandle, gridView1.VisibleColumns[columnIndex]);
                string value = cellValue.ToString();

                if (parentForm is MailFiltre form1)
                {
                    form1.SetData(value);
                }
                else if (parentForm is Form3 form2)
                {
                    form2.SetData(value);
                }
            }
            this.Close();
        }
    }
}
