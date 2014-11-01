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
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                var model = _uow.Posts.Find(id.Value);
                return View(model);
                
            }
            return HttpNotFound();
        }

        [AllowAnonymous]
        public ActionResult DisplayCommentsByPostId(int id)
        {
            if (id>0)
            {
                var comments = _uow.Comments.List(comment => comment.PostId == id);
                return Json(comments, JsonRequestBehavior.AllowGet);
            }
            return Json(new List<Comment>(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult AddComment(string body, int postID)
        {
            Comment newComment = new Comment() { Body = body, PostId = postID };
            _uow.Comments.Add(newComment);
            _uow.Save();
            return Json(newComment);
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
        public ActionResult Create([Bind(Exclude = "Tags")] Posts postToCreate, string Tags) //, Tag newTag)
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
            if (model != null)
                return View(model);
            return HttpNotFound();
        }

        //
        // POST: /Posts/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Posts postToUpdate, string Tags)
        {
            try
            { // TODO: Add update logic here
                var existedPost = _uow.Posts.Find(id);
                if (existedPost != null)
                {
                    existedPost.Title = postToUpdate.Title;
                    existedPost.Content = postToUpdate.Content;
                    existedPost.Tags.Clear();
                    string[] alltags = Tags.Split(new char[] { ',' });

                    foreach (var tag in alltags)
                    {
                        var existedTag = GetTagIfExisted(tag.Trim());
                        if (existedTag != null)
                        {
                            existedPost.Tags.Add(existedTag);
                        }
                        else
                        {
                            existedPost.Tags.Add(new Tag { TagName = tag.Trim() });
                        }
                    }
                    _uow.Posts.Edit(id, existedPost);
                    _uow.Save();
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Edit",postToUpdate);
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
            if (model != null)
                return View(model);
            return HttpNotFound();
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
                _uow.Save();
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
