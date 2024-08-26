using Microsoft.EntityFrameworkCore;
using OnKanBan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnKanBan.Persistence
{
    public class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options) : base(options)
        {
        }

        public DbSet<Card> Cards => Set<Card>();
        public DbSet<Comments> Comments => Set<Comments>();
        public DbSet<Lista> Lista => Set<Lista>();
        public DbSet<WhiteBoard> WhiteBoards => Set<WhiteBoard>();


        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);

    }
    
}
