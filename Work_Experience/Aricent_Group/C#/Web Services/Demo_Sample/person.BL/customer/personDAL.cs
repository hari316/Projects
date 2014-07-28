using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using logDB;
using utilities;

namespace person.BL
{
    public class personDAL
    {
        public personDAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(personDAL));
        dbLog sqlDBLog = new dbLog();

        public personEntity[] retrieve()
        {

            logger.Info("Control Flow : retrieve method of personDAL.cs");

            logger.Info("SQL Connection to the database");
            DataBaseLayer dbconstr = new DataBaseLayer();
            //string connStr = dbconstr.getConnectionStringDetails();
            string connStr = dbconstr.connectionInfo;
            //string connStr = "data source=BGHWF4002\\SQLEXPRESS;Initial Catalog=sample; Integrated Security =True;";
   
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            // dbConnection db = new dbConnection();
            // SqlConnection conn = db.db_user_details;

            SqlDataAdapter da = new SqlDataAdapter();

            logger.Info("SQL Query select command executed from table emp_detail for retrieving the data");
            string selectString = @"SELECT * FROM emp_details ORDER BY UserId";
         
            //SqlParameter param = new SqlParameter();
            //param.ParameterName = "@id";
            //param.Value = 11;

            SqlCommand s_cmd = new SqlCommand(selectString, conn);

            sqlDBLog.sqlCmdLog(s_cmd);

           // logger.Debug("SQL Query : " + selectString);
            //cmd.Parameters.Add(param);

            da.SelectCommand = s_cmd;
            DataSet dSet = new DataSet();
            da.Fill(dSet);

            personEntity[] per = new personEntity[dSet.Tables[0].Rows.Count];

            int i = 0;

            try
            {
                logger.Info("Data from the dataset (da) is loaded into person object");
                logger.Debug("Values retrived from the table : ");
                foreach (DataRow dr in dSet.Tables[0].Rows)
                {
                    personEntity p = new personEntity();
                    p.userId = Convert.ToInt32(dr[0]);
                    p.firstName = dr[1].ToString();
                    p.lastName = dr[2].ToString();
                    p.age = Convert.ToInt32(dr[3]);
                    logger.Debug("Person "+i+" : "+ p);
                    per[i++] = p;
                }
            }


            catch(Exception ex)
            {
                logger.Error("Error occured while loading data from dataset (dSet) into person object");
                throw new myCustomException(301,ex.Message);
            }

            finally
            {
                s_cmd.Dispose();
                dSet.Dispose();
                conn.Close();
                conn.Dispose();
                 
            
            }

            logger.Info("Array of person objects containing all details in the emp_detail table returned");
            return per;

        }

        public int insertion(personEntity p)
        {
            logger.Info("Control Flow : insertion method of personDAL.cs");
            logger.Info("SQL Connection to the database");

            DataBaseLayer dbconstr = new DataBaseLayer();
            string connStr = dbconstr.connectionInfo;
            //string connStr = "data source=BGHWF4002\\SQLEXPRESS;Initial Catalog=sample; Integrated Security =True;";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            logger.Info("SQL Query insert command executed from table emp_detail for inserting the data");
            string insertString = @"INSERT into emp_details(FirstName,LastName,Age) VALUES (@fname,@lname,@age)";
                      
            SqlCommand i_cmd = new SqlCommand(insertString, conn);
            
            try
            {
                logger.Info("SQL insert Query parameters initialised");
                i_cmd.Parameters.AddWithValue("@fname", p.firstName);
                i_cmd.Parameters.AddWithValue("@lname", p.lastName);
                i_cmd.Parameters.AddWithValue("@age", p.age);

                sqlDBLog.sqlCmdLog(i_cmd);
            
                return (i_cmd.ExecuteNonQuery());
            }
            catch(Exception ex)
            {
                logger.Error("SQL insert Query parameter not initialised with valid value");
                throw new myCustomException(302, ex.Message);              
            }
            finally
            {
                i_cmd.Dispose();
                conn.Close();
                conn.Dispose();

            }

        }

        public int updation(personEntity p, int[] entry)
        {

            logger.Info("Control Flow : updation method of personDAL.cs");
            logger.Info("SQL Connection to the database");

            DataBaseLayer dbconstr = new DataBaseLayer();
            string connStr = dbconstr.connectionInfo;
            //string connStr = "data source=BGHWF4002\\SQLEXPRESS;Initial Catalog=sample; Integrated Security =True;";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            logger.Info("SQL Query update command executed from table emp_detail for updating the data");
            string updateString = "UPDATE emp_details set ";
            if (entry[0] == 1)
                updateString = updateString + "FirstName = @fname ";
            if (entry[1] == 1)
                updateString = updateString + ",LastName = @lname, ";
            if (entry[2] == 1)
                updateString = updateString + "Age = @age ";
            updateString = updateString + "where UserId = @id ";

            SqlCommand u_cmd = new SqlCommand(updateString, conn);

            try
            {
                logger.Info("SQL update Query parameters initialised");
                u_cmd.Parameters.AddWithValue("@id", p.userId);
                u_cmd.Parameters.AddWithValue("@fname", p.firstName);
                u_cmd.Parameters.AddWithValue("@lname", p.lastName);
                u_cmd.Parameters.AddWithValue("@age", p.age);

                sqlDBLog.sqlCmdLog(u_cmd);

                logger.Info("SQL update Query executed and the result is returned");
                return (u_cmd.ExecuteNonQuery());
            }
            catch(Exception ex)
            {
                logger.Error("SQL update Query parameter not initialised with valid value");
                throw new myCustomException(303, ex.Message);
            }
            finally
            {
                u_cmd.Dispose();
                conn.Close();
                conn.Dispose();

            }

        }

        public int deletion(int id)
        {
            logger.Info("Control Flow : deletion method of personDAL.cs");
            logger.Info("SQL Connection to the database");

            DataBaseLayer dbconstr = new DataBaseLayer();
            string connStr = dbconstr.connectionInfo;
            //string connStr = "data source=BGHWF4002\\SQLEXPRESS;Initial Catalog=sample; Integrated Security =True;";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            logger.Info("SQL Query delete command executed from table emp_detail for deleting the data");
            string deleteString = @"DELETE FROM emp_details where UserId = @id";

            SqlCommand d_cmd = new SqlCommand(deleteString, conn);

            try
            {
                logger.Info("SQL delete Query parameters initialised");
                d_cmd.Parameters.AddWithValue("@id", id);

                sqlDBLog.sqlCmdLog(d_cmd);
               
                logger.Info("SQL delete Query executed and the result is returned");
                return (d_cmd.ExecuteNonQuery());
            }
            catch(Exception ex)
            {
                logger.Error("SQL delete Query parameter not initialised with valid value");
                throw new myCustomException(304, ex.Message);
            }
            finally
            {
                d_cmd.Dispose();
                conn.Close();
                conn.Dispose();

            }

        }
    }

}
