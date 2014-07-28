----------------------------------------------------------------------------------------
TEAM MEMBERS:
----------------------------------------------------------------------------------------

1)  Name: Abhishek Agrawal
    USC ID: 5831-2307-00

2)  Name: Vaibhav Mathur
    USC ID: 1807-6607-45

3)  Name: Hari Haran Venugopal
    USC-ID: 2041-2140-07

----------------------------------------------------------------------------------------
PROGRAM EXECUTION STEPS:
----------------------------------------------------------------------------------------
Navigate to the code sample home directory.
Execute the following command.


Step 1. Make sure you have the following data files in the directory:
	
	copy file conf/solr.war into tomcat/webapps and conf/solr.xml into tomcat/conf/catalina/localhosts.

	solrj directory wil contain below files-
	keywords.txt
	US.txt - (Geonames.org dataset)
	TikaHW.java - (Source code for parsing pdf and creating term frequency and sending data to solrss)

	Vault will be required to be saved in tomcat/webapps/solr folder
	./vault - Directory containing all the vault files

	Front end web page created using bootstrap framework will be saved in webapps/solr folder
	webapps/solr/Conspiracy.html

Step 2: Start the tomcat server using file startup.bat present in C:/tomcat/bin.(The url for solr would be http://localhost:8080/solr)
	When Tomcat server starts, it deploys solr using sold.war file and a new folder webapps/solr in the tomcat will be created.

Step 3: Copy the following jar files into the source folder:
	common-lan3.jar
	commons-codec-1.6.jar
	All Jars from solr-4.8.0/dist directory
	tika-app-1.5.jar

Step 4: Compile the code using the following command:
	javac -cp .;* TikaHW.java

	Run the command using following command:
	java -cp .;* -Xmx819200k TikaHW

Step 5: The code will execute and index all the files on solr
 
Step 6: Execute the Conspiracy.html page.

----------------------------------------------------------------------------------------
SOURCE FILES:
----------------------------------------------------------------------------------------

-TikaHW.java

Description: This java progrom extracts all the texts from the PDF files and index the docs with keyword contents, latitude and longtude on the Solr. This uses SolrJ framework and libraries
TikaHW.java is avaialble in the directory "solrj"


-webapps directory
This directory contains the Solr configurations.
This also contains the WAR file which generate the Solr directory.

-Conspiracy.html
This webpage is the user interface for the queries.
This page is available in webapps/solr directory.