using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecBot_GUI
{
   public class QuizManager
    {
        private List<Question> questions;
        private int index;
        private int score;

        public void Load()
        {
            questions = DatabaseManager.LoadQuestions();

            index = 0;

            score = 0;

            questions = questions
                .OrderBy(x => Guid.NewGuid())
                .ToList();
        }

        public Question Current()
        {
            if (questions == null || index >= questions.Count)
                return null;

            return questions[index];
        }

        // Accepts A / B / C
        public bool Submit(string selected)
        {
            Question q = Current();

            if (q == null)
                return false;

            bool correct =
                DatabaseManager.CheckQuizAnswer(
                    q.Id,
                    selected);

            if (correct)
                score++;

            return correct;
        }
        public bool Next()
        {
            if (questions == null) return false;

            if (index < questions.Count - 1)
            {
                index++;
                return true;
            }

            return false;
        }

        public int Score()
        {
            return score;
        }

        public int Total()
        {
            return questions?.Count ?? 0;
        }

        public int CurrentIndex()
        {
            return index + 1;
        }

        public void Reset()
        {
            index = 0;
            score = 0;
        }
    }

    // Make sure this matches DB structure
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string Answer { get; set; } // A / B / C
    }
}