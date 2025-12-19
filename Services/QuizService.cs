using Quizapp.Data;
using Quizapp.Models;

namespace Quizapp.Services
{
    public class QuizService
    {
        private readonly QuizDbContext _context;

        public QuizService(QuizDbContext context)
        {
            _context = context;
        }

        public List<QuestionModel> GetAllQuestions(string userId)
        {
            return _context.Questions.Where(q => q.UserId == userId).ToList();
        }

        public void AddQuestion(QuestionModel question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();
        }

        public QuestionModel? GetRandomQuestion(string userId)
        {
            var userQuestions = _context.Questions.Where(q => q.UserId == userId);
            int count = userQuestions.Count();

            if (count == 0) return null;

            var random = new Random();
            int skip = random.Next(0, count);

            return userQuestions
                .OrderBy(q => q.Id)
                .Skip(skip)
                .Take(1)
                .FirstOrDefault();
        }

        public QuestionModel? GetQuestionById(int id)
        {
            return _context.Questions.FirstOrDefault(q => q.Id == id);
        }

        public void DeleteQuestion(int id, string userId)
        {
            var question = _context.Questions.FirstOrDefault(q => q.Id == id && q.UserId == userId);
            if (question != null)
            {
                _context.Questions.Remove(question);
                _context.SaveChanges();
            }
        }
    }
}