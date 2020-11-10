using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Win32;
using SudokuSolver_WPF.BusinessLogic;
using SudokuSolver_WPF.Commands;

namespace SudokuSolver_WPF.ViewModels
{
    /// <summary>
    /// This is the view model for the Sudoku user control.
    /// </summary>
    public class SudokuViewModel : INotifyPropertyChanged
    {
        private int dimension;
        private ObservableCollection<SudokuCell> cells;
        private SudokuParser parser;
        private bool isSudokuLoaded;
        private SudokuSolver solver;

        public SudokuViewModel()
        {
            this.parser = new SudokuParser();
            this.solver = new SudokuSolver();
            this.Cells = new ObservableCollection<SudokuCell>();
        }

        /// <summary>
        /// Collection containing game cells displayed to the user.
        /// </summary>
        public ObservableCollection<SudokuCell> Cells
        {
            get
            {
                return this.cells;
            }

            set
            {
                this.cells = value ?? throw new ArgumentNullException(nameof(value), "Cell collection must not be null.");
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets whether a sudoku is currently loaded.
        /// </summary>
        public bool IsSudokuLoaded
        {
            get
            {
                return this.isSudokuLoaded;
            }

            private set
            {
                this.isSudokuLoaded = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the sudoku dimension.
        /// </summary>
        public int Dimension
        {
            get
            {
                return this.dimension;
            }

            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "dimension must not be negative");
                
                this.dimension = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Command that allows the user to load a sudoku from a text file.
        /// </summary>
        public ICommand LoadSudoku
        {
            get
            {
                return new MyCommand(p =>
                {
                    var dialog = new OpenFileDialog();
                    dialog.Filter = "Text Files|*.txt";

                    if (dialog.ShowDialog() == true)
                    {
                        var input = File.ReadAllText(dialog.FileName);
                        this.Cells = new ObservableCollection<SudokuCell>(this.parser.Parse(input));
                        this.IsSudokuLoaded = true;
                        this.Dimension = Convert.ToInt32(Math.Sqrt(this.Cells.Count));
                    }
                },
                p => true);
            }
        }

        /// <summary>
        /// Gets the command that allows the user to solve a sudoku.
        /// </summary>
        public ICommand SolveSudoku
        {
            get
            {
                return new MyCommand(p =>
                {
                    var toSolve = this.Cells.ToArray();
                    var success = this.solver.TrySolve(toSolve, 0, 1);

                    if (success)
                        this.Cells = new ObservableCollection<SudokuCell>(toSolve);
                    else
                        this.RaiseSudokuNotSolved();
                },
                p => this.IsSudokuLoaded);
            }
        }

        /// <summary>
        /// Gets the command that allows the user to unload a loaded sudoku.
        /// </summary>
        public ICommand UnloadSudoku
        {
            get
            {
                return new MyCommand(p =>
                {
                    this.Cells = new ObservableCollection<SudokuCell>();
                    this.IsSudokuLoaded = false;
                    this.Dimension = 0;
                },
                p => this.IsSudokuLoaded);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the event that signals that some property changed.
        /// </summary>
        /// <param name="property">The changed property.</param>
        protected virtual void RaisePropertyChanged([CallerMemberName]string property = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Raises the event that signals that the sudoku was not solved.
        /// </summary>
        protected virtual void RaiseSudokuNotSolved()
        {
            throw new NotImplementedException();
        }
    }
}
