using Askanser.Core.Entities;
using System.Collections.Generic;

namespace Askanser.Core.Interfaces
{
    public interface IAskanserProvider
    {
        IEnumerable<Question> GetQuestions();
        Question GetQuestion(int questionId);

        bool AddQuestion(string text, string userId);
        bool AddAnswer(int questionId, string text, string userId);

        IEnumerable<Quanswer> GetAnswersForMyQuestions(string userId);
        Quanswer GetAnswerForMyQuestion(int answerId);
        IEnumerable<Quanswer> GetMyAnswers(string userId);
        bool SetScore(int answerId, int score);

        Quanswer GetTopOneQuanswer();
    }
}
