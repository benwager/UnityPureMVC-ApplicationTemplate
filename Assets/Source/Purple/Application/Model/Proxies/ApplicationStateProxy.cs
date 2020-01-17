using PureMVC.Patterns.Proxy;
using UnityPureMVC.Application.Controller.Notes;
using UnityPureMVC.Application.Model.VO;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;

namespace UnityPureMVC.Application.Model.Proxies
{
    internal class ApplicationStateProxy : Proxy
    {
        new internal const string NAME = "ApplicationStateProxy";

        internal ApplicationStateVO ApplicationStateVO { get { return Data as ApplicationStateVO; } }

        /// <summary>
        /// 
        /// </summary>
        internal ApplicationStateProxy() : base(NAME)
        {
            DebugLogger.Log(NAME + "::__Contstruct");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationStateVO"></param>
        internal ApplicationStateVO CurrentState
        {
            get
            {
                return ApplicationStateVO;
            }
            set
            {
                Data = value;
                SendNotification(ApplicationNote.APPLICATION_STATE_CHANGED, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal ApplicationStateVO PreviousState
        {
            get
            {
                return ApplicationStateVO.previousState;
            }
        }
    }
}
