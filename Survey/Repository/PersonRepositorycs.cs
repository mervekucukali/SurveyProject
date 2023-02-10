using Survey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Repository
{
    public class PersonRepositorycs : IGenericRepository<Person>
    {
        SurveyEntities5 db = new SurveyEntities5();
        public bool Add(Person entity)
        {
            bool result = false;
            try
            {
                var person = new Person();
                person.NameSurname = entity.NameSurname;
                person.UserName = entity.UserName;
                person.Password = entity.Password;
                person.CreateBy = entity.CreateBy;
                person.CreateDate = entity.CreateDate;
                person.IsAdmin = entity.IsAdmin;
                db.Person.Add(person);
                db.SaveChanges();
                result = true;
            }
            catch { }
            return result;
        }

        public bool Delete(int id)
        {
            var user = db.Person.Find(id);
            bool result = false;

            db.SaveChanges();
            
            return result;
        }

        public Person Detail(int id)
        {
            var person = db.Person.Find(id);

            return person;
        }

        public bool Edit(Person entity)
        {
            bool result = false;
            try
            {

                Person person = db.Person.Find(entity.Id);
                person.NameSurname = entity.NameSurname;
                person.UserName = entity.UserName;
                person.Password = entity.Password;
                person.CreateBy = entity.CreateBy;
                person.CreateDate = entity.CreateDate;

                db.SaveChanges();
                result = true;
            }
            catch { }
            return result;

        }

        public List<Person> List()
        {
            return db.Person.ToList();
        }
    }
}