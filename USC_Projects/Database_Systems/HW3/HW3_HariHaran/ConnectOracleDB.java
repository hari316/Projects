import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

import oracle.jdbc.driver.OracleSQLException;


public class ConnectOracleDB {

	/**
	 * @param args
	 */
	
	public static Connection getConnection() throws OracleSQLException {
		
		//final String driverName ="com.microsoft.sqlserver.jdbc.SQLServerDriver";
		Connection oracleDBconn = null;
		String URL = "jdbc:oracle:thin:@localhost:1522:cs585";
		String dbUserName = "system";
		String dbPwd = "Hari_316";
	
		//System.out.println(URL);
		try {
			//Class.forName(driverName);
			oracleDBconn = DriverManager.getConnection(URL,dbUserName,dbPwd);
			
			if(oracleDBconn != null) {
				System.out.println("Connection to database established ...");			
			}
			else {
				System.out.println("Failed to create database connection ... ");				
			}
				
		//}catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			//e.printStackTrace();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		
		return oracleDBconn;
		
	}
	

}
