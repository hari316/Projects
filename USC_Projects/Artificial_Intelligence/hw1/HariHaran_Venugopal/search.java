import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.Map;

public class search {

	static Map<String, Node> nodes = new HashMap<String, Node>();
	//static Map<String, Integer> priorityLevel = new HashMap<String, Integer>();
	static ArrayList<String> priorityLevel = new ArrayList<String>();

	public static void main(String[] args) {

		int index = 0;
		int taskId = 0;
		String start = null;
		String goal = null;
		String inputFile = null;
		String tiebreakFile = null;
		String outputFile = null;
		String logFile = null;
		boolean flag = true;
		
		for(index = 0;index < args.length; index++) {
			if(args[index].contentEquals("-t")) {
				if(flag) {
					taskId = Integer.parseInt(args[++index].toString());
					flag = false;
				}else {
					tiebreakFile = args[++index];
				}
			}else if(args[index].contentEquals("-s")) {
				start = args[++index];
			}else if(args[index].contentEquals("-g")) {
				goal = args[++index];
			}else if(args[index].contentEquals("-i")) {
				inputFile = args[++index];
			}else if(args[index].contentEquals("-op")) {
				outputFile = args[++index];
			}else if(args[index].contentEquals("-ol")) {
				logFile = args[++index];
			}
		}
	
		
		switch(taskId) {
		
			case 1: 
				readFromFile(inputFile, tiebreakFile, 1);					
				Algorithms.breadthFirstSearch(nodes.get(start), nodes.get(goal), nodes,outputFile,logFile);
				break;
			case 2:
				readFromFile(inputFile, tiebreakFile, 0);					
				Algorithms.depthFirstSearch(nodes.get(start), nodes.get(goal), nodes,outputFile,logFile);
				break;
			case 3:
				readFromFile(inputFile, tiebreakFile, 1);					
				Algorithms.uniformCostSearch(nodes.get(start), nodes.get(goal), nodes, priorityLevel,outputFile,logFile);

				break;
			case 4:
				readFromFile(inputFile, tiebreakFile, 1);					
				Algorithms.findConnectedComponent(nodes.get(priorityLevel.get(0)), nodes,outputFile,logFile);
				break;
				
			default:
				System.out.println("Invalid Task ID !! please try again.");
				break;
		}
	}
	
	public static void readFromFile(String inputFile, String tieBreak, int Flag) {
		
		String line = null;
		String fields[];
		Node fnode = null;
		Node bnode = null;
		Edge edge1 = null;
		Edge edge2 = null;
		
		try {
			FileReader fileName = new FileReader(tieBreak);
			BufferedReader reader = new BufferedReader(fileName);			
			while ((line = reader.readLine()) != null) {
				priorityLevel.add(line);
			}
			fileName.close();
			reader.close();
			
			fileName = new FileReader(inputFile);
			reader = new BufferedReader(fileName);
			while ((line = reader.readLine()) != null) {	
				
				fields = line.split(",");
				
				if(nodes.containsKey(fields[0])) {
					fnode = nodes.get(fields[0]);
				}else {
					fnode = new Node(fields[0]);
				}
				
				if(nodes.containsKey(fields[1])) {
					bnode = nodes.get(fields[1]);
				}else {
					bnode = new Node(fields[1]);
				}
				
				edge1 = new Edge(Double.parseDouble(fields[2]), bnode);						
				fnode.adjEdge.add(edge1);
				
				if(1==Flag) {
					Collections.sort(fnode.adjEdge,(new search()).new customBFSSort());
				}else {
					Collections.sort(fnode.adjEdge,(new search()).new customDFSSort());
				}
			    
				edge2 = new Edge(Double.parseDouble(fields[2]), fnode);
				bnode.adjEdge.add(edge2);
				if(1==Flag) {
					Collections.sort(bnode.adjEdge,(new search()).new customBFSSort());
				}else {
					Collections.sort(fnode.adjEdge,(new search()).new customDFSSort());
				}
				
				nodes.put(fields[0], fnode);
				nodes.put(fields[1], bnode);
			}
			reader.close();

		} catch (FileNotFoundException noFile) {
			System.out.println("Given Input File Not Found !!");
			noFile.printStackTrace();
		} catch (IOException e) {
			System.out.println("Exception Occured: "+e.getMessage());
			e.printStackTrace();
		}

	}
			
	class customBFSSort implements Comparator<Edge>{

		@Override
		public int compare(Edge arg0, Edge arg1) {
			return(priorityLevel.indexOf(arg0.target.name) - priorityLevel.indexOf(arg1.target.name));
		}
	}
	
	class customDFSSort implements Comparator<Edge>{

		@Override
		public int compare(Edge arg0, Edge arg1) {
			return(priorityLevel.indexOf(arg1.target.name) - priorityLevel.indexOf(arg0.target.name));
		}
	}
	
}