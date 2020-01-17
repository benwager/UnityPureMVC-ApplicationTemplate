using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityPureMVC.Application.Controller.Notes;
using UnityPureMVC.Application.Model.Proxies;
using UnityPureMVC.Application.Model.VO;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
using UnityPureMVC.Modules.DataLoader.Controller.Notes;

namespace UnityPureMVC.Application.Controller.Commands.Result
{
    internal class ApplicationDataReloadedCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            DebugLogger.Log("ApplicationDataReloadedCommand::Execute");

            ApplicationDataProxy applicationDataProxy = Facade.RetrieveProxy(ApplicationDataProxy.NAME) as ApplicationDataProxy;

            // Check data type
            if ((notification.Body as ApplicationDataVO) != null)
            {
                // Maintain Session when updating application data
                Session session = applicationDataProxy.Session;
                applicationDataProxy.Data = notification.Body as ApplicationDataVO;
                applicationDataProxy.Session = session;
            }
            else
            {
                // Report an error
                SendNotification(DataLoaderNote.REQUEST_LOAD_DATA_ERROR);
                return;
            }

            // Clean up
            Facade.RemoveCommand(DataLoaderNote.DATA_LOADED);
            Facade.RemoveCommand(DataLoaderNote.REQUEST_LOAD_DATA_ERROR);

            // Notify System that application is ready
            SendNotification(ApplicationNote.APPLICATION_DATA_RELOADED);
        }
    }
}
