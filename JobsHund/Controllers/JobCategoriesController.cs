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
    public class JobCategoriesController : Controller
    {
        private JobshuntdbEntities1 db = new JobshuntdbEntities1();

        // GET: JobCategories
        public ActionResult Index()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))

            {
                return RedirectToAction("Login", "User");
            }
            return View(db.JobCategories.ToList());
        }

        

        // GET: JobCategories/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))

            {
                return RedirectToAction("Login", "User");
            }
            return View(new JobCategory());
        }

        // POST: JobCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( JobCategory jobCategory)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))

            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                db.JobCategories.Add(jobCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobCategory);
        }

        // GET: JobCategories/Edit/5
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
            JobCategory jobCategory = db.JobCategories.Find(id);
            if (jobCategory == null)
            {
                return HttpNotFound();
            }
            return View(jobCategory);
        }

        // POST: JobCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobCategory jobCategory)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))

            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                db.Entry(jobCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobCategory);
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
