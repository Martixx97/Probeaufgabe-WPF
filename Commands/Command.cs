using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Probeaufgabe_WPF.Commands
{
    internal class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Action<object> _Execute { get; set; }

        private Func<object, bool> _CanExecute { get; set; }

        public Command(Func<object, bool> CanExecuteMethod, Action<object> ExecuteMethod)
        {
            _Execute = ExecuteMethod;
            _CanExecute = CanExecuteMethod;
        }

        public virtual bool CanExecute(object? parameter)
        {
            return _CanExecute(parameter);
        }
        public virtual void Execute(object? parameter)
        {
            _Execute(parameter);
        }
    }
}
