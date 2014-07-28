import java.util.ArrayList;
import java.util.Arrays;
import java.util.Comparator;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.TreeSet;

public class pl {

	public static StringBuilder output = new StringBuilder();
	public static StringBuilder logs = new StringBuilder();
	
	public static void main(String[] args) {
		
		int index = 0;
		int taskId = 0;
		
		String inputFile = null;
		String outputFile = null;
		String queryFile = null;
		String logFile = null;
		
		for(index = 0;index < args.length; index++) {
			if(args[index].contentEquals("-t")) {
				taskId = Integer.parseInt(args[++index].toString());
			}else if(args[index].contentEquals("-kb")) {
				inputFile = args[++index];
			}else if(args[index].contentEquals("-q")) {
				queryFile = args[++index];
			}else if(args[index].contentEquals("-oe")) {
				outputFile = args[++index];
			}else if(args[index].contentEquals("-ol")) {
				logFile = args[++index];
			}
		}
	
		List<String> KB = Utility.readFromFile(inputFile);
		List<String> qList = Utility.readFromFile(queryFile);
		//taskId =3;
		switch(taskId) {
		
		case 1: 
			logs.append("<Known/Deducted facts>#Rules Fires#NewlyEntailedFacts\n");
			for(String q: qList) {
				boolean res = forwardChaining(KB, q);
				if(true == res) {
					output.append("YES\n");
				}else {
					output.append("NO\n");
				}				
			}
			//System.out.println(output);
			//System.out.println(logs);
			Utility.writeToFile(outputFile, output.toString());
			Utility.writeToFile(logFile, logs.toString());
			
			break;
			
		case 2:
			logs.append("<Queue of Goals>#Relevant Rules/Fact#New Goal Introduced\n");
			for(String q: qList) {
				boolean res = backwardChaining(KB, q);
				if(true == res) {
					output.append("YES\n");
				}else {
					output.append("NO\n");
				}				
			}
			//System.out.println(output);
			//System.out.println(logs);
			Utility.writeToFile(outputFile, output.toString());
			Utility.writeToFile(logFile, logs.toString());
			
			break;
			
		case 3:
			logs.append("Resolving clause 1#Resolving clause 2#Added clause");
			for(String q: qList) {
				boolean res = resolution(KB, q);
				logs.append("\n-------------------------------------------------------------");
				if(true == res) {
					output.append("YES\n");
				}else {
					output.append("NO\n");
				}				
			}
			//System.out.println(output);
			//System.out.println(logs);
			Utility.writeToFile(outputFile, output.toString());
			Utility.writeToFile(logFile, logs.toString());
			break;
		default:
			System.out.println("Invalid Task ID !! please try again.");
			break;
		}
		
		System.out.println("Completed Processing Successfully !!");
	}
	
	public static boolean resolution(List<String> KB, String q) {
		
		int count =1;
		int matchCount = 0;
		boolean isSubset = false;
		List<String> cs = convertToCNF(KB);
		Set<String> clauses = new LinkedHashSet<String>(cs);
		clauses.add("-"+q);
		List<String> newClause = new ArrayList<String>();
		
		while(true) {
			logs.append("\nITERATION = "+count++);
			String Ci = null;
			String Cj = null;
			int i=0;
			//for(String Ci:clauses) {
			List<String> clausesList = new ArrayList<String>(clauses);
			for(i=0; i<clausesList.size();i++) {
				Ci=clausesList.get(i).trim();
				//for(String Cj: clauses) {	
				for(int j=i+1; j<clausesList.size();j++) {
					Cj=clausesList.get(j).trim();
					if(Cj.equals(Ci)) {
						continue;
					}
					String[] chArr = Ci.split("\\|");
					String resolvents = new String();
					int sign = 0;
					matchCount = 0;
					
					for(String ch:chArr) {
						ch = ch.trim();						
						sign = 0;
						if(ch.contains("-") && !Cj.contains(ch)) {
							sign = 1;
							if(Cj.contains(ch.replace("-", ""))) {
								if(1 < matchCount) {
									break;
								}
								matchCount++;
							}
						}
						else if(0 == sign && Cj.contains("-"+ch)) {
							if(1 < matchCount) {
								break;
							}
							matchCount++;													
						}						
					}
					
					if(1 == matchCount) {
						for(String ch:chArr) {
							ch = ch.trim();
							
							sign = 0;
							if(ch.contains("-") && !Cj.contains(ch)) {
								
								sign = 1;
								ch = ch.replace("-", "");
								if(Cj.contains(ch)) {
									resolvents = resolve(Ci, Cj, ch, sign);
									if(0 == resolvents.length()) {
										logs.append("\n"+Ci.replaceAll("\\|", " OR ")+" # "+ Cj.replaceAll("\\|", " OR ")+" # "+ "Empty");									
										return true;
									}
									logs.append("\n"+Ci.replaceAll("\\|", " OR ")+" # "+ Cj.replaceAll("\\|", " OR ")+" # "+ resolvents.replaceAll("\\|", " OR "));
									if(!newClause.contains(resolvents)) {
										newClause.add(resolvents);
									}
									break;
								}
							}
							else if(0 == sign && Cj.contains("-"+ch)) {
								
								resolvents = resolve(Ci, Cj, ch, sign);
								if(0 == resolvents.length()) {
									logs.append("\n"+Ci.replaceAll("\\|", " OR ")+" # "+ Cj.replaceAll("\\|", " OR ")+" # "+ "Empty");
									return true;
								}
								logs.append("\n"+Ci.replaceAll("\\|", " OR ")+" # "+ Cj.replaceAll("\\|", " OR ")+" # "+ resolvents.replaceAll("\\|", " OR "));
								
								if(!newClause.contains(resolvents)) {
									newClause.add(resolvents);
								}
								break;						
							}
							
						}
					}
				
				}
				
			}

			isSubset = true;
			for(String eC: newClause) {
				if(!clauses.contains(eC)) {
					isSubset = false;
					break;
				}
			}
			if(isSubset) {
				return false;
			}else {	
				if(!clauses.contains(newClause)) {
					clauses.addAll(newClause);
				}
			}
		}

	}
	
	public static String resolve(String ci, String cj, String ch, int sign) {
		String res = null;
		String str1 = null;
		String str2 = null;
	
		if(1 == sign) {
			if(ci.contains("|-"+ch)) {
				str1 = ci.replace("|-"+ch, "");
			}else if(ci.contains("-"+ch+"|")) {
				str1 = ci.replace("-"+ch+"|", "");
			}else{
				str1 = ci.replace("-"+ch, "");
			}
			
			if(cj.contains("|"+ch)) {
				str2 = cj.replace("|"+ch, "");
			}else if(cj.contains(ch+"|")) {
				str2 = cj.replace(ch+"|", "");
			}else {
				str2 = cj.replace(ch, "");
			}			
		}else {
			if(ci.contains("|"+ch)) {
				str1 = ci.replace("|"+ch, "");
			}else if(ci.contains(ch+"|")) {
				str1 = ci.replace(ch+"|", "");
			}else {
				str1 = ci.replace(ch, "");
			}
			
			if(cj.contains("|-"+ch)) {
				str2 = cj.replace("|-"+ch, "");
			}else if(cj.contains("-"+ch+"|")) {
				str2 = cj.replace("-"+ch+"|", "");
			}else{
				str2 = cj.replace("-"+ch, "");
			}
			
		}
		if(str1.length() > 0 && str2.length() > 0) {
			str2 = "|"+str2;
			res = str1+str2;
		}else if(str1.length() > 0) {
			res = str1;
		}else{
			res = str2;
		}
		
		TreeSet<String> unique = new TreeSet<String>(Arrays.asList(res.split("\\|")));
		unique = (TreeSet<String>) unique.descendingSet();
		Iterator<String> iterator = unique.iterator();
		StringBuilder resolvent = new StringBuilder();
		while (iterator.hasNext()){
		        //System.out.println(iterator.next() + " ");
			resolvent.append(iterator.next());
			if(iterator.hasNext()) {
				resolvent.append("|");
			}
		}
		
		String[] symbols = resolvent.toString().split("\\|");
		Arrays.sort(symbols,new customSort());
		resolvent.setLength(0);
		for (int i = 0, il = symbols.length; i < il; i++) {
	        if (i > 0)
	        	resolvent.append("|");
	        resolvent.append(symbols[i]);
	    }
		
		return resolvent.toString();
	}
	
	public static List<String> convertToCNF(List<String> KB) {
		
		List<String> clauses = new ArrayList<String>();
		for(String clause: KB) {
			String[] clauseArr = clause.split(":-");
			//Arrays.sort(clauseArr);
			if(1 == clauseArr.length) {
				clauses.add(clauseArr[0].trim());
			}else {
				if(1==clauseArr[1].trim().length()) {
					clauses.add(clauseArr[0].trim()+"|"+"-"+clauseArr[1].trim());
				}else {
					StringBuilder str = new StringBuilder();
					str.append(clauseArr[0].trim());
					String[] symbols = clauseArr[1].split(",");
					
					for(String s:symbols) {
						s = s.trim();
						str.append("|"+"-"+s);
					}
					symbols = str.toString().split("\\|");
					Arrays.sort(symbols,new customSort());
					str.setLength(0);
					for (int i = 0, il = symbols.length; i < il; i++) {
				        if (i > 0)
				            str.append("|");
				        str.append(symbols[i]);
				    }
					
					clauses.add(str.toString());
				}
			}
			
		}
		return clauses;
	}
	
	public static class customSort implements Comparator<String> {
		@Override
		public int compare(String o1, String o2) {
			o1 = o1.trim();
			o2 = o2.trim();
			if(o1.contains("-") && o2.contains("-"))
				return o1.compareTo(o2);
			return o2.compareTo(o1);
		}
	}
	
	public static boolean forwardChaining(List<String> KB, String q) {
		
		//System.out.println(q);
		
		Map<String,List<String>> count = new HashMap<String, List<String>>();
		Map<String, Boolean> inferred = new HashMap<String, Boolean>();
		List<String> agenda = new ArrayList<String>();
		Set<String> knownfacts = new TreeSet<String>();
		List<String> rules = new ArrayList<String>();
		
		Map<String, String> head = new HashMap<String, String>();
		
		computeKnowledgeBase(KB, count, inferred, agenda);
		knownfacts.addAll(agenda);
		//System.out.println(count.keySet());
		if(agenda.contains(q)) {
			logs.append(knownfacts.toString().replace("[","").replace("]", "")+"#"+"N/A"+" # "+"N/A"+"\n");
			logs.append("-------------------------------------------------------------\n");
			return true;
		}
		
		while(0 != agenda.size()) {
			String p = agenda.remove(0);
			if(false == inferred.get(p)) {
				inferred.put(p, true);
				for(String clause: count.keySet()) {
					
					List<String> premises = count.get(clause);
					premises.remove(p);
					
					if(0 == premises.size() && !rules.contains(clause)) {
						
						rules.add(clause);
												
						String[] h = clause.split(":-");
						head.put(clause, h[0].trim());
						
						logs.append(knownfacts.toString().replace("[","").replace("]", "")+"#"+clause+" # "+h[0]+"\n");
						knownfacts.add(h[0].trim());
						
						//System.out.println("q = "+ q+" "+head.get(clause));
						if(q.equals(head.get(clause))) {
							
							logs.append("-------------------------------------------------------------\n");
							return true;
						}
						
						agenda.add(h[0].trim());
					}
				}
			}
		}
		logs.append("-------------------------------------------------------------\n");
		return false;
	}
	
	
	
public static boolean backwardChaining(List<String> KB, String q) {
		
		Map<String,List<String>> count = new LinkedHashMap<String, List<String>>();
		Map<String, Boolean> inferred = new HashMap<String, Boolean>();
		List<String> agenda = new ArrayList<String>();
		Set<String> knownfacts = new HashSet<String>();
		List<String> rules = new ArrayList<String>();
		boolean flag = false;
		computeKnowledgeBase(KB, count, inferred, agenda);
		knownfacts.addAll(agenda);
		
		if(agenda.contains(q)) {
			logs.append(q+" # "+q+" # "+"N/A\n");
			logs.append("-------------------------------------------------------------\n");
			
			return true;
		}
		
		for(String clause: count.keySet()) {
			String[] h = clause.split(":-");
			if(q.equals(h[0].trim())) {
				inferred.put(q, true);
				if(rules.contains(clause)) {
					//logs.append(q+" # "+"CYCLE DETECTED"+" # "+"N/A\n");
				}else {
					rules.add(clause);
					List<String> premises = count.get(clause);
					logs.append(q+" # "+clause+" # "+premises.toString().replace("[","").replace("]", "")+"\n");
					for(String s: premises) {
						if(validatePremises(s, rules, count, inferred, agenda)){
							agenda.add(s);
							flag = true;
						}else {
							flag = false;
							break;
						}
					}
					if(flag) {
						logs.append("-------------------------------------------------------------\n");
						return true;
					}
				}
			}
		}
		
		if(!inferred.get(q)) {
			logs.append(q+" # N/A # N/A\n");
		}
		
		logs.append("-------------------------------------------------------------\n");
	
		return false;
	}
	
	public static boolean validatePremises(String q, List<String> rules, Map<String,List<String>> count, Map<String, Boolean> inferred, List<String> agenda) {
	
		if(agenda.contains(q)) {
			logs.append(q+" # "+q+" # "+"N/A\n");
			return true;
		}
		boolean flag = false;
		for(String clause: count.keySet()) {
			String[] h = clause.split(":-");
			if(q.equals(h[0].trim())) {
				inferred.put(q, true);
				if(rules.contains(clause)) {
					logs.append(q+" # "+"CYCLE DETECTED"+" # "+"N/A\n");
				}else {
					rules.add(clause);
					List<String> premises = count.get(clause);
					logs.append(q+" # "+clause+" # "+premises.toString().replace("[","").replace("]", "")+"\n");
					for(String s: premises) {
						if(validatePremises(s, rules, count, inferred, agenda)){
							agenda.add(s);
							flag = true;
						}else {
							flag = false;
							break;
						}
					}
					if(flag)
					return true;
					
				}
			}
		}
		if(!inferred.get(q)) {
			logs.append(q+" # N/A # N/A\n");
		}
		
		return false;
	}
	
	public static void computeKnowledgeBase(List<String> KB, Map<String,List<String>> count, Map<String, Boolean> inferred, List<String> agenda ) {
		
		for(String clause: KB) {
			String[] clauseArr = clause.split(":-");
			inferred.put(clauseArr[0].trim(), false);
			if(null == inferred.get(clauseArr[0])) {
				inferred.put(clauseArr[0].trim(), false);
			}
			if(1 == clauseArr.length) {
				agenda.add(clauseArr[0].trim());
			}else {
				String[] symbols = clauseArr[1].split(",");
				for(String s:symbols) {
					s = s.trim();
					inferred.put(s, false);
					List<String> premises = count.get(clause); 
					if(null == premises) {
						premises = new ArrayList<String>();
						premises.add(s);
						count.put(clause, premises);
					}else {
						premises.add(s);
						count.put(clause, premises);
					}
				}
			}
			
		}
	}
}
