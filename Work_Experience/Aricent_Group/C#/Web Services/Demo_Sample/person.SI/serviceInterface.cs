using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using person.BL;


namespace person.SI
{
    
    public class serviceInterface
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(serviceInterface));

       
        public personEntity[] viewDetail()
        {               
            try 
	        {
                logger.Info("Control Flow : viewDetail service interface Method Started");
		        personBAL selectOp = new personBAL();
                logger.Info("Load operation called from Bussiness logic class");
                personEntity[] val = selectOp.Load();
                logger.Info("Result of the Load opertaion is returned");
                logger.Info("Control Flow : viewDetail service interface Method Stopped");
                return val;
	        }
	        catch (Exception ex)
	        {
                logger.Error("Error occured in load operation of personBAL class");
                logger.Info("Control Flow : viewDetail service interface Method Stopped");               
		        throw(ex);
	        }
            
        }

        public string insertDetail(personEntity p)
        {
            try 
	        {
                logger.Info("Control Flow : insertDetail service interface Method started");
                personBAL insertOp = new personBAL();
                logger.Info("Insert operation called from Bussiness logic class");
                string value = insertOp.Insert(p);
                logger.Info("Result of the Insert opertaion is returned");
                logger.Info("Control Flow : insertDetail service interface Method stopped");
                return value;
	        }
	        catch (Exception ex)
	        {
                logger.Error("Error occured in insert operation of personBAL class");
                logger.Info("Control Flow : insertDetail service interface Method stopped");
		        throw(ex);
	        }
                
        }

        public string updateDetail(personEntity p)
        {
            try
            {
                logger.Info("Control Flow : updateDetail service interface Method Started");
                personBAL updateOp = new personBAL();
                logger.Info("Update operation called from Bussiness logic class");
                string value = updateOp.Update(p);
                logger.Info("Result of the Update opertaion is returned");
                logger.Info("Control Flow : updateDetail service interface Method Stopped");
                return value;
            }
            catch (Exception ex)
            {
                logger.Error("Error occured in update operation of personBAL class");
                logger.Info("Control Flow : updateDetail service interface Method Stopped");
                throw (ex);
            }
            
        }

        public string delete(int id)
        {
            try
            {
                logger.Info("Control Flow : delete service interface Method Started");       
                personBAL deleteOp = new personBAL();
                logger.Info("Delete operation called from Bussiness logic class");
                string value = deleteOp.Delete(id);
                logger.Info("Result of the Delete opertaion is returned");
                logger.Info("Control Flow : deleteDetail service interface Method Stopped");
                return value;
            }
            catch (Exception ex)
            {
                logger.Error("Error occured in delete operation of personBAL class");
                logger.Info("Control Flow : deleteDetail service interface Method Stopped");
                throw (ex);
            }

        }
    }
}
