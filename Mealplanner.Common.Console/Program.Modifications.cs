using Microsoft.EntityFrameworkCore; // ExecuteUpdate, ExecuteDelete
using Microsoft.EntityFrameworkCore.ChangeTracking; // EntityEntry<T>
using Microsoft.EntityFrameworkCore.Storage; // IDbContextTransaction

using Mealplanner.Net;

partial class Program
{
    // TODO: Move this function to Program.Manipulation.cs
    static int AddMeal(Meal meal)
    {
        using (MealplannerContext db = new())
        {
            db.Meals.Add(meal);
            db.SaveChanges();
            return meal.MealId;
        }
    }

    static int DeleteMealById(int id)
    {
        using (MealplannerContext db = new())
        {
            IQueryable<Meal> meals = db.Meals.Where(m => m.MealId == id);

            if (meals is null || (!meals.Any()))
            {
                // no meal with id found
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"No meal with id {id} found in the database.");
                Console.ResetColor();
                return -1;
            }
            else
            {
                if (db.Meals is null)
                {
                    return -1;
                }
                else
                {
                    db.Meals.RemoveRange(meals);
                }
            }

            int affected = db.SaveChanges();
            return affected;
        }
    }
}