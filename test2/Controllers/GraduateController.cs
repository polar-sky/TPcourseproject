using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    public class GraduateController : Controller
    {
        universityContext university = new universityContext();

        public ActionResult Index()
        {
            ViewBag.Message = "Список выпускников";
            var graduates = university.Graduate
                .Include(p => p.Company)
                .Include(x => x.Group)
                .Include(y => y.AcademicDegree);

            return View(graduates.ToList());
        }

         public ActionResult Details(int? id)
         {
             Graduate g = university.Graduate
                 .Include(p => p.Company)
                 .Include(x => x.Group)
                 .Include(y => y.AcademicDegree)
                 .FirstOrDefault(t => t.Id == id);

             if (g == null)
             {
                 return HttpNotFound();
             }
             return View(g);
         }

        [HttpGet]
        public ActionResult Create()
        {
            SelectList company = new SelectList(university.Company, "Id", "Name");
            ViewBag.Company = company;
            SelectList group = new SelectList(university.AcademicGroup, "Id", "Name");
            ViewBag.Group = group;
            SelectList degree = new SelectList(university.AcademicDegree, "Id", "Degree");
            ViewBag.Degree = degree;
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "ID")] Graduate graduate)
        {
            university.Entry(graduate).State = EntityState.Added;
            university.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            
            Graduate graduate = university.Graduate.Find(id);
            if (graduate != null)
            {                
                SelectList company = new SelectList(university.Company, "Id", "Name", graduate.CompanyId);
                ViewBag.Company = company;
                SelectList group = new SelectList(university.AcademicGroup, "Id", "Name", graduate.GroupId);
                ViewBag.Group = group;
                SelectList degree = new SelectList(university.AcademicDegree, "Id", "Degree", graduate.AcademicDegreeId);
                ViewBag.Degree = degree;
                return View(graduate);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Graduate graduate)
        {
            university.Entry(graduate).State = EntityState.Modified;
            university.SaveChanges();
            return RedirectToAction("Index");
        }

       /* public ActionResult Delete(int id)
        {
            Graduate g = new Graduate { Id = id };
            university.Entry(g).State = EntityState.Deleted;
            university.SaveChanges();

            return RedirectToAction("Index");
        }*/

        [HttpGet]
        public ActionResult Delete(int id)
        {
            //Graduate g = university.Graduate.Find(id);
            Graduate g = university.Graduate
                .Include(p => p.Company)
                .Include(x => x.Group)
                .Include(y => y.AcademicDegree)
                .FirstOrDefault(t => t.Id == id);

            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Graduate g = university.Graduate.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            university.Graduate.Remove(g);
            university.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}