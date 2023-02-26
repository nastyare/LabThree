/*********************************
 * Рексиус Анастасия             *
 * ПИ-221                        *
 * 3 лаба                        *
 *********************************/

using System;

namespace LabThree
{
    class Operations
    {
        static void Main(string[] args)
        {
            var random = new Random();

            var FirstMatrix = new CreationOfMatrix(5);
            for (int RowIndex = 0; RowIndex < 5; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < 5; ++ColumnIndex)
                {
                    FirstMatrix[RowIndex, ColumnIndex] = random.Next(100);
                }
            }
            Console.WriteLine($"Первая матрица: \n{FirstMatrix}");

            var SecondMatrix = new CreationOfMatrix(5);
            for (int RowIndex = 0; RowIndex < 5; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < 5; ++ColumnIndex)
                {
                    SecondMatrix[RowIndex, ColumnIndex] = random.Next(100);
                }
            }
            Console.WriteLine($"Вторая матрица: \n{SecondMatrix}");


            Console.WriteLine($"Сложение: \n{FirstMatrix + SecondMatrix}");

            Console.WriteLine($"Произведение: \n{FirstMatrix * SecondMatrix}");

            Console.WriteLine($"Матрица А > Матрица Б: {FirstMatrix > SecondMatrix}");
            Console.WriteLine($"Матрица А >= Матрица Б: {FirstMatrix >= SecondMatrix}");
            Console.WriteLine($"Матрица А <= Матрица Б: {FirstMatrix <= SecondMatrix}");
            Console.WriteLine($"Матрица А == Матрица Б: {FirstMatrix == SecondMatrix}");
            Console.WriteLine($"Матрица А != Матрица Б: {FirstMatrix != SecondMatrix}");

            Console.WriteLine($"Детерминант матрицы А: {FirstMatrix.Determinant()}");
                
            try
            {
                var inverseA = FirstMatrix.Inverse();
                Console.WriteLine($"Обратная матрица А:\n{inverseA}");
            }
            catch (MatrixNotInvertibleException ex)
            {
                Console.WriteLine(ex.Message);
            }


            var c = FirstMatrix.Clone();
            Console.WriteLine($"C =\n{c}");
            Console.WriteLine($"Матрица А == C: {FirstMatrix == c}");

            try
            {
                var d = new CreationOfMatrix(0);
            }
            catch (InvalidMatrixSizeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}