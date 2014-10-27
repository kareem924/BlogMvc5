using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogMvc5.Models;
using BlogMvc5.Models.UnitOfWork;

namespace BlogMvc5.Controllers
{
    [Authorize(Roles = "Authors")]
    public class PostController : Controller
    {
       IUnitOfWork _uow;
        public PostController()
        {
            _uow = new UnitOfWork();
        }
        public PostController(IUnitOfWork uof) // Fake 
        {
            _uow = uof;
        }
        //
        // GET: /Posts/
        public ActionResult Index()
        {
            var model = _uow.Posts.List();
            return View(model);
        }

        [AllowAnonymous]
        // GET: /Posts/Details/5
        public ActionResult Details(int id)
        {
            var model = _uow.Posts.Find(id);
            return View(model);
        }
        //
        // GET: /Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Posts/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Posts postToCreate, string Tags) //, Tag newTag)
        {
        
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    string[] AllTags = Tags.Split(new char[] {','});
                    foreach (var tag in AllTags)
                    {
                        var existedTag = GetTagIfExisted(tag);
                        if (existedTag != null)
                        {
                            postToCreate.Tags.Add(existedTag);
                        }
                        else
                        {
                            postToCreate.Tags.Add(new Tag {TagName = tag});
                        }

                    }
                    _uow.Posts.Add(postToCreate);

                    //_uow.Tags.Add(newTag);

                    _uow.Save();
                    //
                    return RedirectToAction("Index");
                }
                return View(postToCreate);
            }
            catch
            {
                return View("error");
            }
        }

        //
        // GET: /Posts/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _uow.Posts.Find(id);
            return View(model);
        }

        //
        // POST: /Posts/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Posts postToUpdate)
        {
            try
            {
                // TODO: Add update logic here
                _uow.Posts.Edit(id, postToUpdate);
                _uow.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Posts/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _uow.Posts.Find(id);
            return View(model);
        }

        //
        // POST: /Posts/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Posts postToDelete)
        {
            try
            {
                // TODO: Add delete logic here
                _uow.Posts.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
          private Tag GetTagIfExisted(string tag)
          {
              var selectedTag = _uow.Tags.List(p => p.TagName == tag).FirstOrDefault();
              return selectedTag;

          }

        public ActionResult Tags(int id)
        {
            var PostedByTag = _uow.Posts.List(post => post.Tags.Where(tag => tag.TagId == id).Count() > 0);
            return View("Index", PostedByTag);
        }

        public JsonResult AutoTags()
        {
            var results = _uow.Tags.List().Select(t=>t.TagName);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}
