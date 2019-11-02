using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365CEMarketingListImportTool.MVVMFramework
{
    public class RelayCommandAsync : ICommand
    {
        private readonly Func<object, Task> _Execute;
        private readonly Func<object, bool> _CanExecute;

        public RelayCommandAsync(Func<object, Task> execute) : this(execute, null)
        {

        }

        public RelayCommandAsync(Func<object, Task> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute", "Execute cannot be null.");
            }

            _Execute = execute;
            _CanExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public async void Execute(object parameter)
        {
            await _Execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (_CanExecute == null)
            {
                return true;
            }

            return _CanExecute(parameter);
        }
    }
}
