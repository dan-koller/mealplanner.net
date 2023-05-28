bool terminated = false;

while (!terminated)
{
    WriteLine("What would you like to do (add, show, plan, save, exit)?");
    string command = ReadLine()!.ToLower();
    switch (command)
    {
        case "add":
            AddMealDialog();
            break;
        case "show":
            ShowMealDialog();
            break;
        case "exit":
            WriteLine("Goodbye!");
            terminated = true;
            break;
        default:
            WriteLine("Please enter a valid command.");
            break;
    }
}

// // test to see if a new meal can be added to the database
// string mealName = "Baked Cauliflower";
// string mealCategory = "Lunch";
// string[] mealIngredients = new string[] { "Cauliflower", "Olive Oil", "Salt", "Pepper" };

// // convert the string[] to a string
// string mealIngredientsString = Program.IngredientArrayToString(mealIngredients);

// // create a new meal
// Meal newMeal = new Meal(mealName, mealCategory, mealIngredientsString);

// // add the new meal to the database
// int id = Program.AddMeal(newMeal);

// // test to see if the meal was added to the database
// WriteLine($"Added meal with id {id} to the database.");

// // test to see if the meal can be deleted from the database
// int latestId = Program.GetLatestMealId();
// WriteLine($"Latest meal id is {latestId}.");
// int affected = Program.DeleteMealById(latestId);
// WriteLine($"Deleted meal with id {latestId} from the database.");
