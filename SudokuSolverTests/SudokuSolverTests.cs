using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SudokuSolver_Logic;
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

            for (int i = 0; i < 81; i++)
            {
                var indexes = this.solver.GetNumbersInSameRow(i);

                for (int j = 0; j < 9; j++)
                {
                    if (cells[i].Content == cells[indexes[j]].Content && indexes[j] != i)
                        Assert.Fail();
                }

                indexes = this.solver.GetNumbersInSameBlock(i);

                for (int j = 0; j < 9; j++)
                {
                    if (cells[i].Content == cells[indexes[j]].Content && indexes[j] != i)
                        Assert.Fail();
                }

                indexes = this.solver.GetNumbersInSameColumn(i);

                for (int j = 0; j < 9; j++)
                {
                    if (cells[i].Content == cells[indexes[j]].Content && indexes[j] != i)
                        Assert.Fail();
                }
            }

            Assert.Pass();
        }

        [TestCase(0)]
        [TestCase(5)]
        [TestCase(20)]
        [TestCase(81)]
        [Test]
        public void Does_Method_GetNumbersInSameColumn_ReturnCorrectValues(int index)
        {
            var result = this.solver.GetNumbersInSameColumn(index);

            var control = new List<int>();

            while (index - 9 >= 0)
            {
                index -= 9;
            }

            for (int i = index; i < 81;)
            {
                control.Add(i);
                i += 9;
            }

            Assert.AreEqual(control.ToArray(), result);
        }

        [Test]
        [TestCase(0)]
        [TestCase(8)]
        [TestCase(11)]
        [TestCase(78)]
        public void Does_Method_GetNumbersInSameRow_ReturnCorrectValues(int index)
        {
            var result = this.solver.GetNumbersInSameRow(index);

            var control = new List<int>();

            while (index % 9 != 0)
            {
                index -= 1;
            }

            for (int i = index; i < index + 9; i++)
            {
                control.Add(i);
            }

            Assert.AreEqual(control.ToArray(), result);
        }

        [Test]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(13)]
        [TestCase(0)]
        [TestCase(79, 60, 61, 62, 69, 70, 71, 78, 79, 80)]
        public void Does_Method_GetNumbersInSameBlock_ReturnCorrectValues(int index, params int[] expected)
        {
            var result = this.solver.GetNumbersInSameBlock(index);

            if (expected.Length == 0)
            {
                var control = new List<int>();

                while (index - 9 >= 0)
                {
                    index -= 9;
                }

                while (index % 3 != 0)
                {
                    index -= 1;
                }

                var counter = 0;

                // Add numbers in block to control
                do
                {
                    control.Add(index + counter);
                    counter += 1;

                    if (counter % 3 == 0)
                    {
                        index += 9;
                        counter = 0;
                    }
                }
                while (control.Count < 9);

                expected = control.ToArray();
            }

            Assert.AreEqual(expected, result);
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
        public void Does_ParsingSudoku_CorrectlyDistinguishBetween_Editable_And_NotEditable_Cells(string input)
        {
            var sudoku = this.parser.Parse(input);

            var splitInput = input.Split('\r', '\n').Where(p => !string.IsNullOrWhiteSpace(p));
            var control = new List<string>();

            foreach (var item in splitInput)
            {
                control.AddRange(item.Replace(" ", string.Empty).Split(','));
            }

            if (control.Count != sudoku.Length)
                Assert.Fail();

            for (int i = 0; i < control.Count; i++)
            {
                bool expected;

                if (int.Parse(control[i]) == 0)
                    expected = true;
                else
                    expected = false;

                if (expected != sudoku[i].IsEditable)
                    Assert.Fail("Editable property did not match");
            }

            Assert.Pass();
        }

        [Test]
        [TestCase(@"5, 3, 0, 0, 7, 0, 0, 0, 0, 5, 5
                    6, 0, 0, 1, 9, 5, 0, 0, 0
                    0, 9, 8, 0, 0, 0, 0, 6, 0
                    8, 0, 0, 0, 6, 0, 0, 0, 3
                    4, 0, 0, 8, 0, 3, 0, 0, 1
                    7, 0, 0, 0, 2, 0, 0, 0, 6
                    0, 6, 0, 0, 0, 0, 2, 8, 0
                    0, 0, 0, 4, 1, 9, 0, 0, 5
                    0, 0, 0, 0, 8, 0, 0, 7, 9")]
        [TestCase(@"5, 3, 0, 0, 7, 0, 0, 0, 0,
                    6, 0, 0, 1, 9, 5, 0, 0, 0
                    0, 9, 8, 0, 0, 0, 0, 6, 0
                    8, 0, 0, 0, 6, 0, 0, 0, 3
                    4, 0, 0, 8, 0, 3, 0, 0, 1
                    7, 0, 0, 0, 2, 0, 0, 0, 6
                    0, 6, 0, 0, 0, 0, 2, 8, 0
                    0, 0, 0, 4, 1, 9, 0, 0, 5
                    0, 0, 0, 0, 8, 0, 0, 7, 9
                    5, 1, 0, 2, 9, 0, 0, 0, 0")]
        [TestCase(@"5, 3, 0, 0, 7, 0, 0, 0, f
                    6, 0, 0, 1, 9, 5, 0, 0, 0
                    0, 9, 8, 0, 0, 0, 0, 6, 0
                    8, 0, 0, 0, 6, 0, 0, 0, 3
                    4, 0, 0, 8, 0, 3, 0, 0, 1
                    7, 0, 0, 0, 2, 0, 0, 0, 6
                    0, 6, 0, 0, 0, 0, 2, 8, 0
                    0, 0, 0, 4, 1, 9, 0, 0, 5
                    0, 0, 0, 0, 8, 0, 0, 7, 9")]
        public void DoesParser_ThrowException_OnInvalidInput(string input)
        {
            Assert.Throws<ArgumentException>(() => { this.parser.Parse(input); });
        }
    }
}