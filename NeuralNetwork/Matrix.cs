using System;
using System.Diagnostics;

public class Mat
{
    private decimal[,] _data; // initialises an empty decimal array called _data
    public int Rows { get; } // set only when matrix is initialised, and cannot be changed
    public int Columns { get; }
    
    // Constructor for Mat class - like an __init__()
    public Mat(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        _data = new decimal[rows, columns]; // initialises the data
    }
    
    public decimal this[int row, int col] // allows use of the matrix as a 2d array
    {
        get => _data[row, col]; // allows you to do value = mat[0, 1]
        set => _data[row, col] = value; // allows you to do things like mat[0, 1] = 3
    }
    public decimal[,] toDec()
    {
        return (decimal[,])_data.Clone(); // Return a copy of the data array as a decimal array
    }

    public int[] shape()
    {
        int y = ((decimal[,])_data).GetLength(0);
        int x = ((decimal[,])_data).GetLength(1);
        int[] shape =  { y, x };
        return shape;
    }
    
    // FUNCTIONS

    public static Mat toMat(decimal[,] array)
    {
        int rows = array.GetLength(0);
        int columns = array.GetLength(1);
        Mat result = new Mat(rows, columns); // creates an empty Mat instance, with the shape of the input array
        
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                result[y, x] = array[y, x]; // changes each item in the empty Mat instance with the value in the array
            }
        }
        return result;
    }
    
    // ############################ ALL ADDING OPERATOR REPLACEMENTS ############################

    public static Mat operator +(Mat matrix1, Mat matrix2) // for when both are Mat types
    {
        decimal[,] mat1 = matrix1.toDec();
        decimal[,] mat2 = matrix2.toDec();
        decimal[,] newMatrix = new decimal[mat1.GetLength(0), mat1.GetLength(1)];
        
        for (int y = 0; y < mat1.GetLength(0); y++)
        {
            for (int x = 0; x < mat1.GetLength(1); x++)
            {
                newMatrix[y, x] = mat1[y, x] + mat2[y, x];
            }
        }
        
        Console.WriteLine(newMatrix.GetType());
        return toMat(newMatrix);
    }

    public static Mat operator +(Mat matrix1, object value2) // Mat is left of + sign
    {
        decimal[,] mat1 = matrix1.toDec();
        decimal[,] newMatrix = new decimal[mat1.GetLength(0), mat1.GetLength(1)];
        
        for (int y = 0; y < mat1.GetLength(0); y++)
        { 
            for (int x = 0; x < mat1.GetLength(1); x++)
            { 
                newMatrix[y, x] = mat1[y, x] + Convert.ToDecimal(value2);
            }
        }
        return toMat(newMatrix);
    }
    
    public static Mat operator +(object value1, Mat matrix2) // mat is right of + sign
    {
        decimal[,] mat2 = matrix2.toDec();
        decimal[,] newMatrix = new decimal[mat2.GetLength(0), mat2.GetLength(1)];
        
        for (int y = 0; y < mat2.GetLength(0); y++)
        { 
            for (int x = 0; x < mat2.GetLength(1); x++)
            {
                newMatrix[y, x] = Convert.ToDecimal(value1) + mat2[y, x];
            }
        }
        return toMat(newMatrix);
    }

    // ############################ ALL SUBTRACTING OPERATOR REPLACEMENTS ############################

    public static Mat operator -(Mat matrix1, Mat matrix2) // for when both are Mat types
    {
        decimal[,] mat1 = matrix1.toDec();
        decimal[,] mat2 = matrix2.toDec();
        decimal[,] newMatrix = new decimal[mat1.GetLength(0), mat1.GetLength(1)];
        
        for (int y = 0; y < mat1.GetLength(0); y++)
        {
            for (int x = 0; x < mat1.GetLength(1); x++)
            {
                newMatrix[y, x] = mat1[y, x] - mat2[y, x];
            }
        }
        
        Console.WriteLine(newMatrix.GetType());
        return toMat(newMatrix);
    }

    public static Mat operator -(Mat matrix1, object value2) // Mat is left of - sign
    {
        decimal[,] mat1 = matrix1.toDec();
        decimal[,] newMatrix = new decimal[mat1.GetLength(0), mat1.GetLength(1)];
        
        for (int y = 0; y < mat1.GetLength(0); y++)
        { 
            for (int x = 0; x < mat1.GetLength(1); x++)
            { 
                newMatrix[y, x] = mat1[y, x] - Convert.ToDecimal(value2);
            }
        }
        return toMat(newMatrix);
    }
    
    public static Mat operator -(object value1, Mat matrix2) // mat is right of - sign
    {
        decimal[,] mat2 = matrix2.toDec();
        decimal[,] newMatrix = new decimal[mat2.GetLength(0), mat2.GetLength(1)];
        
        for (int y = 0; y < mat2.GetLength(0); y++)
        { 
            for (int x = 0; x < mat2.GetLength(1); x++)
            {
                newMatrix[y, x] = Convert.ToDecimal(value1) - mat2[y, x];
            }
        }
        return toMat(newMatrix);
    }
    
    // ############################ ALL MULTIPLIER REPLACEMENTS ############################
    
    public static Mat operator &(Mat matrix1, Mat matrix2)
    {
        decimal[,] mat1 = matrix1.toDec();
        decimal[,] mat2 = matrix2.toDec();
        
        int[] aShape = { mat1.GetLength(0), mat1.GetLength(1) };
        int[] bShape = { mat2.GetLength(0), mat2.GetLength(1) };
        
        if (aShape[1] != bShape[0])
        {
            throw new ArgumentException(string.Format("Shape mismatch error: inner dim size {0} != {1}", aShape[1], bShape[0]));
        }
        
        decimal[,] outputArray = new decimal[aShape[0], bShape[1]];
        
        for (int bx = 0; bx < bShape[1]; bx++) // iterates left to right along the B matrix
        {
            for (int ay = 0; ay < aShape[0]; ay++) // iterates top to bottom along the A matrix
            {
                decimal value = 0;
                for (int i = 0; i < aShape[1]; i++) 
                {
                    decimal temp = mat1[ay, i] * mat2[i, bx];
                    value += temp;
                        
                }
                outputArray[ay, bx] = value;
            }
        }
        
        return toMat(outputArray);
    }
    
    public static Mat operator *(Mat matrix1, Mat matrix2) // element wise
    {
        decimal[,] mat1 = matrix1.toDec();
        decimal[,] mat2 = matrix2.toDec();
        
        int[] shape = { mat1.GetLength(0), mat1.GetLength(1) };
        decimal[,] result = new decimal[shape[0], shape[1]];
        
        for (int x = 0; x < mat1.GetLength(1); x++)
        {
            for (int y = 0; y < mat1.GetLength(0); y++)
            {
                decimal val = mat1[y, x] * mat2[y, x];
                result[y, x] = val;
            }
        }
        return toMat(result);
    }
    
    public static Mat operator *(Mat matrix1, object value2) // element wise
    {
        decimal[,] mat1 = matrix1.toDec();
        
        int[] shape = { mat1.GetLength(0), mat1.GetLength(1) };
        decimal[,] result = new decimal[shape[0], shape[1]];
        
        for (int x = 0; x < mat1.GetLength(1); x++)
        {
            for (int y = 0; y < mat1.GetLength(0); y++)
            {
                decimal val = mat1[y, x] * Convert.ToDecimal(value2);
                result[y, x] = val;
            }
        }
        return toMat(result);
    }
    
    public static Mat operator *(object value1, Mat matrix2) // element wise
    {
        decimal[,] mat1 = matrix2.toDec();
        
        int[] shape = { mat1.GetLength(0), mat1.GetLength(1) };
        decimal[,] result = new decimal[shape[0], shape[1]];
        
        for (int x = 0; x < mat1.GetLength(1); x++)
        {
            for (int y = 0; y < mat1.GetLength(0); y++)
            {
                decimal val = mat1[y, x] * Convert.ToDecimal(value1);
                result[y, x] = val;
            }
        }
        return toMat(result);
    }
    
    // ############################ ALL DIVISOR REPLACEMENTS ############################
    
    public static Mat operator /(Mat matrix1, object value2) // element wise
    {
        decimal[,] mat1 = matrix1.toDec();
        
        int[] shape = { mat1.GetLength(0), mat1.GetLength(1) };
        decimal[,] result = new decimal[shape[0], shape[1]];
        
        for (int x = 0; x < mat1.GetLength(1); x++)
        {
            for (int y = 0; y < mat1.GetLength(0); y++)
            {
                decimal val = mat1[y, x] / Convert.ToDecimal(value2);
                result[y, x] = val;
            }
        }
        return toMat(result);
    }
    
    public static Mat operator /(object value1, Mat matrix2) // element wise
    {
        decimal[,] mat1 = matrix2.toDec();
        
        int[] shape = { mat1.GetLength(0), mat1.GetLength(1) };
        decimal[,] result = new decimal[shape[0], shape[1]];
        
        for (int x = 0; x < mat1.GetLength(1); x++)
        {
            for (int y = 0; y < mat1.GetLength(0); y++)
            {
                decimal val = Convert.ToDecimal(value1) / mat1[y, x];
                result[y, x] = val;
            }
        }
        return toMat(result);
    }
    
    public static Mat operator /(Mat matrix1, Mat matrix2) // element wise
    {
        decimal[,] mat1 = matrix1.toDec();
        decimal[,] mat2 = matrix2.toDec();
        
        int[] shape = { mat1.GetLength(0), mat1.GetLength(1) };
        decimal[,] result = new decimal[shape[0], shape[1]];
        
        for (int x = 0; x < mat1.GetLength(1); x++)
        {
            for (int y = 0; y < mat1.GetLength(0); y++)
            {
                decimal val = mat1[y, x] / mat2[y, x];
                result[y, x] = val;
            }
        }
        return toMat(result);
    }

    // ############################ OTHER ############################

    
    public static Mat ExponentNumBase(double numbase, Mat exponent)
    {
        decimal[,] mat1 = exponent.toDec();
        for (int y = 0; y < mat1.GetLength(0); y++)
        {
            for (int x = 0; x < mat1.GetLength(1); x++)
            {
                mat1[y, x] = (decimal)Math.Pow(Convert.ToDouble(numbase), Convert.ToDouble(mat1[y, x]));
            }
        }

        return toMat(mat1);
    }
    
    public static Mat ExponentMatBase(Mat matbase, object exponent)
    {
        decimal[,] mat1 = matbase.toDec();
        for (int y = 0; y < mat1.GetLength(0); y++)
        {
            for (int x = 0; x < mat1.GetLength(1); x++)
            {
                mat1[y, x] = (decimal)Math.Pow(Convert.ToDouble(mat1[y, x]), Convert.ToDouble(exponent));
            }
        }

        return toMat(mat1);
    }
  
    public static Mat Transpose(Mat matrix)
    {
        decimal[,] mat1 = matrix.toDec();
        decimal[,] newMatrix = new decimal[mat1.GetLength(1), mat1.GetLength(0)]; // flips the dimensions
        for (int y = 0; y < mat1.GetLength(0); y++)
        {
            for (int x = 0; x < mat1.GetLength(1); x++)
            {
                newMatrix[x, y] = mat1[y, x];
            }
        }
        return toMat(newMatrix);
    }
    
    public static void PrintArray(Mat matrix)
    {
        decimal[,] array = matrix.toDec();
        for (int y = 0; y < array.GetLength(0); y++)
        {
            Console.WriteLine();
            for (int x = 0; x < array.GetLength(1); x++)
            {
                Console.Write(array[y, x] + " ");
            }
        }
    }

    public static Mat Zeroes(int width, int height)
    {
        decimal[,] matrix = new decimal[height, width];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                matrix[y, x] = 0;
            }
        }
        return toMat(matrix);
    }

    public static Mat Random(int width, int height)
    {
        decimal[,] matrix = new decimal[height, width];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Random random = new Random();
                matrix[y, x] = 2 * (decimal)random.NextDouble() - 1; // converts random to double and then decimal (and shifts from -1 to 1)
            }
        }
        return toMat(matrix);
    }

    static void Run()
    {
    }
    
    // FUNCTIONS END

}

