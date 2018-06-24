package pacman.controllers.BT;

import pacman.game.Constants.MOVE;
import pacman.game.Game;

public class State extends Node {
	
	DataContext data;
	Game state;

	public State(Game state, DataContext data)
	{
		this.state = state;
		this.data = data;				
	}
	
	public void UpdateState(Game state)
	{
		this.state = state;
	}
	
	public StateValue Process()
	{
		return null;
	}

	public MOVE getNextMove(Game state, int TargetPos)
	{
		return MOVE.NEUTRAL;
	}
}
