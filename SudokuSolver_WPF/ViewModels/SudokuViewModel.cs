using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using SudokuSolver_Logic;
using Microsoft.Win32;
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
            this.Cells = new ObservableCollection<SudokuCell>();
            this.solver = new SudokuSolver();
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
                        try
                        {
                            var input = File.ReadAllText(dialog.FileName);
                            this.Cells = new ObservableCollection<SudokuCell>(this.parser.Parse(input));
                            this.IsSudokuLoaded = true;
                            this.Dimension = Convert.ToInt32(Math.Sqrt(this.Cells.Count));
                        }
                        catch (ArgumentException)
                        {
                            MessageBox.Show("Sudoku was not a valid 9x9 sudoku and could not be parsed", "Sudoku invalid", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        catch(Exception e)
                        {
                            MessageBox.Show($"Some other error occurred: {e.Message}", "An error occurred", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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
                    var success = this.solver.TrySolve(toSolve);

                    if (success)
                        this.Cells = new ObservableCollection<SudokuCell>(toSolve);
                    else
                        this.HandleSudokuNotSolved();
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
        /// Handles the case in which a sudoku could not be solved.
        /// </summary>
        protected virtual void HandleSudokuNotSolved()
        {
            var result = MessageBox.Show("The sudoku could not be solved. Do you want to unload it?", "Sudoku not solved", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
                this.UnloadSudoku.Execute(null);
        }
    }
}
