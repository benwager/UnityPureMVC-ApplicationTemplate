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
using UnityPureMVC.Application.Model.Proxies;
using UnityPureMVC.Application.Model.VO;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
using UnityPureMVC.Modules.DataLoader.Controller.Notes;
using UnityPureMVC.Modules.DataLoader.Model.VO;
using System.IO;

namespace UnityPureMVC.Application.Controller.Commands.Request
{
    internal class RequestReloadApplicationDataCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            DebugLogger.Log("RequestReloadApplicationDataCommand::Execute");

            // Register a command that will respond to the DataSystem DATA_LOADED notification
            Facade.RegisterCommand(DataLoaderNote.DATA_LOADED, typeof(ApplicationDataReloadedCommand));

            // Register a command tha will respond to data load error
            Facade.RegisterCommand(DataLoaderNote.REQUEST_LOAD_DATA_ERROR, typeof(ApplicationDataFailedCommand));

            // Retrieve the applicationDataProxy to get API and store details
            ApplicationDataProxy applicationDataProxy = Facade.RetrieveProxy(ApplicationDataProxy.NAME) as ApplicationDataProxy;

            // Set local strings
            string API_BASE_URI = applicationDataProxy.APIBaseURL;
            string API_DATA_ENDPOINT = applicationDataProxy.applicationSettingsVO.api_data_endpoint;

            // Send the request for applicationData based on store_id from settings
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
