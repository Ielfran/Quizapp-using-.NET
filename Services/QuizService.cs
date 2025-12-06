using System.Text.Json;
using Quizapp.Models;

namespace Quizapp.Services
{
    public class QuizService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _jsonFilePath;

        public QuizService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _jsonFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "questions.json");
            
            if (!File.Exists(_jsonFilePath))
            {
                File.WriteAllText(_jsonFilePath, "[]");
            }
        }

        public List<QuestionModel> GetAllQuestions()
        {
            var json = File.ReadAllText(_jsonFilePath);
            return JsonSerializer.Deserialize<List<QuestionModel>>(json) ?? new List<QuestionModel>();
        }

        public void AddQuestion(QuestionModel question)
        {
            var questions = GetAllQuestions();
            question.Id = questions.Any() ? questions.Max(q => q.Id) + 1 : 1;
            
            questions.Add(question);
            SaveQuestions(questions);
        }

        public QuestionModel? GetRandomQuestion()
        {
            var questions = GetAllQuestions();
            if (!questions.Any()) return null;

            var random = new Random();
            int index = random.Next(questions.Count);
            return questions[index];
        }
        
        public QuestionModel? GetQuestionById(int id)
        {
            return GetAllQuestions().FirstOrDefault(q => q.Id == id);
        }

        private void SaveQuestions(List<QuestionModel> questions)
        {
            var json = JsonSerializer.Serialize(questions, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_jsonFilePath, json);
        }
    }
}