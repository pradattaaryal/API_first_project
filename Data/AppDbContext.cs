using API_first_project.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API_first_project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
