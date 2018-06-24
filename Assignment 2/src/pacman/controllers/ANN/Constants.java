package pacman.controllers.ANN;
import java.util.ArrayList;
import java.util.Random;

public class Constants 
{
	public static ArrayList<String> inputWeights = new ArrayList<String>();
	int numberOfInputs = 4;
	int neuronsPerHiddenLayer = 4;
	int numberOfOutputs = 2;

	public Constants()
	{
		
	}
	
	public double Rnd(double startVal, double endVal)
	{
		double random = new Random().nextDouble();
		double result = startVal + (random * (endVal - startVal));		
		return result;		
	}
}
