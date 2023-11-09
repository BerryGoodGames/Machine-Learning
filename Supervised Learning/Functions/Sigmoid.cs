namespace SupervisedLearning.Functions;

public class Sigmoid: IFunction
{
    public double Get(double x) => 1 / (1 + Math.Exp(-x));
}