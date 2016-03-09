using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IESB_TC2S2015.Commands
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action method;
        private Func<bool> canExecute;

        public Command(Action method, Func<bool> canExecute = null)
        {
            this.method = method;
            this.canExecute = canExecute ?? (() => true);
        }
        public bool CanExecute(object parameter)
        {
            return canExecute();
        }
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                this.method();
        }
    }

    public class Command<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<T> method;
        private Func<bool> canExecute;

        public Command(Action<T> method, Func<bool> cancExecute = null)
        {
            this.method = method;
            this.canExecute = canExecute ?? (() => true);
        }
        public bool CanExecute(object parameter)
        {
            return canExecute();
        }
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                this.method((T)parameter);
        }
    }
}
