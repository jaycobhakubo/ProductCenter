using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace GTI.Modules.ProductCenter.UI.Discounts
{
    /// <summary>
    /// A command that relays its functionality to other objects via delegates.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Events
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command
        /// should execute. 
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        #endregion

        #region Member Variables
        private bool m_isEnabled = true;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the RelayCommand that can always
        /// execute.
        /// </summary>
        /// <param name="execute">A delegate to execution logic.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand.
        /// </summary>
        /// <param name="execute">A delegate to execution logic.</param>
        /// <param name="canExecute">A delegate the execution status
        /// logic.</param>
        /// <exception cref="System.ArgumentNullException">execute is a null
        /// reference.</exception>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            ExecuteDelegate = execute;
            CanExecuteDelegate = canExecute;
        }
        #endregion

        #region Member Methods
        /// <summary>
        /// Called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command
        /// does not require data to be passed, this object can be set to
        /// null.</param>
        public void Execute(object parameter)
        {
            if (IsEnabled)
                ExecuteDelegate(parameter);
        }

        /// <summary>
        /// Determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command
        /// does not require data to be passed, this object can be set to
        /// null.</param>
        /// <returns>true if this command can be executed; otherwise,
        /// false.</returns>
        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate == null)
                return IsEnabled;
            else if (IsEnabled)
                return CanExecuteDelegate(parameter);
            else
                return false;
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets or sets whether this command is enabled.  If the command is
        /// disabled, then it cannot execute.
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return m_isEnabled;
            }
            set
            {
                if (m_isEnabled != value)
                {
                    m_isEnabled = value;
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        /// <summary>
        /// Gets or sets the delegate used to execute the command.
        /// </summary>
        private Action<object> ExecuteDelegate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the delegate used to check if the command can be
        /// executed.
        /// </summary>
        private Predicate<object> CanExecuteDelegate
        {
            get;
            set;
        }
        #endregion
    }
}
