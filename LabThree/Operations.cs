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

            var FirstMatrix = new CreationOfMatrix(3);
            for (int RowIndex = 0; RowIndex < 3; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < 3; ++ColumnIndex)
                {
                    FirstMatrix[RowIndex, ColumnIndex] = Random.Next(100);
                }
            }
            Console.WriteLine($"Первая матрица: \n{FirstMatrix}");

            var SecondMatrix = new CreationOfMatrix(3);
            for (int RowIndex = 0; RowIndex < 3; ++RowIndex)
            {
                for (int ColumnIndex = 0; ColumnIndex < 3; ++ColumnIndex)
                {
                    SecondMatrix[RowIndex, ColumnIndex] = Random.Next(100);
                }
            }
            Console.WriteLine($"Вторая матрица: \n{SecondMatrix}");


            Console.WriteLine($"Сложение: \n{FirstMatrix.Clone() + SecondMatrix.Clone()}");

            Console.WriteLine($"Вычитание: \n{FirstMatrix.Clone() - SecondMatrix.Clone()}");

            Console.WriteLine($"Произведение: \n{FirstMatrix.Clone() * SecondMatrix.Clone()}");

            Console.WriteLine($"Матрица А > Матрица Б: {FirstMatrix.Clone() > SecondMatrix.Clone()}");
            Console.WriteLine($"Матрица А >= Матрица Б: {FirstMatrix.Clone() >= SecondMatrix.Clone()}");
            Console.WriteLine($"Матрица А <= Матрица Б: {FirstMatrix.Clone() <= SecondMatrix.Clone()}");
            Console.WriteLine($"Матрица А < Матрица Б: {FirstMatrix.Clone() < SecondMatrix.Clone()}");
            Console.WriteLine($"Матрица А == Матрица Б: {FirstMatrix.Clone() == SecondMatrix.Clone()}");
            Console.WriteLine($"Матрица А != Матрица Б: {FirstMatrix.Clone() != SecondMatrix.Clone()} \n");

            Console.WriteLine($"Детерминант матрицы А: {FirstMatrix.Determinant()}");

            try
            {
                var InverseA = FirstMatrix.Inverse();
                Console.WriteLine($"Обратная матрица А:\n{InverseA}");
            }
            catch (NotInvertible ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}