package pacman.controllers.BT;
import java.util.ArrayList;

public class Node 
{
	public ArrayList<Node> children = new ArrayList<Node>();
	public Node current_node;
    public StateValue node_state;
	public Node parent;
	DataContext data;
	
	
	public Node process() 
	{
		return null;
	}
	
	public StateValue nodeState()
	{
		return null;
	}
	
	public void Reset()
	{
		this.current_node = null;
	}

	public void addParent(Node current_node) 
	{
		this.parent = current_node;
	}

	public void addChild(Node child) 
	{
		children.add(child);
		child.addParent(this);
	}
	
	public boolean isLeaf() 
	{
		if(children.size()==0) 
		{
			return true;
		}
		else
		{
			return false;			
		}
	}
}
