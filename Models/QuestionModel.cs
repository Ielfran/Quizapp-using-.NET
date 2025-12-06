namespace Quizapp.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        
        public string QuestionText { get; set; } = string.Empty; 
        
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectOptionIndex { get; set; } 
    }
}