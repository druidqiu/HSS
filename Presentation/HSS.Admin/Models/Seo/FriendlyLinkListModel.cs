using HSS.Web.Framework.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HSS.Admin.Models.Seo
{
    /// <summary>
    /// 友情链接列表搜索页
    /// </summary>
    public class FriendlyLinkListModel :BaseModel
    {

        public FriendlyLinkListModel()
        {
            this.AvaiListTarget = new List<SelectListItem>();
        }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keywords { get; set; }

        
        public bool Target { get; set; }
        

        public IList<SelectListItem> AvaiListTarget { get; set; }
    }
}