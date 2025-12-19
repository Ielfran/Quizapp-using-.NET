using System.ComponentModel.DataAnnotations.Schema;

namespace Quizapp.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        
        public string QuestionText { get; set; } = string.Empty;
        
        public string Option1 { get; set; } = string.Empty;
        public string Option2 { get; set; } = string.Empty;
        public string Option3 { get; set; } = string.Empty;
        public string Option4 { get; set; } = string.Empty;

        public int CorrectOptionIndex { get; set; }

        public string UserId { get; set; } = string.Empty;

        [NotMapped]
        public List<string> Options => new List<string> { Option1, Option2, Option3, Option4 };
    }
}