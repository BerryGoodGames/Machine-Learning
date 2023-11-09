namespace SupervisedLearning.NeuralNetworks;

public class NeuralNetwork
{
    private readonly Neuron[] input;
    private readonly Neuron[] output;
    private readonly Neuron[,] hiddenLayers;
    
    public IEnumerable<double> Input
    {
        get => input.Select(neuron => neuron.Value);

        set
        {
            int i = 0;

            foreach (double doubleInput in value)
            {
                if (i >= input.Length)
                    throw new($"Input is too large, it should be {input.Length} long, but it is {value.Count()} long");
                
                input[i].Value = doubleInput;
                i++;
            }
        }
    }

    public IEnumerable<double> Output => output.Select(neuron => neuron.Value);

    public NeuralNetwork(uint inputAmount, uint outputAmount, int hiddenLayerWidth, int hiddenLayerHeight)
    {
        Random rng = new();
        
        // initialize input neurons
        input = new Neuron[inputAmount];
        
        for (int i = 0; i < input.Length; i++)
        {
            input[i] = new(rng.NextDouble());
        }
        
        // initialize hidden layers
        hiddenLayers = new Neuron[hiddenLayerWidth, hiddenLayerHeight];
        
        for (int i = 0; i < hiddenLayers.GetLength(0); i++)
        {
            for (int j = 0; j < hiddenLayers.GetLength(1); j++)
            {
                // create connections
                List<NeuronConnection> connections = new();
                
                // get from input
                if (i == 0)
                {
                    connections.AddRange(input.Select(inputNeuron => new NeuronConnection(inputNeuron, rng.NextDouble() * 2 - 1)));
                }
                // get from last layer
                else
                {
                    for (int k = 0; k < hiddenLayers.GetLength(1); k++)
                    {
                        connections.Add(new(hiddenLayers[i - 1, k], rng.NextDouble() * 2 - 1));
                    }
                }
                
                hiddenLayers[i, j] = new(connections.ToArray(), rng.NextDouble() * 2 - 1);
            }
        }
        
        // initialize output neurons
        output = new Neuron[outputAmount];
        
        for (int i = 0; i < output.Length; i++)
        {
            List<NeuronConnection> connections = new();
            
            // create connections
            for (int k = 0; k < hiddenLayers.GetLength(1); k++)
            {
                
                connections.Add(new(hiddenLayers[hiddenLayers.GetLength(0) - 1, k], rng.NextDouble() * 2 - 1));
            }

            output[i] = new(connections.ToArray(), rng.NextDouble() * 2 - 1);
        }
    }
    
    
}