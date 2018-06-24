package pacman.controllers.ANN;
import java.util.ArrayList;

public class NeuralNet 
{
	int numberOfInputs;
	int numberOfOutputs;
	int neuronsPerHiddenLayer;	
	int hiddenLayers;
	
	ArrayList<NeuronLayer> layers = new ArrayList<NeuronLayer>();
	
	Constants consts;
	
	public NeuralNet()
	{
		consts = new Constants();
		numberOfInputs = consts.numberOfInputs;
		numberOfOutputs = consts.numberOfOutputs;
		neuronsPerHiddenLayer = consts.neuronsPerHiddenLayer;
		
		hiddenLayers = 1;
		CreateNeuralNet();
	}
	
	void CreateNeuralNet()
	{
		layers.add(new NeuronLayer(neuronsPerHiddenLayer, numberOfInputs));
		layers.add(new NeuronLayer(numberOfOutputs, neuronsPerHiddenLayer));
	}

	double Sigmoid(double actvation, double response)
	{
		return (1/(1+ Math.exp(-actvation/response)));
	}
	
	//Z = sum FIELD_VALUE * INPUT_WEIGHT 
	//Z = sigmoid(Z) // Hidden
	
	//Z = Z * INPUT_WEIGHT
	//Z = sigmoid(Z) // Output
	
	ArrayList<Double> UpdateWeights(ArrayList<Double> inputs)
	{
		ArrayList<Double> outputs = new ArrayList<Double>();

		if(inputs.size() != numberOfInputs)
		{
			return outputs;
		}

		for (int i = 0; i < hiddenLayers; i++)
		{	
			outputs.clear();

			for (int j = 0; j < layers.get(i).neurons; j++) 
			{
				double outputWeight = 0;
				int numInputs = layers.get(i).neuronsInLayer.get(j).input;
	
				for (int k = 0; k < numInputs; k++) 
				{
					outputWeight += layers.get(i).neuronsInLayer.get(j).weights.get(k) * inputs.get(k);
				}
				outputs.add(Sigmoid(outputWeight, 1));
			}
		}
		
		for (int i = 0; i < outputs.size(); i++) 
		{
			outputs.set(i, Sigmoid(outputs.get(i) *  inputs.get(i), 1)); 
		}	
		return outputs;
	}
}