using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace DispatchRegisterAPI.Implementation
{
    public class SQLDAL
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();



        public SQLDAL()
        {
            con = new SqlConnection();
        }

        public void OpenConnection()
        {

            try
            {
                ////if(con!=null)
                if (con.State == ConnectionState.Closed)
                {
                    con.ConnectionString = GetSqlConnectionString();
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }




        /// Method For Execute Query
        public void ExecuteSQL(string sSQL)
        {
            OpenConnection();
            SqlCommand cmd = new SqlCommand(sSQL, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        //ExecuteSP Method with parameter
        public void ExecuteSP(string SPName, SqlParameter[] ParamArray)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = SPName;

                foreach (SqlParameter p in ParamArray)
                {
                    cmd.Parameters.Add(p);
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex1)
            {
                throw ex1;
                con.Close();
            }
        }

        /// <summary>
        /// For sending mail
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="ParamArray"></param>
        public void SendEmail(string from,string pwd,string to,string subject,string msgbody)
        {
            try
            {
                
                    MailMessage msg = new MailMessage();

                    msg.From = new MailAddress(from.ToString(), pwd.ToString());

                    msg.Subject = subject.ToString();

                    msg.To.Add(new MailAddress(to.ToString()));
                   

                    string body = msgbody.ToString();
                
                    msg.Body = body;
                    //msg.Body =  
                    msg.IsBodyHtml = true;
                    var smtp = new System.Net.Mail.SmtpClient();
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(from.ToString(), pwd.ToString());
                        smtp.EnableSsl = true;
                    }
                    // Passing values to smtp object
                    smtp.Send(msg);
                

               
            }
            catch (Exception w)
            {
                throw w;
            }
        }

        /// <summary>
        /// Function For Sending SMS
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="ParamArray"></param>
        /// <returns></returns>

        public void SendingSMS(string sender_name,string sender_number,string receiver_name,string receiver_number,string txt_of_sms)
        {
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(
                "http://103.16.101.52:8080/sendsms/bulksms?username=bukl-softmass&password=123456&type=0&dlr=1&destination="
                + receiver_number
                + "&source=IPMSCH&message="
                +"Received Sms From :"
                + sender_name
                +"\n"+"Mobile No."
                + sender_number
                +"\n"
                + "Message:"+"\n"
                + txt_of_sms
                + " ");
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            }
            catch (Exception w)
            {
                throw w;
            }
        }


        public int ExecuteSP1(string SPName, SqlParameter[] ParamArray)
        {
            int k;
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = SPName;

                foreach (SqlParameter p in ParamArray)
                {
                    cmd.Parameters.Add(p);
                }

             return  k=cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex1)
            {
                throw ex1;
                con.Close();
            }
        }

        // ExecuteSP Method without parameter
        public void ExecuteSP(string SPName)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = SPName;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                con.Close();
            }
        }



        //ExecuteDataset Method with parameters
        public DataSet ExecuteDatasetSP(string SPName, SqlParameter[] ParamArray)
        {
            try
            {
                OpenConnection();

                DataSet ds = new DataSet();
                SqlDataAdapter da;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = SPName;
                da = new SqlDataAdapter(cmd);
                foreach (SqlParameter p in ParamArray)
                {
                    cmd.Parameters.Add(p);
                }
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;

            }

            finally
            {
                con.Close();
            }
        }


        //ExecuteDataset Method without parameter
        public DataSet ExecuteDatasetSP(string SPName)
        {
            try
            {
                OpenConnection();
                DataSet ds = new DataSet();
                SqlDataAdapter da;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = SPName;
                da = new SqlDataAdapter(cmd);

                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;

            }

            finally
            {
                con.Close();
            }
        }

        //ExecuteScalarSP Method 
        public object ExecuteScalarSP(string SPName)
        {
            try
            {
                object obj = new object();
                OpenConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = SPName;
                obj = cmd.ExecuteScalar().ToString();
                return obj;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        public object ExecuteScalarSP(string SPName, SqlParameter[] ParamArray)
        {
            try
            {
                object obj = new object();
                OpenConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = SPName;

                foreach (SqlParameter p in ParamArray)
                {
                    cmd.Parameters.Add(p);
                }
                //obj = cmd.ExecuteScalar().ToString();
                obj = Convert.ToString(cmd.ExecuteScalar());
                return obj;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        public SqlDataReader ExecuteReaderSP(string SPName)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = SPName;
                SqlDataReader reader;


                //foreach (SqlParameter p in ParamArray)
                //{
                //    cmd.Parameters.Add(p);
                //}
                reader = cmd.ExecuteReader();

                return reader;

            }
            catch (Exception ex)
            {
                throw ex;


            }
            finally
            {
                con.Close();
            }

        }


        public SqlDataReader ExecuteReaderSP(string SPName, SqlParameter[] ParamArray)
        {
            SqlDataReader reader;
            try
            {

                OpenConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = SPName;



                foreach (SqlParameter p in ParamArray)
                {
                    cmd.Parameters.Add(p);
                }
                reader = cmd.ExecuteReader();

                return reader;

            }
            catch (Exception ex)
            {
                throw ex;


            }
            finally
            {
                con.Close();
            }

        }

        //ExecuteReader Methode for simple query 
        public DataTable ExecuteReader(string sSQL)
        {
            DataTable table;
            try
            {
                OpenConnection();

                SqlCommand cmd = new SqlCommand(sSQL, con);

                SqlDataReader reader = cmd.ExecuteReader();
                table = new DataTable();
                table.Load(reader);

                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return table;
        }

        //ExecuteScalar Methode for simple query 
        public string ExecuteScalar(string sSQL)
        {
            string value = "";
            try
            {
                //cmd.Connection.Open();
                SqlCommand cmd = new SqlCommand(sSQL, con);
                value = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
                
            }
            return value;
        }
        // MIlind--
        public String ExecuteMMScalarSP(string SPName)
        {
            try
            {

                OpenConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = SPName;


                //obj = cmd.ExecuteScalar().ToString();
                return Convert.ToString(cmd.ExecuteScalar());


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        //MIlind 
        public string ExecuteMMScalarSP(string SPName, SqlParameter[] ParamArray)
        {
            try
            {

                OpenConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = SPName;

                foreach (SqlParameter p in ParamArray)
                {
                    cmd.Parameters.Add(p);
                }
                //obj = cmd.ExecuteScalar().ToString();
                return Convert.ToString(cmd.ExecuteScalar());


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        //Connection String   


        private string GetSqlConnectionString()
        {
            string Constr = System.Configuration.ConfigurationSettings.AppSettings["connectionString"].ToString();
            return Constr;
        }




       
    }
}
