package com.dbConnection;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import com.data.EmpData;

public class EmployeeDAL {
	
	private static Connection myConn = null;
	private Statement stmt = null;
	private List<EmpData> _result;

	public EmployeeDAL() {
		
		try {
			myConn = (new SqlConnect()).getConnection();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}
	
	public void add(EmpData emp) {

		try {
			
			System.out.println("Employee ID: "+emp.getEmpID());
			System.out.println("Employee Name: "+emp.getEmpName());
			System.out.println("Employee Age: "+emp.getEmpAge());
			System.out.println("Employee Designation: "+emp.getDesignation());
					
			stmt = myConn.createStatement();
			
			String insertQuery = "INSERT INTO EmployeeDetails VALUES("+
								 "'"+emp.getEmpID()+"',"+
								 "'"+emp.getEmpName()+"',"+
								 	 emp.getEmpAge()+","+
								 "'"+emp.getDesignation()+"')";
			
			System.out.println("INSERT Query: "+insertQuery);
			
			stmt.executeUpdate(insertQuery);
			
			System.out.println("Data insersted successfully");
			
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}
	
	public List<EmpData>retrieve(int empID) {

		try {
			
			_result = new ArrayList<EmpData>();	
			stmt = myConn.createStatement();			
			String selectQuery;
			EmpData _employee = null;
			
			if(empID == 0) {
				selectQuery = "SELECT * FROM EmployeeDetails";
			}
			else {
				selectQuery = "SELECT * FROM EmployeeDetails WHERE empID='"+empID+"'";
			}
			
			ResultSet rs = stmt.executeQuery(selectQuery);
			System.out.println("No. of records found: "+rs.getFetchSize());
			
			while(rs.next()) {
				_employee = new EmpData();
				_employee.setEmpID(Integer.parseInt(rs.getString("EmpID")));
				_employee.setEmpName(rs.getString("EmpName"));
				_employee.setEmpAge(Integer.parseInt(rs.getString("Age")));
				_employee.setDesignation(rs.getString("Designation"));
				_result.add(_employee);
			}
			
			System.out.println("Search was successfully");
				
			
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return _result;
		
	}

}
