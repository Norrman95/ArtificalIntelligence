package pacman.controllers.BT;
import pacman.game.Constants.DM;
import pacman.game.Constants.GHOST;
import pacman.game.Constants.MOVE;
import pacman.game.Game;

public class DataContext 
{
	MOVE currMove;
	Game state;
	GHOST ghostNode;

	public void UpdateState(Game state)
	{
		this.state = state;
	}
	
	public boolean ClosestEdibleGhost(Game state)
	{
		for(GHOST ghost : GHOST.values())
		{
			if(state.getGhostEdibleTime(ghost) != 0 && state.getGhostLairTime(ghost) == 0)
			{
				if(state.getShortestPathDistance(state.getPacmanCurrentNodeIndex(), state.getGhostCurrentNodeIndex(ghost)) < 40)
				{
					SetCurrentMove(state.getNextMoveTowardsTarget(state.getPacmanCurrentNodeIndex(), state.getGhostCurrentNodeIndex(ghost), DM.PATH));
					ghostNode = ghost;
					return true;
				}
			}	
		}
		return false;
	}	
	public boolean ClosestHostileGhost(Game state)
    {
		for(GHOST ghost : GHOST.values())
		{
			if(state.getGhostEdibleTime(ghost) == 0 && state.getGhostLairTime(ghost) == 0) 
			{
				if(state.getShortestPathDistance(state.getPacmanCurrentNodeIndex(), state.getGhostCurrentNodeIndex(ghost)) < 40)
				{
					SetCurrentMove(state.getNextMoveAwayFromTarget(state.getPacmanCurrentNodeIndex(), state.getGhostCurrentNodeIndex(ghost), DM.PATH));
					ghostNode = ghost;
					return true;
				}				
			}		
		}
		return false;
    }
	
	public int DistanceToClosestGhost(Game state)
	{
		for(GHOST ghost : GHOST.values())
		{
			if(state.getGhostEdibleTime(ghost) == 0 && state.getGhostLairTime(ghost) == 0) 
			{	
				return state.getShortestPathDistance(state.getPacmanCurrentNodeIndex(), state.getGhostCurrentNodeIndex(ghost));
			}					
		}
		return 40;
	}

	public int[] Pills(Game state)
	{
		int currPill = 0;
		int[] pills = state.getPillIndices();
		int[] availablePills = new int[state.getNumberOfActivePills()];

		for(int i = 0 ;i < pills.length;i++)		
		{
			if(state.isPillStillAvailable(i))
			{
				availablePills[currPill] = pills[i];
				currPill++;
			}
		}
		return availablePills;
	}
	
	public int[] PowerPills(Game state) 
	{
		int currPill = 0;
		int[] pills = state.getPowerPillIndices();
		int[] availablePills = new int[state.getNumberOfActivePowerPills()];

		for(int i=0;i < pills.length;i++)		
		{
			if(state.isPowerPillStillAvailable(i))
			{
				availablePills[currPill] = pills[i];
				currPill++;
			}
		}
		return availablePills;
	}
	
	public int ClosestPowerPill(Game state)
	{
		int[] availablePowerPills = PowerPills(state);
		int dist = state.getClosestNodeIndexFromNodeIndex(state.getPacmanCurrentNodeIndex(), availablePowerPills, DM.PATH);
		
		if(dist < 0)
		{
			dist = 500;
		}
		return dist;
	}
	
	public void SetCurrentGhostNode(GHOST ghost)
	{
		ghostNode = ghost;
	}
	public GHOST GetCurrentGhostNode()
	{
		return ghostNode;
	}
	public void SetCurrentMove(MOVE move)
	{
		currMove = move;
	}
	public MOVE GetCurrentMove()
	{
		return currMove;
	}
}
