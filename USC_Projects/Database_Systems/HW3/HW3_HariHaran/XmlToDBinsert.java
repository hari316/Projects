/**
 * 
 */

/**
 * @author HariHaran
 *
 */
public class XmlToDBinsert {

	public static void main(String[] args) {
		
		String bookpath = args[0];		
		String reviewpath = args[1];	
		SaxParser parser = new SaxParser();
		parser.loadXMLDataToDB(bookpath,0);
		parser.loadXMLDataToDB(reviewpath,1);
		
	}
}
