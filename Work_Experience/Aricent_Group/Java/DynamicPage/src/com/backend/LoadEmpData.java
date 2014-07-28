package com.backend;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import com.XMLParse.SaxParser;
import com.data.EmpData;

/**
 * Servlet implementation class LoadEmpData
 */
public class LoadEmpData extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public LoadEmpData() {
        super();
        // TODO Auto-generated constructor stub
    }

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
	
		System.out.println("Servlet called ...");
		EmpData result;
		List <EmpData> employees = new ArrayList<EmpData>();

		SaxParser  sp = new SaxParser();
		 
		employees = sp.getXMLData();
		 
		System.out.println("XML Content parsed successfully.");
		 
		JSONObject obj,responseJson = new JSONObject();
		JSONArray jsonArray = null;
		 //System.out.println(employees);
		
		int listLength = employees.size();
		System.out.println("No. of Employees in result set: "+listLength);
		 
		if(listLength == 1) {
			result = employees.get(0);
			obj = new JSONObject();
			if(result == null){
				System.out.println("No Records Found !!!");
				obj.put("Flag","0");
			}else {	 		 
				obj.put("Flag","1");
			 	obj.put("Id",result.getEmpID());
				obj.put("Name",result.getEmpName());
				obj.put("Age", result.getEmpAge());
				obj.put("Designation",result.getDesignation());	 
			}
			responseJson.put("Success",obj);
		}
		else {
			  
			jsonArray = new JSONArray();
			int count = 0;
			for(int i=0;i<listLength;i++) {
				obj = new JSONObject();
				obj.put("Id",employees.get(i).getEmpID());
				obj.put("Name",employees.get(i).getEmpName());
				obj.put("Age",employees.get(i).getEmpAge());
				obj.put("Designation",employees.get(i).getDesignation());		 
				jsonArray.add(obj); 
				count++;
			}
			obj = new JSONObject();
			obj.put("Flag","2");
			obj.put("Count",count);
			responseJson.put("All",jsonArray);
			responseJson.put("Success",obj);
		}
		 
		 /*
		 if(result == null){
			System.out.println("No Records Found !!!");
			 obj.put("Flag","0");
		 }else {
		     obj.put("Flag","1");
		 	 obj.put("Id",result.getEmpID());
			 obj.put("Name",result.getEmpName());
			 obj.put("Age", result.getEmpAge());
			 obj.put("Designation",result.getDesignation()); 
		 }*/
		 
		 System.out.println("JSON Response: "+responseJson);	
		 response.setContentType("text");
		 PrintWriter out = response.getWriter();
		 out.print((responseJson));
	
		 //out.close();

	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
	}

}
