using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using log4net;

namespace logDB
{
    public class dbLog
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(dbLog));
        public void sqlCmdLog(SqlCommand cmd)
        {
            string sqlQuery = string.Empty;
            sqlQuery = cmd.CommandText;
            logger.Debug("SQL Query : " + sqlQuery);
           
            if (cmd.Parameters.Count != 0)
            {
               foreach(SqlParameter param in cmd.Parameters)
                {
                    logger.Debug("SQL parameter : " + param.ParameterName +" value = " +param.Value);
                }
            }
        }
    }
}
