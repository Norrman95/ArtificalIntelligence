package pacman.controllers.BT;
import pacman.game.Constants.DM;
import pacman.game.Game;
import pacman.controllers.BT.DataContext;

public class Escape extends State
{
	public Escape(Game state, DataContext data) 
	{
		super(state, data);
	}

	@Override
	public StateValue Process()
	{
        if(data.ClosestHostileGhost(state) == true)
		{
        	data.SetCurrentMove(state.getNextMoveAwayFromTarget(state.getPacmanCurrentNodeIndex(), 
        						state.getGhostCurrentNodeIndex(data.GetCurrentGhostNode()), state.getPacmanLastMoveMade(), DM.MANHATTAN));
		  
		    return StateValue.RUNNING;
		}             				 					
		return StateValue.SUCCESS;
	}
}
