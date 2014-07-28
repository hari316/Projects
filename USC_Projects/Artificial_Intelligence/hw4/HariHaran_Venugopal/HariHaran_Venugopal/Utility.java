import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;


public class Utility {
	
	public static void printSoln(String path, double cost, String outputFile) {
		
		StringBuilder str = new StringBuilder();
		for(int i=0; i < path.length();i++) {
			str.append(Character.toString(path.charAt(i)));
			str.append(System.getProperty("line.separator"));
		}
		
		str.append("Total Tour Cost: "+cost);
		//System.out.println(str);
		writeToFile(outputFile, str.toString());
	}
	
	public static List<String> readFromFile(String fileName) {
		
		List<String> strList = new ArrayList<String>();
		
		try {
			FileReader fileread = new FileReader(fileName);
			BufferedReader buffread = new BufferedReader(fileread);
			
			String lineStr = null; 
			while(null != (lineStr = buffread.readLine())) {
				strList.add(lineStr);
			}		    
			buffread.close();
			fileread.close();
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		return strList;
	}
	
	public static void writeToFile(String fileName, String content) {
		
		File file = new File(fileName);
		try {
			file.createNewFile();
			FileWriter fw = new FileWriter(file.getAbsoluteFile(),false);
			BufferedWriter bw = new BufferedWriter(fw);
			bw.write(content);
			bw.close();				
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

}
