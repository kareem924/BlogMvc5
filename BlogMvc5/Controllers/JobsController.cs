using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogMvc5.Models;

namespace BlogMvc5.Controllers
{
    public class JobsController : Controller
    {
        //
        // GET: /Jobs/
        public ActionResult Index()
        {
            ViewBag.JobTitle = new SelectList(new[] { "Junior .NET", "Senior .NET", ".NET TeamLeader", "Database AdminStrator", "Junior PHP" });
            return View();
        }
        public ActionResult Apply(Candidate candidate)
        {
            string CvPath = Server.MapPath("~/Content/CV");
            candidate.Cv.SaveAs(Path.Combine(CvPath, candidate.Cv.FileName));
            return RedirectToAction("Index");
        }
	}
}