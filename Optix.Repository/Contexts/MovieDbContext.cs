﻿using Microsoft.EntityFrameworkCore;
using Optix.Domain.Models;

namespace Optix.Repository.Contexts
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().Navigation(m => m.Genres).AutoInclude();
        }
    }
}