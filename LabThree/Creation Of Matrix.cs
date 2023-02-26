using System;

namespace LabThree
{
    public class CreationOfMatrix
    {
        private double[,] matrix;

        public int Dimension { get; }

        public CreationOfMatrix(int dimension)
        {
            Dimension = dimension;
            matrix = new double[Dimension, Dimension];
        }

        public CreationOfMatrix(int dimension, double minValue, double maxValue)
        {
            Dimension = dimension;
            matrix = new double[Dimension, Dimension];
            var random = new Random();
            for (int ColumnCounter = 0; ColumnCounter < Dimension; ++ColumnCounter)
            {
                for (int RowCounter = 0; RowCounter < Dimension; ++RowCounter)
                {
                    matrix[ColumnCounter, RowCounter] = random.NextDouble() * (maxValue - minValue) + minValue;
                }
            }
        }

        public double this[int RowIndex, int ColumnIndex]
        {
            get { return matrix[RowIndex, ColumnIndex]; }
            set { matrix[RowIndex, ColumnIndex] = value; }
        }

        public static CreationOfMatrix operator +(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            if (FirstMatrix.Dimension != SecondMatrix.Dimension)
                throw new ArgumentException("Матрицы должны быть одинакового размера.");

            var result = new CreationOfMatrix(FirstMatrix.Dimension);

            for (int RowIndex = 0; RowIndex < result.Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < result.Dimension; ++ColumnIndex)
                {
                    result[RowIndex, ColumnIndex] = FirstMatrix[RowIndex, ColumnIndex] 
                                                                    + SecondMatrix[RowIndex, ColumnIndex];
                }
            }

            return result;
        }

        public static CreationOfMatrix operator *(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            if (FirstMatrix.Dimension != SecondMatrix.Dimension)
                throw new ArgumentException("Матрицы должны быть одного размера.");

            var result = new CreationOfMatrix(FirstMatrix.Dimension);

            for (int RowIndex = 0; RowIndex < result.Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < result.Dimension; ++ColumnIndex)
                {
                    double sum = 0;
                    for (int Index = 0; Index < result.Dimension; ++Index)
                    {
                        sum += FirstMatrix[RowIndex, Index] * SecondMatrix[Index, ColumnIndex];
                    }
                    result[RowIndex, ColumnIndex] = sum;
                }
            }

            return result;
        }

        public static bool operator >(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            if (FirstMatrix.Dimension != SecondMatrix.Dimension)
                throw new ArgumentException("Матрицы должны быть одного размера.");

            for (int RowIndex = 0; RowIndex < FirstMatrix.Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < FirstMatrix.Dimension; ++ColumnIndex)
                {
                    if (FirstMatrix[RowIndex, ColumnIndex] <= SecondMatrix[RowIndex, ColumnIndex])
                    {
                        return false;
                    } else
                    {
                        return true;
                    }
                }
            }

            return true;
        }

        public static bool operator <(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            if (FirstMatrix.Dimension != SecondMatrix.Dimension)
                throw new ArgumentException("Матрицы должны быть одного размера.");

            for (int RowIndex = 0; RowIndex < FirstMatrix.Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < FirstMatrix.Dimension; ++ColumnIndex)
                {
                    if (FirstMatrix[RowIndex, ColumnIndex] >= SecondMatrix[RowIndex, ColumnIndex])
                    {
                        return false;
                    } else
                    {
                        return true;
                    }
                }
            }

            return true;
        }

        public static bool operator >=(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            if (FirstMatrix.Dimension != SecondMatrix.Dimension)
                throw new ArgumentException("Матрицы должны быть одного размера.");

            for (int RowIndex = 0; RowIndex < FirstMatrix.Dimension; RowIndex++)
            {
                for (int RowCounter = 0; RowCounter < FirstMatrix.Dimension; RowCounter++)
                {
                    if (FirstMatrix[RowIndex, RowCounter] < SecondMatrix[RowIndex, RowCounter])
                    {   
                        return false;
                    } else
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        public static bool operator <=(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            if (FirstMatrix.Dimension != SecondMatrix.Dimension)
                throw new ArgumentException("Матрицы должны быть одного размера.");

            for (int RowIndex = 0; RowIndex < FirstMatrix.Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < FirstMatrix.Dimension; ++ColumnIndex)
                {
                    if (FirstMatrix[RowIndex, ColumnIndex] > SecondMatrix[RowIndex, ColumnIndex])
                    {
                        return false;
                    } else
                    {
                        return true;
                    }
                }
            }

            return true;
        }

        public static bool operator ==(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            if (FirstMatrix is null)
                return SecondMatrix is null;

            if (SecondMatrix is null || FirstMatrix.Dimension != SecondMatrix.Dimension)
                return false;

            for (int RowIndex = 0; RowIndex < FirstMatrix.Dimension; ++RowIndex)
            {
                for (int CloumnIndex = 0; CloumnIndex < FirstMatrix.Dimension; ++CloumnIndex)
                {
                    if (FirstMatrix[RowIndex, CloumnIndex] != SecondMatrix[RowIndex, CloumnIndex])
                    {
                        return false;
                    } else
                    {
                        return true;
                    }
                }
            }

            return true;
        }

        public static bool operator !=(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            return !(FirstMatrix == SecondMatrix);
        }

        public static explicit operator bool(CreationOfMatrix matrix)
        {
            return matrix != null && matrix.Dimension > 0;
        }

        public double Determinant()
        {
            if (Dimension == 1)
            {
                return matrix[0, 0];
            }
            else if (Dimension == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            else
            {
                double result = 0;
                int sign = 1;
                for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
                {
                    var subMatrix = SubMatrix(RowIndex, 0);
                    result += sign * matrix[RowIndex, 0] * subMatrix.Determinant();
                    sign = -sign;
                }
                return result;
            }
        }

        public CreationOfMatrix Inverse()
        {
            var determinant = Determinant();
            if (determinant == 0)
            {
                throw new InvalidOperationException("Эта матрица не может быть обратной.");
            }
            var result = new CreationOfMatrix(Dimension);

            int sign = 1;
            for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex)
                {
                    var subMatrix = SubMatrix(RowIndex, ColumnIndex);
                    result[ColumnIndex, RowIndex] = sign * subMatrix.Determinant() / determinant;
                    sign = -sign;
                }
            }

            return result;
        }

        private CreationOfMatrix SubMatrix(int rowToRemove, int columnToRemove)
        {
            var subMatrix = new CreationOfMatrix(Dimension - 1);
                
            int subRow = 0;
            for (int row = 0; row < Dimension; ++row)
            {
                if (row == rowToRemove)
                    continue;

                int subColumn = 0;
                for (int column = 0; column < Dimension; ++column)
                {
                    if (column == columnToRemove)
                        continue;

                    subMatrix[subRow, subColumn] = matrix[row, column];
                    subColumn++;
                }

                subRow++;
            }

            return subMatrix;
        }

        public override string ToString()
        {
            string result = "";
            for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex)
                {
                    result += $"{matrix[RowIndex, ColumnIndex]} ";
                }
                result += "\n";
            }
            return result;
        }

        public int CompareTo(CreationOfMatrix other)
        {
            if (other is null)
                return 1;

            if (Dimension != other.Dimension)
                return Dimension.CompareTo(other.Dimension);

            for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex)
                {
                    int compare = matrix[RowIndex, ColumnIndex].CompareTo(other.matrix[RowIndex, ColumnIndex]);
                    if (compare != 0)
                        return compare;
                }
            }

            return 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is CreationOfMatrix))
                return false;

            return this == (CreationOfMatrix)obj;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;
                for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
                {
                    for (int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex)
                    {
                        hashCode = hashCode * 23 + matrix[RowIndex, ColumnIndex].GetHashCode();
                    }
                }
                return hashCode;
            }
        }

        public CreationOfMatrix Clone()
        {
            var clone = new CreationOfMatrix(Dimension);
            for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex)
                {
                    clone[RowIndex, ColumnIndex] = matrix[RowIndex, ColumnIndex];
                }
            }
            return clone;
        }
    }
}