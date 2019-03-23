using System;
using System.Windows;
using System.Windows.Input;
using WpfSimpleMVVM.Exeptions;

namespace WpfSimpleMVVM.Tools
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<T> execute) : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            try
            {

                return _canExecute?.Invoke((T)parameter) ?? true;
            }
            catch (EMailExeption e)
            {
                MessageBox.Show("Email ERROR");
                throw;
            }
        }

        public void Execute(object parameter) => _execute((T)parameter);
    }
}
