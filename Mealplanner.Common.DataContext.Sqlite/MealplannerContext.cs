using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Mealplanner.Net;

public partial class MealplannerContext : DbContext
{
    public MealplannerContext()
    {
        // Ensure the database is created. This is only used during development
        // and not suitable for production. In a production environment, use
        // migrations to create the database.
        Database.EnsureCreated();
    }

    public MealplannerContext(DbContextOptions<MealplannerContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    // This represents the table in the database.
    public virtual DbSet<Meal> Meals { get; set; } = null!;
    public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string dir = Environment.CurrentDirectory;
            string path = string.Empty;

            if (dir.EndsWith("net7.0"))
            {
                // Running in the <project>\bin\<Debug|Release>\net7.0 directory.
                path = Path.Combine("..", "..", "..", "..", "Mealplanner.db");
            }
            else
            {
                // Running in the <project> directory.
                path = Path.Combine("..", "Mealplanner.db");
            }

            optionsBuilder.UseSqlite($"Filename={path}");
        }
    }
}
