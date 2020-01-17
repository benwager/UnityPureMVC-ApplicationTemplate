/***************************************************
 * 
 * A request to handle processing application data
 * via the DataSystem module
 * 
 * Responds to ApplicationNote.REQUEST_LOAD_APPLICATION_DATA
 * Registered in ApplicationControllerPrepareCommand
 * 
 ***************************************************/

using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityPureMVC.Application.Controller.Commands.Result;
using UnityPureMVC.Application.Model.VO;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
using UnityPureMVC.Modules.DataLoader.Controller.Notes;
using UnityPureMVC.Modules.DataLoader.Model.VO;
using System.IO;

namespace UnityPureMVC.Application.Controller.Commands.Request
{
    internal class RequestLoadApplicationDataCommand : SimpleCommand
    {
        private ApplicationSettingsVO applicationSettingsVO;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        public override void Execute(INotification notification)
        {
            DebugLogger.Log("RequestLoadApplicationDataCommand::Execute");

            // Register a command that will respond to the DataSystem DATA_LOADED notification
            Facade.RegisterCommand(DataLoaderNote.DATA_LOADED, typeof(ApplicationDataLoadedCommand));

            // Register a command tha will respond to data load error
            Facade.RegisterCommand(DataLoaderNote.REQUEST_LOAD_DATA_ERROR, typeof(ApplicationDataFailedCommand));

            // Send the request for application Settings
            SendNotification(DataLoaderNote.REQUEST_LOAD_DATA, new RequestLoadDataVO
            {
                requestType = RequestLoadDataType.WWW,
                path = Path.Combine(UnityEngine.Application.dataPath, "Data/Settings.json"),
                dataType = typeof(ApplicationSettingsVO),
                cache = false,
                onComplete = SettingsLoaded
            });
        }

        /// <summary>
        /// After settings have loaded, we request the data
        /// </summary>
        /// <param name="data"></param>
        private void SettingsLoaded(object data)
        {
            applicationSettingsVO = data as ApplicationSettingsVO;

            string API_BASE_URI = applicationSettingsVO.api_base_url;
            string API_DATA_ENDPOINT = applicationSettingsVO.api_data_endpoint;

            if (string.IsNullOrEmpty(API_BASE_URI))
            {
                API_BASE_URI = UnityEngine.Application.dataPath;
            }

            // Send the request for application data
            SendNotification(DataLoaderNote.REQUEST_LOAD_DATA, new RequestLoadDataVO
            {
                requestType = RequestLoadDataType.WWW,
                path = Path.Combine(API_BASE_URI, API_DATA_ENDPOINT),
                dataType = typeof(ApplicationDataVO),
                cache = true,
                forceCacheRefresh = true
            });
        }
    }
}
