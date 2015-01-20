using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCzasu
{
    class User_Recommendation /* : Questionnaire*/
    {

        User_Recommendation()
        {

        }
      /*  public int ocena;
        public String comment;

        public List<Questionnaire> Question = new List<Questionnaire>();

        public User_Recommendation(int _ocena, String _comment)
        {
            this.ocena = _ocena;
            this.comment = _comment;
        }


        public void showQuestion()
        {
            foreach (var f in Question)
            {
                Console.WriteLine(f.ToString());
            }
        }


        public void editQuestionnaire(Questionnaire _questionnaire)
        {
            Question.Add(_questionnaire);
        }


        public void removeQuestion(Questionnaire _questionnaire)
        {
            Question.Remove(_questionnaire);
        }


        public bool Question_DB(DateTime date, String subject, String additional)
        {
            string SQLdate = DataBase.ToSQLString(date);
            string SQLsubject = DataBase.ToSQLString(subject);
            string SQLadditional = DataBase.ToSQLString(additional);

            if (!DataBase.Insert(Tables.ankieta,
                new Dictionary<Enum, String>()
                {
                    {ankieta.date,      SQLdate},
                    {ankieta.subject,  SQLsubject},
					{ankieta.additional,  SQLadditional},

                }))
                return false;
        }


        public bool Recommendation_DB(int ocena, String comment)
        {
            string SQLocena = DataBase.ToSQLString(ocena);
            string SQLcomment = DataBase.ToSQLString(comment);

            if (!DataBase.Insert(Tables.rekomendacje,
                new Dictionary<Enum, String>()
        {
            {rekomendacje.ocena,      SQLmark},
            {rekomendacje.comment,  SQLcomment},
		}))
                return false;
        }


        public String Get_Question_DB()
        {
            List<List<String>> helpList = DataBase.SelectWhere(Tables.ankieta, new Dictionary<Enum, string>() { { ankieta.date, DataBase.ToSQLString(date) } }, ankieta.subject);
            String subject = helpList[0][0];

            if (subject.Equals(DataBase.NULL))
                return "GetQuestion_DB Error subject";

            helpList = DataBase.SelectWhere(Tables.ankieta, new Dictionary<Enum, string>() { { ankieta.subject, subject } }, ankieta.additional);
            String additional = helpList[0][0];

            if (additional.Equals(DataBase.NULL))
                return "GetQuestion_DB Error additional";

            return additional;
        }


        public String Get_Recommendation_DB()
        {
            List<List<String>> helpList = DataBase.SelectWhere(Tables.rekomendacje, new Dictionary<Enum, string>() { { rekomendacje.ocena, DataBase.ToSQLString(ocena) } }, rekomendacje.comment);
            String comment = helpList[0][0];

            if (comment.Equals(DataBase.NULL))
                return "GetRecommendation_DB Error comment";

            return comment;
        }*/
    }
}