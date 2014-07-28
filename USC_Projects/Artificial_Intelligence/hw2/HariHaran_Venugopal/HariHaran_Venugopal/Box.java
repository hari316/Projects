public class Box {

	private int X;
	private int Y;
	private int priority=0;
	
	private boolean isTraversible = true;
	
	private Box parent;
	
	private double g = 0;
	private double h = 0;
	private double f = 0;
	
	public Box(int x, int y) {
		super();
		X = x;
		Y = y;
	}

	public Box(Box b) {
		this.X = b.getX();
		this.Y = b.getY();
		this.f = b.getF();
		this.g = b.getG();
		this.h = b.getH();
		this.isTraversible = b.isTraversible;
		this.parent = b.getParent();
	}

	
	
	public int getX() {
		return X;
	}

	public void setX(int x) {
		X = x;
	}

	public int getY() {
		return Y;
	}

	public void setY(int y) {
		Y = y;
	}
	
	
	public int getPriority() {
		return priority;
	}

	public void setPriority(int priority) {
		this.priority = priority;
	}

	public boolean isTraversible() {
		return isTraversible;
	}

	public void setTraversible(boolean isTraversible) {
		this.isTraversible = isTraversible;
	}

	public Box getParent() {
		return parent;
	}

	public void setParent(Box parent) {
		this.parent = parent;
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
	
	@Override
	public String toString() {
		
		System.out.println("X: "+X+" and Y: "+Y);
		return "";
		
	}
	

}
