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
            try
            {
                ViewBag.JobTitle = new SelectList(new[] { "Junior .NET Developer", "Senior .NET Developer", ".NET Team Leader" });

                if (ModelState.IsValid)
                {
                    if (candidate.Cv != null)
                    {
                        string cvPath = Server.MapPath("~/Content/CV");
                        candidate.Cv.SaveAs(Path.Combine(cvPath, candidate.Cv.FileName));

                        //candidate.JobTitle 
                        //candidate.Mail 

                        // Database  / Mail 

                        ViewBag.Message = "Your Application has been sent Successfully!";
                        ViewBag.Status = "success";
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Please select your CV!";
                        ViewBag.Status = "danger";
                        return View("index", candidate);
                    }
                }
                else
                {
                    ViewBag.Message = "Please, correct your info , and try again !";
                    ViewBag.Status = "danger";
                    return View("Index", candidate);
                }
            }
            catch (Exception)
            {
                ViewBag.Message = "We cann't send your message , please try again";
                ViewBag.Status = "danger";
                return View("index", candidate);
            }
        }
	}
}