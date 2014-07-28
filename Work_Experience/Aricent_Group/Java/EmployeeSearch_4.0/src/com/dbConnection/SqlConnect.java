/**
 * 
 */
package com.dbConnection;

import java.sql.Connection;
import java.sql.DatabaseMetaData;
import java.sql.DriverManager;
import java.sql.SQLException;

/**
 * @author BGH29309
 *
 */
public class SqlConnect {
	
	public Connection getConnection() throws SQLException {
		
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
			System.out.println("Exception: Failed to create database connection ... ");	
			e.printStackTrace();
		}
		
		
		return sqlDBconn;
		
	}

}
