namespace MachineLearning;

public class Image
{
    public Image(byte label, byte[,] data)
    {
        Label = label;
        Data = data;
    }


    public byte Label { get; set; }
    public byte[,] Data { get; set; }
}