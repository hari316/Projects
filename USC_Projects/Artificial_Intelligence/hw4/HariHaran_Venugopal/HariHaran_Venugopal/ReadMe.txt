----------------------------------------------------------------------------------------
Name: 			Hari Haran Venugopal
Student ID: 		2041214007
Email Id:		hvenugop@usc.edu
----------------------------------------------------------------------------------------

Assignment 4 – Propositional Logic Inference Algorithm

A program to implement various inference algorithms for propositional logic (PL) . The aim of logical inference is to decide whether KB ? a for some sentence a.
We will implement 3 specific inference algorithms
1) PL Forward chaining.
2) PL Backward chaining.
3) PL Resolution.

----------------------------------------------------------------------------------------
PROGRAM STRUCTURE:
----------------------------------------------------------------------------------------

Program consist of 6 Java/Class files :

a)  pl.java :		
	This is the main class whose functions is to parse command line arguments, reads input files and determines which task to be performed
	based on task -t option we choose Algorithms accordingly. Also Includes Forward chaining, Backward chaining and Resolution algorithm implementation.
	
b)  Utility.java:
	This is general utility class which contains methods to write optimal solutions and log details to the output files. 

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

- Input Files: 		kb1.txt, q1.txt
- Output Files:		output.txt, log.txt

----------------------------------------------------------------------------------------
TASK EXPLANATION
----------------------------------------------------------------------------------------

Task 1: To determine if given query entails Knowledge Base using Forward Chaining Algorithm.

Command:
java pl -t 1 -kb kb1.txt -q q1.txt -oe output1.txt -ol log1.txt

----------------------------------------------------------------------------------------

Task 2: To determine if given query entails Knowledge Base using Backward Chaining Algorithm.

Command:
java pl -t 2 -kb kb1.txt -q q1.txt -oe output1.txt -ol log1.txt
----------------------------------------------------------------------------------------

Task 3:	To determine if given query entails Knowledge Base using Resolution Algorithm.

Command:
java pl -t 3 -kb kb1.txt -q q1.txt -oe output1.txt -ol log1.txt

----------------------------------------------------------------------------------------


ALGORITHM ANALYSIS
----------------------------------------------------------------------------------------
8.1.4 Your analysis of similarities/differences in terms of results between task1, task2 and task3.
----------------------------------------------------------------------------------------

In Task1 : 

Forward Chaining is data-driven, automatic, unconscious processing and have to do lots of work(newly entailed facts) that is irrelevant to the goal.
One of the advantages of forward-chaining over backward-chaining is that the reception of new data can trigger new inferences,
which makes the engine better suited to dynamic situations in which conditions are likely to change.

In Task2 :

Backward Chaining is goal-driven, appropriate for problem-solving and complexity is much less than linear size of KB. 
The opposite of forward chaining is backward chaining.
- Avoid loops: check if new subgoal is already on the goal. 
- Avoid repeated work: check if new subgoal has already been proved true or false.

We aim at showing the truth of a given query q by considering those implications in the knowledge base that conclude q (have it as the head)
• By backward chaining one examines the whether all the premises of one of those implications can be proved true.
• Backward chaining touches only relevant facts, while forward chaining blindly produces all facts.

PERFORMANCE:

In Best case both Forward Chaining and Backward Chaining will perform equally with same number of Iterations where as in worst case Backward Chaining will 
have less number of iteration i.e perform better, since its goal driven and avoids loop check (CYCLE DETECTION) and repeated work.

In Task3 :

Is a rule of inference leading to a refutation theorem-proving technique for sentences in propositional logic and first-order logic.
In other words, iteratively applying the resolution rule in a suitable way allows for telling whether a propositional formula is satisfiable and
for proving that a first-order formula is unsatisfiable. Only issue is resolution only works for Knowledge Base in Conjunctive Normal Form.
As per the implementation we try to resolve all possible pairs of clauses in CNF until we get an "Empty" set hence it takes long time to compute(iterations)
when compared to the performance of Forward and Backward Chaining alogorithms.