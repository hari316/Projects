import java.util.ArrayList;

public class Node {

	private final String name;
	private double pathCost = 0;
	private Node parent;
	public ArrayList<Edge> adjEdge = new ArrayList<Edge>();
	private boolean visited=false;
	private int depth = 0;

	public Node(String name) {
		super();
		this.name = name;
		this.pathCost = 0;
	}

	public String toString() {
		return name;

	}

	public double getPathCost() {
		return pathCost;
	}

	public void setPathCost(double pathCost) {
		this.pathCost = pathCost;
	}

	public Node getParent() {
		return parent;
	}

	public void setParent(Node parent) {
		this.parent = parent;
	}

	public boolean isVisited() {
		return visited;
	}

	public void setVisited(boolean visited) {
		this.visited = visited;
	}

	public int getDepth() {
		return depth;
	}

	public void setDepth(int depth) {
		this.depth = depth;
	}

	public String getName() {
		return name;
	}
	
	

}
