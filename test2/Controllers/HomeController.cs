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
            //ViewBag.Message = university.AcademicDegree.Include(x => x.Teacher.Where(y => y.LastName == "ВауКруто"));
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
    }
}