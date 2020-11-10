using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver_WPF.BusinessLogic
{
    public class SudokuSolver
    {
        public SudokuSolver()
        {

        }

        /// <summary>
        /// Tries to solve a specified sudoku.
        /// </summary>
        /// <param name="sudoku">The specified sudoku.</param>
        /// <returns>A value indicating whether the sudoku could be solved.</returns>
        public bool TrySolve(SudokuCell[] sudoku, int currentIndex = 0, int currentValue = 1)
        {
            // Wenn index größer als array.length dann geschafft.
            if (currentIndex < 0)
                throw new Exception();

            if (!sudoku[currentIndex].IsEditable)
            {
                return this.TrySolve(sudoku, currentIndex + 1, 1);
            }

            if (currentValue > 9)
            {
                sudoku[currentIndex].Content = 0;

                do
                {
                    currentIndex -= 1;
                    currentValue = sudoku[currentIndex].Content + 1;
                }
                while (!sudoku[currentIndex].IsEditable || currentValue == 9);

                return this.TrySolve(sudoku, currentIndex, currentValue);
            }

            sudoku[currentIndex].Content = currentValue;

            if (!this.IsValidMove(sudoku, currentIndex))
            {
                this.TrySolve(sudoku, currentIndex, currentValue + 1);
            }

            return this.TrySolve(sudoku, currentIndex + 1, 1);
        }

        public bool Test(SudokuCell[] cells)
        {
            // Für jede Zelle beginnend bei 1 alle Möglichkeiten durchgehen. Danach immer prüfen ob korrekt.
            // Wenn ja --> weiter. Nächste Zelle neu anfangen 
            // Wenn Nein --> Zahl um 1 erhöhen und neu versuchen.
            // Wenn bei 9 und passt nicht --> eine Zelle zurück springen und dort bei aktuellem Content anfangen hochzuzählen.
            // Wenn bei erster Zelle und 9 keine Zahl passt, not solvable.

            int currentValue = 1;

            for (int i = 0; i < cells.Length; i++)
            {
                if (!cells[i].IsEditable)
                    continue;

                cells[i].Content = currentValue;

                if (this.IsValidMove(cells, i))
                {
                    currentValue = 1;
                    continue;
                }
                else
                {
                    currentValue++;

                    if (currentValue > 9)
                    {
                        cells[i].Content = 0;

                        do
                        {
                            i = this.GetPreviousEditableCell(i, cells);
                            currentValue = cells[i].Content;
                        }
                        while (currentValue == 9);

                        currentValue++;
                        i--;
                        continue;
                    }

                    i--;
                }
            }

            return true;
        }

        private bool IsValidMove(SudokuCell[] cells, int currentIndex)
        {
            int[] relatedIndexes = this.GetRelatedCellIndexes(currentIndex, (int)Math.Sqrt(cells.Length));

            foreach (var item in relatedIndexes)
            {
                if (item == currentIndex)
                    continue;

                if (cells[currentIndex].Content == cells[item].Content)
                    return false;
            }

            return true;
        }

        private int[] GetRelatedCellIndexes(int currentCellIndex, int sudokuDimension)
        {
            if (currentCellIndex < 0)
                throw new ArgumentException(nameof(currentCellIndex), "Current index must not be negative.");

            int[] numbersInSameRow;
            int[] numbersInSameColumn;
            int[] numbersInSameBlock;

            numbersInSameRow = this.GetNumbersInSameRow(currentCellIndex, sudokuDimension);

            // All numbers that have the same result when taken modulo by the dimension are related due to being in the same column.
            numbersInSameColumn = Enumerable.Range(0, sudokuDimension * sudokuDimension).Where(p => p % sudokuDimension == currentCellIndex % sudokuDimension).ToArray();
            numbersInSameBlock = this.GetNumbersInSameBlock(currentCellIndex, sudokuDimension);

            return numbersInSameRow.Concat(numbersInSameColumn).Concat(numbersInSameBlock).Distinct().ToArray();
        }

        private int[] GetNumbersInSameBlock(int currentCellIndex, int sudokuDimension)
        {
            // The idea here is to first calculate the row and column positions, and then work with the modulo of 3, as there are 3 cells per 
            // block, to get the starting index of the block, from which point I can subsequently add the cells of a block.
            // -1 again to compensate for the index.
            var column = currentCellIndex % sudokuDimension;
            var row = currentCellIndex / sudokuDimension != 0 ? (currentCellIndex / sudokuDimension) - 1 : 0;
            var blockColumnStartIndex = column - column % 3;
            var blockRowStartIndex = row - row % 3;

            var firstBlockCellIndex = blockRowStartIndex * 3 + blockColumnStartIndex;

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
        private int[] GetNumbersInSameRow(int currentCellIndex, int dimension)
        {
            if (currentCellIndex < 0 | dimension < 0)
                throw new ArgumentOutOfRangeException("Either current cell index or dimension were invalid due to being negative.");

            int lowerBound;
            int stepsToLeft;

            // Subtract 1 from upper bound to compensate for the index starting with 0.
            stepsToLeft = currentCellIndex % dimension;
            lowerBound = currentCellIndex - stepsToLeft;

            return Enumerable.Range(lowerBound, dimension).ToArray();
        }

        private int GetPreviousEditableCell(int currentIndex, SudokuCell[] cells)
        {
            if (currentIndex <= 0)
                throw new ArgumentException("Not solvbable");

            currentIndex--;

            while (!cells[currentIndex].IsEditable)
            {
                currentIndex--;

                if (currentIndex < 0)
                    throw new ArgumentException("not solvable");
            }

            return currentIndex;
        }
    }
}
