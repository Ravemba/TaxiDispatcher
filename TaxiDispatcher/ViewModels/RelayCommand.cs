using System;
using System.Diagnostics;
using System.Windows.Input;

namespace TaxiDispatcher.ViewModels
{
    internal sealed class RelayCommand : ICommand
    {
        private readonly Action<object> _executionAction;
        private readonly Predicate<object> _canExecutePredicate;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            _executionAction = execute;
            _canExecutePredicate = canExecute;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecutePredicate?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException("The command is not valid for execution, check the CanExecute method before attempting to execute.");
            }
            _executionAction(parameter);
        }
    }
}