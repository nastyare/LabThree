/*********************************
 * Рексиус Анастасия             *
 * ПИ-221                        *
 * 3 лаба                        *
 *********************************/

using System;

namespace LabThree
{
    public class NotInvertible : Exception
    {
        public NotInvertible() : base("Матрицу нельзя обратить.")
        {
        }
    }

    class Operations
    {
        static void Main(string[] args)
        {
            var Random = new Random();

            var FirstMatrix = new CreationOfMatrix(5);
            for (int RowIndex = 0; RowIndex < 5; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < 5; ++ColumnIndex)
                {
                    FirstMatrix[RowIndex, ColumnIndex] = Random.Next(100);
                }
            }
            Console.WriteLine($"Первая матрица: \n{FirstMatrix}");

            var SecondMatrix = new CreationOfMatrix(5);
            for (int RowIndex = 0; RowIndex < 5; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < 5; ++ColumnIndex)
                {
                    SecondMatrix[RowIndex, ColumnIndex] = Random.Next(100);
                }
            }
            Console.WriteLine($"Вторая матрица: \n{SecondMatrix}");


            Console.WriteLine($"Сложение: \n{FirstMatrix + SecondMatrix}");

            Console.WriteLine($"Вычитание: \n{FirstMatrix - SecondMatrix}");

            Console.WriteLine($"Произведение: \n{FirstMatrix * SecondMatrix}");

            Console.WriteLine($"Матрица А > Матрица Б: {FirstMatrix > SecondMatrix}");
            Console.WriteLine($"Матрица А >= Матрица Б: {FirstMatrix >= SecondMatrix}");
            Console.WriteLine($"Матрица А <= Матрица Б: {FirstMatrix <= SecondMatrix}");
            Console.WriteLine($"Матрица А < Матрица Б: {FirstMatrix < SecondMatrix}");
            Console.WriteLine($"Матрица А == Матрица Б: {FirstMatrix == SecondMatrix}");
            Console.WriteLine($"Матрица А != Матрица Б: {FirstMatrix != SecondMatrix} \n");

            Console.WriteLine($"Детерминант матрицы А: {FirstMatrix.Determinant()}");

          /*  try
            {
                var InverseA = FirstMatrix.Inverse();
                Console.WriteLine($"Обратная матрица А:\n{InverseA}");
            }
            catch (NotInvertible ex)
            {
                Console.WriteLine(ex.Message);
            }*/
        }
    }
}