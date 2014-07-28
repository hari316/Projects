using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services.Protocols;
using logDB;

/// <summary>
/// Summary description for AuthenticationSoapHeader
/// </summary>
public class AuthenticationSoapHeader : SoapHeader
{
        public string Username ;
        public string Password ;

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AuthenticationSoapHeader));
        public int authFlag = 0;

       // class validate
      //  {
        public string authenticate()
        {

            logger.Info("Control Flow : Authenticate method Started");


            if (Username == string.Empty || Password == string.Empty)
            {
                logger.Warn("UserName / UserPwd field empty in soap header");
                return "YOU ARE NOT AUTHORIZED please Enter a valid  username & password !!";
            }
            // try
            // {
            logger.Info("SQL Connection to the database");
            string constr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = constr;
            conn.Open();
            logger.Info("SQL Query select command executed from table user_pwd for authentication");
            string selectString = "select * from user_pwd where userName = @name and userPwd = @pwd";

            SqlCommand s_cmd = new SqlCommand(selectString, conn);

            s_cmd.Parameters.AddWithValue("@name", Username);
            s_cmd.Parameters.AddWithValue("@pwd", Password);

            logger.Info("SQL Query parameters initialised");

            SqlDataAdapter da = new SqlDataAdapter();

            dbLog sqlDBLog = new dbLog();
            sqlDBLog.sqlCmdLog(s_cmd);

            da.SelectCommand = s_cmd;

            DataSet dSet = new DataSet();
            da.Fill(dSet);
            int res = dSet.Tables[0].Rows.Count;

            if (res == 1)
            {
                authFlag = 1;
                logger.Info("Authentication Flag set to true value (1)");
                return "ACCESS GRANTED : Authorized to access the CRUD operations!!!";
            }
            else
            {
                return "ACCESS DENIED : YOU ARE NOT AUTHORIZED !!";
            }

            // }
            //  catch(Exception ex)
            //  {
            //   logger.Error("SQL Query parameter not initialised with valid value : ",ex);

            //   ThrowSoapException soapEx = new ThrowSoapException();
            //    soapEx.myThrow(ex);


            //  }
            // finally
            //  {
            //    s_cmd.Dispose();
            //    conn.Close();
            //    conn.Dispose();
            //    logger.Info("Control Flow : Authenticate method Stopped");
            // }
            // }
            // catch (Exception ex)
            // {
            //      logger.Fatal("Error while establishing connection to the database : ", ex);
            // myThrow1(ex);
            // }


        }
     //   }

}