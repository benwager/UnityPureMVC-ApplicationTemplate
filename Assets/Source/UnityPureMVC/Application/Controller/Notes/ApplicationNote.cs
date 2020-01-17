namespace UnityPureMVC.Application.Controller.Notes
{
    internal struct ApplicationNote
    {
        internal const string START = "application/Start";
        internal const string REQUEST_LOAD_APPLICATION_DATA = "application/requestLoadApplicationData";
        internal const string REQUEST_RELOAD_APPLICATION_DATA = "application/requestReloadApplicationData";
        internal const string REQUEST_CHECK_APPLICATION_DATA_UPDATE = "application/requestCheckApplicationDataUpdate";
        internal const string APPLICATION_READY = "application/applicationReady";
        internal const string APPLICATION_DATA_RELOADED = "application/applicationDataReloaded";
        internal const string REQUEST_APPLICATION_STATE_CHANGE = "application/requestApplicationStateChange";
        internal const string APPLICATION_STATE_CHANGED = "application/applicationStateChanged";
    }
}