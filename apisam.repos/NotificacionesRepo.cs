namespace apisam.repos
{
    using apisam.entities;
    using Newtonsoft.Json.Linq;
    using ServiceStack;
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class NotificacionesRepo
    {
        //public async Task SendNotifications(List<string> registrationTokens, string body)
        //{



        //    try
        //    {
        //        FirebaseApp.Create(new AppOptions
        //        { Credential = GoogleCredential.FromFile("KeyFirebaseAdmin.json"), });
        //        var messages = new List<Message>();


        //        registrationTokens.ForEach(token =>
        //        {
        //            var message = new Message()
        //            {
        //                Token = token,
        //                Notification = new Notification()
        //                {
        //                    Title = "Sistema de Administración Médica",
        //                    Body = body,
        //                },
        //                //Android = new AndroidConfig()
        //                //{

        //                //    TimeToLive = TimeSpan.FromHours(1),
        //                //    Notification = new AndroidNotification()
        //                //    {
        //                //        Title = "Sistema de Administración Médica",
        //                //        Body = body,
        //                //        Icon = "stock_ticker_update",
        //                //        Color = "#f45342",
        //                //        ClickAction = "FLUTTER_NOTIFICATION_CLICK",
        //                //        Sound = "default",
        //                //    },
        //                //},

        //                //Apns = new ApnsConfig()
        //                //{
        //                //    Aps = new Aps()
        //                //    {
        //                //        Badge = 1,
        //                //        Sound = "default",

        //                //    },
        //                //},
        //            };
        //            messages.Add(message);
        //        });



        //        var resp = await FirebaseMessaging.DefaultInstance.SendAllAsync(messages);





        //    }


        //    catch (Exception ex)
        //    {
        //        var a = ex.Message;
        //    }
        //}
        public async Task SendNoti(string token, string body)
        {
            var hhtp = new HttpClient();
            var apiKey = "AAAAQJdZFO0:APA91bF9RDeLhPxW9BIalh83T-JWJNym08-GucjjNe6qn3ErdZlW_okEc1p3RNMdK4xukxqr0tbpnl68kdcCztRTsO2knA9qmwTwnN_Tl_6rJdR0mkK_MGC1XX8F1m2HFp2NSY1ugv0d";
            hhtp.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("key", "=" + apiKey);

            var url = "https://fcm.googleapis.com/fcm/send";
            try
            {
                var mensaje = new NotificationMessage()
                {
                    To = token,
                    Notification = new NotificationModel()
                    {

                        Sound = "default",
                        Title = "Sistema de Administración Médica",
                        Body = body,
                        ClickAction = "FLUTTER_NOTIFICATION_CLICK"
                    }
                };

                var json = mensaje.ToJson();
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = await hhtp.PostAsync(url, data);

                var starus = resp.StatusCode;


            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
        }

        public async Task<bool> Exists(int doctorId, int esPreclinica)
        {
            try
            {
                var hhtp = new HttpClient();
                var _url = "";
                //preclinica 1 agenda 0
                if(esPreclinica ==1)
                {
                    _url = $"https://us-central1-sam-app-446ee.cloudfunctions.net/api/consulta/{doctorId}";
                }
                else
                {
                    _url = $"https://us-central1-sam-app-446ee.cloudfunctions.net/api/agenda/{doctorId}";
                }
              

                var _resp = await hhtp.GetAsync(_url);

                if (_resp.IsSuccessStatusCode)
                {
                    var result = await _resp.Content.ReadAsStringAsync();
                    dynamic json = JObject.Parse(result);
                    var st = json.status;
                    if (st == 404)
                    {
                        return false;
                    }
                    else
                    {
                        return true;

                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public async Task CrearNotificacion(CrearNotificacionModel model,int esPreclinica)
        {
            var hhtp = new HttpClient();
            string _url;
            if (esPreclinica == 1)
            {
                _url = "https://us-central1-sam-app-446ee.cloudfunctions.net/api/consulta";
            }
            else
            {
                _url = "https://us-central1-sam-app-446ee.cloudfunctions.net/api/agenda";
            }

            try
            {
                var json = model.ToJson();
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = await hhtp.PostAsync(_url, data);

                var status = resp.StatusCode;
            }
            catch (Exception )
            {

                throw;
            }
        }

        public async Task SumaNotificacion(int doctorId, int esPreclinica)
        {
            var hhtp = new HttpClient();
            string _url;
            if (esPreclinica == 1)
            {
                _url = $"https://us-central1-sam-app-446ee.cloudfunctions.net/api/consulta/suma/{doctorId}";
            }
            else
            {
                _url = $"https://us-central1-sam-app-446ee.cloudfunctions.net/api/agenda/suma/{doctorId}";
            }

            try
            {
                var menti = "";
                      var data = new StringContent(menti, Encoding.UTF8, "application/json");

                var resp = await hhtp.PostAsync(_url,data);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task RestaNotificacion(int doctorId, int esPreclinica)
        {
            var hhtp = new HttpClient();
            string _url;
            if (esPreclinica == 1)
            {
                _url = $"https://us-central1-sam-app-446ee.cloudfunctions.net/api/consulta/resta/{doctorId}";
            }
            else
            {
                _url = $"https://us-central1-sam-app-446ee.cloudfunctions.net/api/agenda/resta/{doctorId}";
            }

            try
            {
                var menti = "";
                var data = new StringContent(menti, Encoding.UTF8, "application/json");

                var resp = await hhtp.PostAsync(_url, data);


            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
