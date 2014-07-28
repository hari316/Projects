import java.util.ArrayList;
import java.util.Comparator;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Map;
import java.util.PriorityQueue;
import java.util.Queue;
import java.util.SortedSet;
import java.util.Stack;
import java.util.TreeSet;

public class Algorithms {
	
	static SortedSet<String> nodeSet = new TreeSet<String>(new Comparator<String>() {

		@Override
		public int compare(String o1, String o2) {
			return(search.priorityLevel.indexOf(o1) - search.priorityLevel.indexOf(o2));
		}
		
	});
	
	static SortedSet<String> connectedSet = new TreeSet<String>(new Comparator<String>() {
		@Override
		public int compare(String o1, String o2) {
			return(search.priorityLevel.indexOf(o1) - search.priorityLevel.indexOf(o2));
		}
	});
	
	static SortedSet<String> visitedSet = new TreeSet<String>(new Comparator<String>() {
		@Override
		public int compare(String o1, String o2) {
			return(search.priorityLevel.indexOf(o1) - search.priorityLevel.indexOf(o2));
		}
	});
	
	static StringBuilder path = new StringBuilder();
	static StringBuilder logDetails = new StringBuilder("name,depth,group"+System.getProperty("line.separator"));
	static StringBuilder searchlogDetails = new StringBuilder("name,depth,cost"+System.getProperty("line.separator"));
	static int group=0;

	public static void findConnectedComponent(Node startNode, Map<String, Node> nodes, String outputFile, String logFile){
		
		resetVisitedNodes(nodes);
		group++;
		Queue<Node> nodeQueue = new LinkedList<Node>();
		nodeSet.addAll(nodes.keySet());
		startNode.depth = 0;
		startNode.visited = true;
		nodeQueue.add(startNode);
		visitedSet.add(startNode.name);
		logDetails.append(startNode.name+","+startNode.depth+","+group+System.getProperty("line.separator"));
		
		while(!nodeQueue.isEmpty()){
			Node currNode = (Node) nodeQueue.poll();
			connectedSet.add(currNode.name);
			for(Edge adjEdge : currNode.adjEdge){
				Node adjNode = adjEdge.target;
				if(!adjNode.visited){
					adjNode.visited=true;
					adjNode.parent = currNode;
					adjNode.depth = currNode.depth+1;
					visitedSet.add(adjNode.name);
					logDetails.append(adjNode.name+","+adjNode.depth+","+group+System.getProperty("line.separator"));
					nodeQueue.offer(adjNode);
				}				
			}
		}
		
		nodeSet.removeAll(visitedSet);
		Iterator<String> itConn = connectedSet.iterator();
		
	      while (itConn.hasNext()) {
	         // Get element
	         Object element = itConn.next();
	         path.append(element.toString());
	         //System.out.print(element.toString());
	         if(itConn.hasNext()){
	        	 path.append(",");
	        	 //System.out.print(",");
	         }
	      }
	
		if(0 < nodeSet.size()) {
			//System.out.println();
			Iterator<String> itNode = nodeSet.iterator();
			connectedSet.removeAll(connectedSet);
			//path.append("\n");
			path.append(System.getProperty("line.separator"));
			logDetails.append("---------------------"+System.getProperty("line.separator"));
			//System.out.println();
			findConnectedComponent(nodes.get(itNode.next().toString()), nodes, outputFile, logFile);
		}else{
			Utility.writeToFile(outputFile,path.toString());
			logDetails.append("---------------------"+System.getProperty("line.separator"));
			Utility.writeToFile(logFile, logDetails.toString());
			//System.out.println();
		}
				
	}

	
	public static void breadthFirstSearch(Node startNode, Node goalNode, Map<String, Node> nodes, String outputFile, String logFile){

		boolean found = false;
		resetVisitedNodes(nodes);
		Queue<Node> nodeQueue = new LinkedList<Node>();
		startNode.depth = 0;
		startNode.visited = true;
		nodeQueue.add(startNode);
		
		while(!nodeQueue.isEmpty()){
			Node currNode = (Node) nodeQueue.poll();
			//path.append(currNode.name + System.getProperty("line.separator"));
			searchlogDetails.append(currNode.name+","+currNode.depth+","+currNode.pathCost+System.getProperty("line.separator"));
			if(currNode.name.equals(goalNode.name)) {
				found = true;
				break;
			}
			
			for(Edge adjEdge : currNode.adjEdge){
				Node adjNode = adjEdge.target;
				if(!adjNode.visited){
					adjNode.visited=true;
					adjNode.pathCost = currNode.pathCost + adjEdge.cost;
					adjNode.parent=currNode;
					adjNode.depth = currNode.depth+1;
					nodeQueue.offer(adjNode);
				}
			}
			
		}
		
		Utility.writeToFile(logFile, searchlogDetails.toString());
		if(found) {
			Utility.printOptimalSoln(goalNode, outputFile);
		}else {
			Utility.writeToFile(outputFile, "BFS Unsuccessful !! Goal Node Not Found.");
		}
	}

	public static void depthFirstSearch(Node startNode, Node goalNode, Map<String, Node> nodes, String outputFile, String logFile) {

		boolean found = false;
		
		resetVisitedNodes(nodes);
		Stack<Node> nodeStack = new Stack<Node>();
		startNode.depth = 0;
		startNode.visited = true;
		nodeStack.push(startNode);
		
		while(!nodeStack.isEmpty()){
			Node currNode = nodeStack.pop();
			//path.append(currNode.name + System.getProperty("line.separator"));
			searchlogDetails.append(currNode.name+","+currNode.depth+","+currNode.pathCost+System.getProperty("line.separator"));
			if(currNode.name.equals(goalNode.name)) {
				found = true;
				break;
			}
			for(Edge adjEdge : currNode.adjEdge){
				Node adjNode = adjEdge.target;
				if(!adjNode.visited){
					adjNode.visited=true;
					adjNode.pathCost = currNode.pathCost + adjEdge.cost;
					adjNode.parent=currNode;
					adjNode.depth = currNode.depth+1;
					nodeStack.push(adjNode);
				}
			}
		} 
		
		Utility.writeToFile(logFile, searchlogDetails.toString());
		if(found) {
			Utility.printOptimalSoln(goalNode, outputFile);
		}else {
			Utility.writeToFile(outputFile, "DFS Unsuccessful !! Goal Node Not Found.");
		}
	}

	public static void uniformCostSearch(Node startNode, Node goalNode, Map<String, Node> nodes, final ArrayList<String> priorityLevel, String outputFile, String logFile) {

		boolean found = false;
		startNode.pathCost = 0;
		resetVisitedNodes(nodes);
		
		PriorityQueue<Node> nodeQueue = new PriorityQueue<Node>(nodes.size(), new Comparator<Node>() {
			@Override
			public int compare(Node o1, Node o2) {
                if(o1.pathCost > o2.pathCost){
                        return 1;
                }else if (o1.pathCost < o2.pathCost){
                        return -1;
                }else{
                	if(priorityLevel.indexOf(o1.name) > priorityLevel.indexOf(o2.name)) {
                		return 1;
                	}else {
                        return -1;
                	}
                }
			}						
		});
		
		nodeQueue.add(startNode);
		startNode.visited=true;
		while(!nodeQueue.isEmpty()){
			Node currNode = (Node) nodeQueue.poll();
			//path.append(currNode.name +" "+currNode.pathCost+ System.getProperty("line.separator"));
			searchlogDetails.append(currNode.name+","+currNode.depth+","+currNode.pathCost+System.getProperty("line.separator"));
			if(currNode.name.equals(goalNode.name)) {
				found = true;
				break;
			}
			for(Edge adjEdge : currNode.adjEdge){
				Node adjNode = adjEdge.target;
				
				if(!adjNode.visited && !nodeQueue.contains(adjNode)){
					
					adjNode.pathCost = currNode.pathCost + adjEdge.cost;
					adjNode.visited=true;
					adjNode.parent=currNode;
					adjNode.depth = currNode.depth+1;
					nodeQueue.offer(adjNode);
					
				}else if(nodeQueue.contains(adjNode) && adjNode.pathCost > (currNode.pathCost + adjEdge.cost)){
					
					adjNode.pathCost = currNode.pathCost + adjEdge.cost;
					adjNode.parent = currNode;
					adjNode.depth = currNode.depth+1;
					nodeQueue.remove(adjNode);
					nodeQueue.add(adjNode);			
					
				}else {
					if(adjNode.pathCost > (currNode.pathCost + adjEdge.cost)) {
						adjNode.pathCost = currNode.pathCost + adjEdge.cost;
						adjNode.parent = currNode;
						adjNode.depth = currNode.depth+1;
						//adjNode.visited = false;
						nodeQueue.add(adjNode);	
					}
					// If a node is visited and not in queue then that node is not part of optimal path solution.
				}
			}
			
		}
		
		Utility.writeToFile(logFile, searchlogDetails.toString());
		if(found) {
			Utility.printOptimalSoln(goalNode, outputFile);
		}else {
			Utility.writeToFile(outputFile, "UCS Unsuccessful !! Goal Node Not Found.");
		}
	}

	
	public static void resetVisitedNodes( Map<String, Node> nodes) {
		for(Node node:nodes.values()) {
			for(Edge edge:node.adjEdge) {
				Node adjNode = edge.target;
				adjNode.visited = false;
			}
		}
	}

}
