/**
 * 
 */
package com.XMLParse;

import java.io.File;
import java.io.IOException;
import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.xml.parsers.ParserConfigurationException;
import javax.xml.parsers.SAXParser;
import javax.xml.parsers.SAXParserFactory;

import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.XMLReader;
import org.xml.sax.helpers.DefaultHandler;
import org.xml.sax.helpers.XMLReaderFactory;

import com.data.EmpData;
import com.dbConnection.EmployeeDAL;

/**
 * @author BGH29309
 *
 */
public class SaxParser extends DefaultHandler  {

	/**
	 * 
	 */
	
	public SaxParser() {
		// TODO Auto-generated constructor stub
	}

	
	private String _tempVal;
	private EmpData _employee;
	private List<EmpData> _result;
    private EmployeeDAL empDAO = new EmployeeDAL();
	
	private Map <Integer,EmpData> _employees = new HashMap<Integer, EmpData>();
	
	public List<EmpData> empSearch(int empID) {
		
		System.out.println("Employee List: "+_employees.values());
		
		_result = empDAO.retrieve(empID);

		return _result;		
		
	}
	
	public void loadXMLDataToDB() {
		
		String filePath ="D:/Java Coding/EmployeeSearch_4.0/EmployeeDetails.xml";
		
		File xmlFile = new File(filePath);
			
		if(xmlFile.exists())
		{
			try {		
				
					XMLReader reader = XMLReaderFactory.createXMLReader();
					reader.parse(filePath);
					System.out.println("Input XML file parsed via SAX Parser");
					
					SAXParserFactory spF = SAXParserFactory.newInstance();
					SAXParser sp = spF.newSAXParser();

					sp.parse(xmlFile, this);
					
					System.out.println("Employee List: "+_employees.values());				
				
			} catch (SAXException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (ParserConfigurationException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
			
		else {
		
				System.out.println("File not found !!!");		
		}

	}
	
	@Override
	public void characters(char[] ch, int start, int length) throws SAXException {
			// TODO Auto-generated method stub
			//super.characters(ch, start, length);
			_tempVal = new String(ch,start,length);
	}
	
	@Override
	public void startElement(String uri, String localName, String qName,
				Attributes attributes) throws SAXException {
			// TODO Auto-generated method stub
			//super.startElement(uri, localName, qName, attributes);
		_tempVal = "";
		if(qName.equalsIgnoreCase("Employee")) {
			_employee = new EmpData();
		}
	}
	
	@Override
	public void endElement(String uri, String localName, String qName)
			throws SAXException {
		// TODO Auto-generated method stub
		//super.endElement(uri, localName, qName);
		if(qName.equalsIgnoreCase("Employee")) {
			
			// Add to Hash-Map
			_employees.put(_employee.getEmpID(), _employee);
			empDAO.add(_employee);

			
		} else if(qName.equalsIgnoreCase("Name")) {
			_employee.setEmpName(_tempVal);
		} else if(qName.equalsIgnoreCase("Id")) {
			_employee.setEmpID(Integer.parseInt(_tempVal));
		} else if(qName.equalsIgnoreCase("Age")) {
			_employee.setEmpAge(Integer.parseInt(_tempVal));
		} else if(qName.equalsIgnoreCase("Designation")) {
			_employee.setDesignation(_tempVal);
		}
	}

	
	class Compare implements Comparator<EmpData>{
		
        @Override 
        public int compare(EmpData E1, EmpData E2) {
            return (E2.getEmpID() - E1.getEmpID());
        }
	}
	
}
