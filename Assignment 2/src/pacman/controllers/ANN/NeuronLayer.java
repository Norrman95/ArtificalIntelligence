package pacman.controllers.ANN;
import java.util.ArrayList;

public class NeuronLayer 
{
	public int neurons;
	public int neuronInput;
	public ArrayList<Neuron> neuronsInLayer = new ArrayList<Neuron>();
	
	public NeuronLayer(int neurons, int neuronInput)
	{
		this.neurons = neurons;
		this.neuronInput = neuronInput;
		
		for(int i = 0; i < neurons; ++i)
		{
			neuronsInLayer.add(new Neuron(neuronInput));
		}
	}
}