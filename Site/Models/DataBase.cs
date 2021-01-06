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
                    if (questions.Count == 0 ||
                        questions[questions.Count - 1].QuestionText != sqlReader["Question"].ToString())
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
            int testId;
            string queryString;

            sqlConnection.Open();

            // Get current test id.
            testId = GetMaxTestId() + 1;

            // Insert line in Tests.
            queryString = "insert into Tests values (@testId, @testName)";
            ExecuteQuery( queryString,
                new KeyValuePair<string, object>("@testId", testId),
                new KeyValuePair<string, object>("@testName", test.TestName));

            int questionsCount = test.Questions.Count;
            for (int questionId = 0; questionId < questionsCount; questionId++)
            {
                // Insert line in Questions.
                queryString = "insert into Questions values (@testId, @questionId, @questionText, @rightAnswerId)";
                ExecuteQuery( queryString,
                    new KeyValuePair<string, object>("@testId", testId),
                    new KeyValuePair<string, object>("@questionId", questionId + 1),
                    new KeyValuePair<string, object>("@questionText", test.Questions[questionId].QuestionText),
                    new KeyValuePair<string, object>("@rightAnswerId", test.Questions[questionId].RightAnswerId));

                int answersCount = test.Questions[questionId].Answers.Count;
                for (int answerId = 0; answerId < answersCount; answerId++)
                {
                    // Insert line in Answers.
                    queryString = "insert into Answers values (@testId, @questionId, @answerId, @answerText)";
                    ExecuteQuery( queryString,
                        new KeyValuePair<string, object>("@testId", testId),
                        new KeyValuePair<string, object>("@questionId", questionId + 1),
                        new KeyValuePair<string, object>("@answerId", answerId + 1),
                        new KeyValuePair<string, object>("@answerText", test.Questions[questionId].Answers[answerId].AnswerText));
                }
            }

            sqlConnection.Close();
        }

        public static void DeleteTest(int testId)
        {
            string queryString;

            sqlConnection.Open();

            // Delete from Tests.
            queryString = "delete from Tests where Id=@id";
            ExecuteQuery(queryString, new KeyValuePair<string, object>("@id", testId));

            // Delete from Questions.
            queryString = "delete from Questions where TestId=@testId";
            ExecuteQuery(queryString, new KeyValuePair<string, object>("@testId", testId));

            // Delete from Answers.
            queryString = "delete from Answers where TestId=@testId";
            ExecuteQuery(queryString, new KeyValuePair<string, object>("@testId", testId));

            sqlConnection.Close();
        }

        private static int GetMaxTestId()
        {
            int id = 0;

            string queryString = "select max(Id) from Tests";
            SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
            using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
            {
                if (sqlReader.Read())
                {
                    try
                    {
                        id = int.Parse(sqlReader[0].ToString());
                    }
                    catch { }
                }
            }

            return id;
        }

        private static void ExecuteQuery(string queryString, params KeyValuePair<string, object>[] sqlParameters)
        {
            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand(queryString, sqlConnection);
            foreach (var p in sqlParameters)
            {
                sqlCommand.Parameters.AddWithValue(p.Key, p.Value);
            }
            sqlCommand.ExecuteNonQuery();
        }
    }
}



// 173 lines

// ?