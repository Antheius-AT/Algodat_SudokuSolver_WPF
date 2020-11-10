using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
using SudokuSolver_WPF.BusinessLogic;
using SudokuSolver_WPF.Commands;

namespace SudokuSolver_WPF.ViewModels
{
    /// <summary>
    /// View Model for the main window view.
    /// </summary>
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            this.SudokuViewModel = new SudokuViewModel();
        }

        /// <summary>
        /// Reference to the view model of the sudoku control.
        /// </summary>
        public SudokuViewModel SudokuViewModel
        {
            get;
            set;
        }
    }
}
