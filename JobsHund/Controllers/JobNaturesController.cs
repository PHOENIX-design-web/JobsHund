using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using jobhundClassLibrary1;

namespace JobsHund.Controllers
{
    public class JobNaturesController : Controller
    {
        private JobshuntdbEntities1 db = new JobshuntdbEntities1();

        // GET: JobNatures
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))

            {
                return RedirectToAction("Login", "User");
            }
            return View(db.JobNatures.ToList());
        }

       

        // GET: JobNatures/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))

            {
                return RedirectToAction("Login", "User");
            }
            return View(new JobNature());
        }

        // POST: JobNatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobNatureId,JobNature1")] JobNature jobNature)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))

            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                db.JobNatures.Add(jobNature);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobNature);
        }

        // GET: JobNatures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))

            {
                return RedirectToAction("Login", "User");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobNature jobNature = db.JobNatures.Find(id);
            if (jobNature == null)
            {
                return HttpNotFound();
            }
            return View(jobNature);
        }

        // POST: JobNatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobNatureId,JobNature1")] JobNature jobNature)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))

            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                db.Entry(jobNature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobNature);
        }

       
     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
