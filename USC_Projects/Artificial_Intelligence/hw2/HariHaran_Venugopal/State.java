import java.util.LinkedHashSet;
import java.util.Set;

public class State{
	
	private String current_checkpoint;
	public  Set<String> visted_checkpoints = new LinkedHashSet<String>();
	//public  List<String> visted_checkpoints = new ArrayList<String>();
	
	private double g;
	private double h;
	private double f;
	
	
	public String getCurrent_checkpoint() {
		return current_checkpoint;
	}
	public void setCurrent_checkpoint(String current_checkpoint) {
		this.current_checkpoint = current_checkpoint;
	}
	public double getG() {
		return g;
	}
	public void setG(double g) {
		this.g = g;
	}
	public double getH() {
		return h;
	}
	public void setH(double h) {
		this.h = h;
	}
	public double getF() {
		return f;
	}
	public void setF(double f) {
		this.f = f;
	}


}