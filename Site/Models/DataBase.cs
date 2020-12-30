using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Site.Models
{
    public static class DataBase
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["TestsDB"].ToString();
        static SqlConnection sqlConnection = new SqlConnection(connectionString);

        public static Dictionary<int, string> GetTestNames()
        {
            Dictionary<int, string> testNames = new Dictionary<int, string>();

            string queryString = "select Id, TestName from Tests";
            SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
            sqlConnection.Open();

            using(SqlDataReader sqlReader = sqlCommand.ExecuteReader())
            {
                while (sqlReader.Read())
                {
                    testNames.Add(int.Parse(sqlReader["Id"].ToString()), sqlReader["TestName"].ToString());
                }
            }

            sqlConnection.Close();

            return testNames;
        }

        public static List<Question> GetQuestions(int testId)
        {
            List<Question> questions = new List<Question>();

            string queryString = 
                "select Question, RightAnswerId, a.Id, Answer " +
                "from Questions q " +
                "inner join Answers a " +
                "on q.Id = a.QuestionId " +
                "where (q.TestId = @id and a.TestId = @id)";
            SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
            sqlCommand.Parameters.AddWithValue("id", testId);
            sqlConnection.Open();

            using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
            {
                while (sqlReader.Read())
                {
                    if (questions.Count == 0 || questions[questions.Count - 1].QuestionText != sqlReader["Question"].ToString())
                    {
                        Question question = new Question();
                        question.QuestionText = sqlReader["Question"].ToString();
                        question.RightAnswer = int.Parse(sqlReader["RightAnswerId"].ToString());
                        question.Answers.Add(int.Parse(sqlReader["Id"].ToString()), sqlReader["Answer"].ToString());
                        questions.Add(question);
                    }
                    else
                    {
                        questions[questions.Count - 1].Answers.Add
                        (
                            int.Parse(sqlReader["Id"].ToString()),
                            sqlReader["Answer"].ToString()
                        );
                    }
                }
            }

            sqlConnection.Close();

            return questions;
        }

        public static void AddTest(Test test)
        {

        }

        public static void RemoveTest(int testId)
        {

        }
    }
}