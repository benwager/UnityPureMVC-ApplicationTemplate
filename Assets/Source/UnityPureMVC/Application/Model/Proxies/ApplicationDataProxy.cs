using PureMVC.Patterns.Proxy;
using UnityPureMVC.Application.Model.VO;
using UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging;
using System;
using System.Linq;

namespace UnityPureMVC.Application.Model.Proxies
{
    internal class ApplicationDataProxy : Proxy
    {
        new internal const string NAME = "ApplicationDataProxy";

        internal ApplicationDataVO ApplicationDataVO { get { return Data as ApplicationDataVO; } }

        internal ApplicationSettingsVO applicationSettingsVO;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:UnityPureMVC.Core.Model.Proxies.ApplicationDataProxy"/> class.
        /// </summary>
        internal ApplicationDataProxy() : base(NAME)
        {
            DebugLogger.Log(NAME + "::__Contstruct");
        }

        /// <summary>
        /// Gets the API Base URL from Settings
        /// </summary>
        /// <returns></returns>
        internal string APIBaseURL
        {
            get
            {
                string uri = applicationSettingsVO.api_base_url;
                if (string.IsNullOrEmpty(uri))
                {
                    uri = UnityEngine.Application.dataPath;
                }
                return uri;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal string DataVersion
        {
            get
            {
                return ApplicationDataVO.data_version;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal DateTime LastUpdatedDate
        {
            get
            {
                return ApplicationDataVO.last_updated;
            }
            set
            {
                ApplicationDataVO.last_updated = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal Session Session
        {
            get
            {
                return ApplicationDataVO.session;
            }
            set
            {
                ApplicationDataVO.session = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckSession()
        {
            if (ApplicationDataVO.session == null)
            {
                ApplicationDataVO.session = new Session();
            }
        }
    }
}