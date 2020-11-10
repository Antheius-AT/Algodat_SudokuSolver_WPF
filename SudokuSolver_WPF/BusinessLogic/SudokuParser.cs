using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver_WPF.BusinessLogic
{
    public class SudokuParser
    {
        /// <summary>
        /// Attempt to parse a sudoku.
        /// </summary>
        /// <param name="input">The sudoku formatted as an input string.</param>
        /// <returns>The parsed Sudoku as an array of cells.</returns>
        public SudokuCell[] Parse(string input)
        {
            var splitInput = input.Split('\r', '\n').Where(p => !string.IsNullOrWhiteSpace(p));
            var dimension = splitInput.Count();
            var cellArray = new SudokuCell[dimension * dimension];

            //Validierung noch einbauen.

            for (int i = 0; i < dimension; i++)
            {
                var current = splitInput.ElementAt(i).Replace(" ", string.Empty).Split(',');
                var row = current.Select(p => Convert.ToInt32(p) != 0 ? new SudokuCell(Convert.ToInt32(p), false) : new SudokuCell(0, true)).ToArray();

                if (row.Length != dimension)
                    throw new ArgumentException(nameof(input), "Input wasnt formed correctly");

                Array.Copy(row, 0, cellArray, i * dimension, row.Length);
            }

            return cellArray;
        }
    }
}
