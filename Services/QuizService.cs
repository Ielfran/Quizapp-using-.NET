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

        public List<QuestionModel> GetAllQuestions()
        {
            return _context.Questions.ToList();
        }

        public void AddQuestion(QuestionModel question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();
        }

        public QuestionModel? GetRandomQuestion()
        {
            var count = _context.Questions.Count();
            if (count == 0) return null;

            var random = new Random();
            int skip = random.Next(0, count);
            
            return _context.Questions
                           .OrderBy(q => q.Id)
                           .Skip(skip)
                           .Take(1)
                           .FirstOrDefault();
        }
        
        public QuestionModel? GetQuestionById(int id)
        {
            return _context.Questions.FirstOrDefault(q => q.Id == id);
        }

        public void DeleteQuestion(int id)
        {
            var question = _context.Questions.FirstOrDefault(q => q.Id == id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                _context.SaveChanges();
            }
        }
    }
}