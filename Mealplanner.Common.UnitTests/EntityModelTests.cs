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
            // Create db if it doesn't exist.
            db.Database.EnsureCreated();

            Meal meal = new("Test Meal", "lunch", "test, test, test");
            db.Meals.Add(meal);
            db.SaveChanges();

            Assert.True(db.Meals.Any(m => m.MealName == "Test Meal"));
        }
    }

    [Fact]
    public void DeleteMealTest()
    {
        using (MealplannerContext db = new())
        {
            // Delete the Test Meal.
            IQueryable<Meal> meals = db.Meals.Where(m => m.MealName == "Test Meal");
            if (meals.Any())
            {
                db.Meals.RemoveRange(meals);
                db.SaveChanges();
            }

            Assert.False(db.Meals.Any(m => m.MealName == "Test Meal"));
        }
    }
}