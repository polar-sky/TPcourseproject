using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    public class HomeController : Controller
    {
        universityContext university = new universityContext();

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /*public ActionResult Graduate()
        {
            ViewBag.Message = "Список выпускников";
            var graduates = university.Graduate
                .Include(p => p.Company)
                .Include(x => x.Group)
                .Include(y => y.AcademicDegree);

            return View(graduates.ToList());
        }*/
    }
}