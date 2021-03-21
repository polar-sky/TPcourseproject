using System.Linq;
using System.Web.Mvc;
using University.Models;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;



namespace University.Controllers
{
    public class ReportController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
        public ActionResult Download_PDF()
        {

            universityContext university = new universityContext();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reportss"), "CrystalReport1.rpt"));
            rd.SetDataSource(university.Gqw.Select(c => new
            {
                GraduateId = c.Graduate.LastName,
                TeacherId = c.Teacher.LastName,
                Theme = c.Theme,
                Grade = c.Grade,
                ReviewerGrade = c.ReviewerGrade
            }).Where(c => c.Grade == 5).ToList());

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            rd.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            rd.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(5, 5, 5, 5));
            rd.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA5;

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "GQWReport.pdf");
        }
    }
}