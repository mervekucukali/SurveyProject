using Survey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Repository
{
    public class QuestionRepository : IGenericRepository<Question>
    {
        SurveyEntities5 db=new SurveyEntities5();
        public bool Add(Question entity)
        {
            bool result = false;
            try
            {
                var question = new Question();
               question.QuestionLine = entity.QuestionLine;
                question.QuestionType = entity.QuestionType;
                question.QuestionLine=entity.QuestionLine;
                question.CreateDate = entity.CreateDate;
                question.CategoryName = entity.CategoryName;
                question.CreateBy = entity.CreateBy;

                db.Question.Add(question);
                db.SaveChanges();
                result = true;
            }
            catch { }
            return result;
        }

        public bool Delete(int id)
        {
            var question = db.Question.Find(id);
            bool result = false;

            db.SaveChanges();

            return result;
        }

        public Question Detail(int id)
        {
            var question = db.Question.Find(id);

            return question;
        }
        public Question Detail1(string categoryname)
        {
            var question = db.Question.Find(categoryname);

            return question;
        }

        public bool Edit(Question entity)
        {
            bool result = false;
            try
            {

                Question question = db.Question.Find(entity.Id);
                question.QuestionLine = entity.QuestionLine;
                question.QuestionType = entity.QuestionType;
                question.QuestionLine = entity.QuestionLine;
                question.CreateDate = entity.CreateDate;
                question.CategoryName = entity.CategoryName;
                question.CreateBy = entity.CreateBy;

                db.SaveChanges();
                result = true;
            }
            catch { }
            return result;
        }

        public List<Question> List()
        {
            return db.Question.ToList();
        }
    }
}