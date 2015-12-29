using System.Linq;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private WorldContext _context;

        public AppController(IMailService mailService, WorldContext context)
        {
            _mailService = mailService;
            _context = context;
        }

        public IActionResult Index()
        {
            var trips = _context
                            .Trips
                            .OrderBy(t=>t.Name)
                            .ToList();

            return View(trips);
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
