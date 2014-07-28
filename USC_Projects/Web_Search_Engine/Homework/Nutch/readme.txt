----------------------------------------------------------------------------------------
TEAM MEMBERS:
----------------------------------------------------------------------------------------

1)


2)


3)	Name: Hari Haran Venugopal
	USC-ID: 2041214007



----------------------------------------------------------------------------------------
PROGRAM EXECUTION STEPS:
----------------------------------------------------------------------------------------

bin/nutch crawl urls/ -dir crawl -depth 10

// "crawl" folder containing all the crawled data  

bin/nutch mergesegs crawl/merged/ crawl/segments/*

//"crawl/merged" folder containing one crawled segment obtained after merging all the segments.

----------------------------------------------------------------------------------------
SOURCE FILES:
----------------------------------------------------------------------------------------

-PDFExtractor.java

Description: This java progrom extracts all the PDFs from Sequence File compressed format.
Put these in a directory pdfextract

Command:

javac -classpath apache-nutch-1.6:hadoop-core-1.0.3:common-lang3:commons.logging-1.1.1:commons-configuration-1.7:commons-logging-api:apache-commons-lang: *.java

java -classpath apache-nutch-1.6:hadoop-core-1.0.3:common-lang3:commons.logging-1.1.1:commons-configuration-1.7;

commons-logging-api;apache-commons-lang; PDFExtractor <Input-Directory> <Output-Directory>

