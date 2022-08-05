using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BO_SPP.Models
{
    public class NotificationSetting
    {
        public string ID { get; set; }
        public string SMTPAddress { get; set; }
        public string SMTPPort { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string SenderName { get; set; }
        public bool EnableSSL { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool UseAsync { get; set; }
        public bool EnableServices { get; set; }

        public bool NewUser { get; set; }
        public bool NewRoleAssignment { get; set; }
        public bool UserPasswordReset { get; set; }
        public bool Messaging { get; set; }
        public bool ReminderServices { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedOn { get; set; }


    }
}
