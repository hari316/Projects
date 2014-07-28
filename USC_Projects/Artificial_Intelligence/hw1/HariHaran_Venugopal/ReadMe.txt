----------------------------------------------------------------------------------------
Name: 		Hari Haran Venugopal
Student ID: 	2041214007
Email Id:	hvenugop@usc.edu
----------------------------------------------------------------------------------------

Assignment 1 – Search Algorithms

The program implements the following search algorithms Breadth-first search, Depth-first search and Uniform-cost search and determines the connected components for a given undirected weighted graph.
Program Structure:

----------------------------------------------------------------------------------------
PROGRAM STRUCTURE:
----------------------------------------------------------------------------------------

Program consist of 5 Java/Class files :

a)  search.java :		
	This is the main class whose functions is to parse command line arguments, reads input files and determines which task to be performed based on task -t option accordingly.

b)  Algorithms.java : 	
	This class contains the implementation of Breath First Search, Uniform Cost Search and Depth First Search Algorithms.

c)  Node.java and Edge.java: 
	These are POJO classes which represent Node and Edge structures respectively.	
	i)  Node Structure - Name, pathCost, parent, list of edges, depth and visited boolean varaible.
	ii) Edge Structure - Edge Cost and Target Node.

d)  Utility.java:
	This is general utility class which contains methods to write optimal solutions and log details to the output files. 

Priority of each node is mantained for tiebreaking decisions using tiebreaking file where in order determines the priority level.

----------------------------------------------------------------------------------------
STEPS TO COMPILE AND EXECUTE:
----------------------------------------------------------------------------------------

Java Version: 1.6

setenv JAVA_HOME /usr/usc/jdk/1.6.0_23 
setenv PATH /usr/usc/jdk/1.6.0_23/bin:${PATH}

1) Unzip folder : HariHaran_Venugopal.zip
2) cd  HariHaran_Venugopal
3) javac *.java
4) Place input file and tiebreaking file in the folder HariHaran_Venugopal

INPUT PARAMETERS:

- Input Files: 		input.txt, tiebreak.txt
- Output Files:		output.txt, log.txt
- Start Node:		For task 1,2,3.
- Goal Node:		For task 1,2,3.

Note: Dealing with tiebreak the node with higher priority is given preference based on the order mentioned in tiebreak input file.

----------------------------------------------------------------------------------------
TASK EXPLANATION
----------------------------------------------------------------------------------------

Task  1: Find a path between two specific nodes with Breadth-first search in Graph-Search version

Command
java search -t 1 -s Alice -g Noah -i input1.txt -t tiebreaking1.txt -op output1_path_t1.txt -ol output1_tlog_t1.txt

Explanation:

Breadth First Search (BFS) is a uninformed search algorithm that begins at the root node and explores all the neighboring nodes. Then for each of those nearest nodes, it explores their unexplored neighbor nodes, and so on, until it finds the goal.

Queuing Scheme: First In First Out.
Time Complexity:  b^d
Space Complexity: b^d
Complete if b is finite.
Optimal Solution if cost is constant per cost.
----------------------------------------------------------------------------------------

Task 2:	Find a path between two specific nodes with Depth-first search in Graph-Search version.

Command:
java search -t 2 -s Alice -g Noah -i input1.txt -t tiebreaking1.txt -op output1_path_t2.txt -ol output1_tlog_t2.txt

Explanation:

Depth First Search (DFS) is an uninformed search that progresses by expanding the first child node of the search tree that appears and thus going deeper and deeper until a goal node is found, or until it hits a node that has no children.
Then the search backtracks, returning to the most recent node it hasn't finished exploring. 

Queuing Scheme: Last In First Out (stack).
Time Complexity:  b^m
Space Complexity: bm
Not Complete fails in infinite state-space.
Not Optimal solution.
----------------------------------------------------------------------------------------

Task 3: Find the optimal path between two specific nodes with Uniform-cost search in Graph-Search version.
The optimal path is the path that has minimal delivery time end to end.

Command
java search -t 3 -s Alice -g Noah -i input1.txt -t tiebreaking1.txt -op output1_path_t3.txt -ol output1_tlog_t3.txt

Explanation:

Uniform Cost Search (UCS) is a uninformed search a variant of best-first search where in first visits the node with the shortest path costs (sum of edge weights) from the root node.

Queuing Scheme: Priority Queue based on path cost(First In First Out).
Time Complexity:  b^d
Space Complexity: b^d
Complete If step cost >= e > 0
Optimal solution.

Note: Breadth-first search (BFS) is a special case of uniform-cost search when all edge costs are positive and identical. 

----------------------------------------------------------------------------------------

Task 4: Find communities (connected components) using breadth-first search or depth-first search. Specify in the readme.txt which algorithm that you use.

Command
java search -t 4 -i input1.txt -t tiebreaking1.txt -op output1_cc_t4bfs.txt -ol output1_tlog_t4bfs.txt

Output:

Alice,Ben,Dan,Emma,Gil,Helen,Lena,Noah
Claire,Ian,Mark
Frank,Jenny,Kevin

Explanation:

Algorithm chosen to perform Task 4 is Breath First Search to determine all the connected components.

If Uniform Cost Search used to determine the connected component, we will get same results:

Alice,Ben,Dan,Emma,Gil,Helen,Lena,Noah
Claire,Ian,Mark
Frank,Jenny,Kevin

But Log trace will be different since node with lower path cost will be given the higher preference over other neighboring nodes. 

Time Complexity and Space Complexity can be worst than Breadth First Search Algorithm i.e

Let C* be the cost of the least-cost goal O(b^(C*/e)), possibly C*/e >> d
 
Time Complexity:  b^(d+1)
Space Complexity: b^(d+1)