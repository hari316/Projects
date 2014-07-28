README
----------------------------------------------------------------------------------------
Name: 		Hari Haran Venugopal
Student ID: 	2041214007
Email Id:	hvenugop@usc.edu
----------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------
List of the Submitted Files.:
----------------------------------------------------------------------------------------
Java Codes:
	- XmlToDBInsert
	- SaxParser
	- ConnecToDB
XML Schema:
	- review.xsd
	- book.xsd
XML Files:
	- review.xml
	- book.xml
Queries:
	- query1.xquery
	- query2.xquery
	- query3.xquery
	- query4.xquery
	- query5.xquery
	- query6.xquery
	- dropTable.sql
	- createTable.sql	
----------------------------------------------------------------------------------------
For Compilation:
----------------------------------------------------------------------------------------

Eclipise IDE and include external jar files: ojdbc6.jar, sdoapi.jar and sdoutl.jar

OR

javac -classpath ojdbc6.jar;sdoapi.jar;sdoutl.jar; *.java
Example:
java -classpath ojdbc6.jar;sdoapi.jar;sdoutl.jar; XmlToDBInsert book.xml review.xml