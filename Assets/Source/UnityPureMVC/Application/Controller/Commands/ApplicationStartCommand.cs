namespace UnityPureMVC.Application.Controller.Commands
{
    using PureMVC.Interfaces;
    using PureMVC.Patterns.Command;
    using UnityPureMVC.Application.Controller.Commands.Prepare;
    using UnityPureMVC.Application.Controller.Notes;
    using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;

    internal class ApplicationStartCommand : MacroCommand
    {
        /// <summary>
        /// Initializes the macro command.
        /// </summary>
        protected override void InitializeMacroCommand()
        {
            DebugLogger.Log("ApplicationStartCommand::InitializeMacroCommand");

            AddSubCommand(typeof(ApplicationModelPrepareCommand));
            AddSubCommand(typeof(ApplicationViewPrepareCommand));
            AddSubCommand(typeof(ApplicationControllerPrepareCommand));

        }
        public override void Execute(INotification notification)
        {
            base.Execute(notification);
            Facade.RemoveCommand(ApplicationNote.START);
        }
    }
}