namespace MachineLearning.Reader;

public static class DataReader
{
    private const string trainImages = @"C:\Users\Adrian\Desktop\programs\Supervised Learning\Supervised Learning\Data\Training Data\train-images.idx3-ubyte";
    private const string trainLabels = @"C:\Users\Adrian\Desktop\programs\Supervised Learning\Supervised Learning\Data\Training Data\train-labels.idx1-ubyte";
    private const string testImages = @"C:\Users\Adrian\Desktop\programs\Supervised Learning\Supervised Learning\Data\Test Data\t10k-images.idx3-ubyte";
    private const string testLabels = @"C:\Users\Adrian\Desktop\programs\Supervised Learning\Supervised Learning\Data\Test Data\t10k-labels.idx1-ubyte";

    public static IEnumerable<Image> ReadTrainingData() => Read(trainImages, trainLabels);

    public static IEnumerable<Image> ReadTestData() => Read(testImages, testLabels);

    private static IEnumerable<Image> Read(string imagesPath, string labelsPath)
    {
        BinaryReader labels = new(new FileStream(labelsPath, FileMode.Open));
        BinaryReader images = new(new FileStream(imagesPath, FileMode.Open));

        int magicNumber = images.ReadBigInt32();
        int numberOfImages = images.ReadBigInt32();
        int width = images.ReadBigInt32();
        int height = images.ReadBigInt32();

        int magicLabel = labels.ReadBigInt32();
        int numberOfLabels = labels.ReadBigInt32();

        for (int i = 0; i < numberOfImages; i++)
        {
            byte[] bytes = images.ReadBytes(width * height);
            byte[,] imageData = new byte[height, width];

            imageData.ForEach((j,k) => imageData[j, k] = bytes[j * height + k]);

            yield return new(labels.ReadByte(), imageData);
        }
    }

    private static int ReadBigInt32(this BinaryReader br)
    {
        byte[] bytes = br.ReadBytes(sizeof(int));
        if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
        return BitConverter.ToInt32(bytes, 0);
    }

    private static void ForEach<T>(this T[,] source, Action<int, int> action)
    {
        for (int w = 0; w < source.GetLength(0); w++)
        {
            for (int h = 0; h < source.GetLength(1); h++)
            {
                action(w, h);
            }
        }
    }
}