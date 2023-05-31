using Mealplanner.Net;

partial class Program
{
    static int GetLatestMealId()
    {
        using (MealplannerContext db = new())
        {
            IQueryable<Meal> meals = db.Meals.OrderByDescending(m => m.MealId);

            if (meals is null || (!meals.Any()))
            {
                // no meal with id found
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"No meal found in the database.");
                Console.ResetColor();
                return -1;
            }
            else
            {
                return meals.First().MealId;
            }
        }
    }

    static List<Meal> GetMealsByCategory(string category)
    {
        using (MealplannerContext db = new())
        {
            IQueryable<Meal> meals = db.Meals.Where(m => m.MealCategory == category);

            if (meals is null || (!meals.Any()))
            {
                // no meal with id found
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"No meals found in the {category} category.");
                Console.ResetColor();
                return null!;
            }
            else
            {
                return meals.ToList();
            }
        }
    }

    static List<Ingredient> GetAllIngredients()
    {
        using (MealplannerContext db = new())
        {
            IQueryable<Ingredient> ingredients = db.Ingredients;

            if (ingredients is null || (!ingredients.Any()))
            {
                // no meal with id found
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"No ingredients found in the database.");
                Console.ResetColor();
                return new List<Ingredient>(); // return empty list
            }
            else
            {
                return ingredients.ToList();
            }
        }
    }
}