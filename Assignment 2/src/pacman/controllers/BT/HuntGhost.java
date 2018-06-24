package pacman.controllers.BT;
import pacman.game.Game;

public class HuntGhost extends State
{
	public HuntGhost(Game state, DataContext data) 
	{
		super(state,data);
	}

	@Override
	public StateValue Process()
	{	    
		if(data.ClosestEdibleGhost(state) == true)
        {             
			return StateValue.RUNNING;        			
        }
		else
		{
			return StateValue.SUCCESS;
		}		
	}
}