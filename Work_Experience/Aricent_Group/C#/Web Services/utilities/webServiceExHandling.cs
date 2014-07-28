using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.OracleClient;


namespace utilities
{
    public class webServiceExHandling
    {
        public webServiceExHandling()
        {
            // TODO: Add constructor logic here 
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(webServiceExHandling));


        public static void ExceptionLog(Exception Ex)
        {
            logger.Fatal("An Exception occured at Method Name  : {0}" + Ex.TargetSite.ToString());
            logger.Debug("Exception Description  : {0}" + Ex.Message.ToString());
        }

        public static void ExceptionLog(SqlException sqlEx)
        {
            logger.Fatal("An Exception occured at Method Name  : {0}" + sqlEx.TargetSite.ToString());
            logger.Debug("Exception Description  : {0}" + sqlEx.Message.ToString());
        }

        public static void ExceptionLog(OracleException oracleEx)
        {
            logger.Fatal("An Oracle Exception occured at Method Name  : {0}" + oracleEx.TargetSite.ToString());
            logger.Debug("Exception Description  : {0}" + oracleEx.Message.ToString());
        }

        public static void Send_Email(Dictionary<string,string> Mail, string strBody = "")
        {
            try
            {
                logger.Info("Method Send_Email on Exception : Start");

                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                smtp.Host = Mail["SMTPID"];
                smtp.Port = Convert.ToInt32(Mail["PORT"]);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.EnableSsl = true;
                mail.To.Add(Mail["TO"]);
                mail.From = new MailAddress(Mail["FROM"]);
                mail.Subject = Mail["SUBJECT"];
                mail.Body = strBody;
                smtp.Send(mail);

                logger.Info("Method Send_Email on Exception : Stop");
            }
            catch (Exception mailEx)
            {
                logger.Fatal("Exception At Method : Send_Email" + mailEx.Message.ToString());
                logger.Error("Method Send_Email Exception : Stop");
            }

        }
    }
}
