using Survey.Models;
using Survey.Repository;
using Survey.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Survey.Controllers
{
    public class AnswerController : BaseController
    {
        // GET: Answer
        public ActionResult Index()
        {
            var model = db.Answer.Where(m => m.UserName == UserName).ToList();
            return View(model);
        }
        public ActionResult Create() 
        {
            if (CategoryName == null)
            {
                List<SelectListItem> categoryList = (from question in db.Question
                                                     where question.CategoryName != CategoryName
                                                     select new SelectListItem
                                                     {
                                                         Text = question.CategoryName,
                                                         Value = question.CategoryName.ToString()
                                                     }).Distinct().ToList();


                ViewBag.Question = new SelectList(categoryList.OrderBy(m => m.Text), "Value", "Text");
                var questionModel = db.Question.ToList();
                return View(questionModel);
            }

            else
            {
                return RedirectToAction("Index");
            }


        }
        [HttpPost]
        public ActionResult Create(string CategoryName)
        {
            var categoryList = db.Question.Where(m => m.CategoryName == CategoryName).ToList();
            List<SelectListItem> categoryList1 = (from question in db.Question
                                                  where question.CategoryName != CategoryName
                                                  select new SelectListItem
                                                  {
                                                      Text = question.CategoryName,
                                                      Value = question.CategoryName.ToString()
                                                  }).Distinct().ToList();


            ViewBag.Question = new SelectList(categoryList1.OrderBy(m => m.Text), "Value", "Text");
            var questionModel = categoryList;
            return View(questionModel);
        }
        [HttpGet]
        public ActionResult Getir(string categoryname)
        {
            QuestionRepository questionRepository = new QuestionRepository();
            var model = questionRepository.Detail1(categoryname);
            return View(model);
        }
        [HttpPost]
        public ActionResult Getir(string categoryname, int id)
        {
            QuestionRepository questionRepository = new QuestionRepository();
            var model = questionRepository.Detail1(categoryname);
            return Redirect(Request.UrlReferrer.ToString());
        }

        public String SendData(AnswerModel answerModel)
        {
            int? month = DateTime.Now.Month;
            var model = db.Answer.FirstOrDefault(m => m.CategoryName == answerModel.CategoryName && m.UserName == UserName && m.CreateDate.Value.Month == month);

            if (model != null)
            {
                SaveAnswerLine(answerModel.Question, answerModel.Answer, model.Id);
            }
            else
            {
                Answer answer = new Answer();
                answer.CategoryName = answerModel.CategoryName;
                answer.PersonName = answerModel.NameSurname;
                answer.UserName = UserName;
                answer.CreateDate = DateTime.Now;
                answer.CreateBy = NameSurname;
                db.Answer.Add(answer);
                db.SaveChanges();
                SaveAnswerLine(answerModel.Question, answerModel.Answer, answer.Id);
            }
            return "True";
        }

        public void SaveAnswerLine(string question, string Answer, int answerId)
        {
            var model = db.AnswerLinee.FirstOrDefault(m => m.AnswerId == answerId && m.Question == question);

            if (model != null)
            {
                model.Answer = Answer;
                db.SaveChanges();
            }
            else
            {
                AnswerLinee answerLine = new AnswerLinee();
                answerLine.AnswerId = answerId;
                answerLine.Answer = Answer;
                answerLine.Question = question;
                db.AnswerLinee.Add(answerLine);
                db.SaveChanges();
            }

        }
        public ActionResult Detail(int? Id)
        {
            var model = db.AnswerLinee.Where(m => m.AnswerId == Id).ToList();
            return View(model);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return HttpNotFound();
            }
            var model = db.Person.Find(Id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Question question, string CategoryName)
        {
            db.Entry(question).State = System.Data.Entity.EntityState.Modified;
            db.Entry(question).Property(e => e.CreateBy).IsModified = false;
            db.Entry(question).Property(e => e.CreateDate).IsModified = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}