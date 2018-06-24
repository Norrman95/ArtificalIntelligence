package pacman.controllers.BT;
import pacman.game.Game;
import pacman.game.Constants.DM;

public class Eat extends State 
{
    public Eat(Game state, DataContext data)
    {
    	super(state,data);
    }

    @Override
	public StateValue Process()
	{
		int[] allPills = data.Pills(state);
            	
		if(allPills.length > 0 && data.ClosestHostileGhost(state) == false )
		{
            data.SetCurrentMove(state.getNextMoveTowardsTarget(state.getPacmanCurrentNodeIndex(),
            state.getClosestNodeIndexFromNodeIndex(state.getPacmanCurrentNodeIndex(),allPills,DM.PATH),DM.PATH));
            return StateValue.RUNNING;
		}
		return StateValue.FAILURE;
	}
}
