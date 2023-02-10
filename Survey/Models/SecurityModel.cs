using Survey.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Models
{
    public class SecurityModel
    {
        SurveyEntities5 db = new SurveyEntities5();

        public int Singup(string NameSurname, string Password, string UserName)
        {
            var IsUser = db.Person.Where(x => x.NameSurname == NameSurname).FirstOrDefault();
            var IsUserName = db.Person.Where(x => x.UserName == UserName).FirstOrDefault();
            int result = 0;
            if (IsUser != null)
            {
                result = 2;
            }
            else
            {
                if (IsUserName != null)
                {
                    result = 3;
                }
                else
                {
                    PersonRepositorycs personRepository = new PersonRepositorycs();
                    Person person = new Person()
                    {
                        UserName = UserName,
                        Password = Password,
                        NameSurname = NameSurname,
                        IsAdmin = false,
                        CreateDate = DateTime.Now,
                        CreateBy = "Sıgn Up"
                    };
                    personRepository.Add(person);
                    result = 1;
                }
            }
            return result;
        }
    }
}