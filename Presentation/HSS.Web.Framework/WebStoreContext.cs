using HSS.Core;
using HSS.Models.Configuration;

namespace HSS.Web.Framework
{
    public partial class WebStoreContext : IStoreContext
    {
        private readonly CommonSettings _commonSettings;
        public WebStoreContext(CommonSettings commonSettings)
        {
            this._commonSettings = commonSettings;
        }

        public bool SslEnable
        {
            get
            {
                return _commonSettings.SiteSslEnabled;
            }
        }
    }
}
