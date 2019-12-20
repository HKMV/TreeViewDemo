using System;
using System.Reflection;
using System.Windows.Input;

namespace TreeViewDemo
{
    /// <summary>
    /// 双向绑定中用的事件
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;

            if (canExecute != null)
            {
                _canExecute = canExecute;
            }
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public virtual void Execute(object parameter)
        {
            if (CanExecute(parameter) && _execute != null)
            {
                _execute();
            }
        }
    }
    /// <summary>
    /// 双向绑定中用的事件
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute, bool keepTargetAlive = false)
          : this(execute, (Func<T, bool>)null, keepTargetAlive)
        {
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute, bool keepTargetAlive = false)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            this._execute = new Action<T>(execute);
            if (canExecute == null)
                return;
            this._canExecute = new Func<T, bool>(canExecute);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this._canExecute == null)
                    return;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (this._canExecute == null)
                    return;
                CommandManager.RequerySuggested -= value;
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                return _canExecute == null || _canExecute((T)parameter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void Execute(object parameter)
        {
            if (CanExecute(parameter) && _execute != null)
            {
                try
                {
                    _execute((T)parameter);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}