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
}