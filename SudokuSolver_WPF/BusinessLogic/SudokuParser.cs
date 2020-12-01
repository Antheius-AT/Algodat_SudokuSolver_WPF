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
        /// <exception cref="ArgumentException">
        /// Is thrown if the input is not a valid 9x9 Sudoku.
        /// </exception>
        public SudokuCell[] Parse(string input)
        {
            var splitInput = input.Split('\r', '\n').Where(p => !string.IsNullOrWhiteSpace(p));
            
            var cellArray = new SudokuCell[9 * 9];

            if (splitInput.Count() != 9)
                throw new ArgumentException(nameof(input), "The input was not a 9x9 sudoku. This solver currently only supports the standard 9x9 Sudoku type.");

            for (int i = 0; i < 9; i++)
            {
                var current = splitInput.ElementAt(i).Replace(" ", string.Empty).Split(',');

                if (!this.ValidateRow(current))
                    throw new ArgumentException(nameof(input), "Input was not a valid 9x9 sudoku.");

                // After checking whether the row is valid, this line right here creates a sudoku cell based on the current number in the row.
                var row = current.Select(p => Convert.ToInt32(p) != 0 ? new SudokuCell(Convert.ToInt32(p), false) : new SudokuCell(0, true)).ToArray();

                // Copy parsed row into the cell array.
                Array.Copy(row, 0, cellArray, i * 9, row.Length);
            }

            return cellArray;
        }

        /// <summary>
        /// Validates whether a specified row contains only numbers from 0 to 9.
        /// </summary>
        /// <param name="row">The row to check.</param>
        /// <returns>Whether the row is valid.</returns>
        /// <exception cref="ArgumentNullException">
        /// Is thrown if row is null.
        /// </exception>
        private bool ValidateRow(string[] row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row), "Row must not be null.");

            if (row.Length != 9)
                return false;

            foreach (var item in row)
            {
                if (!int.TryParse(item, out int result) || (result < 0 || result > 9))
                    return false;
            }

            return true;
        }
    }
}
