import java.io.*;
import java.util.*;
import java.util.Map.Entry;
import java.util.regex.*;
import java.lang.String;

import javax.swing.plaf.basic.BasicInternalFrameTitlePane.SystemMenuBar;

import org.apache.tika.exception.TikaException;
import org.apache.tika.metadata.Metadata;
import org.apache.tika.parser.ParseContext;
import org.apache.tika.parser.Parser;
import org.apache.tika.parser.pdf.PDFParser;
import org.apache.tika.sax.BodyContentHandler;
import org.xml.sax.ContentHandler;
import org.apache.commons.lang3.StringUtils;

import org.apache.solr.client.solrj.SolrServer;
import org.apache.solr.client.solrj.impl.HttpSolrServer;
import org.apache.solr.common.SolrInputDocument;

public class TikaHW {

	List<String> keywords;
	PrintWriter logfile,processFile;
	int num_keywords, num_files, num_fileswithkeywords;
	Map<String,Integer> keyword_counts = new HashMap<String,Integer>();
	Map<String,Integer> wordCount;
	Map<String,String> kwPdf = new HashMap<String,String>();
//	TikaHW[] tk = new TikaHW[20];
	Map<String,String> placemap2;
	Map<String,String> pdfLocation = new HashMap<String,String>();
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
		wordCount = new HashMap<String,Integer>();
		keyword_counts = new HashMap<String,Integer>();
		placemap2 = new HashMap<String,String>();
		
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
			
			BufferedReader keyword_reader = new BufferedReader(new FileReader("US.txt"));
			String str="";
			int cnt=0;
			//placemap[0] = new HashMap<String,String>();
			while ((str = keyword_reader.readLine()) != null) {
				String[] arr = str.split("\t");
				cnt++;
				//if(arr[6].equals("A") || arr[6].equals("P"))
				{
					System.out.println(cnt + " "+arr[1].toLowerCase() + "  "+arr[4]+", "+arr[5]);
					
					placemap2.put(arr[1].toLowerCase(), arr[4]+","+arr[5]);
				}
					
			
			}
			keyword_reader.close();
			
			keyword_reader = new BufferedReader(new FileReader("keywords.txt"));
			str="";
			
			while((str = keyword_reader.readLine()) != null)
			{
				System.out.println("KEYWORD: "+str.toLowerCase());
				keywords.add(str.toLowerCase());
				keyword_counts.put(str.toLowerCase(), 0);
				num_keywords++;
			}
			
//			for(String k:placemap2.keySet())
//			{
//				System.out.println(k+":"+placemap2.get(k));
//			}

			
			
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		// Open all pdf files, process each one
		File pdfdir = new File("../vault");
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

		
		
		
		try
		{
		    //create a temporary file
		    //String timeLog = new SimpleDateFormat("yyyyMMdd_HHmmss").format(Calendar.getInstance().getTime());
		    File logFile=new File("FileCoOrdinates.txt");
		    String url = "http://localhost:8080/solr";
		    SolrServer solrCore = new HttpSolrServer(url);
		    
		    BufferedWriter writer = new BufferedWriter(new FileWriter(logFile));
		    int cnt = 0;
			for (File pdf:pdfs) {
				cnt++;
			    SolrInputDocument doc1 = new SolrInputDocument();
			    doc1.addField( "id", cnt, 1.0f );
			    doc1.addField( "file_name", pdf.getName(), 1.0f );
		
				if(pdfLocation.get(pdf.getName())==null)
				{
					doc1.addField( "file_content","", 1.0f);
				    doc1.addField( "file_lat_long","0,0", 1.0f); 
				    
					writer.write(pdf.getName()+"\tnull\tnull\n");
				}else
				{
					
					for(String s:kwPdf.get(pdf.getName()).split(","))
						doc1.addField( "file_content", s, 1.0f);
				    doc1.addField( "file_lat_long", pdfLocation.get(pdf.getName()).split(":")[1], 1.0f); 
				    
					writer.write(pdf.getName()+"\t"+pdfLocation.get(pdf.getName()).split(":")[0]+"\t"+pdfLocation.get(pdf.getName()).split(":")[1]+ "\t"+kwPdf.get(pdf.getName())+"\n");					
				}
				
				updatelog(pdf.getName() + "indexed!");
			    solrCore.add(doc1);
			    solrCore.commit();

				System.out.println(pdf.getName()+" "+pdfLocation.get(pdf.getName()));
			}


		    //Close writer
		    writer.close();
		} catch(Exception e)
		{
			System.out.println("Exceptiooon");
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
		// to update the log file with a search hit, use:
		
		try
		{
		    wordCount.clear();
		    String kwfound = "";
		    //Initialise Parsing Variables
			InputStream ipStream = new FileInputStream(f);
			Parser par = new PDFParser();
			ContentHandler cntHandler = new BodyContentHandler(Integer.MAX_VALUE);
			Metadata md = new Metadata();
			par.parse(ipStream, cntHandler, md, new ParseContext());
			String fileContents = cntHandler.toString().toLowerCase();
			
					
					//Replacing all the new line characters with white space and then splitting the whole string in array 
					//of words.
					String[] arr = fileContents.replace("\n"," ").split(" ");
					String stop = "of an the a from as for from not be it and or was were to his on";
					System.out.println(num_files + " CURRENT EXECUTING..."+f.getName());
					//System.out.println("KEYWORDS: "+keywords.size());
//					if(arr.length==0)
//						System.out.println("File Content... "+fileContents);
					
					//Preparation of the n-grams where n= kwCount i.e. number of words in the keyword
					for(int lp=0;lp<arr.length-2;lp++)
					{
						String stt0 ="",stt1="";
						
						//The method returns the string with n-strings conacatenated
						//stt = concatValues(arr,lp,lp+kwCount);
						stt0 = arr[lp];
						stt1 = arr[lp+1];
						
						stt0 = stt0.replaceAll("\\W", ""); 
//						stt0 = stt0.replaceAll("\\d", ""); 

						stt1 = stt1.replaceAll("\\W", ""); 
	//					stt1 = stt1.replaceAll("\\d", ""); 

						
						if(stop.contains(stt0) || stt0.length()<3)
						{
							continue;
						}
						
						
						if(wordCount.containsKey(stt0))
							{
								int freqCount = wordCount.get(stt0) + 1;
								wordCount.put(stt0, freqCount);
							
							}
						else
						{
							wordCount.put(stt0,1);
						}
					
						String stt = stt0+" "+stt1;
						System.out.println("Keyword 0 is "+keywords.get(0));
						if(keywords.contains(stt0))
								{

									int r=keyword_counts.get(stt0)+1;
									keyword_counts.put(stt0,r);
									if(!(kwfound.contains(stt0)))
									{
										kwfound += stt0+ ",";
										}
								}
							
						if(keywords.contains(stt))
						{
							int r=keyword_counts.get(stt)+1;
							keyword_counts.put(stt,r);
							if(!(kwfound.contains(stt)))
								kwfound += stt+ ",";
						}				
						
						if(wordCount.containsKey(stt))
						{
							int freqCount = wordCount.get(stt) + 1;
							wordCount.put(stt, freqCount);
						
						}
					else
					{
						wordCount.put(stt,1);
					}
						
						//Compiling for a general pattern match
					    
						
					}
					
					
					
					//Update Records if keyword is found in the file
			////System.out.println("File Length" + fileContents.length());
					
			kwPdf.put(f.getName(),kwfound);
			System.out.println(f.getName()+":"+kwfound);
			Map<String,Integer> sorted = sortByValues(wordCount);
			for(String keyss: sorted.keySet())
			{
				if(sorted.get(keyss)/11 > 1)
				{
				    updatelog(f.getName() + " Frequency of "+keyss+ " : "+sorted.get(keyss)/11);
				    
					if(placemap2.containsKey(keyss))
					////if(placemap2)
					{
						pdfLocation.put(f.getName(),keyss+":"+placemap2.get(keyss)+":"+kwfound);
						updatelog("Place found......... "+ keyss+" Co-ordinates: "+placemap2.get(keyss) + "Keywords: "+kwfound);
						break;
					}
				}
			}
			
		}
		catch(TikaException exp)
		{
			System.out.println("Exception: "+ exp.getMessage());
		}
		catch(Exception ex1)
		{
			System.out.println("Exception: "+ex1.getMessage());
		}

	}


	/**
	* Method to create N-Grams based on the number of words in keyword phrase.
	* arrContents: Array of all the white space separated string content
	* startIndex: starting index to create the N gram
	* end Index: Ending index to create the N-gram
	* Returns: Concatenated String 
	*/
	public static String concatValues(String[] arrContents, int startIndex, int endIndex) {
        StringBuilder sbNGram = new StringBuilder();
        for (int i = startIndex; i < endIndex; i++)
            sbNGram.append((i > startIndex ? " " : "") + arrContents[i]);
        return sbNGram.toString();
    }
	
	  public static <K extends Comparable<String>,V extends Comparable<Integer>> Map<K,V> sortByValues(Map<K,V> map){
	        List<Map.Entry<K,V>> entries = new LinkedList<Map.Entry<K,V>>(map.entrySet());
	      
	        Collections.sort(entries, new Comparator<Map.Entry<K,V>>() {

				@Override
				public int compare(Entry<K, V> o1, Entry<K, V> o2) {
	                return o2.getValue().compareTo((Integer) o1.getValue());
				}
	        });
	      
	        //LinkedHashMap will keep the keys in the order they are inserted
	        //which is currently sorted on natural ordering
	        Map<K,V> sortedMap = new LinkedHashMap<K,V>();
	      
	        for(Map.Entry<K,V> entry: entries){
	            sortedMap.put(entry.getKey(), entry.getValue());
	        }
	      
	        return sortedMap;
	    }

	
	/**
	 * Update the log file with search hit
	 * Appends a log entry with the system timestamp, keyword found, and filename of PDF file containing the keyword
	 * DO NOT MODIFY
	 */	 	
	private void updatelog(String text) {
		timestamp.setTime(System.currentTimeMillis());
		logfile.println(timestamp + " -- \"" + text );
		System.out.println(text);
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
