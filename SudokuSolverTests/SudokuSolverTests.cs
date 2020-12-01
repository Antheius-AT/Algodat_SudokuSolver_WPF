using System;
using System.Linq;
using NUnit.Framework;
using SudokuSolver_WPF.BusinessLogic;

namespace SudokuSolverTests
{
    public class Tests
    {
        private SudokuSolver solver;
        private SudokuParser parser;

        [SetUp]
        public void Setup()
        {
            this.solver = new SudokuSolver();
            this.parser = new SudokuParser();
        }

        [Test]
        [TestCase(@"5, 3, 0, 0, 7, 0, 0, 0, 0
                    6, 0, 0, 1, 9, 5, 0, 0, 0
                    0, 9, 8, 0, 0, 0, 0, 6, 0
                    8, 0, 0, 0, 6, 0, 0, 0, 3
                    4, 0, 0, 8, 0, 3, 0, 0, 1
                    7, 0, 0, 0, 2, 0, 0, 0, 6
                    0, 6, 0, 0, 0, 0, 2, 8, 0
                    0, 0, 0, 4, 1, 9, 0, 0, 5
                    0, 0, 0, 0, 8, 0, 0, 7, 9")]
        public void Does_Sudoku_GetCreated_From_Valid_Input(string input)
        {
            var cells = this.parser.Parse(input);
            var splitInput = input.Split('\r', '\n').Where(p => !string.IsNullOrWhiteSpace(p));

            if (cells.Length == splitInput.Count())
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    Assert.That(cells[i].Content == Convert.ToInt32(splitInput.ElementAt(i)));
                }
            }
            else
            {
                Assert.Fail("Cells and split input were not same length");
            }
        }
    }
}