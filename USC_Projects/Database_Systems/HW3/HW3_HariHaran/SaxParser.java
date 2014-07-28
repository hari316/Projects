
import java.io.File;
import java.io.IOException;
import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Statement;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.parsers.SAXParser;
import javax.xml.parsers.SAXParserFactory;

import oracle.jdbc.driver.OracleSQLException;

import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.XMLReader;
import org.xml.sax.helpers.DefaultHandler;
import org.xml.sax.helpers.XMLReaderFactory;


public class SaxParser extends DefaultHandler  {
	
	private String tempVal;
	String queryFormat;
	Connection myConn;
	Statement stmt; 	
	StringBuilder rowStr;
	
	public void loadXMLDataToDB(String filePath, int flag) {
		
		File xmlFile = new File(filePath);
			
		if(xmlFile.exists())
		{
			try {		
				
					XMLReader reader = XMLReaderFactory.createXMLReader();
					reader.parse(filePath);
					System.out.println("Input XML file parsed via SAX Parser: "+filePath);
					
					if(0 == flag) {
						queryFormat = "INSERT INTO BOOKS VALUES (xmltype('%s'))";
					}else {
						queryFormat = "INSERT INTO REVIEWS VALUES (xmltype('%s'))";
					}
					
					try {
						myConn = ConnectOracleDB.getConnection();
						stmt = myConn.createStatement();
					} catch (OracleSQLException e) {
						e.printStackTrace();
					} catch (SQLException e) {
						e.printStackTrace();
					}
					
					SAXParserFactory spF = SAXParserFactory.newInstance();
					SAXParser sp = spF.newSAXParser();

					sp.parse(xmlFile, this);
					
					System.out.println("Records successfully inserted ...\n");				
				
			} catch (SAXException e) {
				e.printStackTrace();
			} catch (IOException e) {
				e.printStackTrace();
			} catch (ParserConfigurationException e) {
				e.printStackTrace();
			}
		}
			
		else {
		
				System.out.println("File not found !!!");		
		}

	}
	
	@Override
	public void characters(char[] ch, int start, int length) throws SAXException {
			//super.characters(ch, start, length);
			tempVal = new String(ch,start,length);
	}
	
	@Override
	public void startElement(String uri, String localName, String qName,
				Attributes attributes) throws SAXException {
			//super.startElement(uri, localName, qName, attributes);
		tempVal = "";
		if(qName.equalsIgnoreCase("book") || qName.equalsIgnoreCase("review")) {
			rowStr = new StringBuilder();
			rowStr.append("<"+qName+">");
		}
	}
	
	@Override
	public void endElement(String uri, String localName, String qName)
			throws SAXException {
		
		if(qName.equalsIgnoreCase("book") || qName.equalsIgnoreCase("review")) {			
			rowStr.append("</"+qName+">");
			String query = String.format(queryFormat, rowStr.toString().replaceAll("'", "''"));
			try {
				//System.out.println(query);
			    stmt.executeUpdate(query);
			} catch (SQLException e) {
				e.printStackTrace();
			}
		}
		rowStr.append("<"+qName+">"+tempVal+"</"+qName+">");
	}

	
}
