using ctutorial.Models;
using Microsoft.EntityFrameworkCore;

namespace ctutorial.Data
{
    public class CtutorialContext<TContext> : DbContext where TContext : CtutorialContext<TContext>
    {
        private readonly string _connectionString;
        public CtutorialContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public CtutorialContext(DbContextOptions<TContext> options) : base(options)
        {
            _connectionString = Database.GetConnectionString();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_connectionString))
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }
        public DbSet<HealthCheckEntity> HealthChecks { get; set; }
        public DbSet<UsersEntity> Users { get; set; }
        public DbSet<AccountingEntity> Accountings { get; set; }
    }
}
