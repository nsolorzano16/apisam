using apisam.entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace apisam.interfaces
{
    public interface IEmail
    {
    IRestResponse SendEmail(string emailAddress, string body, MailJet options);
    }
}
