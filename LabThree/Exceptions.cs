using System;

namespace LabThree
{
    public class InvalidMatrixSizeException : Exception
    {
        public InvalidMatrixSizeException(int dimension) : base($"Неправильный размер матрицы: {dimension}")
        {
        }
    }

    public class MatrixNotInvertibleException : Exception
    {
        public MatrixNotInvertibleException() : base("Эта матрица не может быть обратной.")
        {
        }
    }
}