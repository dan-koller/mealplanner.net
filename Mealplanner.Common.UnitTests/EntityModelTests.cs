using Mealplanner.Net;

namespace Mealplanner.Common.UnitTests;

public class EntityModelTests
{
    [Fact]
    public void DatabaseConnectTest()
    {
        using (MealplannerContext db = new())
        {
            // Create db if it doesn't exist.
            db.Database.EnsureCreated();

            Assert.True(db.Database.CanConnect());
        }
    }

    [Fact]
    public void AddMealTest()
    {
        using (MealplannerContext db = new())
        {
            Meal meal = new("Test Meal", "lunch", "test, test, test");
            db.Meals.Add(meal);
            db.SaveChanges();

            Assert.True(db.Meals.Any(m => m.MealName == "Test Meal"));
        }
    }

    [Fact]
    public void AddIngredientTest()
    {
        using (MealplannerContext db = new())
        {
            int initialIngredientCount = db.Ingredients.Count();

            // Add 3 test ingredients.
            Ingredient ingredient1 = new("test");
            Ingredient ingredient2 = new("test");
            Ingredient ingredient3 = new("test");

            List<Ingredient> testIngredients = new() { ingredient1, ingredient2, ingredient3 };
            List<Ingredient> ingredients = db.Ingredients.ToList();

            foreach (Ingredient ingredient in testIngredients)
            {
                // Only add the ingredient if it doesn't already exist.
                if (!ingredients.Contains(ingredient))
                {
                    db.Ingredients.Add(ingredient);
                    db.SaveChanges();
                }
                // Update the list of ingredients.
                ingredients = db.Ingredients.ToList();
            }

            int currentIngredientCount = db.Ingredients.Count();

            System.Console.WriteLine($"Initial ingredient count: {initialIngredientCount}");
            System.Console.WriteLine($"Current ingredient count: {currentIngredientCount}");

            Assert.True(currentIngredientCount == initialIngredientCount + 1);
        }
    }

    [Fact]
    public void DeleteMealTest()
    {
        using (MealplannerContext db = new())
        {
            // Delete the test meal.
            IQueryable<Meal> meals = db.Meals.Where(m => m.MealName == "Test Meal");
            if (meals.Any())
            {
                db.Meals.RemoveRange(meals);
                db.SaveChanges();
            }

            Assert.False(db.Meals.Any(m => m.MealName == "Test Meal"));
        }
    }

    [Fact]
    public void DeleteIngredientTest()
    {
        using (MealplannerContext db = new())
        {
            // Delete the test ingredients.
            IQueryable<Ingredient> ingredients = db.Ingredients.Where(i => i.IngredientName == "test");
            if (ingredients.Any())
            {
                db.Ingredients.RemoveRange(ingredients);
                db.SaveChanges();
            }

            Assert.False(db.Ingredients.Any(i => i.IngredientName == "test"));
        }
    }
}