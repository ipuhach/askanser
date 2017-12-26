using Askanser.Core.Entities;
using Askanser.Core.Interfaces;
using Dapper;
using System.Collections.Generic;

namespace Askanser.Data.Repositories
{
    public class AskanserProvider : BaseRepository, IAskanserProvider
    {
        /// <summary>
        /// User Repository constructor passing injected connection factory to the Base Repository
        /// </summary>
        /// <param name="connectionFactory">The injected connection factory.  It is injected with the constructor argument that is the connection string.</param>
        public AskanserProvider(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public IEnumerable<Question> GetQuestions()
        {
            return WithConnection(async connection =>
            {
                string query = "SELECT * FROM Questions";
                return connection.Query<Question>(query);

            }).Result;
            //throw new ArgumentException("ups..");
        }

        public Question GetQuestion(int questionId)
        {
            return WithConnection(async connection =>
            {
                string query = "SELECT TOP 1 * FROM Questions WHERE Id = @QuestionId";
                return connection.Query<Question>(query, new { QuestionId = questionId }).AsList()[0];

            }).Result;
            //throw new ArgumentException("ups..");
        }


        public bool AddQuestion(string text, string userId)
        {
            return WithConnection(async connection =>
            {
                string query = "INSERT INTO Questions(Text, UserId) VALUES (@Text, @UserId)";
                var x = connection.Execute(query, new { Text = text, UserId = userId });
                if (x == 1) return true;
                else return false;
            }).Result;
        }


        public bool AddAnswer(int questionId, string text, string userId)
        {
            return WithConnection(async connection =>
            {
                string query = "INSERT INTO Answers(QuestionId, Text, UserId) VALUES (@QuestionId,@Text, @UserId)";
                var x = connection.Execute(query, new { QuestionId = questionId, Text = text, UserId = userId });
                if (x == 1) return true;
                else return false;
            }).Result;
        }



        public IEnumerable<Quanswer> GetAnswersForMyQuestions(string userId)
        {
            return WithConnection(async connection =>
            {
                string query = "SELECT a.Id as AnsId, q.Text as QueText, a.Text as AnsText, a.Score as Score " +
                                "FROM Answers as a, Questions as q WHERE a.QuestionId = q.Id AND q.UserId like @UserId AND a.Score = 0";


                return connection.Query<Quanswer>(query, new { UserId = userId });

            }).Result;
        }

        //sidenote This is for perticular answer, the one above get a list of all answers

        public Quanswer GetAnswerForMyQuestion(int answerId)
        {
            return WithConnection(async connection =>
            {
                string query = "SELECT a.Id as AnsId, q.Text as QueText, a.Text as AnsText, a.Score as Score " +
                                "FROM Answers as a, Questions as q WHERE a.QuestionId = q.Id AND a.Id = @AnswerId";


                return connection.Query<Quanswer>(query, new { AnswerId = answerId }).AsList()[0];

            }).Result;
        }

        public IEnumerable<Quanswer> GetMyAnswers(string userId)
        {
            return WithConnection(async connection =>
            {
                string query = "SELECT a.Id as AnsId, q.Text as QueText, a.Text as AnsText, a.Score as Score " +
                                "FROM Answers as a, Questions as q WHERE a.QuestionId = q.Id AND a.UserId like @UserId AND a.Score != 0";


                return connection.Query<Quanswer>(query, new { UserId = userId });

            }).Result;
        }
        public bool SetScore(int answerId, int score)
        {
            return WithConnection(async connection =>
            {
                string query = "UPDATE Answers SET Score = @Score where Id = @AnswerId";


                int x = connection.Execute(query, new { AnswerId = answerId, Score = score });
                if (x == 1) return true;
                else return false;

            }).Result;
        }


        public Quanswer GetTopOneQuanswer()
        {
            return WithConnection(async connection =>
            {
                string query = "SELECT a.Id as AnsId, q.Text as QueText, a.Text as AnsText, a.Score as Score " +
                                "FROM Answers as a, Questions as q " +
                                "WHERE a.QuestionId = q.Id AND a.Score = (SELECT MAX(Score) FROM Answers)";
                var x =  connection.Query<Quanswer>(query).AsList()[0];
                return x;

            }).Result;
        }

        public void Dispose()
        {
        }
    }
}
