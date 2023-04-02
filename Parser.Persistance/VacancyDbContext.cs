using Microsoft.EntityFrameworkCore;
using Parser.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Persistance
{
    public class VacancyDbContext : DbContext
    {
        public DbSet<Vacancy> Vacances { get; set; }
        public VacancyDbContext(DbContextOptions<VacancyDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
