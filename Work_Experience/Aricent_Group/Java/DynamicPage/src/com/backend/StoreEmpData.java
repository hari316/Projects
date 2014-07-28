package com.backend;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.List;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.json.simple.JSONObject;

import com.data.EmpData;
import com.dbConnection.EmployeeDAL;

/**
 * Servlet implementation class StoreEmpData
 */
public class StoreEmpData extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public StoreEmpData() {
        super();
        // TODO Auto-generated constructor stub
    }

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		
		System.out.println("Servlet called ...");
		String[] _empID = request.getParameterValues("EmpID");
		
		System.out.println(_empID[0]);
		System.out.println(_empID.length);
		
		JSONObject obj,responseJson = new JSONObject();
		
		obj = new JSONObject();
		obj.put("Flag","0");		 	 
		responseJson.put("Success",obj);
		System.out.println("JSON Response: "+responseJson);	
		response.setContentType("text");
		PrintWriter out = response.getWriter();
		out.print((responseJson));
		 
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		System.out.println("Servlet called ...");
		String[] _empID = request.getParameterValues("EmpID");
		String[] _empName = request.getParameterValues("EmpName");
		String[] _empAge = request.getParameterValues("EmpAge");
		String[] _empDesg = request.getParameterValues("EmpDesg");
		int count = _empID.length;
		
		System.out.println("No. of employee details to be stored in the database: "+count);
 
		EmpData _employee;
	    EmployeeDAL empDAO = new EmployeeDAL();
	    
	    for(int i=0;i<count;i++) {
	    	_employee = new EmpData();
	    	_employee.setEmpID(Integer.parseInt(_empID[i]));
	    	_employee.setEmpName(_empName[i]);
	    	_employee.setEmpAge(Integer.parseInt(_empAge[i]));
	    	_employee.setDesignation(_empDesg[i]);
	    	empDAO.add(_employee);
	    }
		response.sendRedirect("index.jsp");
	}

}
