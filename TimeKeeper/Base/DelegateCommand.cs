using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TimeKeeper.Base
{
    public class DelegateCommand : Observable, IDelegateCommand
    {
        public event EventHandler? CanExecuteChanged;
        Action _execute;
        Func<bool> _canExecute;

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged(); }
        }

        public DelegateCommand(Action execute)
        {
            _execute = execute;
            _canExecute = () => true;
        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        public bool CanExecute(object? parameter)
        {
            return _canExecute();
        }

        public void Execute(object? parameter)
        {
            _execute();
        }

        public void Refresh()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public class DelegateCommand<T> : Observable, IDelegateCommand where T : new()
    {
        public event EventHandler? CanExecuteChanged;

        Action<T> _execute;
        Func<T, bool> _canExecute;

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged(); }
        }


        public DelegateCommand(Action<T> execute)
        {
            _execute = execute;
            _canExecute = (o) => true;
        }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter is T t)
            {
                return _canExecute(t);
            }
            return false;
        }

        public void Execute(object? parameter)
        {
            if (parameter is T t)
            {
                _execute(t);
            }
        }

        public void Refresh()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
