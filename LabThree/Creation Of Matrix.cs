using System;

namespace LabThree
{
    public class CreationOfMatrix
    {
        private double[,] Matrix;

        public int Dimension { get; }

        public CreationOfMatrix(int dimension)
        {
            Dimension = dimension;
            Matrix = new double[Dimension, Dimension];
        }

        public CreationOfMatrix(int dimension, double MinValue, double MaxValue)
        {
            Dimension = dimension;
            Matrix = new double[Dimension, Dimension];
            var Random = new Random();
            for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex)
                {
                    Matrix[RowIndex, ColumnIndex] = Random.NextDouble() * (MaxValue - MinValue) + MinValue;
                }
            }
        }

        public double this[int RowIndex, int ColumnIndex]
        {
            get { return Matrix[RowIndex, ColumnIndex]; }
            set { Matrix[RowIndex, ColumnIndex] = value; }
        }

        public static CreationOfMatrix operator +(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            if (FirstMatrix.Dimension != SecondMatrix.Dimension)
                throw new ArgumentException("Матрицы должны быть одинакового размера.");

            var Result = new CreationOfMatrix(FirstMatrix.Dimension);

            for (int RowIndex = 0; RowIndex < Result.Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Result.Dimension; ++ColumnIndex)
                {
                    Result[RowIndex, ColumnIndex] = FirstMatrix[RowIndex, ColumnIndex]
                                                                    + SecondMatrix[RowIndex, ColumnIndex];
                }
            }
            return Result;
        }

        public static CreationOfMatrix operator -(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {

            var Result = new CreationOfMatrix(FirstMatrix.Dimension);

            for (int RowIndex = 0; RowIndex < Result.Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Result.Dimension; ++ColumnIndex)
                {
                    Result[RowIndex, ColumnIndex] = FirstMatrix[RowIndex, ColumnIndex]
                                                                    - SecondMatrix[RowIndex, ColumnIndex];
                }
            }
            return Result;
        }
        public static CreationOfMatrix operator *(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
            var Result = new CreationOfMatrix(FirstMatrix.Dimension);

            for (int RowIndex = 0; RowIndex < Result.Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Result.Dimension; ++ColumnIndex)
                {
                    double sum = 0;
                    for (int Index = 0; Index < Result.Dimension; ++Index)
                    {
                        sum += FirstMatrix[RowIndex, Index] * SecondMatrix[Index, ColumnIndex];
                    }
                    Result[RowIndex, ColumnIndex] = sum;
                }   
            }
            return Result;
        }

        public static bool operator >(CreationOfMatrix FirstMatrix, CreationOfMatrix SecondMatrix)
        {
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
            {
                return SecondMatrix is null;
            }

            if (SecondMatrix is null || FirstMatrix.Dimension != SecondMatrix.Dimension)
            {
                return false;
            }

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

        public static explicit operator bool(CreationOfMatrix Matrix)
        {
            return Matrix != null && Matrix.Dimension > 0;
        }

        public double Determinant()
        {
            if (Dimension == 1)
            {
                return Matrix[0, 0];
            } else if (Dimension == 2)
            {
                return Matrix[0, 0] * Matrix[1, 1] - Matrix[0, 1] * Matrix[1, 0];
            } else {
                double result = 0;
                int sign = 1;
                for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
                {
                    var subMatrix = SubMatrix(RowIndex, 0);
                    result += sign * Matrix[RowIndex, 0] * subMatrix.Determinant();
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

                    subMatrix[subRow, subColumn] = Matrix[row, column];
                    subColumn++;
                }
                subRow++;
            }
            return subMatrix;
        }

        public override string ToString()
        {
            string Result = "";
            for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex)
                {
                    Result += $"{Matrix[RowIndex, ColumnIndex]} ";
                }
                Result += "\n";
            }
            return Result;
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
                    int compare = Matrix[RowIndex, ColumnIndex].CompareTo(other.Matrix[RowIndex, ColumnIndex]);
                    if (compare != 0)
                        return compare;
                }
            }
            return 0;
        }

        public override bool Equals(object Obj)
        {
            if (Obj is null || !(Obj is CreationOfMatrix))
            {
                return false;
            }

            return this == (CreationOfMatrix)Obj;
        }

        public override int GetHashCode()
        {
            return Matrix.GetHashCode();
        }

        public CreationOfMatrix Clone()
        {
            var clone = new CreationOfMatrix(Dimension);
            for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < Dimension; ++ColumnIndex)
                {
                    clone[RowIndex, ColumnIndex] = Matrix[RowIndex, ColumnIndex];
                }
            }
            return clone;
        }
    }
}