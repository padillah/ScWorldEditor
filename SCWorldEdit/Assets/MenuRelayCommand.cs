using System;

namespace SCWorldEdit.Assets
{
    public class MenuRelayCommand : RelayCommand
    {
        public bool IsSeparator { get; set; }
        public string HeaderText { get; set; }

        public new bool CanExecute
        {
            get
            {
                return _canExecuteParameterless == null || _canExecuteParameterless();
            }
        }

        public MenuRelayCommand(string argHeaderText, Action argExecute, Func<bool> argCanExecute = null)
            : base(argExecute, argCanExecute)
        {
            HeaderText = argHeaderText;
        }

        public MenuRelayCommand(string argHeaderText, RelayCommand argCommand)
            : this(argHeaderText, argCommand.Execute, argCommand.CanExecute)
        {}

        public MenuRelayCommand()
            : base(null, null)
        {
            IsSeparator = true;            
        }

    }
}