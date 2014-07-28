import java.util.Set;
import java.util.TreeSet;


public class State {

	private final String playerName;
	private String action;
	private String dest;
	private double eval;
	public int depth;
	public Set<String>cpCityList = new TreeSet<String>();
	public double cptotal;
	public Set<String>opCityList = new TreeSet<String>();
	public double optotal;
	
	//public Set<State> childStates = new TreeSet<State>();
	
	public State(String name) {
		super();
		this.playerName = name;
	}
	public String getAction() {
		return action;
	}
	public void setAction(String action) {
		this.action = action;
	}
	public String getDest() {
		return dest;
	}
	public void setDest(String dest) {
		this.dest = dest;
	}
	public double getEval() {
		return eval;
	}
	public void setEval(double eval) {
		this.eval = eval;
	}
	
	public String getplayerName() {
		return playerName;
	}
	
	
}
