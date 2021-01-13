using University.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace University.Controllers
{
    public class GraduateController : Controller
    {
        universityContext _con;
        public GraduateController()
        {
            _con = new universityContext();
        }

        public ActionResult Index()
        {
            return View(_con.Graduate.ToList());
        }
    }
}