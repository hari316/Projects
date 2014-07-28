/**
 * 
 */
package com.data;

/**
 * @author Spartans
 *
 */
public class EmpData {

	/**
	 * 
	 */
	
	private int empID;
	private String empName;
	private int empAge;
	private String designation;
	
	public EmpData() {
		// TODO Auto-generated constructor stub
	}
	
	public EmpData(int ID,String Name, int Age ,String Degn) {
		this.empID = ID;
		this.empName = Name;
		this.empAge = Age;
		this.designation = Degn;
	}

	public int getEmpID() {
		return empID;
	}

	public void setEmpID(int empID) {
		this.empID = empID;
	}

	public String getEmpName() {
		return empName;
	}

	public void setEmpName(String empName) {
		this.empName = empName;
	}

	public int getEmpAge() {
		return empAge;
	}

	public void setEmpAge(int empAge) {
		this.empAge = empAge;
	}

	public String getDesignation() {
		return designation;
	}

	public void setDesignation(String designation) {
		this.designation = designation;
	}
	
	

}
