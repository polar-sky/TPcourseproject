using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using University.Models;
using PagedList;

namespace University.Controllers
{
    public class GQWController : Controller
    {
        universityContext university = new universityContext();

        public ViewResult Index(int? page)
        {
            ViewBag.Message = "Список выпускных квалификационных работ";

            IQueryable<Gqw> gqws = university.Gqw
                .Include(x => x.Graduate)
                .Include(y => y.Reviewer)
                .Include(z => z.Teacher)
                .Include(w => w.Sec);

            page = 1;

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(gqws.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            Gqw gqw = university.Gqw
               .Include(x => x.Graduate)
               .Include(y => y.Reviewer)
               .Include(z => z.Teacher)
               .Include(w => w.Sec)
               .FirstOrDefault(t => t.Id == id);

            if (gqw == null)
            {
                return HttpNotFound();
            }
            return View(gqw);
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
        public ActionResult Create([Bind(Exclude = "ID")] Gqw gqw)
        {
            university.Entry(gqw).State = EntityState.Added;
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

            Gqw gqw = university.Gqw.Find(id);
            if (gqw != null)
            {
                SelectList graduate = new SelectList(university.Graduate, "Id", "Last Name", gqw.GraduateId);
                ViewBag.Graduate = graduate;
                SelectList reviewer = new SelectList(university.Partner, "Id", "Last Name", gqw.ReviewerId);
                ViewBag.Reviewer = reviewer;
                SelectList teacher = new SelectList(university.Teacher, "Id", "Last Name", gqw.TeacherId);
                ViewBag.Teacher = teacher;
                SelectList sec = new SelectList(university.Sec, "Id", "Year", gqw.SecId);
                ViewBag.Sec = sec;
                return View(gqw);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Gqw gqw)
        {
            university.Entry(gqw).State = EntityState.Modified;
            university.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Gqw gqw = university.Gqw
                .Include(x => x.Graduate)
                .Include(y => y.Reviewer)
                .Include(z => z.Teacher)
                .Include(w => w.Sec)
                .FirstOrDefault(t => t.Id == id);

            if (gqw == null)
            {
                return HttpNotFound();
            }
            return View(gqw);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Gqw gqw = university.Gqw.Find(id);
            if (gqw == null)
            {
                return HttpNotFound();
            }
            university.Gqw.Remove(gqw);
            university.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}