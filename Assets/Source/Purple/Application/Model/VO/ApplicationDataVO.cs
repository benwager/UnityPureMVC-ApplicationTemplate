//
// This is passed through to the DataSystem to convert the json data at Assets/Resources/Data/Data.json
// in to a readable object
//
using UnityPureMVC.Modules.DataLoader.Model.VO;
using System;

namespace UnityPureMVC.Application.Model.VO
{
    public class Session
    {
    
    }

    [System.Serializable]
    public class ApplicationDataVO : IDataLoaderResult
    {
        public string data_version;
        public DateTime last_updated;
        public Session session;
    }
}