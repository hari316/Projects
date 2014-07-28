import java.util.ArrayList;

public class Node {

	private final String name;
	private double resource = 0;
	public int depth;
	public ArrayList<Edge> adjEdge = new ArrayList<Edge>();
	public int priorityLevel = 0;
	private String player = null;

	public Node(String name) {
		super();
		this.name = name;
		this.resource = 0;
		this.depth = 0;
	}

	public String toString() {
		return name;
	}

	public double getResource() {
		return resource;
	}

	public void setResource(double resource) {
		this.resource = resource;
	}

	public String getPlayer() {
		return player;
	}

	public void setPlayer(String player) {
		this.player = player;
	}

	public String getName() {
		return name;
	}
	
	

}
