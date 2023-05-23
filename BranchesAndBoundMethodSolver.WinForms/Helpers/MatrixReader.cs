using System.Collections.Generic;
using System.Text.RegularExpressions;
using BranchesAndBoundMethodSolver.Logic.Models;

namespace BranchesAndBoundMethodSolver.WinForms.Helpers
{
    public class MatrixReader
    {
        public static Matrix ReadMatrix(string fileName)
		{
			string[] linesFromFile = File.ReadAllLines(fileName);
			linesFromFile = PrepareLinesFromFile(linesFromFile);

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

		private static string[] PrepareLinesFromFile(string[] linesFromFile)
		{
			for (int i = 0; i < linesFromFile.Length; i++)
			{
				string line = linesFromFile[i];
				string cleanedLine = Regex.Replace(line, "[^0-9, ]", "").Trim();
				linesFromFile[i] = cleanedLine;
			}

			bool containsOnlyCommas = Array.Exists(linesFromFile, line => line.Trim(',') == "");

			if (containsOnlyCommas)
				linesFromFile = linesFromFile.Where(line => line.Trim(',') != "").ToArray();

			return linesFromFile;
		}
	}
}