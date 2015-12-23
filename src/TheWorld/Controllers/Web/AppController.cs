using Microsoft.AspNet.Mvc;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;

        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];

                bool mailSent = false;
                if (string.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError("", "Could not send email, configuration problem.");
                }
                else
                {
                    mailSent = _mailService.SendMail(email,
                                                     email,
                                                     $"Contact Page from {contact.Name} ({contact.Email})",
                                                     contact.Message);
                }

                if (mailSent)
                {
                    ModelState.Clear();

                    ViewBag.Message = "Mail Sent. Thanks!";
                }
                else
                {
                    ModelState.AddModelError("", "Could not send mail, ");
                }
            }

            return View();
        }
    }
}
