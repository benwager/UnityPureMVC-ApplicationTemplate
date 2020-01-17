using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityPureMVC.Application.Controller.Commands.Request;
using UnityPureMVC.Application.Controller.Notes;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;

namespace UnityPureMVC.Application.Controller.Commands.Prepare
{
    internal class ApplicationControllerPrepareCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            DebugLogger.Log("ApplicationControllerPrepareCommand::Execute");

            // Register Requests
            Facade.RegisterCommand(ApplicationNote.REQUEST_LOAD_APPLICATION_DATA, typeof(RequestLoadApplicationDataCommand));
            Facade.RegisterCommand(ApplicationNote.REQUEST_CHECK_APPLICATION_DATA_UPDATE, typeof(RequestCheckApplicationDataUpdateCommand));
            Facade.RegisterCommand(ApplicationNote.REQUEST_RELOAD_APPLICATION_DATA, typeof(RequestReloadApplicationDataCommand));
            Facade.RegisterCommand(ApplicationNote.REQUEST_APPLICATION_STATE_CHANGE, typeof(RequestApplicationStateChangeCommand));
        }
    }
}