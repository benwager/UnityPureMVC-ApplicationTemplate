using PureMVC.Interfaces;
using UnityPureMVC.Application.Controller.Notes;
using UnityPureMVC.Application.Model.Enums;
using UnityPureMVC.Application.Model.Proxies;
using UnityPureMVC.Application.Model.VO;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
using UnityPureMVC.Core.View.Mediators;
using System.Collections.Generic;
using UnityEngine;

namespace UnityPureMVC.Application.View.Mediators
{
    internal abstract class ApplicationBaseMediator : CoreBaseMediator
    {
        new internal string NAME = "ApplicationBaseMediator";

        // Store references to application data and state proxies
        protected ApplicationDataProxy applicationDataProxy;
        protected ApplicationStateProxy applicationStateProxy;

        // Associated View component (GameObject) prefab path
        protected string viewPrefabPath = "";

        protected Transform m_transform;

        protected GameObject m_gameObject;

        // Cast ViewComponent to GameObject
        new protected GameObject ViewComponent { get { return m_gameObject; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:UnityPureMVC.Application.View.Mediators.ApplicationBaseMediator"/> class.
        /// </summary>
        internal ApplicationBaseMediator(string _NAME) : base(_NAME) { NAME = _NAME; }

        /// <summary>
        /// Gets the list notification interests.
        /// </summary>
        /// <value>The list notification interests.</value>
        public override IEnumerable<string> ListNotificationInterests
        {
            get
            {
                return new List<string>(base.ListNotificationInterests)
                {
                    ApplicationNote.APPLICATION_READY,
                    ApplicationNote.APPLICATION_STATE_CHANGED,
                    ApplicationNote.APPLICATION_DATA_RELOADED
                };
            }
        }

        /// <summary>
        /// Handles the notification.
        /// </summary>
        /// <param name="notification">Notification.</param>
        public override void HandleNotification(INotification notification)
        {
            base.HandleNotification(notification);

            switch (notification.Name)
            {
                case ApplicationNote.APPLICATION_READY:
                    OnApplicationReady();
                    break;

                case ApplicationNote.APPLICATION_STATE_CHANGED:
                    OnApplicationStateChanged(notification.Body as ApplicationStateVO);
                    break;

                case ApplicationNote.APPLICATION_DATA_RELOADED:
                    OnApplicationDataReloaded();
                    break;

                default: break;
            }
        }

        /// <summary>
        /// Called once application data has been loaded
        /// </summary>
        protected virtual void OnApplicationReady()
        {
            DebugLogger.Log(NAME + "::OnApplicationReady");

            // Example of how to reference data and state proxies
            // Register Proxies in Application.Controller.Commands.Prepare.ApplicationModelPrepareCommand
            // Then they are available in any Mediator or Command class like this...
            applicationDataProxy = Facade.RetrieveProxy(ApplicationDataProxy.NAME) as ApplicationDataProxy;
            applicationStateProxy = Facade.RetrieveProxy(ApplicationStateProxy.NAME) as ApplicationStateProxy;

            InitializeViewComponent();
        }

        /// <summary>
        /// Instantiate the associated ViewComponent
        /// </summary>
        /// <remarks>
        ///     If a prefab path has been set in the class extending this
        ///     We load it and cast ViewComponent to GameObject to access later
        /// </remarks>
        protected virtual void InitializeViewComponent()
        {
            if (string.IsNullOrEmpty(viewPrefabPath))
            {
                DebugLogger.LogWarning("No ViewPrefabPath set in {0}", NAME);
                return;
            }

            m_gameObject = GameObject.Instantiate(Resources.Load(viewPrefabPath)) as GameObject;
            m_transform = m_gameObject.transform;
            m_viewComponent = m_gameObject;
        }

        /// <summary>
        /// Handle application state changes
        /// </summary>
        /// <remarks>
        ///     Override to receive notice of application state changes
        ///     check the applicationStateVO.state to filter for required state
        /// </remarks>
        /// <param name="applicationStateVO">The new application state object</param>
        protected virtual void OnApplicationStateChanged(ApplicationStateVO applicationStateVO)
        {
            switch (applicationStateVO.state)
            {
                case ApplicationState.LOADING:
                    DebugLogger.Log(NAME + "::OnApplicationStateChanged -> ApplicationState.LOADING");
                    break;

                case ApplicationState.MAIN_VIEW:
                    DebugLogger.Log(NAME + "::OnApplicationStateChanged -> ApplicationState.MAIN_VIEW");
                    break;
            }
        }

        /// <summary>
        /// Called when application data is reloaded
        /// </summary>
        /// <remarks>
        ///     Override this method to instigate an action when application data is reloaded
        ///     Requires use of the Data Module
        ///     Updated application data will be available via applicationDataProxy
        /// </remarks>
        protected virtual void OnApplicationDataReloaded()
        {
            // Do something 
        }
    }
}
