﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMvc5.Controllers
{
    public class UploaderController : Controller
    {
        //
        // GET: /Uploader/
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            var path = Path.Combine(Server.MapPath("~/Uploads/"), upload.FileName);
            upload.SaveAs(path);

            return Content(string.Format("http://{0}/Uploads/{1}", Request.Url.Authority, upload.FileName));
        }
	}
}