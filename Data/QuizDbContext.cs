using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quizapp.Models;
using Microsoft.AspNetCore.Identity;

namespace Quizapp.Data
{
    public class QuizDbContext : IdentityDbContext<IdentityUser>
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
        {
        }

        public DbSet<QuestionModel> Questions { get; set; }
    }
}