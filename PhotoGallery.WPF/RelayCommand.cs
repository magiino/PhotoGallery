using System;
using System.Windows.Input;

namespace PhotoGallery.WPF
{
    public class RelayCommand : ICommand
    {
        #region Private Members
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        #endregion

        #region Public Events
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        #endregion

        #region Constructor
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute = null)
            : this(p => execute(), p => canExecute?.Invoke() ?? true) { }

        #endregion

        #region Command Methods
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        #endregion
    }
}