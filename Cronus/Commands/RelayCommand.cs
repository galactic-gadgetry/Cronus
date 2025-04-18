using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cronus.Commands
{
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// The delegate to determine if the <seealso cref="_execute"/> delegate can
        /// be called.
        /// </summary>
        readonly Predicate<object?>? _canExecute = null;

        /// <summary>
        /// The method to execute when the command is called.
        /// </summary>
        readonly Action<object?> _execute;


        /// <summary>
        /// Initiates a new instance of <see cref="DelegateCommand(object)"/>.
        /// </summary>
        /// <param name="execute">Delegate to execute when Execute is called on the
        /// command.</param>
        /// <remarks><seealso cref="CanExecute"/> will always return True.</remarks>
        public RelayCommand(Action<object?> execute) : this(execute, null) { }

        /// <summary>
        /// Initiates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <exception cref="ArgumentNullException">Thrown if execute is null.</exception>
        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute)
        {
            _canExecute = canExecute;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }


        /// <summary>
        /// Defines the method that determines whether the command can execute in its
        /// current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not
        /// require data to be passed, this object can be set to null.</param>
        /// <returns>True if this command can be executed; otherwise false.</returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should
        /// execute.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not
        /// require data to be passed, this object can be set to null.</param>
        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
