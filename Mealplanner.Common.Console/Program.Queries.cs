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

    static List<Ingredient> GetIngredientsForPlan()
    {
        List<Ingredient> ingredients = new();

        using (MealplannerContext db = new())
        {
            foreach (Plan plan in db.Plans)
            {
                AddIngredientsFromMeal(db, ingredients, plan.Breakfast);
                AddIngredientsFromMeal(db, ingredients, plan.Lunch);
                AddIngredientsFromMeal(db, ingredients, plan.Dinner);
            }
        }

        return ingredients;
    }

    static void AddIngredientsFromMeal(MealplannerContext db, List<Ingredient> ingredients, string mealName)
    {
        Meal? meal = db.Meals.FirstOrDefault(m => m.MealName == mealName);

        if (meal is not null)
        {
            // The string is separated by commas, so we need to split it
            List<Ingredient> mealIngredients = meal.MealIngredients.Split(',').Select(i => new Ingredient(i)).ToList();
            ingredients.AddRange(mealIngredients);
        }
    }

    static List<Plan> GetAllPlans()
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
                return new List<Plan>(); // return empty list
            }
            else
            {
                return plans.ToList();
            }
        }
    }
}