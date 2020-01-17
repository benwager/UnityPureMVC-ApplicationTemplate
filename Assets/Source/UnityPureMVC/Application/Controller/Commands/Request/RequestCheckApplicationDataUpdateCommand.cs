/****************************************************** 
 * A request to handle processing application data
 * via the DataSystem module
 * 
 * Responds to ApplicationNote.REQUEST_LOAD_APPLICATION_DATA
 * Registered in ApplicationControllerPrepareCommand
 * 
 ***************************************************/

using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityPureMVC.Application.Controller.Notes;
using UnityPureMVC.Application.Model.Proxies;
using UnityPureMVC.Application.Model.VO;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
using UnityPureMVC.Modules.DataLoader.Controller.Notes;
using UnityPureMVC.Modules.DataLoader.Model.VO;
using System;
using System.IO;
using UnityEngine;

namespace UnityPureMVC.Application.Controller.Commands.Request
{
    internal class RequestCheckApplicationDataUpdateCommand : SimpleCommand
    {
        ApplicationDataProxy applicationDataProxy;

        public override void Execute(INotification notification)
        {
            DebugLogger.Log("RequestCheckApplicationDataUpdateCommand::Execute");

            // We will handle the response in an onComplete Callback instead of passing to a Result command
            //Facade.RegisterCommand(DataSystemNote.DATA_LOADED, typeof(ApplicationDataReloadedCommand));

            // Register a command tha will respond to data load error
            // Silently fail data reloads
            // Facade.RegisterCommand(DataSystemNote.REQUEST_LOAD_DATA_ERROR, typeof(ApplicationDataFailedCommand));

            // Retrieve the applicationDataProxy to get API details
            applicationDataProxy = Facade.RetrieveProxy(ApplicationDataProxy.NAME) as ApplicationDataProxy;

            // Set local strings
            string API_BASE_URI = applicationDataProxy.APIBaseURL;
            string API_DATA_ENDPOINT = applicationDataProxy.applicationSettingsVO.api_data_endpoint;

            // Send the request for application Data
            SendNotification(DataLoaderNote.REQUEST_LOAD_DATA, new RequestLoadDataVO
            {
                requestType = RequestLoadDataType.WWW,
                path = Path.Combine(API_BASE_URI, API_DATA_ENDPOINT),
                dataType = typeof(ApplicationDataVO),
                cache = false,
                forceCacheRefresh = true,
                onComplete = DataReceived
            });
        }

        // This is where we check the last and new updated dates
        protected void DataReceived(object data)
        {
            ApplicationDataVO incomingData = data as ApplicationDataVO;

            // Clear the error handler ready for next shot
            Facade.RemoveCommand(DataLoaderNote.REQUEST_LOAD_DATA_ERROR);

            // Get the most recent update time
            DateTime last_updated = applicationDataProxy.LastUpdatedDate;

            DebugLogger.Log("{0} :: {1}", incomingData.last_updated.ToString(), last_updated.ToString());

            if (incomingData.last_updated > last_updated)
            {
                applicationDataProxy.LastUpdatedDate = incomingData.last_updated;

                PlayerPrefs.SetString("last_updated", applicationDataProxy.LastUpdatedDate.ToString());

                // Put in a request to reload the scene data
                SendNotification(ApplicationNote.REQUEST_RELOAD_APPLICATION_DATA);
            }
        }
    }
}
