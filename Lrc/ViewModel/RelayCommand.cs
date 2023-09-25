using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lrc.ViewModel
{
    internal class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Predicate<object> _canExecute;
        
     
        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null) 
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null) return true;
            return _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
