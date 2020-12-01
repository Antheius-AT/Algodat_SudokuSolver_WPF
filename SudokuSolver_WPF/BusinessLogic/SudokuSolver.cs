using System;
using System.Linq;

namespace SudokuSolver_WPF.BusinessLogic
{
    public class SudokuSolver
    {
        private SudokuCell[] cells;

        /// <summary>
        /// Tries to solve a specified sudoku.
        /// </summary>
        /// <param name="sudoku">The specified sudoku.</param>
        /// <returns>A value indicating whether the sudoku could be solved.</returns>
        public bool TrySolve(SudokuCell[] sudoku)
        {
            this.cells = sudoku;
            return this.TrySolveRecursively(0, 1);
        }

        /// <summary>
        /// This method tries to solve the sudoku recursively using backtracking.
        /// </summary>
        /// <param name="currentIndex">The index of the current cell.</param>
        /// <param name="currentValue">The current value that is tried to be placed in the cell.</param>
        /// <returns>Whether the sudoku was solved or not.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Is thrown if current index is negative or greater than 80.
        /// Is thrown if current value is less than 1 or greater than 9.
        /// </exception>
        private bool TrySolveRecursively(int currentIndex, int currentValue)
        {
            if (currentIndex < 0 || currentIndex > 80)
                throw new ArgumentOutOfRangeException(nameof(currentIndex), "Index was out of range of a 9x9 sudoku.");

            if (currentValue < 1 || currentValue > 9)
                throw new ArgumentOutOfRangeException(nameof(currentValue), "Current value was out of range of a 9x9 sudoku.");

            // This piece of code checks whether a given cell can be edited, and if it cannot be, it looks for the index
            // of the next editable cell.
            if (!this.cells[currentIndex].IsEditable)
            {
                do
                {
                    currentIndex += 1;
                }
                while (currentIndex != 81 && !this.cells[currentIndex].IsEditable);
            }

            if (currentIndex > this.cells.Length - 1)
                return true;

            // This loop tries all of the currently possible numbers, 1-9 for the current cell.
            for (int i = currentValue; i <= 9; i++)
            {
                if (IsValidMove(currentIndex, i))
                {
                    this.cells[currentIndex].Content = i;

                    if (this.TrySolveRecursively(currentIndex + 1, 1))
                        return true;
                }
            }

            this.cells[currentIndex].Content = 0;
            return false;
        }

        /// <summary>
        /// Checks whether a move is valid.
        /// </summary>
        /// <param name="currentIndex">The index of the current cell.</param>
        /// <param name="currentValue">The value that is checked for, whether it is valid.</param>
        /// <returns>Whether the value can be placed in the cell at current index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if current index is negative.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if current value is less than 1 or greater than 9.
        /// </exception>
        private bool IsValidMove(int currentIndex, int currentValue)
        {
            if (currentIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(currentIndex), "Current index was negative.");

            if (currentValue < 0 || currentValue > 9)
                throw new ArgumentOutOfRangeException(nameof(currentValue), "Current value was out of range for a 9x9 sudoku.");

            int[] relatedIndexes = this.GetRelatedCellIndexes(currentIndex);

            foreach (var item in relatedIndexes.Distinct())
            {
                if (item == currentIndex)
                    continue;

                if (currentValue == cells[item].Content)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// This method gets all of the related cell indexes that need to be checked to determine whether a move is valid.
        /// </summary>
        /// <param name="currentCellIndex">The current cell index to which all the related indexes are needed.</param>
        /// <returns>An array containing related cell indexes.</returns>
        /// <exception cref="ArgumentException">
        /// Is thrown if current index is negative.
        /// </exception>
        private int[] GetRelatedCellIndexes(int currentCellIndex)
        {
            if (currentCellIndex < 0)
                throw new ArgumentException(nameof(currentCellIndex), "Current index must not be negative.");

            int[] numbersInSameRow;
            int[] numbersInSameColumn;
            int[] numbersInSameBlock;

            numbersInSameRow = this.GetNumbersInSameRow(currentCellIndex);
            numbersInSameColumn = this.GetNumbersInSameColumn(currentCellIndex);
            numbersInSameBlock = this.GetNumbersInSameBlock(currentCellIndex);

            return numbersInSameRow.Concat(numbersInSameColumn).Concat(numbersInSameBlock).Distinct().ToArray();
        }

        /// <summary>
        /// Gets numbers that are contained in the same block as the number at the specified index.
        /// </summary>
        /// <param name="currentCellIndex">The current cell index.</param>
        /// <returns>An array containing all of the indexes that are contained in the same block as the current cell index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if current cell index is negative or greater than 80.
        /// </exception>
        private int[] GetNumbersInSameBlock(int currentCellIndex)
        {
            if (currentCellIndex < 0 || currentCellIndex > 80)
                throw new ArgumentOutOfRangeException(nameof(currentCellIndex), "Index was out of range of a 9x9 sudoku.");

            var column = currentCellIndex % 9;
            var row = currentCellIndex / 9 != 0 ? (currentCellIndex / 9) : 0;
            var blockColumnStartIndex = column - column % 3;
            var blockRowStartIndex = row - row % 3;

            var firstBlockCellIndex = blockRowStartIndex * 9 + blockColumnStartIndex;

            var blockRelatedIndexes = new int[]
            {
                firstBlockCellIndex,
                firstBlockCellIndex + 1,
                firstBlockCellIndex + 2,
                firstBlockCellIndex + 9,
                firstBlockCellIndex + 10,
                firstBlockCellIndex + 11,
                firstBlockCellIndex + 18,
                firstBlockCellIndex + 19,
                firstBlockCellIndex + 20,
            };

            return blockRelatedIndexes;
        }

        /// <summary>
        /// Takes the current cell index as input and calculates all related indexes that are in the same row.
        /// </summary>
        /// <param name="currentCellIndex">The index of the cell that is currently being worked on.</param>
        /// <param name="dimension">The dimension of the sudoku.</param>
        /// <returns>The indexes of the sudoku cells which are related to the cell that is currently being worked on.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if either of the parameters are negative.
        /// </exception>
        private int[] GetNumbersInSameRow(int currentCellIndex)
        {
            if (currentCellIndex < 0)
                throw new ArgumentOutOfRangeException("Either current cell index or dimension were invalid due to being negative.");

            int lowerBound;
            int stepsToLeft;

            stepsToLeft = currentCellIndex % 9;
            lowerBound = currentCellIndex - stepsToLeft;

            return Enumerable.Range(lowerBound, 9).ToArray();
        }

        /// <summary>
        /// Gets numbers that are contained in the same column as the number at the specified index.
        /// </summary>
        /// <param name="currentCellIndex">The current cell index.</param>
        /// <returns>A collection of numbers that are contained in the same column.</returns>
        private int[] GetNumbersInSameColumn(int currentCellIndex)
        {
            int stepsToTop;
            int lowestIndex;

            stepsToTop = currentCellIndex / 9;
            lowestIndex = currentCellIndex - (stepsToTop * 9);

            var blockRelatedIndexes = new int[]
          {
                lowestIndex,
                lowestIndex + 9,
                lowestIndex + 18,
                lowestIndex + 27,
                lowestIndex + 36,
                lowestIndex + 45,
                lowestIndex + 54,
                lowestIndex + 63,
                lowestIndex + 72,
          };

            return blockRelatedIndexes;
        }
    }
}
