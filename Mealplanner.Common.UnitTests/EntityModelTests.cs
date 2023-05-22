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
}