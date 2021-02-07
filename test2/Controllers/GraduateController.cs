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
    public class GraduateController : Controller
    {
        universityContext university = new universityContext();

        public ViewResult Index(string currentFilter, string searchString, int? page, string graduateGroup, SortState sortOrder = SortState.NameAsc)
        {
            ViewBag.Message = "Список выпускников";
            /***************************************
             * no sort, no filtr                   *
             * ソートなし、フィルターなし             *
             ***************************************
             
            var graduates = university.Graduate
                .Include(p => p.Company)
                .Include(x => x.Group)
                .Include(y => y.AcademicDegree);

            return View(graduates.ToList());*/


            /***************************************
             * with filtr, but dont work           * 
             * but i save this                     *
             * フィルター付きですが、機能しません      *
             * しかし、私はこれを保存します           *
             *************************************** 
             

            string sortOrder
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

            /*********************************************
             * its dont work without select requests :(  *
             * 選択したリクエストなしでは機能しません (╥﹏╥) *
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