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
            sqlConnection.Close();
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
                        question.RightAnswerId = int.Parse(sqlReader["RightAnswerId"].ToString());
                        question.Answers.Add(new Answer
                        {
                            Id = int.Parse(sqlReader["Id"].ToString()),
                            AnswerText = sqlReader["Answer"].ToString()
                        });
                        questions.Add(question);
                    }
                    else
                    {
                        questions[questions.Count - 1].Answers.Add(new Answer
                        {
                            Id = int.Parse(sqlReader["Id"].ToString()),
                            AnswerText = sqlReader["Answer"].ToString()
                        });
                    }
                }
            }

            sqlConnection.Close();

            return questions;
        }

        public static void AddTest(Test test)
        {
            int maxTestId;
            int currentTestId;
            string queryString;
            SqlCommand sqlCommand;

            sqlConnection.Open();

            // Get max test id.
            maxTestId = 0;
            queryString = "select max(Id) from Tests";
            sqlCommand = new SqlCommand(queryString, sqlConnection);
            using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
            {
                if (sqlReader.Read())
                {
                    try
                    {
                        maxTestId = int.Parse(sqlReader[0].ToString());
                    }
                    catch
                    {
                        maxTestId = 0;
                    }
                }
            }

            // Insert line in Tests.
            currentTestId = maxTestId + 1;
            queryString = "insert into Tests values (@testId, @testName)";
            sqlCommand = new SqlCommand(queryString, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@testId", currentTestId);
            sqlCommand.Parameters.AddWithValue("@testName", test.TestName);
            sqlCommand.ExecuteNonQuery();

            int questionsCount = test.Questions.Count;
            for (int questionId = 0; questionId < questionsCount; questionId++)
            {
                // Insert line in Questions.
                queryString = "insert into Questions values (@testId, @questionId, @questionText, @rightAnswerId)";
                sqlCommand = new SqlCommand(queryString, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@testId", currentTestId);
                sqlCommand.Parameters.AddWithValue("@questionId", questionId + 1);
                sqlCommand.Parameters.AddWithValue("@questionText", test.Questions[questionId].QuestionText);
                sqlCommand.Parameters.AddWithValue("@rightAnswerId", test.Questions[questionId].RightAnswerId);
                sqlCommand.ExecuteNonQuery();

                int answersCount = test.Questions[questionId].Answers.Count;
                for (int answerId = 0; answerId < answersCount; answerId++)
                {
                    // Insert line in Answers.
                    queryString = "insert into Answers values (@testId, @questionId, @answerId, @answerText)";
                    sqlCommand = new SqlCommand(queryString, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@testId", currentTestId);
                    sqlCommand.Parameters.AddWithValue("@questionId", questionId + 1);
                    sqlCommand.Parameters.AddWithValue("@answerId", answerId + 1);
                    sqlCommand.Parameters.AddWithValue("@answerText", test.Questions[questionId].Answers[answerId].AnswerText);
                    sqlCommand.ExecuteNonQuery();
                }
            }

            sqlConnection.Close();
        }

        public static void RemoveTest(int testId)
        {
            string queryString;
            SqlCommand sqlCommand;

            sqlConnection.Open();

            // Delete from Tests.
            queryString = "delete from Tests where Id=@id";
            sqlCommand = new SqlCommand(queryString, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", testId);
            sqlCommand.ExecuteNonQuery();

            // Delete from Questions.
            queryString = "delete from Questions where TestId=@testId";
            sqlCommand = new SqlCommand(queryString, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@testId", testId);
            sqlCommand.ExecuteNonQuery();

            // Delete from Answers.
            queryString = "delete from Answers where TestId=@testId";
            sqlCommand = new SqlCommand(queryString, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@testId", testId);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
    }
}