//
// This is passed through to the DataSystem to convert the json data at Assets/Resources/Data/Data.json
// in to a readable object
//
using UnityPureMVC.Modules.DataLoader.Model.VO;

namespace UnityPureMVC.Application.Model.VO
{
    [System.Serializable]
    public class ApplicationSettingsVO : IDataLoaderResult
    {
        public string dataVersion;
        public string api_base_url;
        public string api_data_endpoint;
    }
}