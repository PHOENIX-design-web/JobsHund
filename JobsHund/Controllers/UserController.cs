using jobhundClassLibrary1;
using JobsHund.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobsHund.Controllers
{
    public class UserController : Controller
    {
        private JobshuntdbEntities1 Db = new JobshuntdbEntities1();
        // GET: User
        public ActionResult NewUser()
        {
            return View(new UserMv() );
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewUser(UserMv userMV)
        {
            if(ModelState.IsValid)
            {
                var checkuser = Db.Users.Where(u => u.Email == userMV.Email ).FirstOrDefault();
                if(checkuser !=null)
                {
                    ModelState.AddModelError("Email", "Email is Already Registerd!");
                    return View(userMV);
                }
                checkuser = Db.Users.Where(u => u.UserName == userMV.UserName).FirstOrDefault();
                if (checkuser != null)
                {
                    ModelState.AddModelError("UserName", "UserName is Already Registerd!");
                    return View(userMV);
                }
                using (var trans = Db.Database.BeginTransaction())
                {
                    try
                    {
                        var user = new User();
                        user.UserName = userMV.UserName;
                        user.Password = userMV.Password;
                        user.Phone = userMV.Phone;
                        user.Email = userMV.Email;
                        user.UserTypeId = userMV.AreYouProvider == true ? 2 : 3;
                        Db.Users.Add(user);
                        Db.SaveChanges();
                        if (userMV.AreYouProvider == true)
                        {


                            var company = new Company();
                            company.UserId = user.UserId;
                            if (String.IsNullOrEmpty(userMV.Company.Email))
                            {
                                trans.Rollback();
                                ModelState.AddModelError("Company.Email", "Required!");
                                return View(userMV);
                            }
                            if (String.IsNullOrEmpty(userMV.Company.CompanyName))
                            {
                                trans.Rollback();
                                ModelState.AddModelError("Company.CompanyName", "Required!");
                                return View(userMV);
                            }
                           
                            if (String.IsNullOrEmpty(userMV.Company.Description))
                            {
                                trans.Rollback();
                                ModelState.AddModelError("Company.Description", "Required!");
                                return View(userMV);
                            }
                            company.Email = userMV.Company.Email;
                            company.CompanyName = userMV.Company.CompanyName;
                            company.ContactNo = userMV.Phone;
                            company.Phone = userMV.Company.Phone;
                            company.Logo = "~/Content/assets/img/logo/logo2_footer.png";
                            company.Description = userMV.Company.Description;
                            Db.Companies.Add(company);
                            Db.SaveChanges();

                        }
                        trans.Commit();
                        return RedirectToAction("Login");
                    }
                    catch ( Exception  )
                    {
                        ModelState.AddModelError(string.Empty, "Please Provide Correct Details");
                        trans.Rollback();
                    }
                  
                }
            }
            return View(userMV);
        }
        public ActionResult Login()
        {
            return View(new UserLoginMv());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginMv userLoginMv)
        {
            if(ModelState.IsValid)
            {
                var user=Db.Users.Where(u => u.UserName == userLoginMv.UserName && u.Password == userLoginMv.Password).FirstOrDefault();
                if(user==null)
                {
                    ModelState.AddModelError(string.Empty, "UserName & Password is inCorrect");
                    return View(userLoginMv);

                }
                Session["UserId"] = user.UserId;
                Session["UserName"] = user.UserName;
                Session["UserTypeId"] = user.UserTypeId;

                if (user.UserTypeId ==2)
                {
                    Session["CompanyId"] = user.Companies.FirstOrDefault().CompanyId;
                }
              return     RedirectToAction("Index", "Home");
            }
            return View(userLoginMv);
        }
        public ActionResult LogOut()
        {
            Session["UserId"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["CompanyId"] = string.Empty; 
            Session["UserTypeId"] = string.Empty;

            return RedirectToAction("Index", "Home");
        }
        public ActionResult AllUsers()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeId"])))

            {
                return RedirectToAction("Login", "User");
            }
            var users = Db.Users.ToList();
            return View(users);
        }
        public ActionResult Forgot()
        {
            return View( new ForgotPasswordMv());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forgot(ForgotPasswordMv forgotPasswordMv)
        {
            var user = Db.Users.Where(u => u.Email == forgotPasswordMv.Email).FirstOrDefault();
            if(user != null)
            {
                string usernameandapassword = "User Name" + user.UserName + "\n" + "Password" + user.Password ;


                string body = usernameandapassword;

               
                bool IsSendEmail =JobsHund.Forgot.Email.MailSend(user.Email,"Account Details",body,true);
                if (!IsSendEmail)
                {

                    ModelState.AddModelError(string.Empty, "UserName And Password Is Sent!");
                }
                else
                {
                    ModelState.AddModelError("Email", "Your Email IS Regiostered!Currently Email Sending Is Not Working Properly,Plaese Try Again Later ");

                }
            }
            else
            {
                ModelState.AddModelError("Email", "Email Is Not Registered!");
            }
            return View(forgotPasswordMv);

        }
    }
}