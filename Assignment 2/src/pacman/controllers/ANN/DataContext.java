package pacman.controllers.ANN;
import java.util.ArrayList;

import pacman.game.Constants.DM;
import pacman.game.Constants.GHOST;
import pacman.game.Constants.MOVE;
import pacman.game.Game;

public class DataContext 
{
	AssignmentController controller;
	int oldState;
	
	ArrayList<Integer> oldStates = new ArrayList<Integer>(); 
	
	public DataContext(AssignmentController controller)
	{
		this.controller = controller;
	}
	public ArrayList<Double> PossibleMoves(Game state)
	{
		ArrayList<Double> moves = new ArrayList<Double>(); 
		moves.add(DistanceToClosestHostileGhost(state) / 1000);
		moves.add(DistanceToClosestEdibleGhost(state) / 1000);
		moves.add(ClosestPill(state) / 1000);
		moves.add(ClosestPowerPill(state) / 1000);
		return moves;		
	}
	
	public MOVE MoveToNearestEdibleGhost(Game state)
	{
		for(GHOST ghost : GHOST.values())
		{
			if(state.getGhostEdibleTime(ghost) != 0 && state.getGhostLairTime(ghost) == 0)
			{
				if(state.getShortestPathDistance(state.getPacmanCurrentNodeIndex(), state.getGhostCurrentNodeIndex(ghost)) < 40)
				{
					return state.getNextMoveTowardsTarget(state.getPacmanCurrentNodeIndex(), state.getGhostCurrentNodeIndex(ghost), DM.PATH);
				}
			}	
		}
		return controller.move;	
	}	
	public MOVE MoveFromNearestHostileGhost(Game state)
	{
		for(GHOST ghost : GHOST.values())
		{
			if(state.getGhostEdibleTime(ghost) == 0 && state.getGhostLairTime(ghost) == 0)
			{
				if(state.getShortestPathDistance(state.getPacmanCurrentNodeIndex(), state.getGhostCurrentNodeIndex(ghost)) < 40)
				{
					return state.getNextMoveAwayFromTarget(state.getPacmanCurrentNodeIndex(), state.getGhostCurrentNodeIndex(ghost), DM.PATH);
				}
			}	
		}
		return controller.move;	
	}
	
	public MOVE MoveToNearestPowerPill(Game state)
	{		
		return state.getNextMoveTowardsTarget(state.getPacmanCurrentNodeIndex(), (int)ClosestPowerPill(state), DM.PATH);
	}
	public MOVE MoveToNearestPill(Game state)
	{		
		return state.getNextMoveTowardsTarget(state.getPacmanCurrentNodeIndex(), (int)ClosestPill(state), DM.PATH);
	}
	
	public double DistanceToClosestEdibleGhost(Game state)
	{
		double dist = 500.0;
		for(GHOST ghost : GHOST.values())
		{
			if(state.getGhostEdibleTime(ghost) != 0 && state.getGhostLairTime(ghost) == 0) 
			{	
				dist = state.getShortestPathDistance(state.getPacmanCurrentNodeIndex(), state.getGhostCurrentNodeIndex(ghost));
			}					
		}
		return dist;
	}
	public double DistanceToClosestHostileGhost(Game state)
	{
		double dist = 0.0;
		for(GHOST ghost : GHOST.values())
		{
			if(state.getGhostEdibleTime(ghost) == 0 && state.getGhostLairTime(ghost) == 0) 
			{	
				dist = state.getShortestPathDistance(state.getPacmanCurrentNodeIndex(), state.getGhostCurrentNodeIndex(ghost));
			}					
		}
		return dist;
	}
	public double ClosestPowerPill(Game state)
	{
		int[] availablePowerPills = PowerPills(state);
		
		return state.getClosestNodeIndexFromNodeIndex(state.getPacmanCurrentNodeIndex(), availablePowerPills, DM.PATH);
	}
	public double ClosestPill(Game state)
	{
		int[] availablePills = Pills(state);
		
		return state.getClosestNodeIndexFromNodeIndex(state.getPacmanCurrentNodeIndex(), availablePills, DM.PATH);
	}
	public int[] Pills(Game state)
	{
		int currPill = 0;
		int[] pills = state.getPillIndices();
		int[] availablePills = new int[state.getNumberOfActivePills()];

		for(int i=0;i < pills.length;i++)		
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
}
