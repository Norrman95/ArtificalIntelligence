package pacman.controllers.BT;

public class Sequence extends Node
{
	public Node ChildNodes()
	{
		for(int i =0; i < children.size(); i++)
		{
			current_node = children.get(i).process();
			if(current_node.nodeState() == StateValue.FAILURE)
			{
		         return current_node;
			}			
			else if(current_node.nodeState() == StateValue.RUNNING)
			{
		         return current_node;
			}											
		}
		return current_node;
	}
	
	@Override
	public Node process()
	{      
        return ChildNodes();
	}
	
	@Override
	public StateValue nodeState()
	{
		return this.current_node.nodeState();
	}
}