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
    public class MainWindowViewModel
    {
        private SudokuParser parser;

        public MainWindowViewModel()
        {
            this.SudokuViewModel = new SudokuViewModel();
            this.parser = new SudokuParser();
        }

        public SudokuViewModel SudokuViewModel
        {
            get;
            set;
        }

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
                        this.SudokuViewModel.Cells = new ObservableCollection<SudokuCell>(this.parser.Parse(input));
                    }
                },
                p => true);
            }
        }
    }
}
