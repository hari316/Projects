using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace person.BL
{
    public class personBAL
    {
      
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(personBAL));

        public personBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public personEntity[] Load()
        {

            personDAL selectOp = new personDAL();

            try
            {
                logger.Info("Retrieve operation called from Data Access Layer Class");
                personEntity[] val = selectOp.retrieve();
                logger.Info("Returning all the data from emp_detail table");
                return val;
            }

            catch
            {

                throw;

            }

            finally
            {

                selectOp = null;

            }

        }

        public string Insert(personEntity person)
        {

            personDAL insertOP = new personDAL();

            try
            {
                logger.Info("Insert operation called from Data Access Layer Class");
                if ( person.firstName == string.Empty || person.lastName == string.Empty || person.age == 0)
                {
                    logger.Warn("Invalid parameters passed so delete opertion unsuccessfull");
                    return "Error while inserting the data as parameters passed are not valid";
                }
                int res = insertOP.insertion(person);
                if (res == 0)
                {
                    logger.Error("Error in inserting data to the emp_detail table");
                    return "Error while inserting the data";
                }
                else
                {
                    logger.Info("Successfully entered the data into the emp_detail table");
                    return "Successfully inserted the data";
                }

            }

            catch
            {

                throw;

            }

            finally
            {

                insertOP = null;

            }

        }

        public string Update(personEntity person)
        {

            personDAL updateOP = new personDAL();

            try
            {
                int[] entry = { 0, 0, 0 };

                if (person.userId == 0)
                    return "Error while updating the data please enter a empId";

                if (!(person.firstName == string.Empty))
                    entry[0] = 1;
                if (!(person.lastName == string.Empty))
                    entry[1] = 1;
                if (!(person.age == 0))
                    entry[2] = 1;

                logger.Info("Update operation called from Data Access Layer Class");
                int res = updateOP.updation(person, entry);
                if (res == 0)
                {
                    logger.Error("Error in updating data to the emp_detail table");
                    return "Error while updating the data please enter a valid empId";
                }
                else
                {
                    logger.Info("Successfully updated the data into the emp_detail table");
                    return "Successfully updated the data";
                }

            }

            catch
            {

                throw;

            }

            finally
            {

                updateOP = null;

            }

        }

        public string Delete(int empId)
        {

            personDAL deleteOP = new personDAL();

            try
            {
                if (empId == 0)
                {
                    logger.Warn("EmpId Value = 0 so delete opertion unsuccessfull");
                    return "Error while deleting the data EmpId Value = 0 is not a valid";
                }
                logger.Info("Delete operation called from Data Access Layer Class");
                int res = deleteOP.deletion(empId);
                if (res == 0)
                {
                    logger.Error("Error in deleting data to the emp_detail table");
                    return "Error while deleting the data please enter a valid empId";
                }
                else
                {
                    logger.Info("Successfully deleted the data from the emp_detail table");
                    return "Successfully deleted the data";
                }

            }

            catch
            {

                throw;

            }

            finally
            {

                deleteOP = null;

            }

        }

    }

    
}
