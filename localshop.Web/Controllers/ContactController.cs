using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace localshop.Controllers
{
    public class ContactController : Controller
    {
        private IContactRepository _contactRepo;

        public ContactController(IContactRepository contactRepo)
        {
            _contactRepo = contactRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessage(ContactDTO contact)
        {
            if (!ModelState.IsValid)
            {
                return View("index", contact);
            }

            var result = _contactRepo.Save(contact);
            if (result == null)
            {
                TempData["SendMessageSuccess"] = "false";
            }

            TempData["SendMessageSuccess"] = "true";
            return RedirectToAction("index");
        }
    }
}