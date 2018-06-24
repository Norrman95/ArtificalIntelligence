package pacman.controllers.BT;

import pacman.game.Game;
import pacman.game.Constants.DM;

public class PowerUp extends State 
{
	public PowerUp(Game state, DataContext data) 
	{
		super(state, data);
	}

	@Override
	public StateValue Process()
	{
		int[] powerPills;
		int closestPill;
		int closestGhost;
		int path;
		
		powerPills = data.PowerPills(state);
		closestPill = data.ClosestPowerPill(state);
		closestGhost = data.DistanceToClosestGhost(state);
		path = state.getShortestPathDistance(state.getPacmanCurrentNodeIndex(), closestPill);
		
		if(powerPills.length > 0 && path < 40 && path > 0 || data.Pills(state).length==0)
		{
			if(closestGhost <= closestPill) 
			{
				return StateValue.FAILURE;
			}
			
			data.SetCurrentMove(state.getNextMoveTowardsTarget(state.getPacmanCurrentNodeIndex(),
								state.getClosestNodeIndexFromNodeIndex(state.getPacmanCurrentNodeIndex(),powerPills,DM.PATH),DM.PATH));
			
			return StateValue.RUNNING;		
		}
		return StateValue.SUCCESS;	
	}
}