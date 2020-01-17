namespace UnityPureMVC.Application
{
    using PureMVC.Interfaces;
    using PureMVC.Patterns.Facade;
    using UnityPureMVC.Application.Controller.Commands;
    using UnityPureMVC.Application.Controller.Notes;
    using UnityPureMVC.Core;
    using System;
    using UnityEngine;

    internal sealed class ApplicationBehaviour : CoreBehaviour
    {
        /// <summary>
        /// The core facade.
        /// </summary>
        private IFacade facade;

        /// <summary>
        /// Start this instance.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            try
            {
                facade = Facade.GetInstance("Core");
                facade.RegisterCommand(ApplicationNote.START, typeof(ApplicationStartCommand));
                facade.SendNotification(ApplicationNote.START, this, null, "Core");
            }
            catch (Exception exception)
            {
                throw new UnityException("Unable to initiate Facade", exception);
            }
        }

        /// <summary>
        /// On destroy.
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (facade != null)
            {
                facade.Dispose();
                facade = null;
            }
        }
    }
}