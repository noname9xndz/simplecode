using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nexmo.Api.Messaging;
using Nexmo.Api.Request;

namespace SendSmsAspDotnetMvc.Controllers
{
    public class SmsController : Controller
    {
        public IConfiguration Configuration { get; set; }

        public SmsController(IConfiguration config)
        {
            Configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Sms(Models.SmsModel sendSmsModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var NEXMO_API_KEY = Configuration["NEXMO_API_KEY"];
                    var NEXMO_API_SECRET = Configuration["NEXMO_API_SECRET"];
                    var credentials = Credentials.FromApiKeyAndSecret(NEXMO_API_KEY, NEXMO_API_SECRET);
                    var client = new SmsClient(credentials);
                    var request = new SendSmsRequest { To = sendSmsModel.To, From = sendSmsModel.From, Text = sendSmsModel.Text };
                    var response = client.SendAnSms(request);
                    ViewBag.MessageId = response.Messages[0].MessageId;
                }
                catch (NexmoSmsResponseException ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return View("Index");
        }
    }
}