using System;
using System.Diagnostics;

namespace NeuralNetwork
{
    class NeuralNetwork
    {
        // Declare weight and bias layers as fields (accessible across methods)
        
        private static int m = 1; // batchsize
        private static decimal lr = 0.001m; // learn rate
        
        private static Mat weightLayer1 = Mat.Random(30, m);
        private static Mat weightLayer2 = Mat.Random(m, 16);
        private static Mat weightLayer3 = Mat.Random(16, 2);

        private static Mat biasLayer1 = Mat.Zeroes(m, 32);
        private static Mat biasLayer2 = Mat.Zeroes(m, 16);
        private static Mat biasLayer3 = Mat.Zeroes(m, 2);

        public static Mat Sigmoid(Mat matrix)
        {
            return 1 / (1 + Mat.ExponentNumBase(Math.E, matrix));
        }

        static Mat ForwardProp(Mat x)
        {
            var inputs = Mat.Transpose(x);
            var logits1 = (weightLayer1 & inputs) + biasLayer1;
            var activation1 = Sigmoid(logits1);
            var logits2 = (weightLayer2 & activation1) + biasLayer2;
            var activation2 = Sigmoid(logits2);
            var logits3 = (weightLayer3 & activation2) + biasLayer3;
            var activation3 = Sigmoid(logits3);
            
            return activation3;
        }

        static void Main()
        {
            Mat input = Mat.Random(30, m);
            Mat output = ForwardProp(input);
            Mat.PrintArray(output);
        }
    }
}