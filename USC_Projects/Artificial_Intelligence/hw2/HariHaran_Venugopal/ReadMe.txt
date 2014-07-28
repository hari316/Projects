----------------------------------------------------------------------------------------
Name: 		Hari Haran Venugopal
Student ID: 	2041214007
Email Id:	hvenugop@usc.edu
----------------------------------------------------------------------------------------

Assignment 2 – A* Search Algorithms

The program implements the a variant of the traveling salesman problem (TSP) using A* search algorithm with two heuristics: 1) Manhattan distance
2) minimum spanning tree.

----------------------------------------------------------------------------------------
PROGRAM STRUCTURE:
----------------------------------------------------------------------------------------

NOTE: Prims Algorithm is used to implement Minimum Spanning Tree.

Program consist of 6 Java/Class files :

a)  tsp.java :		
	This is the main class whose functions is to parse command line arguments, reads input files and determines which task to be performed based
	on task -t option accordingly. Also Includes A* algorithm implementation using Manhattan distance and Minimum spanning tree.

b)  Box.java :
	A data structure which represents each cell in the maze along with X axis and Y axis coordinate.

c)  Node.java and Edge.java: 
	These are POJO classes which represent Node and Edge structures respectively.	
	i)  Node Structure - Name, pathCost, parent, list of edges, depth and visited boolean varaible.
	ii) Edge Structure - Edge Cost and Target Node.

d)  State.java:
	This class mantains the state of the Traversal sales man route along with list of visited and not visited checkpoints.
	
e)  Utility.java:
	This is general utility class which contains methods to write optimal solutions and log details to the output files. 

Priority of each node is determined for tiebreaking decisions using postion of the node in the Maze.

----------------------------------------------------------------------------------------
STEPS TO COMPILE AND EXECUTE:
----------------------------------------------------------------------------------------

Java Version: 1.6

setenv JAVA_HOME /usr/usc/jdk/1.6.0_23 
setenv PATH /usr/usc/jdk/1.6.0_23/bin:${PATH}

1) Unzip folder : HariHaran_Venugopal.zip
2) cd  HariHaran_Venugopal
3) javac *.java
4) Place the input files in the folder HariHaran_Venugopal

INPUT PARAMETERS:

- Input Files: 		map1.txt, map2.txt
- Output Files:		output.txt, log.txt

Note: Dealing with tiebreak the node with higher priority is given preference based on the order in Graph.

----------------------------------------------------------------------------------------
TASK EXPLANATION
----------------------------------------------------------------------------------------

Task  1: To construct a shortest path graph by calculating the pairwise distance for all checkpoints on the map.

Command:
java tsp -t 1 -i map1.txt -op output1_path_sgraph.txt -ol output1_tlog_sgraph.txt

----------------------------------------------------------------------------------------

Task 2:	Find the shortest way for the robot to visit all the checkpoints exactly once and return to its starting checkpoint.

Command:
java tsp -t 2 -i map1.txt -op output2_path_sgraph.txt -ol output2_tlog_sgraph.txt

----------------------------------------------------------------------------------------


HEURISTIC DISTANCE
----------------------------------------------------------------------------------------
9.1.3 If we use Euclidean (straight line) distance as the heuristic (rather than Manhattan distance) for Task 1
----------------------------------------------------------------------------------------

The number of iteration (i.e., nodes explored) is higher in number when Euclidean distance is choosen as the Heuristic because the distance
calculated is the shortest (i.e path along diagnal) where as in case of maze traversal along a path is restricted to vertical and horizaontal axis,
So the Heuristic choosen understimates the path cost to goal node, where as in case of manhattan distance the cost is optimal to goal node.
The feature of a good Heuristic is to estimate a path cost from current node to goal node which is in close proximity to actual poth cost which in this
case (Maze problem) Manhattan distance provides a better Heuristic.
