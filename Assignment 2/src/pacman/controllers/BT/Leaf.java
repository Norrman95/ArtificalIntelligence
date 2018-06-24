package pacman.controllers.BT;

public class Leaf extends Node 
{
	public Node parent;
	State state; 
	
	public Leaf(State state)
	{
		this.state = state;
	}
	
	@Override
	public Node process() 
	{
		node_state = nodeState();
		return this;	
	}

	public void addParent(Node current_node)
	{
		this.parent = current_node;
	}
	
	@Override
	public StateValue nodeState()
	{
        return state.Process();	
    }
}