using System;
using System.Collections.Generic;
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
        [TestCase(@"0,2,0,0,0,8,7,0,0
                    0,0,0,5,0,0,0,4,3
                    0,6,3,0,4,0,0,0,0
                    3,0,0,0,0,0,2,0,0
                    0,0,0,0,0,0,0,0,0
                    5,0,7,0,0,4,0,0,0
                    0,0,0,4,0,2,0,0,8
                    0,3,1,9,7,5,0,0,0
                    0,5,4,3,0,6,0,9,7")]

        public void Does_Sudoku_GetCreated_From_Valid_Input(string input)
        {
            var cells = this.parser.Parse(input);

            var splitInput = input.Split('\r', '\n').Where(p => !string.IsNullOrWhiteSpace(p));
            var control = new List<string>();

            foreach (var item in splitInput)
            {
                control.AddRange(item.Replace(" ", string.Empty).Split(','));
            }

            if (control.Count != cells.Length)
                Assert.Fail();

            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i].Content != int.Parse(control[i]))
                    Assert.Fail();
            }

            Assert.Pass();
        }

        [Test]
        [TestCase(@"5, 3, 5, 0, 7, 0, 0, 0, 0
                    6, 0, 0, 1, 9, 5, 0, 0, 0
                    0, 9, 8, 0, 0, 0, 0, 6, 0
                    8, 0, 0, 0, 6, 0, 0, 0, 3
                    4, 0, 0, 8, 0, 3, 0, 0, 1
                    7, 0, 0, 0, 2, 0, 0, 0, 6
                    0, 6, 0, 0, 0, 0, 2, 8, 0
                    0, 0, 0, 4, 1, 9, 0, 0, 5
                    0, 3, 0, 0, 8, 0, 0, 7, 9")]
        [TestCase(@"2, 0, 0, 0, 0, 8, 7, 0, 0
                    0, 0, 0, 5, 0, 0, 0, 4, 3
                    0, 6, 3, 0, 4, 0, 0, 0, 0
                    3, 0, 0, 0, 0, 0, 2, 0, 0
                    0, 0, 0, 0, 0, 0, 0, 0, 0
                    5, 0, 7, 0, 0, 4, 0, 0, 0
                    0, 0, 0, 4, 0, 2, 0, 0, 8
                    0, 3, 1, 9, 7, 5, 0, 0, 0
                    0, 0, 4, 3, 0, 6, 0, 9, 7")]

        public void Does_Solve_On_Invalid_Sudoku_ReturnFalse(string input)
        {
            var cells = this.parser.Parse(input);

            var isValid = this.solver.TrySolve(cells);

            Assert.False(isValid);
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
        [TestCase(@"0,2,0,0,0,8,7,0,0
                    0,0,0,5,0,0,0,4,3
                    0,6,3,0,4,0,0,0,0
                    3,0,0,0,0,0,2,0,0
                    0,0,0,0,0,0,0,0,0
                    5,0,7,0,0,4,0,0,0
                    0,0,0,4,0,2,0,0,8
                    0,3,1,9,7,5,0,0,0
                    0,5,4,3,0,6,0,9,7")]
        public void Does_Solve_On_Valid_Sudoku_ReturnTrue(string input)
        {
            var cells = this.parser.Parse(input);

            var isValid = this.solver.TrySolve(cells);

            Assert.True(isValid);
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
        [TestCase(@"0,2,0,0,0,8,7,0,0
                    0,0,0,5,0,0,0,4,3
                    0,6,3,0,4,0,0,0,0
                    3,0,0,0,0,0,2,0,0
                    0,0,0,0,0,0,0,0,0
                    5,0,7,0,0,4,0,0,0
                    0,0,0,4,0,2,0,0,8
                    0,3,1,9,7,5,0,0,0
                    0,5,4,3,0,6,0,9,7")]
        public void Does_Solve_On_Valid_Sudoku_SolveItCorrectly(string input)
        {
            var cells = this.parser.Parse(input);

            this.solver.TrySolve(cells);

            for (int i = 1; i < 10; i++)
            {
                if (cells.Count(cell => cell.Content == i) != 9)
                    Assert.Fail();
            }

            Assert.Pass();
        }
    }
}