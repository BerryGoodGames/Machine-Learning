namespace SupervisedLearning.NeuralNetworks;

public class NeuronConnection
{
    public double Value => sourceNeuron.Value * weight;
    private readonly Neuron sourceNeuron;
    private double weight;

    public NeuronConnection(Neuron sourceNeuron, double weight)
    {
        this.sourceNeuron = sourceNeuron;
        this.weight = weight;
    }
}