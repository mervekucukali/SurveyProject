using Survey.Models;
using Survey.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Survey.Controllers
{
    public class LoginController : Controller
    {
        SurveyEntities5 db = new SurveyEntities5();
        SecurityModel security = new SecurityModel();
        Person person = new Person();
        PersonRepositorycs personRepositorycs = new PersonRepositorycs();

        // GET: Login
        public ActionResult SignIn(string UserName, string Password)
        {
            if (UserName == null)
            {
                return View();
            }
            else
            {
                var person = db.Person.FirstOrDefault(m => m.UserName == UserName && m.Password == Password);
                if (person != null)
                {
                    Session["UserName"] = person.UserName;
                    Session["NameSurname"] = person.NameSurname;
                    if (person.IsAdmin == true)
                    {
                        Session["Admin"] = "Admin";
                    }
                    return RedirectToAction("Create", "Answer");
                }
                else
                {
                    ViewBag.Error = "Kullanıcı Adı veya Şifre Hatalı";
                    return View();

                }
            }
        }
        public ActionResult Singup()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Singup(string UserName, string NameSurname, string Password)
        {
            int result = security.Singup(UserName, Password, NameSurname);

            ViewBag.Message = result == 2 ?
                            "UserName Sistemde Kayıtlı. Lütfen Farklı Bir UserName Seçiniz" :
                            result == 3 ?
                            "NameSurname sistemde Kayıtlı. Lütfen Farklı Bir NameSurname  Deneyiniz" :
                            result == 1 ?
                            "Kullanıcı Kaydı Başarılı" : "Beklenmedik Bir Hata Oluştu";

            return RedirectToAction("SignIn", "Login");

            //return View();

        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("SignIn", "Login");
        }
    }
}