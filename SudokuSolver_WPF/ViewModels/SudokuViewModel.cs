using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SudokuSolver_WPF.BusinessLogic;

namespace SudokuSolver_WPF.ViewModels
{
    public class SudokuViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<SudokuCell> cells;

        public SudokuViewModel()
        {
            this.Cells = new ObservableCollection<SudokuCell>();
        }

        public ObservableCollection<SudokuCell> Cells
        {
            get
            {
                return this.cells;
            }

            set
            {
                this.cells = value;
                this.RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName]string property = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
