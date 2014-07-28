import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;


public class Utility {
	
	public static void printOptimalSoln(Node target, String fileName) {
		StringBuilder optimalPath = new StringBuilder();
		List<Node> path = new ArrayList<Node>();
        for(Node node = target; node!=null; node = node.parent){
            path.add(node);
        }
        Collections.reverse(path);
        for(Node node:path) {
        	//System.out.println(node);
        	optimalPath.append(node+System.getProperty("line.separator"));
        }
        writeToFile(fileName, optimalPath.toString());
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
