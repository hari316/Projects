import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.PriorityQueue;
import java.util.Set;
import java.util.TreeMap;
import java.util.TreeSet;


public class war {

	static int priority = 0;
	static int turn = 0;
	static int count = 0;
	static int cuttoffDepth = 1;
	
	static String currPlayer = null;
	static String oppPlayer = null;
	final static String fm = "Force March";
	final static String pd = "Paratroop Drop";
	static Map<String, Node> nodes = new TreeMap<String, Node>();
	static Map<String, Player> players = new HashMap<String, Player>();
	static Set<String> uList = new TreeSet<String>();
	static Set<String> cList = new TreeSet<String>();
	
	static State startState = new State("N/A");

	static Set<String> unoccupiedCity = new TreeSet<String>();
	
	public static StringBuilder outputPath = new StringBuilder();
	public static StringBuilder outputLog = new StringBuilder();
	
	static PriorityQueue<State> playerMoves = new PriorityQueue<State>(40, new Comparator<State>() {
		@Override
		public int compare(State s1, State s2) {
			
			if(s1.getAction().equals(fm) && s2.getAction().equals(pd)) {
				return -1;
			}else if(s1.getAction().equals(pd) && s2.getAction().equals(fm)) {
				return 1;
			}else{
				return (nodes.get(s1.getDest()).priorityLevel - nodes.get(s2.getDest()).priorityLevel);					
			}
		}						
	});
	
	public static void main(String[] args) {
		
		
		int index = 0;
		int taskId = 0;
		
		String mapFile = null;
		String initFile = null;
		String outputFile = null;
		String logFile = null;
		
		for(index = 0;index < args.length; index++) {
			if(args[index].contentEquals("-t")) {
				taskId = Integer.parseInt(args[++index].toString());
			}else if(args[index].contentEquals("-d")) {
				cuttoffDepth = Integer.parseInt(args[++index]);
			}else if(args[index].contentEquals("-m")) {
				mapFile = args[++index];
			}else if(args[index].contentEquals("-i")) {
				initFile = args[++index];
			}else if(args[index].contentEquals("-op")) {
				outputFile = args[++index];
			}else if(args[index].contentEquals("-ol")) {
				logFile = args[++index];
			}
		}
		
		readFromFile(mapFile, initFile);
		
		Player uObj = new Player();
		uObj.cityList = uList;
		uObj.totalRes = computeEvalFunc(uObj.cityList);
		
		Player cObj = new Player();
		cObj.cityList = cList;
		cObj.totalRes = computeEvalFunc(cObj.cityList);
		
		players.put("Union",uObj);
		players.put("Confederacy", cObj);
		
		startState.setAction("N/A");
		startState.setDest("N/A");
		startState.cpCityList.addAll(players.get("Union").cityList);
		startState.cptotal = players.get("Union").totalRes;
		startState.opCityList.addAll(players.get("Confederacy").cityList);
		startState.optotal = players.get("Confederacy").totalRes;
		startState.setEval(Double.NEGATIVE_INFINITY);
		
		// logs
		//System.out.println(startState.getplayerName()+","+startState.getAction()+","+startState.getDest()+","+(startState.depth+1)+","+startState.getEval());

		
		displayState(startState);
		
		//int algoType = 3;
		
		if(1 == taskId) {
			outputLog.append("Player,Action,Destination,Depth,Value"+System.getProperty("line.separator"));
		}else if(2 == taskId) {
			outputLog.append("Player,Action,Destination,Depth,Value"+System.getProperty("line.separator"));
		}else {
			outputLog.append("Player,Action,Destination,Depth,Value,Alpha,Beta,CUT-OFF?"+System.getProperty("line.separator"));
		}
		
		
		while(0 != unoccupiedCity.size()) {
			if(0 == turn%2) {
				currPlayer = "Union";
				oppPlayer = "Confederacy";
				if(1 == taskId) {
					startState = greedyAlgo(startState);
				}else if(2 == taskId) {
					startState = minimax_decision(startState);
				}else {
					startState = alpha_beta(startState);
				}
			}else {
				currPlayer = "Confederacy";
				oppPlayer = "Union";
				startState = greedyAlgo(startState);
			}
		}
		
		Utility.writeToFile(outputFile,outputPath.toString());
		Utility.writeToFile(logFile, outputLog.toString());
		
		System.out.println("=====================    GAME OVER     ======================");
		
	}
	
	
	public static void readFromFile(String mapFile, String initFile) {
		
		String line = null;
		String fields[];
		Node fnode = null;
		Node bnode = null;
		Edge edge1 = null;
		Edge edge2 = null;
		
		FileReader fileName = null;
		BufferedReader reader = null;
		
		try {
			fileName = new FileReader(mapFile);
		
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
				
				edge1 = new Edge(1, bnode);						
				fnode.adjEdge.add(edge1);
				
				edge2 = new Edge(1, fnode);
				bnode.adjEdge.add(edge2);
				
				nodes.put(fields[0], fnode);
				nodes.put(fields[1], bnode);
			}
		
			fileName.close();
			reader.close();
			
			fileName = new FileReader(initFile);
			Node initNode = null;
			reader = new BufferedReader(fileName);			
			while ((line = reader.readLine()) != null) {
				fields = line.split(",");
				initNode = nodes.get(fields[0]);
				initNode.setResource(Double.parseDouble(fields[1]));
				//initNode.setPlayer(Integer.parseInt(fields[2]));
				initNode.priorityLevel = priority++;
				if(1 == Integer.parseInt(fields[2])) {
					initNode.setPlayer("Union");
					uList.add(initNode.getName());			
				}else if(-1 == Integer.parseInt(fields[2])) {
					initNode.setPlayer("Confederacy");
					cList.add(initNode.getName());		
				}else{
					initNode.setPlayer("Unknown");
					unoccupiedCity.add(initNode.getName());
				}
				initNode.depth = 0;
			}
			fileName.close();
			reader.close();
			
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		
	}
	
	public static State alpha_beta(State currState) {
		
		turn++;
		State nextState = null;
		Set<String> temp = new TreeSet<String>();
		
		if(currState.getplayerName().equals("Confederacy")) {
			temp.addAll(currState.opCityList);
			currState.opCityList.clear();
			currState.opCityList.addAll(currState.cpCityList);
			currState.cpCityList.clear();
			currState.cpCityList.addAll(temp);
			
		}
		
		currState.depth = 0;
		
		double v = alphaBetaMaxValue(currState, Double.NEGATIVE_INFINITY, Double.POSITIVE_INFINITY);
		
		for(State s : playerMoves) {
			if(v == s.getEval()) {
				nextState = s;
				break;
			}
		}
		
		currPlayer = "Union";
		oppPlayer = "Confederacy";
		
		captureCity(nextState);
		unoccupiedCity.remove(nextState.getDest());
		playerMoves.clear();
		
		players.get(currPlayer).cityList = nextState.cpCityList;
		players.get(currPlayer).totalRes = computeEvalFunc(nextState.cpCityList);
		players.get(oppPlayer).cityList = nextState.opCityList;
		players.get(oppPlayer).totalRes = computeEvalFunc(nextState.opCityList);
		
		nextState.cptotal = computeEvalFunc(nextState.cpCityList);
		nextState.optotal = computeEvalFunc(nextState.opCityList);
		
		displayState(nextState);
		
		return nextState;
		
	}
	
	
public static double alphaBetaMaxValue(State s, double a, double b) {
		
		currPlayer = "Union";
		oppPlayer = "Confederacy";
				
		if(terminalTest(s)) {
			double res = alphaBetaUtility(s,a,b);
			currPlayer = "Confederacy";
			oppPlayer = "Union";
			return res;
		}
		
		double val = Double.NEGATIVE_INFINITY;
		State st = null;
		
		Set<String> uncapuredCity = new TreeSet<String>();
		
		uncapuredCity.addAll(nodes.keySet());
		uncapuredCity.removeAll(s.cpCityList);
		uncapuredCity.removeAll(s.opCityList);
		
		if(1 == turn) {
		// logs
		//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val+","+a+","+b);
			outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val+","+a+","+b);
			outputLog.append(System.getProperty("line.separator"));
		}
		
		for(String city:uncapuredCity) {
			
			currPlayer = "Union";
			oppPlayer = "Confederacy";
			
			Node currNode = nodes.get(city);
		
			for(Edge e: currNode.adjEdge) {
				Node n = e.target;					
				
				st = new State(currPlayer);
				st.setAction(fm);
				st.setDest(city);
				
				st.cpCityList = new TreeSet<String>();
				st.opCityList = new TreeSet<String>();
				
				if(s.getplayerName().equals("Confederacy")) {
					st.cpCityList.addAll(s.opCityList);
					st.cpCityList.add(city);
					
					st.opCityList.addAll(s.cpCityList);
					
				}else {
					st.cpCityList.addAll(s.cpCityList);
					st.cpCityList.add(city);
					
					st.opCityList.addAll(s.opCityList);
				}
				
				
				st.setEval(Double.POSITIVE_INFINITY);
				
				if(st.cpCityList.contains(n.getName())) {
			
					forwardMarch(currNode, currPlayer, oppPlayer, st.cpCityList, st.opCityList);
					st.depth = s.depth + 1;

					val = Math.max(val, alphaBetaMinValue(st, a, b));
					
					if(val >= b) {
						
						if(1 == turn) {
						// logs
						//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val+","+st.getEval()+","+b+","+"CUT-OFF");
							outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val+","+st.getEval()+","+b+","+"CUT-OFF");
							outputLog.append(System.getProperty("line.separator"));
						}
						
						return val;
					}
					
					a = Math.max(a, val);
					
					if(1 == turn) {
					// logs
					//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val+","+a+","+b);
						outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val+","+a+","+b);
						outputLog.append(System.getProperty("line.separator"));
					}
									
					if(0 == s.depth) {
						playerMoves.add(st);
					}
					break;
				}	
				
			}	

		}
		
		for(String city:uncapuredCity) {
			
			currPlayer = "Union";
			oppPlayer = "Confederacy";

			st = new State(currPlayer);
			st.setAction(pd);
			st.setDest(city);
			
			st.cpCityList = new TreeSet<String>();
			st.opCityList = new TreeSet<String>();
			
			if(s.getplayerName().equals("Confederacy")) {
				st.cpCityList.addAll(s.opCityList);
				st.cpCityList.add(city);
				
				st.opCityList.addAll(s.cpCityList);
				
			}else {
				st.cpCityList.addAll(s.cpCityList);
				st.cpCityList.add(city);
				
				st.opCityList.addAll(s.opCityList);
			}
			
			st.setEval(Double.POSITIVE_INFINITY);
			
			st.depth = s.depth + 1;
			
			val = Math.max(val, alphaBetaMinValue(st, a, b));
			
			if(val >= b) {
				
				if(1 == turn) {
				// logs
				//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val+","+st.getEval()+","+b+","+"CUT-OFF");
					outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val+","+st.getEval()+","+b+","+"CUT-OFF");
					outputLog.append(System.getProperty("line.separator"));
				}
				return val;
			}
			
			a = Math.max(a, val);
			
			if(1 == turn) {
			// logs
			//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val+","+a+","+b);
				outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val+","+a+","+b);
				outputLog.append(System.getProperty("line.separator"));
			}
			
			if(0 == s.depth) {
				playerMoves.add(st);
			}
			
		}
		
		s.setEval(val);
		
		return val;
	}
	
	
	public static double alphaBetaMinValue(State s, double a, double b) {
		
		currPlayer = "Confederacy";
		oppPlayer = "Union";
				
		if(terminalTest(s)) {
			double res = alphaBetaUtility(s,a,b);
			currPlayer = "Union";
			oppPlayer = "Confederacy";
			return res;
		}
		
		double val = Double.POSITIVE_INFINITY;
		
		State st = null;
		
		Set<String> uncapuredCity = new TreeSet<String>();
		
		uncapuredCity.addAll(nodes.keySet());
		uncapuredCity.removeAll(s.cpCityList);
		uncapuredCity.removeAll(s.opCityList);
		
		if(1 == turn) {
		// logs
		//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val+","+a+","+b);
			outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val+","+a+","+b);
			outputLog.append(System.getProperty("line.separator"));
		}
		
		for(String city:uncapuredCity) {
			
			currPlayer = "Confederacy";
			oppPlayer = "Union";
			
			Node currNode = nodes.get(city);
		
			for(Edge e: currNode.adjEdge) {
				Node n = e.target;					
				
				st = new State(currPlayer);
				st.setAction(fm);
				st.setDest(city);
				
				st.cpCityList = new TreeSet<String>();
				st.opCityList = new TreeSet<String>();
				
				if(s.getplayerName().equals("Union")) {
					st.cpCityList.addAll(s.opCityList);
					st.cpCityList.add(city);
					
					st.opCityList.addAll(s.cpCityList);
					
				}else {
					st.cpCityList.addAll(s.cpCityList);
					st.cpCityList.add(city);
					
					st.opCityList.addAll(s.opCityList);
				}
				
				st.setEval(Double.NEGATIVE_INFINITY);
				
				if(st.cpCityList.contains(n.getName())) {
			
					forwardMarch(currNode, currPlayer, oppPlayer, st.cpCityList, st.opCityList);
					st.depth = s.depth + 1;
	
					//val = Math.min(val, MaxValue(st));
					
					val = Math.min(val, alphaBetaMaxValue(st, a, b));
					
					if(val <= a) {
						
						if(1 == turn) {
						// logs
						//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val+","+a+","+st.getEval()+","+"CUT-OFF");
							outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val+","+a+","+st.getEval()+","+"CUT-OFF");
							outputLog.append(System.getProperty("line.separator"));
						}
						
						return val;
					}
					
					b = Math.min(b, val);
					
					
					if(1 == turn) {
					// logs
					//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val+","+a+","+b);
						outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val+","+a+","+b);
						outputLog.append(System.getProperty("line.separator"));
					}
					
					break;
				}	
				
			}	

		}
		
		for(String city:uncapuredCity) {

			currPlayer = "Confederacy";
			oppPlayer = "Union";
			
			st = new State(currPlayer);
			st.setAction(pd);
			st.setDest(city);
			
			st.cpCityList = new TreeSet<String>();
			st.opCityList = new TreeSet<String>();
			
			if(s.getplayerName().equals("Union")) {
				st.cpCityList.addAll(s.opCityList);
				st.cpCityList.add(city);
				
				st.opCityList.addAll(s.cpCityList);
				
			}else {
				st.cpCityList.addAll(s.cpCityList);
				st.cpCityList.add(city);
				
				st.opCityList.addAll(s.opCityList);
			}
			
			st.setEval(Double.NEGATIVE_INFINITY);
			
			st.depth = s.depth + 1;
	
			//val = Math.min(val, MaxValue(st));
			
			val = Math.min(val, alphaBetaMaxValue(st, a, b));
			
			if(val <= a) {
				
				if(1 == turn) {
				// logs
				//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val+","+a+","+st.getEval()+","+"CUT-OFF");
					outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val+","+a+","+st.getEval()+","+"CUT-OFF");
					outputLog.append(System.getProperty("line.separator"));
				}
				
				return val;
			}
			
			b = Math.min(b, val);
			
			if(1 == turn) {
			// logs
			//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val+","+a+","+b);
				outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val+","+a+","+b);
				outputLog.append(System.getProperty("line.separator"));
			}
			
		}

		s.setEval(val);

		return val;
	}

	
	public static double alphaBetaUtility(State terminalState, Double a, Double b) {
		
		
		if(terminalState.getplayerName().equals("Confederacy")) {
			terminalState.setEval(computeEvalFunc(terminalState.opCityList) - computeEvalFunc(terminalState.cpCityList));
		}else {
			terminalState.setEval(computeEvalFunc(terminalState.cpCityList) - computeEvalFunc(terminalState.opCityList));
		}
		
		if(1 == turn) {
		// logs
		//System.out.println(terminalState.getplayerName()+","+terminalState.getAction()+","+terminalState.getDest()+","+(terminalState.depth+1)+","+terminalState.getEval()+","+a+","+b);		
			outputLog.append(terminalState.getplayerName()+","+terminalState.getAction()+","+terminalState.getDest()+","+terminalState.depth+","+terminalState.getEval()+","+a+","+b);
			outputLog.append(System.getProperty("line.separator"));
		}
				
		return terminalState.getEval();
	}
	
	public static State minimax_decision(State currState) {
		
		turn++;
		State nextState = null;
		Set<String> temp = new TreeSet<String>();
		
		if(currState.getplayerName().equals("Confederacy")) {
			temp.addAll(currState.opCityList);
			currState.opCityList.clear();
			currState.opCityList.addAll(currState.cpCityList);
			currState.cpCityList.clear();
			currState.cpCityList.addAll(temp);
			
		}
		
		currState.depth = 0;
		
		double v = MaxValue(currState);
		
		/*for(State s : playerMoves) {
			System.out.println(s.getplayerName()+" "+s.getAction()+" "+s.getDest() +": "+s.getEval());
		}
		System.out.println("Find value: "+v);
		*/
		for(State s : playerMoves) {
			if(v == s.getEval()) {
				nextState = s;
				break;
			}
		}
		
		currPlayer = "Union";
		oppPlayer = "Confederacy";
		
		captureCity(nextState);
		unoccupiedCity.remove(nextState.getDest());
		playerMoves.clear();
		
		players.get(currPlayer).cityList = nextState.cpCityList;
		players.get(currPlayer).totalRes = computeEvalFunc(nextState.cpCityList);
		players.get(oppPlayer).cityList = nextState.opCityList;
		players.get(oppPlayer).totalRes = computeEvalFunc(nextState.opCityList);
		
		nextState.cptotal = computeEvalFunc(nextState.cpCityList);
		nextState.optotal = computeEvalFunc(nextState.opCityList);
		
		displayState(nextState);
		
		return nextState;
	}
	
	public static boolean terminalTest(State currState) {
	
		int cityCaptured = currState.cpCityList.size()+currState.opCityList.size();
		if(0 == (nodes.size() - cityCaptured) || cuttoffDepth == currState.depth) {
			return true;
		}else {
			return false;
		}
	}
	
	public static double MaxValue(State s) {
		
		currPlayer = "Union";
		oppPlayer = "Confederacy";
				
		if(terminalTest(s)) {
			double res = Utility(s);
			currPlayer = "Confederacy";
			oppPlayer = "Union";
			return res;
		}
		
		double val = Double.NEGATIVE_INFINITY;
		State st = null;
		
		Set<String> uncapuredCity = new TreeSet<String>();
		
		uncapuredCity.addAll(nodes.keySet());
		uncapuredCity.removeAll(s.cpCityList);
		uncapuredCity.removeAll(s.opCityList);
		
		if(1 == turn) {
		// logs
		//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val);
			outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val);
			outputLog.append(System.getProperty("line.separator"));	
		}
		
		for(String city:uncapuredCity) {
			
			currPlayer = "Union";
			oppPlayer = "Confederacy";
			
			Node currNode = nodes.get(city);
		
			for(Edge e: currNode.adjEdge) {
				Node n = e.target;					
				
				st = new State(currPlayer);
				st.setAction(fm);
				st.setDest(city);
				
				st.cpCityList = new TreeSet<String>();
				st.opCityList = new TreeSet<String>();
				
				if(s.getplayerName().equals("Confederacy")) {
					st.cpCityList.addAll(s.opCityList);
					st.cpCityList.add(city);
					
					st.opCityList.addAll(s.cpCityList);
					
				}else {
					st.cpCityList.addAll(s.cpCityList);
					st.cpCityList.add(city);
					
					st.opCityList.addAll(s.opCityList);
				}
				
				
				st.setEval(Double.POSITIVE_INFINITY);
				
				if(st.cpCityList.contains(n.getName())) {
			
					forwardMarch(currNode, currPlayer, oppPlayer, st.cpCityList, st.opCityList);
					st.depth = s.depth + 1;

					val = Math.max(val, MinValue(st));
					
					if(1 == turn) {
					// logs
					//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val);
						outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val);
						outputLog.append(System.getProperty("line.separator"));	
					}
									
					if(0 == s.depth) {
						playerMoves.add(st);
					}
					break;
				}	
				
			}	

		}
		
		for(String city:uncapuredCity) {
			
			currPlayer = "Union";
			oppPlayer = "Confederacy";

			st = new State(currPlayer);
			st.setAction(pd);
			st.setDest(city);
			
			st.cpCityList = new TreeSet<String>();
			st.opCityList = new TreeSet<String>();
			
			if(s.getplayerName().equals("Confederacy")) {
				st.cpCityList.addAll(s.opCityList);
				st.cpCityList.add(city);
				
				st.opCityList.addAll(s.cpCityList);
				
			}else {
				st.cpCityList.addAll(s.cpCityList);
				st.cpCityList.add(city);
				
				st.opCityList.addAll(s.opCityList);
			}
			
			st.setEval(Double.POSITIVE_INFINITY);
			
			st.depth = s.depth + 1;
			
			val = Math.max(val, MinValue(st));
			
			if(1 == turn) {
			// logs
			//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val);
				outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val);
				outputLog.append(System.getProperty("line.separator"));	

			}
			
			if(0 == s.depth) {
				playerMoves.add(st);
			}
			
		}
		
		s.setEval(val);
		
		return val;
	}
	
	
	public static double MinValue(State s) {
		
		currPlayer = "Confederacy";
		oppPlayer = "Union";
				
		if(terminalTest(s)) {
			double res = Utility(s);
			currPlayer = "Union";
			oppPlayer = "Confederacy";
			return res;
		}
		
		double val = Double.POSITIVE_INFINITY;
		
		State st = null;
		
		Set<String> uncapuredCity = new TreeSet<String>();
		
		uncapuredCity.addAll(nodes.keySet());
		uncapuredCity.removeAll(s.cpCityList);
		uncapuredCity.removeAll(s.opCityList);
		
		if(1 == turn) {
		// logs
		//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val);
			outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val);
			outputLog.append(System.getProperty("line.separator"));	
		}
		
		for(String city:uncapuredCity) {
			
			currPlayer = "Confederacy";
			oppPlayer = "Union";
			
			Node currNode = nodes.get(city);
		
			for(Edge e: currNode.adjEdge) {
				Node n = e.target;					
				
				st = new State(currPlayer);
				st.setAction(fm);
				st.setDest(city);
				
				st.cpCityList = new TreeSet<String>();
				st.opCityList = new TreeSet<String>();
				
				if(s.getplayerName().equals("Union")) {
					st.cpCityList.addAll(s.opCityList);
					st.cpCityList.add(city);
					
					st.opCityList.addAll(s.cpCityList);
					
				}else {
					st.cpCityList.addAll(s.cpCityList);
					st.cpCityList.add(city);
					
					st.opCityList.addAll(s.opCityList);
				}
				
				st.setEval(Double.NEGATIVE_INFINITY);
				
				if(st.cpCityList.contains(n.getName())) {
			
					forwardMarch(currNode, currPlayer, oppPlayer, st.cpCityList, st.opCityList);
					st.depth = s.depth + 1;
	
					val = Math.min(val, MaxValue(st));
					
					if(1 == turn) {
					// logs
					//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val);
						outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val);
						outputLog.append(System.getProperty("line.separator"));	
					}
					
					break;
				}	
				
			}	

		}
		
		for(String city:uncapuredCity) {

			currPlayer = "Confederacy";
			oppPlayer = "Union";
			
			st = new State(currPlayer);
			st.setAction(pd);
			st.setDest(city);
			
			st.cpCityList = new TreeSet<String>();
			st.opCityList = new TreeSet<String>();
			
			if(s.getplayerName().equals("Union")) {
				st.cpCityList.addAll(s.opCityList);
				st.cpCityList.add(city);
				
				st.opCityList.addAll(s.cpCityList);
				
			}else {
				st.cpCityList.addAll(s.cpCityList);
				st.cpCityList.add(city);
				
				st.opCityList.addAll(s.opCityList);
			}
			
			st.setEval(Double.NEGATIVE_INFINITY);
			
			st.depth = s.depth + 1;
	
			val = Math.min(val, MaxValue(st));
			
			if(1 == turn) {
			// logs
			//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+(s.depth+1)+","+val);
				outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+val);
				outputLog.append(System.getProperty("line.separator"));	
			}
			
		}

		s.setEval(val);

		return val;
	}
	
	public static double Utility(State terminalState) {
		
		
		if(terminalState.getplayerName().equals("Confederacy")) {
			terminalState.setEval(computeEvalFunc(terminalState.opCityList) - computeEvalFunc(terminalState.cpCityList));
		}else {
			terminalState.setEval(computeEvalFunc(terminalState.cpCityList) - computeEvalFunc(terminalState.opCityList));
		}
		
		if(1 == turn) {
		// logs
		//System.out.println(terminalState.getplayerName()+","+terminalState.getAction()+","+terminalState.getDest()+","+(terminalState.depth+1)+","+terminalState.getEval());		
			outputLog.append(terminalState.getplayerName()+","+terminalState.getAction()+","+terminalState.getDest()+","+terminalState.depth+","+terminalState.getEval());
			outputLog.append(System.getProperty("line.separator"));	
		}
				
		return terminalState.getEval();
	}	
	
	public static State greedyAlgo(State cState) {
		
		    State nextState = null;
		    List<State> moves = new ArrayList<State>();
		    cState.depth = 0;
		    
		    Player cp = players.get(currPlayer);
			Player op = players.get(oppPlayer);
			
		//while(0 != unoccupiedCity.size()) {		
			turn++;
			for(String city: unoccupiedCity) {
				
				Node currNode = nodes.get(city);
				State currState = null;
				
				for(Edge e: currNode.adjEdge) {
					Node n = e.target;					
					
					if(currPlayer.equals(n.getPlayer())) {
				
						Set<String> cpCity = new TreeSet<String>();
						cpCity.addAll(cp.cityList);
						cpCity.add(currNode.getName());
						
						Set<String> opCity = new TreeSet<String>();
						opCity.addAll(op.cityList);
						
						currState = new State(currPlayer);
						currState.setAction(fm);
						currState.setDest(currNode.getName());
						
						currState.depth = cState.depth + 1;
						
						forwardMarch(currNode, currPlayer, oppPlayer, cpCity, opCity);
			
						currState.setEval(computeEvalFunc(cpCity) - computeEvalFunc(opCity));
						currState.cpCityList = cpCity;
						currState.cptotal = computeEvalFunc(currState.cpCityList);
						currState.opCityList = opCity;
						currState.optotal = computeEvalFunc(currState.opCityList);
						//playerMoves.add(currState);
						moves.add(currState);
						//System.out.println(currState.getDest()+"  Eval: "+currState.getEval());
						
						break;
					}
					
					
				}
			}
			
			for(String city: unoccupiedCity) {
				
				Node currNode = nodes.get(city);
				State currState = null;
				
				
				Set<String> cpCity = new TreeSet<String>();
				cpCity.addAll(cp.cityList);
				cpCity.add(currNode.getName());
				//cp.cityList.add(currNode.getName());
				
				Set<String> opCity = new TreeSet<String>();
				opCity.addAll(op.cityList);
				
				
				currState = new State(currPlayer);
				currState.setAction(pd);
				currState.setDest(currNode.getName());
				
				currState.depth = cState.depth + 1;
				
				double eval = computeEvalFunc(cpCity) - computeEvalFunc(opCity);
				
				currState.setEval(eval);
				
				currState.cpCityList = cpCity;
				currState.cptotal = computeEvalFunc(currState.cpCityList);
				currState.opCityList = opCity;
				currState.optotal = computeEvalFunc(currState.opCityList);
				//playerMoves.add(currState);
				moves.add(currState);
				
			}

			State currMove = greedyMoves(moves);
		
			players.get(currPlayer).cityList = currMove.cpCityList;
			players.get(currPlayer).totalRes = computeEvalFunc(currMove.cpCityList);
			players.get(oppPlayer).cityList = currMove.opCityList;
			players.get(oppPlayer).totalRes = computeEvalFunc(currMove.opCityList);
			//nodes.get(currMove.getDest()).setPlayer(currMove.getplayerName());
			captureCity(currMove);
			unoccupiedCity.remove(currMove.getDest());
			playerMoves.clear();
		//}
			currMove.cpCityList = players.get("Union").cityList;
			currMove.cptotal = players.get("Union").totalRes;
			currMove.opCityList = players.get("Confederacy").cityList;
			currMove.optotal = players.get("Confederacy").totalRes;
			displayState(currMove);
			nextState = currMove;
			return nextState;
	}

	public static double computeEvalFunc(Set<String> cityList) {
		double total = 0;
		for(String cityName:cityList) {
			Node n = nodes.get(cityName);
			total += n.getResource();
		}
		return total;
	}
	
	public static void captureCity(State currState){
		
		for(String city:currState.cpCityList) {
			nodes.get(city).setPlayer(currState.getplayerName());
		}
		
	}
	
	public static void forwardMarch(Node n,String curr, String opp, Set<String> cpCityList, Set<String> opCityList) {
		
		for(Edge e: n.adjEdge) {
			if(opCityList.contains(e.target.getName())) {
				opCityList.remove(e.target.getName());
				cpCityList.add(e.target.getName());
			}
		}
		
	}
	
	public static State greedyMoves(List<State> mv) {
		
		if(1 == turn) {
			//System.out.println("Player,Action,Destination,Depth,Value");
		}
		State nextMove = mv.get(0);
		for(State s: mv) {
			if(s.getEval() > nextMove.getEval()) {
				nextMove = s;
			}else if(s.getEval() == nextMove.getEval()) {
				if(s.getAction().equals(fm) && nextMove.getAction().equals(pd)) {
					nextMove = s;
				}
			}
			
			if(1 == turn) {
			// logs	
				//System.out.println(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+s.getEval());
				outputLog.append(s.getplayerName()+","+s.getAction()+","+s.getDest()+","+s.depth+","+s.getEval());
				outputLog.append(System.getProperty("line.separator"));		
			}
		}
		return nextMove;
	}
	
	public static void displayState(State cstate) {
		/*System.out.println("TURN = "+turn);
		System.out.println("Player = "+cstate.getplayerName());
		System.out.println("Action = "+cstate.getAction());
		System.out.println("Destination = "+cstate.getDest());		
		System.out.println("Union, " + cstate.cpCityList+", "+ cstate.cptotal);
		System.out.println("Confederacy, " + cstate.opCityList+", "+ cstate.optotal);
		System.out.println("----------------------------------------------");*/
		
		outputPath.append("TURN = "+turn+System.getProperty("line.separator"));
		outputPath.append("Player = "+cstate.getplayerName()+System.getProperty("line.separator"));
		outputPath.append("Action = "+cstate.getAction()+System.getProperty("line.separator"));
		outputPath.append("Destination = "+cstate.getDest()+System.getProperty("line.separator"));
		outputPath.append("Union,"+printList(cstate.cpCityList)+","+ cstate.cptotal+System.getProperty("line.separator"));
		outputPath.append("Confederacy,"+printList(cstate.opCityList)+","+ cstate.optotal+System.getProperty("line.separator"));
		outputPath.append("----------------------------------------------"+System.getProperty("line.separator"));
	}
	
	public static String printList(Set<String> cityList) {
		
		Iterator<String> itCity = cityList.iterator();
		StringBuilder cities = new StringBuilder();
		cities.append("{");
		while (itCity.hasNext()) {
			Object element = itCity.next();
	         cities.append(element.toString());
	         //System.out.print(element.toString());
	         if(itCity.hasNext()){
	        	 cities.append(",");
	        	 //System.out.print(",");
	         }
		}
		cities.append("}");
		
		return cities.toString(); 
	}
	
}
