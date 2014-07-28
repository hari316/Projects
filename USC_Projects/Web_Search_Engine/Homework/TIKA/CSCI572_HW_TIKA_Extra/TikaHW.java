import java.io.*;
import java.net.ContentHandler;
import java.util.*;
import java.util.regex.*;
import org.apache.commons.lang3.StringUtils;
import org.apache.tika.parser.ParseContext;
import org.apache.tika.parser.Parser;
import org.apache.tika.parser.pdf.PDFParser;
import org.apache.tika.exception.TikaException;
import org.apache.tika.metadata.Metadata;
import org.apache.tika.sax.BodyContentHandler;
import org.xml.sax.SAXException;


public class TikaHW {

	List<String> keywords;
	PrintWriter logfile;
	int num_keywords, num_files, num_fileswithkeywords;
	Map<String,Integer> keyword_counts;
	Date timestamp;

	/**
	 * constructor
	 * DO NOT MODIFY
	 */
	public TikaHW() {
		keywords = new ArrayList<String>();
		num_keywords=0;
		num_files=0;
		num_fileswithkeywords=0;
		keyword_counts = new HashMap<String,Integer>();
		timestamp = new Date();
		try {
			logfile = new PrintWriter("log.txt");
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		}
	}

	/**
	 * destructor
	 * DO NOT MODIFY
	 */
	protected void finalize() throws Throwable {
		try {
			logfile.close();
	    } finally {
	        super.finalize();
	    }
	}

	/**
	 * main() function
	 * instantiate class and execute
	 * DO NOT MODIFY
	 */
	public static void main(String[] args) {
		TikaHW instance = new TikaHW();
		instance.run();
	}

	/**
	 * execute the program
	 * DO NOT MODIFY
	 */
	private void run() {

		// Open input file and read keywords
		try {
			BufferedReader keyword_reader = new BufferedReader(new FileReader("keywords.txt"));
			String str;
			while ((str = keyword_reader.readLine()) != null) {
				keywords.add(str);
				num_keywords++;
				keyword_counts.put(str, 0);
			}
			keyword_reader.close();
		} catch (IOException e) {
			e.printStackTrace();
		}

		// Open all pdf files, process each one
		File pdfdir = new File("./vault");
		File[] pdfs = pdfdir.listFiles(new PDFFilenameFilter());
		for (File pdf:pdfs) {
			num_files++;
			processfile(pdf);
		}

		// Print output file
		try {
			PrintWriter outfile = new PrintWriter("output.txt");
			outfile.print("Keyword(s) used: ");
			if (num_keywords>0) outfile.print(keywords.get(0));
			for (int i=1; i<num_keywords; i++) outfile.print(", "+keywords.get(i));
			outfile.println();
			outfile.println("No of files processed: " + num_files);
			outfile.println("No of files containing keyword(s): " + num_fileswithkeywords);
			outfile.println();
			outfile.println("No of occurrences of each keyword:");
			outfile.println("----------------------------------");
			for (int i=0; i<num_keywords; i++) {
				String keyword = keywords.get(i);
				outfile.println("\t"+keyword+": "+keyword_counts.get(keyword));
			}
			outfile.close();
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		}
	}

	/**
	 * Process a single file
	 * 
	 * Here, you need to:
	 *  - use Tika to extract text contents from the file
	 *  - (optional) check OCR quality before proceeding
	 *  - search the extracted text for the given keywords
	 *  - update num_fileswithkeywords and keyword_counts as needed
	 *  - update log file as needed
	 * 
	 * @param f File to be processed
	 */
	private void processfile(File f) {

		/***** YOUR CODE GOES HERE *****/
		Boolean foundFlag = false;
		// to update the log file with a search hit, use:
		// 	updatelog(keyword,f.getName());
		try {
			
			InputStream is = new FileInputStream(f);
			Metadata metadata = new Metadata();
			BodyContentHandler ch = new BodyContentHandler(-1);
			Parser parser = new PDFParser();
			
			parser.parse(is, ch, metadata, new ParseContext());
			is.close();
			
			String fileContent = ch.toString();
			fileContent = fileContent.toLowerCase();
			//System.out.println(fileContent);
			for(String keyword:keywords) { 
				
				String key = keyword.toLowerCase();
				
				if(5 > key.length()) {	
					String[] allwords = fileContent.split("[\\W]");
					for(String word: allwords) {
						if(word.equals(key)) {
							foundFlag = true;
							int count = keyword_counts.get(keyword); 
							count++; 
							keyword_counts.put(keyword, count); 
							updatelog(keyword,f.getName());
						}
					}
				}else {
					Pattern pattern = Pattern.compile(key);
					Matcher matcher = pattern.matcher(fileContent);
					while(matcher.find()) {  
						foundFlag = true;
						int count = keyword_counts.get(keyword); 
						count++; 
						keyword_counts.put(keyword, count); 
						updatelog(keyword,f.getName());
					} 
				}
				
				if(!foundFlag & (7 < key.length())) {
					String[] allwords = fileContent.split("[\\W]");
					int threshold = 4;
					int dist = 0;
					
					for(String word: allwords) {
						 dist = StringUtils.getLevenshteinDistance(word, key);
						 if(threshold > dist) {
							 foundFlag = true;
								int count = keyword_counts.get(keyword); 
								count++; 
								keyword_counts.put(keyword, count); 
								updatelog(keyword,f.getName());
						 }	
					}
				}
			} 
		    
			if(foundFlag) {
				num_fileswithkeywords++;
			}
			
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (SAXException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (TikaException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

	/**
	 * Update the log file with search hit
	 * Appends a log entry with the system timestamp, keyword found, and filename of PDF file containing the keyword
	 * DO NOT MODIFY
	 */
	private void updatelog(String keyword, String filename) {
		timestamp.setTime(System.currentTimeMillis());
		logfile.println(timestamp + " -- \"" + keyword + "\" found in file \"" + filename +"\"");
		logfile.flush();
	}

	/**
	 * Filename filter that accepts only *.pdf
	 * DO NOT MODIFY 
	 */
	static class PDFFilenameFilter implements FilenameFilter {
		private Pattern p = Pattern.compile(".*\\.pdf",Pattern.CASE_INSENSITIVE);
		public boolean accept(File dir, String name) {
			Matcher m = p.matcher(name);
			return m.matches();
		}
	}
}
