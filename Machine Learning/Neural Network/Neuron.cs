using MachineLearning.Functions;

namespace MachineLearning.NeuralNetworks;

public class Neuron
{
    private static readonly IFunction function = new Sigmoid();
    
    public double Value;
    private readonly NeuronConnection[] connections;
    private double bias;

    public Neuron(double bias)
    {
        this.bias = bias;
        connections = Array.Empty<NeuronConnection>();
    }
    
    public Neuron(NeuronConnection[] connections, double bias)
    {
        this.connections = connections;
        this.bias = bias;
    }

    public void Update()
    {
        // get values from previous neurons multiplied by weight
        Value = connections.Sum(connection => connection.Value);
        
        // add bias
        Value += bias;
        
        // pass value through the function
        Value = function.Get(Value);
    }
}