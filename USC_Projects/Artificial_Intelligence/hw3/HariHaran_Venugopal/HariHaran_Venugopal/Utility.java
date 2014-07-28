import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;


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
