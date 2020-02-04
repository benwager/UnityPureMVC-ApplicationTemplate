//
// This is the primary application mediator. 
// From here we can request to load in application data via the DataSystem module
// And handle changing application state -> Loading / Home etc
//
using UnityPureMVC.Application.Controller.Notes;
using UnityPureMVC.Application.Model.Enums;
using UnityPureMVC.Application.Model.VO;
using UnityPureMVC.Core.Controller.Notes;
using UnityPureMVC.Core.Model.VO;

namespace UnityPureMVC.Application.View.Mediators
{
    internal class ApplicationMediator : ApplicationBaseMediator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:UnityPureMVC.Application.View.Mediators.ApplicationMediator"/> class.
        /// </summary>
        internal ApplicationMediator() : base("ApplicationMediator") { }

        /// <summary>
        /// Core Framework is ready
        /// </summary>
        protected override void OnCoreReady()
        {
            base.OnCoreReady();

            // Request change application state to LOADING
            SendNotification(ApplicationNote.REQUEST_APPLICATION_STATE_CHANGE, new ApplicationStateVO
            {
                state = ApplicationState.LOADING
            });

            // Request load application data
            SendNotification(ApplicationNote.REQUEST_LOAD_APPLICATION_DATA);
        }

        /// <summary>
        /// Handle application ready
        /// </summary>
        protected override void OnApplicationReady()
        {
            viewPrefabPath = "Views/ApplicationView";

            base.OnApplicationReady();

            // Request change application state to MAIN_VIEW
            SendNotification(ApplicationNote.REQUEST_APPLICATION_STATE_CHANGE, new ApplicationStateVO
            {
                state = ApplicationState.MAIN_VIEW
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationStateVO"></param>
        protected override void OnApplicationStateChanged(ApplicationStateVO applicationStateVO)
        {
            // Fade the appilcation view in from black if coming from loading state
            if (applicationStateVO.previousState != null
            && applicationStateVO.previousState.state == ApplicationState.LOADING
            && applicationStateVO.state == ApplicationState.MAIN_VIEW)
            {
                // Black out fades in when loading a new scene ( environment ) 
                // So Request blackout fade out
                // Fades from black to transparent
                SendNotification(CoreNote.REQUEST_BLACKOUT, new RequestBlackoutVO
                {
                    fadeDirection = FadeDirection.OUT,
                    delay = 1.0f
                });
            }
        }
    }
}
