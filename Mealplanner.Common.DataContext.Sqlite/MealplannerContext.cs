using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Mealplanner.Net;

public partial class MealplannerContext : DbContext
{
    public MealplannerContext()
    {
    }

    public MealplannerContext(DbContextOptions<MealplannerContext> options)
        : base(options)
    {
    }

    // This represents the table in the database.
    public virtual DbSet<Meal> Meals { get; set; } = null!;

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
