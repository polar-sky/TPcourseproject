using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    public class GraduateController : Controller
    {
        universityContext university = new universityContext();

        public async Task<ActionResult> Index(SortState sortOrder = SortState.NameAsc)
        {
            ViewBag.Message = "Список выпускников";
            /***************************************
             * no sort, no filtr                   *
             ***************************************
             
            var graduates = university.Graduate
                .Include(p => p.Company)
                .Include(x => x.Group)
                .Include(y => y.AcademicDegree);

            return View(graduates.ToList());*/


            /***************************************
             * with filtr, but dont work           * string sortOrder
             * but i save this                     *
             *************************************** 
             
            IQueryable<Graduate> graduates = university.Graduate
                .Include(p => p.Group)
                .Include(x => x.Company)
                .Include(y => y.AcademicDegree);

            if (group != null && group != 0)
            {
                graduates = graduates.Where(p => p.GroupId == group);
            }

            List<AcademicGroup> groups = university.AcademicGroup.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            groups.Insert(0, new AcademicGroup { Name = "Все", Id = 0 });

            GraduateListViewModel glvm = new GraduateListViewModel
            {
                Graduates = graduates.ToList(),
                Groups = new SelectList(groups, "Id", "Name"),
            };
            return View(glvm);*/

            /********************************************
             * its dont work without select requests :( *
             * ******************************************
            
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.GroupSortParm = String.IsNullOrEmpty(sortOrder) ? "group_desc" : "group";
            var graduates = university.Graduate
                .Include(p => p.Company)
                .Include(x => x.Group)
                .Include(y => y.AcademicDegree);
            switch (sortOrder)
            {
                case "name_desc":
                    graduates = graduates.OrderByDescending(s => s.LastName);
                    break;
                case "group":
                    graduates = graduates.OrderBy(s => s.Group);
                    break;
                case "group_desc":
                    graduates = graduates.OrderByDescending(s => s.Group);
                    break;
                default:
                    graduates = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Graduate, AcademicDegree>)graduates.OrderBy(s => s.LastName);
                    break;
            }
            return View(graduates.ToList());*/

            IQueryable<Graduate> users = university.Graduate
                .Include(x => x.Group)
                .Include(y => y.Company)
                .Include(z => z.AcademicDegree);

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["GroupSort"] = sortOrder == SortState.GroupAsc ? SortState.GroupDesc : SortState.GroupAsc;
            ViewData["LectSort"] = sortOrder == SortState.LectAsc ? SortState.LectDesc : SortState.LectAsc;
            ViewData["LabSort"] = sortOrder == SortState.LabAsc ? SortState.LabDesc : SortState.LabAsc;
            ViewData["CitySort"] = sortOrder == SortState.CityAsc ? SortState.CityDesc : SortState.CityAsc;
            ViewData["CompanySort"] = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;
            ViewData["DegreeSort"] = sortOrder == SortState.DegreeAsc ? SortState.DegreeDesc : SortState.DegreeAsc;

            users = sortOrder switch
            {
                SortState.NameDesc => users.OrderByDescending(s => s.LastName),
                SortState.GroupAsc => users.OrderBy(s => s.Group.Name),
                SortState.GroupDesc => users.OrderByDescending(s => s.Group.Name),
                SortState.LectDesc => users.OrderByDescending(s => s.DisciplineLecture),
                SortState.LectAsc => users.OrderBy(s => s.DisciplineLecture),
                SortState.LabDesc => users.OrderByDescending(s => s.DisciplineLaboratoryWorks),
                SortState.LabAsc => users.OrderBy(s => s.DisciplineLaboratoryWorks),
                SortState.CityDesc => users.OrderByDescending(s => s.CurrentCity),
                SortState.CityAsc => users.OrderBy(s => s.CurrentCity),
                SortState.CompanyDesc => users.OrderByDescending(s => s.Company),
                SortState.CompanyAsc => users.OrderBy(s => s.Company),
                SortState.DegreeDesc => users.OrderByDescending(s => s.AcademicDegree),
                SortState.DegreeAsc => users.OrderBy(s => s.AcademicDegree),
                _ => users.OrderBy(s => s.LastName),
            };
            return View(await users.AsNoTracking().ToListAsync());


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