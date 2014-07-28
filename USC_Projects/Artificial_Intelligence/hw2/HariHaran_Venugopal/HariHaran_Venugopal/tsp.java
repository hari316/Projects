import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.PriorityQueue;
import java.util.Set;
import java.util.TreeMap;
import java.util.TreeSet;


public class tsp {

	public static Map<String, Box> checkPoints = new TreeMap<String, Box>();
	static int rowCount = 0;
	static int colCount = 0;
	static int priorityLevel = 0;
	
	public static HashSet<Box> closedList = new HashSet<Box>();
	public static PriorityQueue<Box> openList;
	public static Box[][] maze = null;
	
	static Map<String, Node> nodes = new HashMap<String, Node>();
	
	//public static PriorityQueue<State> MSTQueue = new PriorityQueue<State>(50,new MSTSort());
	public static List<State> MSTQueue = new ArrayList<State>();
	public static StringBuilder tracePath = new StringBuilder();
	
	public static void main(String[] args) {
		
		int index = 0;
		int taskId = 0;
		
		String inputFile = null;
		String outputFile = null;
		String logFile = null;
		
		for(index = 0;index < args.length; index++) {
			if(args[index].contentEquals("-t")) {
				taskId = Integer.parseInt(args[++index].toString());
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
			readFromFile(inputFile);
			shortestPath_Task1(outputFile,logFile, true);
			break;
			
		case 2:
			readFromFile(inputFile);
			shortestPath_Task1(outputFile,logFile, false);
			TSP_task2(outputFile,logFile);
			break;
			
		default:
			System.out.println("Invalid Task ID !! please try again.");
			break;
		}
		
		
	}
	
	public static void shortestPath_Task1(String outputFile, String logFile, Boolean flag) {
	
		//System.out.println();
		List<String> keys = new ArrayList<String>(checkPoints.keySet());
		
		StringBuilder shortestPath = new StringBuilder();
		double cost = 0;
		Node node1,node2;
		Edge edge1,edge2;
		
		for(int i=0;i<keys.size();i++) {
			
			for(int j=i+1;j<keys.size();j++) {

				if(nodes.containsKey(keys.get(i))) {
					node1 = nodes.get(keys.get(i));
				}else {
					node1 = new Node(keys.get(i));
				}
				
				if(nodes.containsKey(keys.get(j))) {
					node2 = nodes.get(keys.get(j));
				}else {
					node2 = new Node(keys.get(j));
				}
				
				cost = aSearchWithManhattan(maze,keys.get(i),keys.get(j));
				
				edge1 = new Edge(cost, node2);
				edge2 = new Edge(cost, node1);
			
				node1.adjEdge.add(edge1);
				node2.adjEdge.add(edge2);
				
				Collections.sort(node1.adjEdge, new EdgeSort());
				Collections.sort(node2.adjEdge, new EdgeSort());
				
				nodes.put(keys.get(i), node1);
				nodes.put(keys.get(j), node2);
			
				shortestPath.append(keys.get(i)+","+keys.get(j)+","+cost+System.getProperty("line.separator"));
		
			}
		}

		//aSearch(maze,"D", "F");
		//System.out.println(shortestPath);
		if(flag){
			Utility.writeToFile(outputFile,shortestPath.toString());
			Utility.writeToFile(logFile,tracePath.toString());
		}

	}
	
	public static void TSP_task2(String outputFile, String logFile) {
		
		Node start = nodes.get("A");
		
		State tspState = new State();
		
		tspState.setCurrent_checkpoint(start.getName());
		tspState.setG(0);
		tspState.setH(0);
		tspState.setF(0);
	
		MSTQueue.add(tspState);
		
		aSerachWithMST(nodes, start, outputFile, logFile);
	
	}

	public static void readFromFile(String inputFile) {
		String line = null;
		List<String> strs = new ArrayList<String>();
		
		
		try {
			FileReader fileName = new FileReader(inputFile);
			BufferedReader reader = new BufferedReader(fileName);
			
			while((line = reader.readLine()) != null) {
				rowCount++;
				System.out.println(line);
				strs.add(line);
				colCount = line.length();
			}
			reader.close();
			fileName.close();
			
			//System.out.println("No. of Rows " + rowCount);
			//System.out.println("No. of Columns " +colCount);
			
			//String[][] maze = new String[rowCount][colCount];
			maze = new Box[colCount][rowCount];
			Box box;
			
			for(int i = 0; i < rowCount; i++) {

			    //Separate each symbol in corresponding line
			    String rowSymbols = strs.get(i);
				//System.out.println(strs.get(i));
				
			    for(int j = 0; j < colCount; j++) {

			    	box = new Box(j, i);
			    	
			    	if('*' == rowSymbols.charAt(j)) {
			    		box.setTraversible(false);
			    	}else if(' ' == rowSymbols.charAt(j)) {
			    		box.setTraversible(true);
			    	}else {
			    		box.setTraversible(true);
			    		checkPoints.put(Character.toString(rowSymbols.charAt(j)), box);
			    	}
			    	box.setPriority(priorityLevel++);
			    	maze[j][i] = box;			    	
			    	
			    }

			}
			
		} catch (FileNotFoundException e) {
				e.printStackTrace();
			} catch (IOException e) {
				e.printStackTrace();
			}
    }
		
	public static void setVisitedNodes( Set<String> mstSet) {
		
		for(String str:mstSet) {
			nodes.get(str).setVisited(true);
			//nodes.get(str).setParent(null);
		}
	}

	public static void resetVisitedNodes( Set<String> mstSet) {
		
		for(String str:mstSet) {
			nodes.get(str).setVisited(false);
			//nodes.get(str).setParent(null);
		}
	}
	
	private static boolean checkBounds(int i, int j) {
		return i < 0 || i >= colCount || j < 0 || j >= rowCount;
	}
	
	public static ArrayList<Box> findNeighbor(Box square, Box[][] maze) {
		
		if (square == null) {
			System.err.println("Reached Dead End ...");
			return null;
		} else {
			ArrayList<Box> neighbors = new ArrayList<Box>();
			int x = square.getX();
			int y = square.getY();

			for (int i = x - 1; i <= x + 1; i++) {
				for (int j = y - 1; j <= y + 1; j++) {
					if (i == x && j == y)
						continue;
					if ((i==(x+1) && j==(y+1)) || (i==(x-1) && j==(y-1)) || (i==(x+1) && j==(y-1)) || (i==(x-1) && j==(y+1)) )
						continue;
					if (checkBounds(i, j))
						continue;
					if (!maze[i][j].isTraversible())
						continue;

					neighbors.add(maze[i][j]);
				}
			}
			return neighbors;
		}
	}

	
	
	public static void update_Fdist(Box s) {
		//s.setF(Math.max((s.getG() + s.getH()),s.getParent().getF()));
		s.setF(s.getG() + s.getH());
	}


	public static void update_Gdist(Box s, Box d) {
		if (d.equals(s))
			return;

		Box parent = d.getParent();
		int actualDist = Math.abs(parent.getX() - d.getX()) + Math.abs(parent.getY() - d.getY());
		d.setG(parent.getG() + actualDist);
	}

	public static void update_Hdist(Box s, Box d) {
		if (s.equals(d))
			return;
		s.setH(manhattanDist(s,d));
	}

	
	public static double manhattanDist (Box n1, Box n2) {
		return (Math.abs(n1.getX()-n2.getX()) + Math.abs(n1.getY()-n2.getY()));
	}
	
	public static class EdgeSort implements Comparator<Edge> {
		@Override
		public int compare(Edge o1, Edge o2) {
			if(o1.cost > o2.cost) {
				return 1;
			}else if(o1.cost < o2.cost) {
				return -1;
			}else {
				return 0;
			}
		}
	}
	
	public static class MSTSort implements Comparator<State> {
		
		double val1,val2;
		@Override
		public int compare(State arg0, State arg1) {
			
			val1 = arg0.getF();
			val2 = arg1.getF();
			
			if( val1 > val2) {
				return 1;
			}else if((arg0.getF()) < (arg1.getF())) {
				return -1;
			}else {
				if(arg0.visted_checkpoints.size() > arg1.visted_checkpoints.size()) {
					return -1;
				}else if(arg0.visted_checkpoints.size() == arg1.visted_checkpoints.size()) {
					return (int) (arg0.getG() - arg1.getG());
				}else {
					return 1;
				}
			}
			
		}
	}
	
	public static class BoxSort implements Comparator<Box> {
		@Override
		public int compare(Box arg0, Box arg1) {
			if(arg0.getF() > arg1.getF()) {
				return 1;
			}else if(arg0.getF() < arg1.getF()) {
				return -1;
			}else {
				if(arg0.getPriority() > arg1.getPriority()) {
					return 1;
				}else {
					return -1;
				}
			}
			
		}
	}
	
	public static void optimalBox (Box current, Box neighbor, int flag) {
		Box test = new Box(neighbor);
		test.setParent(current);
		update_Gdist(current, test);
		
		if (test.getG() < neighbor.getG()) {
			neighbor.setParent(current);
			update_Gdist(current, neighbor);
			update_Fdist(neighbor);
			if(1 == flag) {
				closedList.remove(neighbor);
				closedList.add(neighbor);
			}else {
				openList.remove(neighbor);
				openList.add(neighbor);
			}
		}
	}
	
	
public static double prim( Map<String, Node> nodes, Node start, State tspState) {
		
		double totalCost = 0;
		Set<String> graphSet = new TreeSet<String>();
		Set<String> mstSet = new TreeSet<String>();
		Set<String> nodeSet = new TreeSet<String>();
		Node currNode;

		graphSet = nodes.keySet();
		nodeSet.addAll(graphSet);
		Set<String> visitedSet = new HashSet<String>(tspState.visted_checkpoints);
		visitedSet.add(start.getName());
		
		nodeSet.removeAll(visitedSet);
		nodeSet.add(start.getName());
		nodeSet.add(tspState.getCurrent_checkpoint());
		
		PriorityQueue<Edge> edgeQueue = new PriorityQueue<Edge>(30, new Comparator<Edge>() {
			@Override
			public int compare(Edge o1, Edge o2) {

				if(o1.cost > o2.cost) {
					return 1;
				}else if(o1.cost < o2.cost) {
					return -1;
				}else {
					if(checkPoints.get(o1.target.getName()).getPriority() > checkPoints.get(o2.target.getName()).getPriority() ) {
						return 1;
					}else {
						return -1;
					}				
				}
			}						
		});
		
		resetVisitedNodes(nodes.keySet());
		setVisitedNodes(tspState.visted_checkpoints);
		start.setVisited(false);
		currNode = start;
		currNode.setVisited(true);
		mstSet.add(currNode.getName());
		
		while(!mstSet.equals(nodeSet)) {
			for(Edge edge:currNode.adjEdge) {
				if(!edge.target.isVisited()) {
					edge.target.setParent(currNode);
					edgeQueue.add(edge);		
				} 
			}
			Edge minEdge = edgeQueue.poll();
			
			while(mstSet.contains(minEdge.target.getName()) || minEdge.target.isVisited()) {
				minEdge = edgeQueue.poll();
			}
			
			minEdge.target.setVisited(true);
			
			totalCost += minEdge.cost;
			mstSet.add(minEdge.target.getName());
			currNode = minEdge.target;
		}
		resetVisitedNodes(mstSet);
		
		return totalCost;
		
	}

	
	public static void aSerachWithMST(Map<String, Node> nodes, Node start, String outputFile, String logFile) {
		
		State tspState;
		StringBuilder strLog = new StringBuilder();
		Node currNode;
		
		while(!MSTQueue.isEmpty()) {
			
			tspState = MSTQueue.get(0);
			MSTQueue.remove(0);
			
			currNode = nodes.get(tspState.getCurrent_checkpoint());
			
			tspState.setH(prim(nodes, start, tspState));
			tspState.setF(tspState.getG()+tspState.getH());
			
			tspState.visted_checkpoints.add(currNode.getName());
			
			StringBuilder strNodes = new StringBuilder();
			for(String s:tspState.visted_checkpoints) {
				strNodes.append(s);
			}
			//System.out.println(strNodes+","+tspState.getG()+","+tspState.getH()+","+tspState.getF());
			strLog.append(strNodes+","+tspState.getG()+","+tspState.getH()+","+tspState.getF());
			strLog.append(System.getProperty("line.separator"));
			
			if(tspState.visted_checkpoints.equals(nodes.keySet())) {
				
				strNodes.append(start.getName());
				tspState.setG(tspState.getF());
				tspState.setH(0);
				//System.out.println(strNodes+","+tspState.getG()+","+tspState.getH()+","+tspState.getF());
				strLog.append(strNodes+","+tspState.getG()+","+tspState.getH()+","+tspState.getF());
				Utility.printSoln(strNodes.toString(), tspState.getF(), outputFile);
				Utility.writeToFile(logFile, strLog.toString());
				return;
			}			
			
			
			for(Edge edge:currNode.adjEdge) {
				
				State newState = new State();
				newState.setCurrent_checkpoint(tspState.getCurrent_checkpoint());
				newState.visted_checkpoints.addAll(tspState.visted_checkpoints);
				newState.setG(tspState.getG());
				newState.setH(tspState.getH());
				newState.setF(tspState.getF());
				
				if(!newState.visted_checkpoints.contains(edge.target.getName())) {
					newState.setCurrent_checkpoint(edge.target.getName());
					edge.target.setParent(currNode);
					edge.target.setPathCost(currNode.getPathCost()+edge.cost);
					
					newState.setG(newState.getG()+edge.cost);
					newState.setH(prim(nodes, start, newState));
					newState.setF(newState.getG()+newState.getH());
					
					MSTQueue.add(newState);
					Collections.sort(MSTQueue,new MSTSort());
					
					
				} 
			}
			
		}
	}
	
	public static double aSearchWithManhattan(Box[][] maze, String src, String dest) {
		
		double optimalCost = 0;
		Box start = checkPoints.get(src);
		Box end = checkPoints.get(dest);
		
		resetBoxList(closedList);
		resetBox(start);
		resetBox(end);
		
	    tracePath.append("from '"+src+"' to '"+dest+"'"+System.getProperty("line.separator"));
	    tracePath.append("-----------------------------------------------"+System.getProperty("line.separator"));
	    tracePath.append("x,y,g,h,f"+System.getProperty("line.separator"));
	    
		Box current;
		openList = new PriorityQueue<Box>(rowCount*colCount,new BoxSort());
		
		update_Hdist(start, end);
		start.setParent(start);
		update_Fdist(start);
		
		openList.add(start);
		
		while(!closedList.contains(end)) {
			
			current = openList.poll();
			
			if(null == current) {
				System.out.println("No path found !!!");
				tracePath.append("-----------------------------------------------"+System.getProperty("line.separator"));
				System.out.println(tracePath);
				
				closedList.clear();
				openList.clear();
				
				return 999;
			}
			tracePath.append(current.getX()+","+current.getY() +","+current.getG()+","+current.getH()+","+current.getF()+System.getProperty("line.separator"));

			if(current.equals(end)) {	
				closedList.add(current);
				//System.out.println("Path Found !!!");
			}else {
				closedList.add(current);
				List<Box> neighbors = findNeighbor(current,maze);
				for (Box neighbor : neighbors) {
					if (closedList.contains(neighbor)) {
						optimalBox(current, neighbor,1);
					} else if (openList.contains(neighbor)) {
						optimalBox(current, neighbor,0);
					} else {
						
						neighbor.setParent(current);
						update_Hdist(neighbor, end);
						update_Gdist(current, neighbor);
						update_Fdist(neighbor);
						openList.add(neighbor);
						//maze.updateAll(neighbor);
					}
				}
			}
 			
		}
		tracePath.append("-----------------------------------------------"+System.getProperty("line.separator"));
		//System.out.println(tracePath);
		/*if(flag) {
			Utility.writeToFile(logFile, tracePath.toString());
		}*/
		
		closedList.clear();
		openList.clear();
		optimalCost = end.getF();
		
		return optimalCost;
	}
	
	public static void resetBoxList(HashSet<Box> closedList) {
		for(Box box:closedList) {
			resetBox(box);
		}
	}
	public static void resetBox(Box target) {
		target.setF(0);
		target.setG(0);
		target.setH(0);
	}
	
	public static void printOptimalSoln(Box target) {
		StringBuilder optimalPath = new StringBuilder();
		List<Box> path = new ArrayList<Box>();
        for(Box Box = target; Box!=null; Box = Box.getParent()){
            path.add(Box);
        }
        Collections.reverse(path);
        for(Box Box:path) {
        	optimalPath.append(Box.getX()+","+Box.getY()+System.getProperty("line.separator"));
        }
        //System.out.println(optimalPath);
        //writeToFile(fileName, optimalPath.toString());
	}
}
