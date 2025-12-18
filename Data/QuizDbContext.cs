using Microsoft.EntityFrameworkCore;
using Quizapp.Models;

namespace Quizapp.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
        {
        }

        public DbSet<QuestionModel> Questions { get; set; }
    }
}