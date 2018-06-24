package pacman.controllers.ANN;
import java.text.DecimalFormat;
import java.util.ArrayList;

public class Neuron 
{
	public int input;
	public ArrayList<Double> weights = new ArrayList<Double>();

	Constants consts;
	
	public Neuron(int NumberOfInputs)
	{
		consts = new Constants();
		input = NumberOfInputs;
				
		for(int i = 0; i < input; ++i)
		{
			double temp = consts.Rnd(-1, 1); 
			
			weights.add(temp);
			
			DecimalFormat format = new DecimalFormat("#.###");
			String ts = format.format(temp);
			pacman.controllers.ANN.Constants.inputWeights.add(ts);
		}
	}
}