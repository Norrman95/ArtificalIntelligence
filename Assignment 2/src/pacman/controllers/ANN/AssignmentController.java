package pacman.controllers.ANN;
import java.util.ArrayList;
import pacman.controllers.Controller;
import pacman.game.Constants.MOVE;
import pacman.game.Game;

public class AssignmentController extends Controller<MOVE>
{
	ArrayList<Double> input = new ArrayList<Double>();
	ArrayList<Double> output = new ArrayList<Double>();

	NeuralNet neuralNet;
	DataContext dataContext; 
	
	MOVE move;
	
	public AssignmentController()
	{
		neuralNet = new NeuralNet();
		dataContext = new DataContext(this);
	}
	
	//0 = hostileGhost
	//1 = edibleGhost
	//2 = pill
	//3 = power
	
	@Override
	public MOVE getMove(Game state, long timeDue) 
	{
		input.clear();
		input = dataContext.PossibleMoves(state);
		System.out.println("inputs:" + input);
		
		output = neuralNet.UpdateWeights(input);
		System.out.println("outputs:"+ output);	
		
		if(output.get(2) > output.get(0) && output.get(2) > output.get(3))
		{
			move = dataContext.MoveToNearestPill(state);	
		}	
		if(output.get(3) > output.get(0) && output.get(3) > output.get(2))
		{			
			move = dataContext.MoveToNearestPowerPill(state);
		}
		if(output.get(0) > output.get(2) && output.get(0) > output.get(3))
		{		
			move = dataContext.MoveFromNearestHostileGhost(state);				
		}
		if(output.get(1) > output.get(2) && output.get(1) > output.get(3))
		{
			move = dataContext.MoveToNearestEdibleGhost(state);
		}	
		return move;
	}
}
