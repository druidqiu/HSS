using HSS.Web.Framework;
using HSS.Web.Framework.Mvc;
using System.Web.Mvc;

namespace HSS.Admin.Models.Settings
{
    public partial class CustomerSettingsModel : BaseModel
    {
        [ResourceDisplayName("Admin.Configuration.Settings.Customer.StoreLastVisitedPage")]
        [AllowHtml]
        public bool StoreLastVisitedPage { get; set; }
        
        [ResourceDisplayName("Admin.Configuration.Settings.Customer.AccessPathEnabled")]
        [AllowHtml]
        public bool AccessPathEnabled { get; set; }

        [ResourceDisplayName("Admin.Configuration.Settings.Customer.AllowUsersToChangeUsernames")]
        [AllowHtml]
        public bool AllowUsersToChangeUsernames { get; set; }

        [ResourceDisplayName("Admin.Configuration.Settings.Customer.UsernameMinLength")]
        [AllowHtml]
        public int UsernameMinLength { get; set; }

        [ResourceDisplayName("Admin.Configuration.Settings.Customer.UsernameMaxLenght")]
        [AllowHtml]
        public int UsernameMaxLenght { get; set; }

        [ResourceDisplayName("Admin.Configuration.Settings.Customer.PasswordMinLength")]
        [AllowHtml]
        public int PasswordMinLength { get; set; }

        [ResourceDisplayName("Admin.Configuration.Settings.Customer.HashedPasswordFormat")]
        [AllowHtml]
        public string HashedPasswordFormat { get; set; }

        [ResourceDisplayName("Admin.Configuration.Settings.Customer.DefaultPasswordFormat")]
        [AllowHtml]
        public int DefaultPasswordFormat { get; set; }
        public SelectList DefaultPasswordFormatValues { get; set; }
    }
}
