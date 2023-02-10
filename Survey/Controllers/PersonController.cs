﻿using Survey.Models;
using Survey.Repository;
using Survey.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Survey.Controllers
{
    public class PersonController : BaseController
    {
        PersonRepositorycs PersonRepositorycs = new PersonRepositorycs();   
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            var model = PersonRepositorycs.List();
            return View(model);
          
        }

        public ActionResult Create(Person person,string Answer)
        {
            if (person.NameSurname != null)
            {
                person.CreateDate = DateTime.Now;
                person.CreateBy = NameSurname;
                if (Answer == Constants.AnswerType.Yes)
                {
                    person.IsAdmin = true;
                }
                else
                {
                    person.IsAdmin = false;
                }
                PersonRepositorycs.Add(person);
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
            var model = db.Person.Find(Id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Person person, string Answer)
        {
            db.Entry(person).State = System.Data.Entity.EntityState.Modified;
            db.Entry(person).Property(e => e.CreateBy).IsModified = false;
            db.Entry(person).Property(e => e.CreateDate).IsModified = false;
            if (Answer == Constants.AnswerType.Yes)
            {
                person.IsAdmin = true;
            }
            else
            {
                person.IsAdmin = false;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return HttpNotFound();
            }
            var person = db.Person.Find(Id);
            db.Person.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}