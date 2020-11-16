using System;
using System.Collections.Generic;
using System.Text;

namespace apisam.entities
{
    public class MailJet
    {
        public string Host { get; set; }
        public string APIKey { get; set; }
        public string APISecret { get; set; }
        public int Port { get; set; }
        public string SenderEmail { get; set; }

    }
}
