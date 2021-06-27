using localshop.Infrastructures.Attributes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Configuration = localshop.Areas.Admin.Models.Configuration;

namespace localshop.Areas.Admin.Controllers
{
    [ManageAuthorize]
    public class ConfigurationController : BaseController
    {
        public ActionResult Index()
        {
            var model = new Configuration
            {
                Mail = ConfigurationManager.AppSettings["Mail"],
                Phone = ConfigurationManager.AppSettings["Phone"],
                Facebook = ConfigurationManager.AppSettings["Facebook"],
                Twitter = ConfigurationManager.AppSettings["Twitter"],
                Github = ConfigurationManager.AppSettings["Github"],
                Instagram = ConfigurationManager.AppSettings["Instagram"],
                Address = ConfigurationManager.AppSettings["Address"],
                OpeningTime = ConfigurationManager.AppSettings["OpeningTime"]
            };

            return View(model);
        }

        public ActionResult Update(Configuration configuration)
        {
           var config = WebConfigurationManager.OpenWebConfiguration("~");

            config.AppSettings.Settings["Mail"].Value = configuration.Mail;
            config.AppSettings.Settings["Phone"].Value = configuration.Phone;
            config.AppSettings.Settings["Facebook"].Value = configuration.Facebook;
            config.AppSettings.Settings["Twitter"].Value = configuration.Twitter;
            config.AppSettings.Settings["Github"].Value = configuration.Github;
            config.AppSettings.Settings["Instagram"].Value = configuration.Instagram;
            config.AppSettings.Settings["Address"].Value = configuration.Address;
            config.AppSettings.Settings["OpeningTime"].Value = configuration.OpeningTime;

            config.Save(ConfigurationSaveMode.Modified);

            TempData["SaveSuccess"] = "true";
            return RedirectToAction("index");
        }
    }
}