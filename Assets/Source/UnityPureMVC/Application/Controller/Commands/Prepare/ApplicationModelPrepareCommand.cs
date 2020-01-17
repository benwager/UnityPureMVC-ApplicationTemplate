using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityPureMVC.Application.Model.Proxies;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;

namespace UnityPureMVC.Application.Controller.Commands.Prepare
{
    internal class ApplicationModelPrepareCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            DebugLogger.Log("ApplicationModelPrepareCommand::Execute");

            Facade.RegisterProxy(new ApplicationDataProxy());
            Facade.RegisterProxy(new ApplicationStateProxy());
        }
    }
}