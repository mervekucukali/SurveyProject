using Survey.Models;
using Survey.Repository;
using Survey.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Survey.Controllers
{
    public class QuestionController : BaseController
    {
        QuestionRepository questionRepository=new QuestionRepository(); 
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var model = questionRepository.List();
                return View(model);
            }
        }

        public ActionResult Create(string CategoryName, string QuestionLine, string qtype)
        {
            Question question1 = new Question();
            if (QuestionLine != null && CategoryName != null)
            {
                question1.CategoryName = CategoryName;
                question1.CreateDate = DateTime.Now;
                question1.CreateBy = NameSurname;
                question1.QuestionLine = QuestionLine;
                question1.QuestionType = qtype;
               questionRepository.Add(question1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else { return View(); }
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return HttpNotFound();
            }
            var model = db.Question.Find(Id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Question question)
        {
            db.Entry(question).State = System.Data.Entity.EntityState.Modified;
            db.Entry(question).Property(e => e.CreateBy).IsModified = false;
            db.Entry(question).Property(e => e.CreateDate).IsModified = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return HttpNotFound();
            }
            var question = db.Question.Find(Id);
            db.Question.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
