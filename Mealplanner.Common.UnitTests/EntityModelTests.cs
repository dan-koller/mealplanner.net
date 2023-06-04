using Mealplanner.Net;

namespace Mealplanner.Common.UnitTests;

[Collection("Serial")]
public class MealplannerContextTests
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
}

[Collection("Serial")]
public class EntityModelTests
{
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

            Assert.True(currentIngredientCount == initialIngredientCount + 1);
        }
    }

    [Fact]
    public void AddPlanTest()
    {
        List<string> weekdays = new() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        List<string> meals = new();
        Plan plan;
        List<Plan> plans = new();
        foreach (string weekday in weekdays)
        {
            string breakfast = "test breakfast";
            string lunch = "test lunch";
            string dinner = "test dinner";
            plan = new(weekday, breakfast, lunch, dinner);
            plans.Add(plan);
        }

        using (MealplannerContext db = new())
        {
            // Add the test plans.
            db.Plans.AddRange(plans);
            db.SaveChanges();

            Assert.True(db.Plans.Any(p => p.Breakfast == "test breakfast"));
            Assert.True(db.Plans.Any(p => p.Lunch == "test lunch"));
            Assert.True(db.Plans.Any(p => p.Dinner == "test dinner"));
        }
    }
}

[Collection("Serial")]
public class EntityModelDeleteTests
{
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

    [Fact]
    public void DeletePlanTest()
    {
        // Delete the test plans.
        using (MealplannerContext db = new())
        {
            IQueryable<Plan> plans = db.Plans.Where(p => p.Breakfast == "test breakfast");
            if (plans.Any())
            {
                db.Plans.RemoveRange(plans);
                db.SaveChanges();
            }

            Assert.False(db.Plans.Any(p => p.Breakfast == "test breakfast"));
        }
    }
}
