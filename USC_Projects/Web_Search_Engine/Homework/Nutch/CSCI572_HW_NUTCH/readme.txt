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
NUTCH CONFIGURATION PARAMETERS
----------------------------------------------------------------------------------------
Nutch Configuration files is nutch_site.xml
Before execution, copy the nutch_site.xml to the nutch/conf directory.
Copy the folder "urls" which contains "seeds.txt" given in the nutchconfig folder into the Apache_Nutch home folder. 

----------------------------------------------------------------------------------------
PROGRAM EXECUTION STEPS:
----------------------------------------------------------------------------------------
Navigate to the apache Nutch home directory.
Execute the following command.

Step 1. Set JAVA_HOME environmant variable using the below command(tested on Ubuntu)
	For ubuntu: export JAVA_HOME=$(readlink -f /usr/bin/java | sed "s:bin/java::")

Step 2. To initiate crawling execute the below command. This will create a "crawl" folder containing all the crawled data  
	bin/nutch crawl urls/ -dir crawl -depth 10
 
Step 3. To merge segments below executing PDFExtractor file."crawl/merged" folder containing one crawled segment obtained after merging all the segments.
	bin/nutch mergesegs <MERGED OUTPUT DIRECTORY> crawl/segments/*
	Sample: bin/nutch mergesegs crawl/merged/ crawl/segments/*

----------------------------------------------------------------------------------------
SOURCE FILES:
----------------------------------------------------------------------------------------

-PDFExtractor.java

Description: This java progrom extracts all the PDFs from Sequence File compressed format.

Input arguments: 
-Source directory of merged segment <MERGED OUTPUT DIRECTORY> as specified in the mergesegs command above.
-Destination path and TAR file name. 

PDFExtractor.java is avaialble in the directory "pdfextract"

On Command prompt, Navigate to the pdfextract folder which contains following JAR files:

apache-nutch-1.6.jar
hadoop-core-1.0.3.jar
common-lang3.jar
commons.logging-1.1.1.jar
commons-configuration-1.7.jar
commons-logging-api.jar
apache-commons-lang.jar
commons-compress-1.0.jar
ojdbc6.jar
sdoapi.jar
sdoutl.jar

COMMANDS:

To Compile: 

javac -cp apache-nutch-1.6.jar:hadoop-core-1.0.3.jar:common-lang3.jar:commons.logging-1.1.1.jar:commons-configuration-1.7.jar:commons-logging-api.jar:apache-commons-lang.jar:commons-compress-1.0.jar:ojdbc6.jar:sdoapi.jar:sdoutl.jar PDFExtractor.java

OR

javac -cp :* PDFExtractor.java

To Execute:

java -cp .:apache-nutch-1.6.jar:hadoop-core-1.0.3.jar:common-lang3.jar:commons.logging-1.1.1.jar:commons-configuration-1.7.jar:commons-loggingpi.jar:apache-commons-lang.jar:commons-compress-1.0.jar:ojdbc6.jar:sdoapi.jar:sdoutl.jar  PDFExtractor <Input-Directory> <Output-Directory>

OR 

java -cp .:* PDFExtractor <Input-Directory> <Output-Directory>

Sample: java -cp .:apache-nutch-1.6.jar:hadoop-core-1.0.3.jar:common-lang3.jar:commons.logging-1.1.1.jar:commons-configuration-1.7.jar:commons-logging-api.jar:apache-commons-lang.jar:commons-compress-1.0.jar:ojdbc6.jar:sdoapi.jar:sdoutl.jar  PDFExtractor /home/ayush/Downloads/apache-nutch-1.6/merged PDFs

The PDFs folder will contain all PDF extracted. To extract pdf from tar use the command
tar -xvf pdf.tar.gz


