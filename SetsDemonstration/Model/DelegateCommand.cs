using System;
using System.Diagnostics;
using System.Windows.Input;

namespace SetsDemonstration.Model {
    public class DelegateCommand : ICommand {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecuteFunc;

        public bool CanExecute(object parameter) {
            return _canExecuteFunc == null || _canExecuteFunc(parameter);
        }

        public void Execute(object parameter) {
            _execute?.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public DelegateCommand(Action<object> execute) : this(execute, null) { }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecuteFunc) {
            _execute = execute;
            _canExecuteFunc = canExecuteFunc;
        }
    }
}