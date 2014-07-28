import java.util.ArrayList;

public class Node {

	public final String name;
	public double pathCost = 0;
	public Node parent;
	public ArrayList<Edge> adjEdge = new ArrayList<Edge>();
	public boolean visited=false;
	public int depth = 0;

	public Node(String name) {
		super();
		this.name = name;
		this.pathCost = 0;
	}

	public  String toString() {
		return name;

	}

}
