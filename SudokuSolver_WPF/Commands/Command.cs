using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SudokuSolver_WPF.Commands
{
    public class MyCommand : ICommand
    {
        private Func<object, bool> canExecute;
        private Action<object> invoke;

        public MyCommand(Action<object> invoke, Func<object, bool> canExecute)
        {
            this.invoke = invoke;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.invoke(parameter);
        }
    }
}
