using jobhundClassLibrary1;
using JobsHund.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobsHund.Controllers
{
    public class JobController : Controller
    {
        private JobshuntdbEntities1 db = new JobshuntdbEntities1();

        // GET: Job
        public ActionResult PostJob()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))
            {
                return RedirectToAction("Login", "User");
            }

            var job = new PostJobMv();
            ViewBag.JobCategoryId = new SelectList(
                db.JobCategories.ToList(),
                "JobCategoryId",
                "JobCategory1", "0" );

            ViewBag.JobNatureId = new SelectList(
                db.JobNatures.ToList(),
                "JobNatureId",
                "JobNature1",
                "0");
            return View(job);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostJob(PostJobMv postJobMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))
            {
                return RedirectToAction("Login", "User");
            }
            int userid = 0;
            int companyid = 0;
            int.TryParse(Convert.ToString(Session["UserId"]), out userid);
            int.TryParse(Convert.ToString(Session["CompanyId"]), out companyid);
            postJobMV.UserId = userid;
            postJobMV.CompanyId = companyid;



            if (ModelState.IsValid)
            {
                var post = new PostJob();
                post.UserId = postJobMV.UserId;
                post.CompanyId = postJobMV.CompanyId;
                post.JobCategoryId = postJobMV.JobCategoryId;
                post.JobTitle = postJobMV.JobTitle;
                post.JobDescription = postJobMV.JobDescription;
                post.MinSalary = postJobMV.MinSalary;
                post.Maxsalary = postJobMV.MaxSalary;
                post.Vecancey = postJobMV.Vecancey;
                post.Location = postJobMV.Location;
                post.PostDate = DateTime.Now;
                post.ApplicationLastdate = postJobMV.ApplicationLastdate;
                post.LastDate = postJobMV.ApplicationLastdate;
                post.JobStatusId = 1;
                post.JobNatureId = postJobMV.JobNatureId;

                post.WebUrl = postJobMV.WebUrl;
                db.PostJobs.Add(post);
                db.SaveChanges();
                return RedirectToAction("CompanyJobList");
    }

            ViewBag.JobCategoryId = new SelectList(
     db.JobCategories.ToList(),
     "JobCategoryId",   // value field
     "JobCategory1",    // text field
    "0");


            ViewBag.JobNatureId = new SelectList(
                db.JobNatures.ToList(),
                "JobNatureId",
                "JobNature1",
                "0");


            return View(postJobMV);
        }
        public ActionResult CompanyJobList()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))
            {
                return RedirectToAction("Login", "User");
            }

            int userid = 0;
            int companyid = 0;
            int.TryParse(Convert.ToString(Session["UserId"]), out userid);
            int.TryParse(Convert.ToString(Session["CompanyId"]), out companyid);

            var allpost = db.PostJobs.Where(c=>c.CompanyId == companyid &&c.UserId==userid).ToList();
            
            return View(allpost);
        }
        public ActionResult AllCompanyPendingJob()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))
            {
                return RedirectToAction("Login", "User");
            }

            int userid = 0;
            int companyid = 0;
            int.TryParse(Convert.ToString(Session["UserId"]), out userid);
            int.TryParse(Convert.ToString(Session["CompanyId"]), out companyid);

            var allpost = db.PostJobs.ToList();
            if(allpost.Count()>0)
            {
                allpost = allpost.OrderByDescending(o => o.PostJobId).ToList();
            }
            return View(allpost);
        }
        public ActionResult AddRequirments(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))
            {
                return RedirectToAction("Login", "User");
            }
            var detail = db.JobRequirementDetails.Where(j => j.PostJobId == id).ToList();
            if (detail.Count() > 0)
            {
                detail = detail.OrderBy(r => r.JobRequirementId).ToList();
            }
            var requirment = new JobRequirments();
            requirment.Details = detail;
            requirment.PostJobId = (int)id;
            ViewBag.JobRequirementId = new SelectList(db.JobRequirements.ToList(), "JobRequirementId", "JobRequirementTitle", "0");
            return View(requirment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRequirments(JobRequirments JobRequirments)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))
            {
                return RedirectToAction("Login", "User");
            }
            var requirments = new JobRequirementDetail();
            requirments.JobRequirementId = JobRequirments.JobRequirementId;
            requirments.JobRequirementDetails = JobRequirments.JobRequirementDetails;
            requirments.PostJobId = JobRequirments.PostJobId;
            db.JobRequirementDetails.Add(requirments);
            db.SaveChanges();
            JobRequirments.JobRequirementDetails = string.Empty;
            var detail = db.JobRequirementDetails.Where(j => j.PostJobId == requirments.PostJobId).ToList();
            if (detail.Count() > 0)
            {
                detail = detail.OrderBy(r => r.JobRequirementId).ToList();
            }
            JobRequirments.Details = detail;
            ViewBag.JobRequirementId = new SelectList(db.JobRequirements.ToList(), "JobRequirementId", "JobRequirementTitle", JobRequirments.JobRequirementId);

            return View(JobRequirments);
        }
        public ActionResult DeleteRequirments(int? id)
        {
            var jobpostid = db.JobRequirementDetails.Find(id).PostJobId;
            var requirments = db.JobRequirementDetails.Find(id);
            db.Entry(requirments).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("AddRequirments", new { id = jobpostid });
        }
        public ActionResult DeleteJobPost(int? id)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))
            {
                return RedirectToAction("Login", "User");
            }
            var jobpost = db.PostJobs.Find(id);
            db.Entry(jobpost).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("CompanyJobList");

        }
        public ActionResult ApprovedPost(int? id)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))
            {
                return RedirectToAction("Login", "User");
            }
            var jobpost = db.PostJobs.Find(id);
            jobpost.JobStatusId = 2;
            db.Entry(jobpost).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AllCompanyPendingJob");

        }
        public ActionResult CancelPost(int? id)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))
            {
                return RedirectToAction("Login", "User");
            }
            var jobpost = db.PostJobs.Find(id);
            jobpost.JobStatusId = 3;
            db.Entry(jobpost).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AllCompanyPendingJob");

        }
        public ActionResult JobDetail(int? id)
        {
            var getpostjob = db.PostJobs.Find(id);
            var postjob = new PostJobDetailMv();
            postjob.PostJobId = getpostjob.PostJobId;
            postjob.Company = getpostjob.Company.CompanyName;
            postjob.JobCategory = getpostjob.JobCategory.JobCategory1;
            postjob.JobTitle = getpostjob.JobTitle;
            postjob.JobDescription = getpostjob.JobDescription;
            postjob.MinSalary = getpostjob.MinSalary;
            postjob.Maxsalary = getpostjob.Maxsalary;
            postjob.Vecancey = getpostjob.Vecancey;
            postjob.Location = getpostjob.Location;
            postjob.PostDate = getpostjob.PostDate;
            postjob.ApplicationLastdate = getpostjob.ApplicationLastdate;
            postjob.JobNature = getpostjob.JobNature.JobNature1;
            postjob.WebUrl = getpostjob.WebUrl;
            getpostjob.JobRequirementDetails= getpostjob.JobRequirementDetails.OrderBy(d => d.JobRequirementId).ToList();
            int jobreqrmentid = 0;
            var jobreqrments = new JobRequirementMv();
            foreach (var detail in getpostjob.JobRequirementDetails)
            {
                var jobreqrmentdetails = new JobRequirementDetailMv();

                if (jobreqrmentid==0)
                {
                    jobreqrments.JobRequirementId = detail.JobRequirementId;
                    jobreqrments.JobRequirementTitle = detail.JobRequirement.JobRequirementTitle;
                    jobreqrmentdetails.JobRequirementId = detail.JobRequirementId;
                    jobreqrmentdetails.JobRequirementDetails = detail.JobRequirementDetails;
                    jobreqrments.Details.Add(jobreqrmentdetails);
                    jobreqrmentid =detail. JobRequirementId;
                }
                else if(jobreqrmentid==detail.JobRequirementId)
                {
                    jobreqrmentdetails.JobRequirementId = detail.JobRequirementId;
                    jobreqrmentdetails.JobRequirementDetails = detail.JobRequirementDetails;
                    jobreqrments.Details.Add(jobreqrmentdetails);
                    jobreqrmentid = detail.JobRequirementId;
                }
                else if (jobreqrmentid != detail.JobRequirementId)
                {
                    postjob.Requirments.Add(jobreqrments);
                    jobreqrments= new JobRequirementMv();
                    jobreqrments.JobRequirementId = detail.JobRequirementId;
                    jobreqrments.JobRequirementTitle = detail.JobRequirement.JobRequirementTitle;
                    jobreqrmentdetails.JobRequirementId = detail.JobRequirementId;
                    jobreqrmentdetails.JobRequirementDetails = detail.JobRequirementDetails;
                    jobreqrments.Details.Add(jobreqrmentdetails);
                    jobreqrmentid = detail.JobRequirementId;
                
            }

            }
            postjob.Requirments.Add(jobreqrments);
            return View(postjob); 
        }
        [HttpGet]
        public ActionResult FillterJob()
        {
            var obj = new FillterJobMv();

            var date = DateTime.Now.Date;
            var result = db.PostJobs
                .Where(r => r.ApplicationLastdate >= date && r.JobStatusId== 2).ToList();
            obj.Result = result;

            ViewBag.JobCategoryId = new SelectList(
                db.JobCategories,
                "JobCategoryId",
                "JobCategory1",
                "0" );

            ViewBag.JobNatureId = new SelectList(
                db.JobNatures,
                "JobNatureId",
                "JobNature1",
                "0");

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillterJob(FillterJobMv fillterJobMv)
        {
            var date = DateTime.Now.Date;
            var result = db.PostJobs
                .Where(r => r.ApplicationLastdate >= date && r.JobStatusId == 2 && (r.JobCategoryId == fillterJobMv.JobCategoryId || r.JobNatureId == fillterJobMv.JobNatureId)).ToList();
            fillterJobMv.Result = result;
            

            ViewBag.JobCategoryId = new SelectList(
                db.JobCategories,
                "JobCategoryId",
                "JobCategory1",
                fillterJobMv.JobCategoryId
            );

            ViewBag.JobNatureId = new SelectList(
                db.JobNatures,
                "JobNatureId",
                "JobNature1",
                fillterJobMv.JobNatureId
            );

            return View(fillterJobMv);
        }


    }
}