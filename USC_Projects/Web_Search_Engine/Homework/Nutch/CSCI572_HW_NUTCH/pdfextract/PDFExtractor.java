
/*Code Reference
 * Java code to read the sequence files and store the pdf files extracted 
 * as a result of the crawl to the tar gzip file.
 * 
 * This code file is a part of Nutch Assignment of CSCI 572 for Spring 2014 at USC
 * Team Members - Vaibhav Mathur, Abhishek Agrawal, Hariharan Venugopal
 * Date: 4/15/2014
 * 
 * Part of the reused code reference:
 * Wiki Nutch
 * CSCI 572 blog for PDF file extraction.
 * File Compression techniques using TarArchive library
 */

//Java Imports
import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileFilter;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.net.URI;
import java.util.ArrayList;
import java.util.Collection;
import java.util.zip.GZIPOutputStream;
 
//Apache Commons imports
import org.apache.commons.compress.archivers.ArchiveOutputStream;
import org.apache.commons.compress.archivers.tar.TarArchiveEntry;
import org.apache.commons.compress.archivers.tar.TarArchiveOutputStream;
import org.apache.commons.compress.utils.IOUtils;

//Hadoop imports
import org.apache.hadoop.conf.Configuration;
import org.apache.hadoop.fs.FileSystem;
import org.apache.hadoop.fs.Path;
import org.apache.hadoop.io.ArrayFile;
import org.apache.hadoop.io.DataOutputBuffer;
import org.apache.hadoop.io.IntWritable;
import org.apache.hadoop.io.SequenceFile;
import org.apache.hadoop.io.SequenceFile.ValueBytes;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.io.UTF8;
import org.apache.hadoop.io.VersionMismatchException;
import org.apache.hadoop.io.Writable;
 
//Nutch imports
import org.apache.nutch.metadata.Metadata;
import org.apache.nutch.protocol.Content;
import org.apache.nutch.util.MimeUtil;
import org.apache.nutch.util.NutchConfiguration;
 
public final class PDFExtractor {
	
	
	public static void main(String argv[]) throws Exception {
	    //int count = 0;
		//Retrieving the input path and output tar file name
		String crawlDir = argv[0];
		String outputDir = argv[1];
		 File craldir = new File(crawlDir);
		 //Selecting the subdirectory of the merged segments folder.
		File[] subDirs = craldir.listFiles(new FileFilter() {  
		    public boolean accept(File pathname) {  
		        return pathname.isDirectory();  
		    }  
		});
		
		
		File outputFile = new File(outputDir);
		if(!outputFile.exists()) {
			outputFile.mkdir();
		}
		
	    Configuration conf = new Configuration();
	    FileSystem fs = FileSystem.get(URI.create(subDirs[0].getPath()), conf);
	    Path path = new Path(subDirs[0].getPath(), Content.DIR_NAME + "/part-00000/data");

	    SequenceFile.Reader reader = null;
	    try {
	      reader = new SequenceFile.Reader(fs, path, conf);
	      Text key = new Text();
	      Content content = new Content();
	      
	      //For all the contents, browse through and map the required files.
	      while (reader.next(key, content)) {
	          String contentType = content.getContentType();
	          //Comparing the content Type to be similar to pdf
	          if (contentType.equalsIgnoreCase("application/pdf")) {
	        	System.out.println(key);
	        	String[] fileURL = key.toString().split("/");
	        	int len = fileURL.length;
	        
	        	String fileName = fileURL[len-1];
	        	
	        	if(fileName.equals("index.html")) {
	        		fileName = fileURL[len-2];
				}else if(fileName.equals("file")) {
					fileName = fileURL[len-3];
				}
	        	//System.out.write( content.getContent(), 0, content.getContent().length );
	        	//System.out.println("Length       :"+ content.getContent().length );
	        	OutputStream out = new FileOutputStream(outputDir+'/'+fileName +".pdf");
	        	out.write(content.getContent());
	        	out.close();
	        	
	        	
	          }
	      }
	      reader.close();
	      
	      //Files Extracted - Now compressing the folder
	      System.out.println("Files Extracted. Now compressing...");
	      File dir = new File(outputDir);
	      Collection<File> lstFiles =new ArrayList<File>();

	      if(dir.isDirectory()){
	          File[] listFiles = dir.listFiles();

	          for(File file : listFiles){
	              if(file.isFile()) {
	            	  lstFiles.add(file);
	              }
	          }
	      }

	      //Method to compress the files
	      compressFiles(lstFiles,new File(outputDir));
	      System.out.println("Files Compressed to "+outputDir+".tar.gz");
	      System.out.println("-----------------------------------------");
	    } 
	        finally {
	        fs.close();
	    }
  }
	
	/*compressFile function compresses a list of files and use GZIP compression to compress to tar gz format
	 * Method takes the list of files and the output name of tar gz file
	 * files: List of files to be compressed
	 * file: name of the output tar gz file 
	 * returns void
	 */
	private static void compressFiles(Collection<File> lstFiles, File file) throws IOException {
		 // Create the output stream for the output file
		  FileOutputStream fos ;
		
		    fos = new FileOutputStream( new File(file.getCanonicalPath()+".tar"+".gz"));
		    // Wrap the output file stream in streams that will tar and gzip everything
		    
		    TarArchiveOutputStream objTAOS = new TarArchiveOutputStream(new GZIPOutputStream(new BufferedOutputStream(fos)));
		    
		    // TAR has an 8 gig file limit by default, this gets around that
		    //taos.setBigNumberMode(TarArchiveOutputStream.BIGNUMBER_STAR); // to get past the 8 gig limit
		    
		    // TAR originally didn't support long file names, so enable the support for it
		    objTAOS.setLongFileMode(TarArchiveOutputStream.LONGFILE_GNU);
		    
		    // Get to putting all the files in the compressed output file
		    for (File f : lstFiles) {
		      addFilesToCompression(objTAOS, f, ".");
		    }

		    // Close everything up
		    objTAOS.close();
		   fos.close();
	
		  }
		 

		
		/* AddFilesToCompression
		 * Method adds all the files to be archived to the output stream 
		 * taos: ArchiveOutputStream where the files after compression will be added to.
		 * file: file content to be added to the archive 
		 * dir: Directory name where the files are stored. "." when same directory.
		 * 
		 */
	
		private static void addFilesToCompression( ArchiveOutputStream objAOS, File file, String dir) throws IOException {

		 // Create an entry for the file
			objAOS.putArchiveEntry(new TarArchiveEntry(file, dir+"/"+file.getName()));
			if (file.isFile()) {
				  // Add the file to the archive
				  BufferedInputStream bis = new BufferedInputStream(new FileInputStream(file));
				  IOUtils.copy(bis, objAOS);
				  objAOS.closeArchiveEntry();
				  bis.close();
		 } else if (file.isDirectory()) {
		  // close the archive entry
			 objAOS.closeArchiveEntry();
		  // go through all the files in the directory and using recursion, add them to the archive
		  for (File chldFile : file.listFiles()) {
		   addFilesToCompression(objAOS, chldFile, file.getName());
		  }
		 }
		}
}