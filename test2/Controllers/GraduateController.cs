using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Web.Mvc;
using University.Models;
using PagedList;

namespace University.Controllers
{
    [Authorize(Roles ="methodist, secretary")]
    public class GraduateController : Controller
    {
        universityContext university = new universityContext();
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(GraduateController));
        public ViewResult Index(string currentFilter, string searchString, int? page, string graduateGroup, SortState sortOrder = SortState.NameAsc)
        {
            ViewBag.Message = "Список выпускников";

            IQueryable<Graduate> graduates = university.Graduate
                .Include(x => x.Group)
                .Include(y => y.Company)
                .Include(z => z.AcademicDegree);

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["GroupSort"] = sortOrder == SortState.GroupAsc ? SortState.GroupDesc : SortState.GroupAsc;
            ViewData["LectSort"] = sortOrder == SortState.LectAsc ? SortState.LectDesc : SortState.LectDesc;
            ViewData["LabSort"] = sortOrder == SortState.LabAsc ? SortState.LabDesc : SortState.LabDesc;
            ViewData["CitySort"] = sortOrder == SortState.CityAsc ? SortState.CityDesc : SortState.CityAsc;
            ViewData["CompanySort"] = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;
            ViewData["DegreeSort"] = sortOrder == SortState.DegreeAsc ? SortState.DegreeDesc : SortState.DegreeAsc;

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
                graduates = graduates.Where(s => s.LastName.Contains(searchString) 
                                                || s.FirstName.Contains(searchString) 
                                                || s.Patronymic.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(graduateGroup))
            {
                graduates = graduates.Where(x => x.Group.Name == graduateGroup);
            }

            graduates = sortOrder switch
            {
                SortState.NameDesc => graduates.OrderByDescending(s => s.LastName),
                SortState.GroupAsc => graduates.OrderBy(s => s.Group.Name),
                SortState.GroupDesc => graduates.OrderByDescending(s => s.Group.Name),
                SortState.LectDesc => graduates.OrderByDescending(s => s.DisciplineLecture),
                SortState.LectAsc => graduates.OrderBy(s => s.DisciplineLecture),
                SortState.LabDesc => graduates.OrderByDescending(s => s.DisciplineLaboratoryWorks),
                SortState.LabAsc => graduates.OrderBy(s => s.DisciplineLaboratoryWorks),
                SortState.CityDesc => graduates.OrderByDescending(s => s.CurrentCity),
                SortState.CityAsc => graduates.OrderBy(s => s.CurrentCity),
                SortState.CompanyDesc => graduates.OrderByDescending(s => s.Company),
                SortState.CompanyAsc => graduates.OrderBy(s => s.Company),
                SortState.DegreeDesc => graduates.OrderByDescending(s => s.AcademicDegree),
                SortState.DegreeAsc => graduates.OrderBy(s => s.AcademicDegree),
                _ => graduates.OrderBy(s => s.LastName),
            };

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(graduates.ToPagedList(pageNumber, pageSize));
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
                logger.Error("Invalid Id");

                return HttpNotFound();
             }
             return View(g);
         }

        [Authorize(Roles ="methodist")]
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

        [Authorize(Roles = "methodist")]
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "ID")] Graduate graduate)
        {
            university.Entry(graduate).State = EntityState.Added;
            university.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "methodist")]
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

        [Authorize(Roles = "methodist")]
        [HttpPost]
        public ActionResult Edit(Graduate graduate)
        {
            university.Entry(graduate).State = EntityState.Modified;
            university.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "methodist")]
        [HttpGet]
        public ActionResult Delete(int id)
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

        [Authorize(Roles = "methodist")]
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

            logger.Warn($"Deletion happened");

            return RedirectToAction("Index");
        }

    }
}