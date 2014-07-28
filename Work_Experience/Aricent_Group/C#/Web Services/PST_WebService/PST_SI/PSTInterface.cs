using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using PST_BL;

namespace PST_SI
{
    public class PSTInterface
    {
        public PSTInterface()
        {
            // TODO: Add constructor logic here 
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PSTInterface));

        /// <summary>
        /// This Method is a interface to searchCriteria function
        /// </summary>
        /// <param name="searchCriteria_SI"></param>
        /// <returns></returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>

        public PST_OutputEntity employeeSearch_SI(PST_InputEntity searchCriteria_SI)
        {
            try
            {
                logger.Info("Control Flow : Method - employeeSearch_SI Start");
                logger.DebugFormat("Employee Name : {0} ",searchCriteria_SI.name);

                PST_BAL search_BAL = new PST_BAL();
                return (search_BAL.employeeSearch_BAL(searchCriteria_SI));
            }
            catch (OracleException dbEx)
            {
                logger.Fatal("Database Exception  At PSTInterface - searchCriteria_SI  : " + dbEx.Message.ToString());
                logger.Error("Control Flow : Method - searchCriteria_SI Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At PSTInterface - searchCriteria_SI  : " + ex.Message.ToString());
                logger.Error("Control Flow : Method - searchCriteria_SI Stop");

                throw ex;
            }
        }
   
    }
}
