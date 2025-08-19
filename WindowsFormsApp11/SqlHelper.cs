using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp11
{
    class SqlHelper
    {

        private static SqlConnection cn = new SqlConnection(Compaments.ConnectionString3);
        private static SqlConnection cnn;
        public static SqlCommand cmd;
        public static SqlDataReader dr;
        private static SqlDataAdapter da;
        public static SqlDataReader reader { get; set; }
        public static DataSet ds;

        #region ExecuteSacalar Parametreli

        public static object ExecuteScalar(string connectionString, string commandText, CommandType commandType, List<SqlParameter> parametreler)
        {
            cnn = new SqlConnection(connectionString);
            cmd = new SqlCommand(commandText, cnn);
            cmd.CommandType = commandType;

            foreach (SqlParameter param in parametreler)
            {
                cmd.Parameters.Add(param);
            }

            object sonuc = null;

            try
            {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                sonuc = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return sonuc;
            }
            catch (FormatException err)
            {

                //HttpContext.Current.Response.Redirect("/Hata",false);
                return sonuc;
            }
            catch (Exception err)
            {

                //HttpContext.Current.Response.Redirect("/Hata",false);
                return sonuc;
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
                cmd.Dispose();
            }


        }
        public static SqlCommand GetCommand(string sProcName, CommandType cmdtype, List<SqlParameter> prms)
        {
            // SqlCommand nesnemizi oluşturuyoruz.
            SqlCommand cmd = new SqlCommand(sProcName, cn);

            // Stored Procedure çağrısı
            cmd.CommandType = cmdtype;

            cmd.Parameters.Clear();
            // Parameters koleksiyonuna Add metodu ile ekleme yap.
            if (prms != null)
            {
                foreach (SqlParameter parameter in prms)
                    cmd.Parameters.Add(parameter);
            }
            //end if

            return cmd; // SqlCommand nesnesini döndür.
        }

        public DataSet ExecuteDataSet(string dataSetAdi, string commandText, CommandType commandType, List<SqlParameter> parametreler)
        {
            try
            {
                using (ds = new DataSet())
                {

                    SqlCommand cmd = GetCommand(commandText, commandType, parametreler);
                    using (da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }

                    return ds;
                }
            }
            catch (Exception err)
            {
                //Genel.HataMesajGonder("SqlHelper : ExecuteAdapter  Parameter", err.Message.ToString(), err.StackTrace.ToString());
                return null;
            }


        }
        #endregion

        #region ExecuteReader Parametresiz


        public static SqlDataReader ExecuteDataReader(string connectionString, string commandText, CommandType commandType)
        {
            cnn = new SqlConnection(connectionString);
            cmd = new SqlCommand(commandText, cnn);
            cmd.CommandType = commandType;

            try
            {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (FormatException err)
            {


                dr = null;
            }
            catch (Exception err)
            {

                dr = null;
            }

            return dr;
        }
        #endregion

        #region ExecuteReader Parameterli



        public static SqlDataReader ExecuteDataReader(string connectionString, string commandText, CommandType commandType, List<SqlParameter> parametreler)
        {

            cnn = new SqlConnection(connectionString);
            cmd = new SqlCommand(commandText, cnn);
            cmd.CommandType = commandType;

            foreach (SqlParameter param in parametreler)
            {
                cmd.Parameters.Add(param);
            }
            SqlDataReader datam;

            try
            {
                if (cnn.State == ConnectionState.Closed) cnn.Open();

                datam = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                reader = datam;



                return reader;


            }
            catch (Exception err)
            {

                return null;
            }

        }

        public static void ExecuteDataReader(string connectionString, string commandText, CommandType commandType, List<SqlParameter> parametreler, ref int kayitSayisi)
        {

            cnn = new SqlConnection(connectionString);
            cmd = new SqlCommand(commandText, cnn);
            cmd.CommandType = commandType;

            foreach (SqlParameter param in parametreler)
            {
                cmd.Parameters.Add(param);
            }


            try
            {
                if (cnn.State == ConnectionState.Closed) cnn.Open();

                SqlDataReader datam = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                reader = datam;
                datam.Close();

                kayitSayisi = (int)cmd.Parameters["@KayitSayisi"].Value;
                cnn.Close();

            }
            catch (FormatException err)
            {

                //HttpContext.Current.Response.Redirect("/Hata", false);

            }
            catch (Exception err)
            {


                //HttpContext.Current.Response.Redirect("/Hata", false);

            }


        }

        #endregion

        public static SqlCommand Command(string connectionString, string commandText, List<SqlParameter> parametreler)
        {
            cnn = new SqlConnection(connectionString);
            cmd = new SqlCommand(commandText, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter param in parametreler)
            {
                cmd.Parameters.Add(param);
            }


            try
            {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                return cmd;
            }
            catch (Exception err)
            {

                return null;
            }

        }

        #region ExecuteAdapter parametreli


        public static SqlDataAdapter ExecuteDataAdapter(string connectionString, string commandText, List<SqlParameter> parametreler)
        {

            try
            {
                cnn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cnn;
                cmd.CommandText = commandText;


                foreach (SqlParameter param in parametreler)
                {
                    cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                return adapter;
            }
            catch (Exception err)
            {

                return null;
            }
        }

        public static SqlDataAdapter ExecuteDataAdapter(string connectionString, string commandText, CommandType commandType, List<SqlParameter> parametreler, out int outParametre)
        {
            cnn = new SqlConnection(connectionString);
            cmd = new SqlCommand(commandText, cnn);
            cmd.CommandType = commandType;

            foreach (SqlParameter param in parametreler)
            {
                cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
            }

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(commandText, cnn);
                return adapter;

            }
            catch (Exception err)
            {

                return null;
                //HttpContext.Current.Response.Redirect("/Hata",false);
            }
            finally
            {
                int sayi = (int)cmd.Parameters["@ToplamKayitSayisi"].Value;

                outParametre = sayi;
                cnn.Close();
                cnn.Dispose();
                cmd.Dispose();
            }


        }
        #endregion

        #region ExecuteDataAdapter Parametresiz


        public static SqlDataAdapter ExecuteDataAdapter(string connectionString, string commandText)
        {
            cnn = new SqlConnection(connectionString);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(commandText, cnn);
                return adapter;
            }
            catch (Exception err)
            {

                return null;
                //HttpContext.Current.Response.Redirect("/Hata",false);
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }
        #endregion

        #region ExecuteNoneQuery parametereli


        public static int ExecuteNoneQuery(string connectionString, string commandText, CommandType commandType, List<SqlParameter> parametreler)
        {
            cnn = new SqlConnection(connectionString);
            cmd = new SqlCommand(commandText, cnn);
            cmd.CommandType = commandType;

            foreach (SqlParameter param in parametreler)
            {
                cmd.Parameters.Add(param);
            }


            int sonuc = 0;
            try
            {
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                sonuc = cmd.ExecuteNonQuery();
            }
            catch (FormatException err)
            {

                //HttpContext.Current.Response.Redirect("/Hata",false);
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString());
                //HttpContext.Current.Response.Redirect("/Hata",false);

            }

            cnn.Close();
            cnn.Dispose();
            cmd.Dispose();



            return sonuc;
        }
        #endregion

        //public  void Dispose()
        //{
        //    GC.SuppressFinalize(this);
        //}

    }
}
