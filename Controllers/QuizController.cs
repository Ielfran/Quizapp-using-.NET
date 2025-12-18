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
            var allQuestions = _quizService.GetAllQuestions();
            return View(allQuestions);
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
                Option1 = option1,
                Option2 = option2,
                Option3 = option3,
                Option4 = option4,
                CorrectOptionIndex = correctIndex
            };

            _quizService.AddQuestion(newQuestion);
            return RedirectToAction("Index"); 
        }

        [HttpGet]
        public IActionResult Play()
        {
            var question = _quizService.GetRandomQuestion();
            if (question == null)
            {
                ViewBag.Error = "No questions found in database.";
                return View("Index", new List<QuestionModel>());
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

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _quizService.DeleteQuestion(id);
            return RedirectToAction("Index");
        }
    }
}