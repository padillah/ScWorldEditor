using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SCWorldEdit.Assets
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event EventHandler CommandExecuted;

        protected Action _executeParameterless;
        protected Func<bool> _canExecuteParameterless;

        public RelayCommand(Action argExecute, Func<bool> argCanExecute = null)
        {
            _executeParameterless = argExecute;
            _canExecuteParameterless = argCanExecute;
        }

        //public RelayCommand(Action argExecute, bool argCanExecute)
        //{
        //    _executeParameterless = argExecute;
        //    if (argCanExecute)
        //    {
        //        _canExecuteParameterless = CanExecuteTrue;
        //    }
        //    else
        //    {
        //        _canExecuteParameterless = CanExecuteFalse;
        //    }
        //}

        public virtual void Execute(object argParameter)
        {
            Execute();
        }

        public virtual bool CanExecute(object argParameter)
        {
            return CanExecute();
        }

        public virtual void Execute()
        {
            if (_executeParameterless != null)
            {
                _executeParameterless();
                CommandExecuted.Raise(this);
            }
        }

        public virtual bool CanExecute()
        {
            return _canExecuteParameterless == null || _canExecuteParameterless();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged.Raise(this, EventArgs.Empty);
        }

        //private bool CanExecuteFalse()
        //{
        //    return false;
        //}

        //private bool CanExecuteTrue()
        //{
        //    return true;
        //}

    }

    public class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event EventHandler CommandExecuted;

        private Action<T> _execute;
        private Predicate<T> _canExecute;

        #region ** Constructors **

        public RelayCommand(Action<T> argExecute, Predicate<T> argCanExecute = null)
        {
            _execute = argExecute;
            _canExecute = argCanExecute;
        }

        #endregion

        #region ** Necessary for Interface **

        public void Execute(object argParameter)
        {
            if (argParameter is T)
            {
                Execute((T)argParameter);
            }
            else
            {
                throw new ArgumentException(String.Format("Argument must be of type {0}. Please refactor.", typeof(T)));
            }
        }

        public bool CanExecute(object argParameter)
        {
            if (argParameter == null)
            {
                return CanExecute();
            }

            if (argParameter is T)
            {
                return CanExecute((T)argParameter);
            }

            throw new ArgumentException(String.Format("Argument must be of type {0}. Please refactor.", typeof(T)));
        }

        #endregion

        public void Execute(T argParameter)
        {
            if (_execute != null)
            {
                _execute(argParameter);
                CommandExecuted.Raise(this);
            }
        }

        public bool CanExecute(T argParameter = default(T))
        {
            return _canExecute == null || _canExecute(argParameter);
        }

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged.Raise(this, EventArgs.Empty);
        }
    }

    public class AsyncRelayCommand : RelayCommand
    {
        private bool _isExecuting;

        public bool IsExecuting
        {
            get { return _isExecuting; }
            set { _isExecuting = value; }
        }

        public event EventHandler Started;

        public event EventHandler Completed;

        public AsyncRelayCommand(Action argExecute, Func<bool> argCanExecute = null) : base(argExecute, argCanExecute) { }

        public override Boolean CanExecute(Object argParameter)
        {
            return ((!this._isExecuting) && (base.CanExecute(argParameter)) );
        }

        public override void Execute(object parameter)
        {
            try
            {
                this._isExecuting = true;
                if (this.Started != null)
                {
                    this.Started(this, EventArgs.Empty);
                }

                Task task = Task.Factory.StartNew(() => this._executeParameterless());
                
                task.ContinueWith(argTask => this.OnRunWorkerCompleted(EventArgs.Empty), TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                this.OnRunWorkerCompleted(new RunWorkerCompletedEventArgs(null, ex, true));
            }
        }

        private void OnRunWorkerCompleted(EventArgs e)
        {
            this._isExecuting = false;
            if (this.Completed != null)
            {
                this.Completed(this, e);
            }
        }

    }

}
