package aricent.com.jdbc;

import java.sql.Connection;
import java.sql.DatabaseMetaData;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;

public class ConnectMySQLdb {
public static Connection getConnection() throws SQLException {
		
		final String driverName ="com.mysql.jdbc.Driver";
		Connection MySqlDBconn = null;
		//String URL = "jdbc:sqlserver://BGHWF9886;databaseName=MyDatabase";
		String URL = "jdbc:mysql://127.0.0.1/test";
		
		String dbUserName = "root";
		String dbPwd = "root123";
	
		System.out.println(URL);
		try {
			Class.forName(driverName);
			MySqlDBconn = DriverManager.getConnection(URL,dbUserName,dbPwd);
			
			if(MySqlDBconn != null) {
				System.out.println("Connection to database established ...");
				DatabaseMetaData metaData = MySqlDBconn.getMetaData();
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
		
		
		return MySqlDBconn;
		
	}
	
	public static void main(String[] args) throws SQLException {
		// TODO Auto-generated method stub
		Connection myConn = getConnection();
		//Statement stmt = myConn.createStatement();
		//String insertQuery = "INSERT INTO EmployeeDetails VALUES ('6666','Bruce',24,'SE')";
		String insertQuery = "INSERT INTO EmployeeDetails VALUES("+
				 "'"+"6666"+"',"+
				 "'"+"BOSS"+"',"+
				 	 24+","+
				 "'"+"SE"+"')";
		
		//stmt.executeUpdate(insertQuery);

		System.out.println("Its Working");
		
	}

}
