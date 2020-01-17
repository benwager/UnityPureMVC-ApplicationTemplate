using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityPureMVC.Application.Model.Proxies;
using UnityPureMVC.Application.Model.VO;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;

namespace UnityPureMVC.Application.Controller.Commands.Request
{
    internal class RequestApplicationStateChangeCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            DebugLogger.Log("RequestApplicationStateChangeCommand::Execute");

            ApplicationStateProxy applicationStateProxy = Facade.RetrieveProxy(ApplicationStateProxy.NAME) as ApplicationStateProxy;

            ApplicationStateVO applicationStateVO = notification.Body as ApplicationStateVO;

            // First check new state is not same as old state
            if (applicationStateProxy.CurrentState != null && applicationStateProxy.CurrentState.state == applicationStateVO.state)
            {
                DebugLogger.LogWarning("Application State is already set to {0}", applicationStateProxy.CurrentState.state.ToString());
                return;
            }

            if (applicationStateVO.previousState == null)
            {
                applicationStateVO.previousState = applicationStateProxy.CurrentState;
            }

            applicationStateProxy.CurrentState = applicationStateVO;
        }
    }
}