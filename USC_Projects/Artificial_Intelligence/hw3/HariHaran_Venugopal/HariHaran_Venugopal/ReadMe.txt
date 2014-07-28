----------------------------------------------------------------------------------------
Name: 			Hari Haran Venugopal
Student ID: 	2041214007
Email Id:		hvenugop@usc.edu
----------------------------------------------------------------------------------------

Assignment 2 – Adversarial Search: War simulation game

The program implements the a varianwrite computer bots to play the War simulation game using Greedy (Minimax with cut-off depth = 1),
Minimax and Alpha-Beta pruning algorithms.

----------------------------------------------------------------------------------------
PROGRAM STRUCTURE:
----------------------------------------------------------------------------------------

Program consist of 6 Java/Class files :

a)  war.java :		
	This is the main class whose functions is to parse command line arguments, reads input files and determines which task to be performed
	based on task -t option Player choose Algorithms accordingly. Also Includes Greedy, Minmax and Alphabeta pruning algorithm implementation.

b)  Player.java :
	A data structure which represents all players in the game and city/nodes captured by them respectively.

c)  Node.java and Edge.java: 
	These are POJO classes which represent Node and Edge structures respectively.	
	i)  Node Structure - Name, player, resource, depth and priority level.
	ii) Edge Structure - Edge Cost and Target Node.

d)  State.java:
	This class mantains the state of the game along with list of cities occupied by both players and the action to be taken by the
	current player and next destination city/node .
	
e)  Utility.java:
	This is general utility class which contains methods to write optimal solutions and log details to the output files. 

Priority of each node is determined for tiebreaking decisions using evaluation function as described in the problem statement.
ie. Sum of resources(current_captured_city) - Sum of resources(opponent_captured_city), then alphabatical order of the city/node.

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

- Input Files: 		map1.txt, init1.txt
- Output Files:		output.txt, log.txt

Note: Dealing with tiebreak the node with higher priority is given preference based on the order in Graph.

----------------------------------------------------------------------------------------
TASK EXPLANATION
----------------------------------------------------------------------------------------

Task  1: Both Players (Union and Confederacy) play there moves using Greedy Algorithm.

Command:
java war -t 1 -d 3 -m map1.txt -i init1.txt -op output1_moves_greedy.txt -ol output1_tlog_greedy.txt

----------------------------------------------------------------------------------------

Task 2:	Player Union play moves based on Minmax Algorithm and player Canfederacy plays moves based on Greedy Algorithm.

Command:
java war -t 2 -d 3 -m map1.txt -i init1.txt -op output1_moves_minmax.txt -ol output1_tlog_minmax.txt

----------------------------------------------------------------------------------------

Task 3:	Player Union play moves based on Alpha-Beta Algorithm and player Canfederacy plays moves based on Greedy Algorithm.

Command:
java war -t 3 -d 3 -m map1.txt -i init1.txt -op output1_moves_alphabeta.txt -ol output1_tlog_alphabeta.txt

----------------------------------------------------------------------------------------


ALGORITHM ANALYSIS
----------------------------------------------------------------------------------------
8.1.4 Your analysis of similarities/differences in terms playing performance/runtime/number of iterations between task1, task2 and task3.
----------------------------------------------------------------------------------------

In Task1 Player Union chooses Greedy Algorithm which is equivalent to Minmax algorithm of depth 1,hence the runtime and number of Iterations
will be comparatively less than Task2 and Task3,but performance wise the player's move is not optimal as it doesn't take into consideration
the subsequent moves(i.e beyond depth 1) to guarantee best chances of winning the game.

In Task2 Player Union chooses Minmax Algorithm, we traverse the game tree till the given depth (Input parameter -d), hence the runtime and
number of Iteration is greater than Greedy but performance wise it determines the move, which will provide best chance of winning the game. 

In Task3 Player Union chooses Alpha–beta pruning which is a search algorithm that seeks to decrease the number of nodes that are evaluated 
by the minimax algorithm in its search tree, since  It stops completely evaluating a move when at least one possibility has been found that
proves the move to be worse than a previously examined move. 

Alpha-Beta pruning can greatly reduce the running time of the min-max algorithm. A best case analysis shows a time complexity of O(bd/2)
where b is the branching factor and d is the tree depth, hence Alpha-Beta performance(task3), runtime and no. of iteration will always be
less than or equal to Minmax algorithm.
