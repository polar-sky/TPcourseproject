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

        public ViewResult Index(string currentFilter, string searchString, int? page, string gqwGroup, SortState sortOrder = SortState.NameAsc)
        {
            ViewBag.Message = "Список выпускных квалификационных работ";

            IQueryable<Gqw> gqws = university.Gqw
                .Include(x => x.Graduate)
                .Include(y => y.Reviewer)
                .Include(z => z.Teacher)
                .Include(w => w.Sec);

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["ArchSort"] = sortOrder == SortState.ArchAsc ? SortState.ArchDesc : SortState.ArchAsc;


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                gqws = gqws.Where(s => s.Graduate.LastName.Contains(searchString)
                                                || s.Graduate.FirstName.Contains(searchString)
                                                || s.Graduate.Patronymic.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(gqwGroup))
            {
                gqws = gqws.Where(x => x.Graduate.Group.Name == gqwGroup);
            }

            gqws = sortOrder switch
            {
                SortState.NameDesc => gqws.OrderByDescending(s => s.Graduate.LastName),
                SortState.ArchAsc => gqws.OrderBy(s => s.IsArchived),
                SortState.ArchDesc => gqws.OrderByDescending(s => s.IsArchived),
                _ => gqws.OrderBy(s => s.Graduate.LastName),
            };

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
            SelectList graduateLastName = new SelectList(university.Graduate, "Id", "LastName");
            ViewBag.GraduateLastN = graduateLastName;
            SelectList graduateName = new SelectList(university.Graduate, "Id", "FirstName");
            ViewBag.GraduateFirstN = graduateName;
            SelectList reviewer = new SelectList(university.Partner, "Id", "LastName");
            ViewBag.Reviewer = reviewer;
            SelectList teacher = new SelectList(university.Teacher, "Id", "LastName");
            ViewBag.Teacher = teacher;
            SelectList sec = new SelectList(university.Sec, "Id", "Year");
            ViewBag.Sec = sec;
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
                SelectList graduate = new SelectList(university.Graduate, "Id", "LastName", gqw.GraduateId);
                ViewBag.Graduate = graduate;
                SelectList reviewer = new SelectList(university.Partner, "Id", "LastName", gqw.ReviewerId);
                ViewBag.Reviewer = reviewer;
                SelectList teacher = new SelectList(university.Teacher, "Id", "LastName", gqw.TeacherId);
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