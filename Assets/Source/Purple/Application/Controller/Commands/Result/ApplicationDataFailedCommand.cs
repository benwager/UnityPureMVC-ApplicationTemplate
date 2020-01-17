using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityPureMVC.Application.Controller.Notes;
using UnityPureMVC.Core.Controller.Notes;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
using UnityPureMVC.Core.Model.VO;
using UnityPureMVC.Modules.DataLoader.Controller.Notes;

namespace UnityPureMVC.Application.Controller.Commands.Result
{
    internal class ApplicationDataFailedCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            DebugLogger.Log("ApplicationDataFailedCommand::Execute");

            // We no longer need to listen for a data loaded message, so we can un register this command
            Facade.RemoveCommand(DataLoaderNote.DATA_LOADED);
            Facade.RemoveCommand(DataLoaderNote.REQUEST_LOAD_DATA_ERROR);

            string msg = (notification.Body != null) ? notification.Body.ToString() : "Error processing data";

            // Request an error dialog
            // Create a button callback to try to load data again
            SendNotification(CoreNote.ERROR, new CoreErrorVO
            {
                title = "ERROR",
                message = msg,
                buttonText = "TRY AGAIN",
                callback = () =>
                {
                    SendNotification(ApplicationNote.REQUEST_LOAD_APPLICATION_DATA);
                }
            });
        }
    }
}
