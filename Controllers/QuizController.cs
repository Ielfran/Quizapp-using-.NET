using Microsoft.AspNetCore.Mvc;
using Quizapp.Models;
using Quizapp.Services;

namespace Quizapp.Controllers
{
    public class QuizController : Controller
    {
        private readonly QuizService _quizService;

        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string questionText, string option1, string option2, string option3, string option4, int correctIndex)
        {
            var newQuestion = new QuestionModel
            {
                QuestionText = questionText,
                Options = new List<string> { option1, option2, option3, option4 },
                CorrectOptionIndex = correctIndex
            };

            _quizService.AddQuestion(newQuestion);
            ViewBag.Message = "Question added successfully!";
            return View();
        }

        [HttpGet]
        public IActionResult Play()
        {
            var question = _quizService.GetRandomQuestion();
            if (question == null)
            {
                return Content("No questions available. Please add some first.");
            }
            return View(question);
        }

        [HttpPost]
        public IActionResult SubmitAnswer(int questionId, int selectedIndex)
        {
            var question = _quizService.GetQuestionById(questionId);
            bool isCorrect = false;
            
            if (question != null)
            {
                isCorrect = (question.CorrectOptionIndex == selectedIndex);
                ViewBag.CorrectIndex = question.CorrectOptionIndex;
            }

            ViewBag.IsCorrect = isCorrect;
            ViewBag.UserChoice = selectedIndex;
            
            return View("Result", question);
        }
    }
}