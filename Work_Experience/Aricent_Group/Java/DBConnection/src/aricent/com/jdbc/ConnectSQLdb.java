/**
 * 
 */
package aricent.com.jdbc;
import java.sql.Connection;
import java.sql.DatabaseMetaData;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;

/**
 * @author BGH29309
 *
 */
public class ConnectSQLdb {

	/**
	 * @param args
	 */
	
	public static Connection getConnection() throws SQLException {
		
		final String driverName ="com.microsoft.sqlserver.jdbc.SQLServerDriver";
		Connection sqlDBconn = null;
		String URL = "jdbc:sqlserver://BGHWF9886;databaseName=MyDatabase";
		String dbUserName = "hari";
		String dbPwd = "hari123";
	
		System.out.println(URL);
		try {
			Class.forName(driverName);
			sqlDBconn = DriverManager.getConnection(URL,dbUserName,dbPwd);
			
			if(sqlDBconn != null) {
				System.out.println("Connection to database established ...");
				DatabaseMetaData metaData = sqlDBconn.getMetaData();
				System.out.println("Driver Name: "+metaData.getDriverName());
				System.out.println("Driver Version: "+metaData.getDriverVersion());				
			}
			else {
				System.out.println("Failed to create database connection ... ");				
			}
				
		}catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		
		return sqlDBconn;
		
	}
	
	public static void main(String[] args) throws SQLException {
		// TODO Auto-generated method stub
		Connection myConn = getConnection();
		Statement stmt = myConn.createStatement();
		//String insertQuery = "INSERT INTO EmployeeDetails VALUES ('6666','Bruce',24,'SE')";
		String insertQuery = "INSERT INTO EmployeeDetails VALUES("+
				 "'"+"6666"+"',"+
				 "'"+"BOSS"+"',"+
				 	 24+","+
				 "'"+"SE"+"')";
		
		stmt.executeUpdate(insertQuery);

		System.out.println("Data insersted");
		
	}

}
