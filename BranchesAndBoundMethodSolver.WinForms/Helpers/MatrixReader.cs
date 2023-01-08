using BranchesAndBoundMethodSolver.Logic.Models;

namespace BranchesAndBoundMethodSolver.WinForms.Helpers
{
    public class MatrixReader
    {
        public static Matrix ReadMatrix(string fileName)
        {
            string[] linesFromFile = File.ReadAllLines(fileName);

            int rows = linesFromFile.Length;
            int columns = linesFromFile.First().Split(',').Length;

            var matrix = new Matrix(rows, columns);

            for (var i = 0; i < rows; i++)
            {
                string[] lineValues = linesFromFile[i].Split(',');

                if (lineValues.Length != columns)
                {
                    throw new Exception($"Invalid input. There are missing or extra column in line {i + 1}.");
                }

                for (var j = 0; j < columns; j++)
                {
                    matrix[i, j] = int.Parse(lineValues[j]);
                }
            }

            return matrix;
        }
    }
}