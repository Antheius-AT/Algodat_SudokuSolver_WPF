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
        /// <summary>
        /// Anonymous function returning a value indicating whether this command is currently able to be executed.
        /// </summary>
        private Func<object, bool> canExecute;

        /// <summary>
        /// Anonymous function which invokes the command.
        /// </summary>
        private Action<object> invoke;

        public MyCommand(Action<object> invoke, Func<object, bool> canExecute)
        {
            this.invoke = invoke;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Executes the <see cref="canExecute"/> delegate, checking whether the command can be executed.
        /// </summary>
        /// <param name="parameter">Parameter that might be used, if so required to check executability of the command.</param>
        /// <returns>Whether the command can execute.</returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        /// <summary>
        /// Executes the command by calling the <see cref="invoke"/> delegate.
        /// </summary>
        /// <param name="parameter">Paramter that might be used on invocation of the command.</param>
        public void Execute(object parameter)
        {
            this.invoke(parameter);
        }
    }
}
