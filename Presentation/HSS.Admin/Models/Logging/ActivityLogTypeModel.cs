using HSS.Web.Framework;
using HSS.Web.Framework.Mvc;

namespace HSS.Admin.Models.Logging
{
    public partial class ActivityLogTypeModel : BaseEntityModel
    {
        [ResourceDisplayName("Admin.Configuration.ActivityLog.ActivityLogType.Fields.Name")]
        public string Name { get; set; }
        [ResourceDisplayName("Admin.Configuration.ActivityLog.ActivityLogType.Fields.Enabled")]
        public bool Enabled { get; set; }
    }
}