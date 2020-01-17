using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityPureMVC.Application.Controller.Notes;
using UnityPureMVC.Application.Model.Proxies;
using UnityPureMVC.Application.Model.VO;
using UnityPureMVC.Core.Controller.Notes;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
using UnityPureMVC.Core.Model.VO;
using UnityPureMVC.Modules.DataLoader.Controller.Notes;
using System.Collections;
using UnityEngine;

namespace UnityPureMVC.Application.Controller.Commands.Result
{
    internal class ApplicationDataLoadedCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            DebugLogger.Log("ApplicationDataLoadedCommand::Execute");

            ApplicationDataProxy applicationDataProxy = Facade.RetrieveProxy(ApplicationDataProxy.NAME) as ApplicationDataProxy;

            // Check data type
            if ((notification.Body as ApplicationSettingsVO) != null)
            {
                applicationDataProxy.applicationSettingsVO = notification.Body as ApplicationSettingsVO;
            }
            else if ((notification.Body as ApplicationDataVO) != null)
            {
                applicationDataProxy.Data = notification.Body as ApplicationDataVO;
                PlayerPrefs.SetString("last_updated", applicationDataProxy.LastUpdatedDate.ToString());
            }
            else
            {
                SendNotification(DataLoaderNote.REQUEST_LOAD_DATA_ERROR);
            }

            // Check if all data is loaded
            // And we can declare application ready
            if (applicationDataProxy.applicationSettingsVO != null
            && applicationDataProxy.ApplicationDataVO != null)
            {
                DebugLogger.Log("All Application data loaded!");

                // Clean up
                Facade.RemoveCommand(DataLoaderNote.DATA_LOADED);
                Facade.RemoveCommand(DataLoaderNote.REQUEST_LOAD_DATA_ERROR);

                // Notify System application is ready
                SendNotification(ApplicationNote.APPLICATION_READY);

                // Set up coroutine to check for updates
                SendNotification(CoreNote.REQUEST_START_COROUTINE, new RequestStartCoroutineVO()
                {
                    coroutine = CheckForUpdates()
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IEnumerator CheckForUpdates()
        {
            while (true)
            {
                SendNotification(ApplicationNote.REQUEST_CHECK_APPLICATION_DATA_UPDATE);
                yield return new WaitForSeconds(5.0f);
            }
        }
    }
}

