using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace utilities
{
    public class queryLog
    {
        public queryLog()
        {
            // TODO: Add constructor logic here 
        }
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(queryLog));

        /// <summary>
        /// This Method will log the Sql Query & the parameter values
        /// </summary>
        /// <param name="cmd"></param>
        /// <history>
        ///     Hari haran      02/05/2012      created
        /// </history>

        public void CmdInfo(SqlCommand cmd)
        {
            try
            {
                logger.Info("Flow Control : Method SqlCmdLog start");

                string sqlQuery = string.Empty;
                sqlQuery = cmd.CommandText;

                logger.Debug("Sql Query : " + sqlQuery);

                if (cmd.Parameters.Count != 0)
                {
                    logger.Debug("Query Parameter Count : " + cmd.Parameters.Count.ToString());

                    foreach (SqlParameter param in cmd.Parameters)
                    {
                        logger.Debug("Sql parameter : " + param.ParameterName + " value = " + param.Value);
                    }
                }

                logger.Info("Flow Control : Method SqlCmdLog stop");

            }
            catch (Exception Ex )
            {
                logger.Error("An Exception occured while logging Sql Query");
                logger.Debug("Exception Info : " + Ex.Message.ToString());

                throw Ex;
            }
        }

        public void CmdInfo(OracleCommand cmd)
        {
            try
            {
                logger.Info("Flow Control : Method OracleCmdLog start");

                string sqlQuery = string.Empty;
                sqlQuery = cmd.CommandText;

                logger.Debug("Oracle Query : " + sqlQuery);

                if (cmd.Parameters.Count != 0)
                {
                    logger.Debug("Query Parameter Count : " + cmd.Parameters.Count.ToString());

                    foreach (OracleParameter param in cmd.Parameters)
                    {
                        logger.Debug("Oracle parameter : " + param.ParameterName + " value = " + param.Value);
                    }
                }

                logger.Info("Flow Control : Method OracleCmdLog stop");

            }
            catch (Exception Ex)
            {
                logger.Error("An Exception occured while logging oracle Query");
                logger.Debug("Exception Info : " + Ex.Message.ToString());

                throw Ex;
            }
        }
    }
}
