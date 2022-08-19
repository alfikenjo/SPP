using BO_SPP.Common;

namespace BO_SPP.Models
{
    public class NotificationSetting
    {
        public string ID { get; set; }

        private string smtpaddress; public string SMTPAddress { get { return smtpaddress; } set { smtpaddress = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string smtpport; public string SMTPPort { get { return smtpport; } set { smtpport = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string emailaddress; public string EmailAddress { get { return emailaddress; } set { emailaddress = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string password; public string Password { get { return password; } set { password = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string sendername; public string SenderName { get { return sendername; } set { sendername = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }

        public string enc_SMTPAddress { get { return smtpaddress; } set { smtpaddress = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_SMTPPort { get { return smtpport; } set { smtpport = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_EmailAddress { get { return emailaddress; } set { emailaddress = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Password { get { return password; } set { password = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_SenderName { get { return sendername; } set { sendername = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }

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
        private string updatedby; public string UpdatedBy { get { return updatedby; } set { updatedby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        public string UpdatedOn { get; set; }


    }
}
