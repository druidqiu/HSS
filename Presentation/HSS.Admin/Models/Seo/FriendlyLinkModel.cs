using HSS.Web.Framework.Mvc;

namespace HSS.Admin.Models.Seo
{
    public class FriendlyLinkModel : BaseEntityModel
    {
        public string Name { get; set; }

        public string Link { get; set; }
    }
}