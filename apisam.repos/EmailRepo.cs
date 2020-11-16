using apisam.entities;
using apisam.interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks; 
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace apisam.repos
{
    public class EmailRepo : IEmail
    {
        public IRestResponse SendEmail(string emailAddress, string body, MailJet options)
        {

            // Compose a message
            RestClient client = new RestClient
            {
                BaseUrl = new Uri("https://api.mailgun.net/v3")
            };
          

            string html = "<!DOCTYPE html><html lang=\"en\">" +
               "<head>    <meta charset=\"UTF-8\">    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">    " +
               "<meta http-equiv=\"X-UA-Compatible\" content=\"ie=edge\">    " +
               " <script src=\"https://code.jquery.com/jquery-3.3.1.slim.min.js\"        integrity=\"sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo\"        crossorigin=\"anonymous\">" +
               "</script>    <script src=\"https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js\"        integrity=\"sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1\"        crossorigin=\"anonymous\"></script>    <script src=\"https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js\"        integrity=\"sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM\"        crossorigin=\"anonymous\">" +
               "</script>    <title>Document</title></head>" +
               "<body>    <div class=\"container\">        " +
               "<div class=\"row align-items-center\">            " +
               "<div class=\"col-md-3\"></div>            " +
               "<div class=\"col-md-6 align-items-center\">    " +
               $"<h2><p><a href=\"" + body+ " \">" +"Click para activar la cuenta</a></p> </h2>   " +             
            "</body></html>";


            client.Authenticator =    new HttpBasicAuthenticator("api",
                "62dc8ac2428e0d1b09cb4b5a8fabb7c0-07e45e2a-17696d41");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox9481509a7839446faf6a80b34501751b.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "SAM <mailgun@sandbox9481509a7839446faf6a80b34501751b.mailgun.org>");
            request.AddParameter("to", "info@app-sam.com");      
            request.AddParameter("subject", "Bienvenido a nuestra plataforma");           
            request.AddParameter("html", html);
            request.Method = Method.POST;
            return client.Execute(request); 
        }
    }
}
