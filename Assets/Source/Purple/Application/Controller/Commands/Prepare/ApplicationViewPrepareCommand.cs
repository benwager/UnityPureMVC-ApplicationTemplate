using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityPureMVC.Application.View.Mediators;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;

namespace UnityPureMVC.Application.Controller.Commands.Prepare
{
    internal class ApplicationViewPrepareCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            DebugLogger.Log("ApplicationViewPrepareCommand::Execute");

            Facade.RegisterMediator(new ApplicationMediator());
        }
    }
}