package pacman.controllers.BT;
import pacman.game.Game;

public class GhostNear extends State
{
	public GhostNear(Game state, DataContext data)
	{
		super(state,data);
	}

	public StateValue GhostsAreNear()
	{
		if(data.DistanceToClosestGhost(state) < 40)
		{
			current_node = children.get(0).process();
			if(current_node.nodeState() == StateValue.FAILURE)
			{
		        return current_node.nodeState();
			}
			else if(current_node.nodeState() == StateValue.RUNNING)
			{
		        return current_node.nodeState();				
			}
			else
			{
				return current_node.nodeState();		
			}	
		}		
		else 
		{
			return StateValue.FAILURE;
		}		
	}
	
	@Override
	public Node process() 
	{
		node_state = GhostsAreNear();
		return this;
	}
}
