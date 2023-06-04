using Microsoft.EntityFrameworkCore; // ExecuteUpdate, ExecuteDelete
using Microsoft.EntityFrameworkCore.ChangeTracking; // EntityEntry<T>
using Microsoft.EntityFrameworkCore.Storage; // IDbContextTransaction

using Mealplanner.Net;

partial class Program
{
    static int AddMeal(Meal meal)
    {
        using (MealplannerContext db = new())
        {
            // Add each ingredient of the meal to the database
            List<string> mealIngredients = new(meal.MealIngredients.Split(","));
            List<Ingredient> ingredients = GetAllIngredients();
            foreach (string ingredient in mealIngredients)
            {
                Ingredient i = new(ingredient);
                if (!ingredients.Contains(i))
                {
                    AddIngredient(i); // don't need the id (may be useful for error handling)
                }
            }
            db.Meals.Add(meal);
            db.SaveChanges();
            return meal.MealId;
        }
    }

    static int AddIngredient(Ingredient ingredient)
    {
        using (MealplannerContext db = new())
        {
            db.Ingredients.Add(ingredient);
            db.SaveChanges();
            return ingredient.IngredientId;
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

    static int ClearPlans()
    {
        using (MealplannerContext db = new())
        {
            IQueryable<Plan> plans = db.Plans;

            if (plans is null || (!plans.Any()))
            {
                // no meal with id found
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"No plans found in the database.");
                Console.ResetColor();
                return -1;
            }
            else
            {
                if (db.Plans is null)
                {
                    return -1;
                }
                else
                {
                    db.Plans.RemoveRange(plans);
                }
            }

            int affected = db.SaveChanges();
            return affected;
        }
    }

    static int AddPlans(List<Plan> plans)
    {
        using (MealplannerContext db = new())
        {
            db.Plans.AddRange(plans);
            db.SaveChanges();
            return plans.Count;
        }
    }
}