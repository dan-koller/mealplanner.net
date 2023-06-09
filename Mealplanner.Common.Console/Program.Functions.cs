using System.Text.RegularExpressions; // Regex
using Mealplanner.Net;

partial class Program
{
    private static readonly Regex _validMealCategory = new("breakfast|lunch|dinner");
    private static readonly Regex _validMealName = new("[a-zA-Z ]+");
    private static readonly Regex _validIngredients = new("([a-zA-Z]+,? ?)+(?<!,)(?<! )");


    private static void AddMealDialog()
    {
        string mealCategory = GetMealCategory();
        string mealName = GetMealName();
        string mealIngredients = GetIngredients();

        Meal meal = new Meal(mealName, mealCategory, mealIngredients);

        int id = AddMeal(meal);
        if (id != -1)
        {
            WriteLine($"Added {mealName} with id {id} to the database.");
        }
        else
        {
            WriteLine($"Failed to add {mealName} to the database.");
        }
    }

    private static string GetMealCategory()
    {
        string? mealCategory = null;
        bool isVerifiedCategory = false;
        WriteLine("Which meal do you want to add (breakfast, lunch, dinner)?");
        while (!isVerifiedCategory)
        {
            mealCategory = ReadLine();
            if (mealCategory is not null && _validMealCategory.IsMatch(mealCategory))
            {
                isVerifiedCategory = true;
            }
            else
            {
                WriteLine("Please enter a valid meal category (breakfast, lunch, dinner).");
            }
        }
        return mealCategory!; // Won't be null because of the while loop
    }

    private static string GetMealName()
    {
        string? mealName = null;
        bool isVerifiedName = false;
        WriteLine("Input the meal's name:");
        while (!isVerifiedName)
        {
            mealName = ReadLine();
            if (mealName is not null && _validMealName.IsMatch(mealName))
            {
                isVerifiedName = true;
            }
            else
            {
                WriteLine("Please enter a valid meal name.");
            }
        }
        return mealName!;
    }

    private static string GetIngredients()
    {
        string[]? ingredients = null;
        bool isVerifiedIngredients = false;
        WriteLine("Input the meal's ingredients (separated by commas):");
        while (!isVerifiedIngredients)
        {
            ingredients = ReadLine()!.Split(",");
            if (ingredients is not null && _validIngredients.IsMatch(IngredientArrayToString(ingredients!)))
            {
                ingredients = RemoveWhitespace(ingredients);
                isVerifiedIngredients = true;
            }
            else
            {
                WriteLine("Please enter valid ingredients (separated by commas).");
            }
        }
        return IngredientArrayToString(ingredients!);
    }

    private static void ShowMealDialog()
    {
        WriteLine("Which category do you want to print (breakfast, lunch, dinner)?");
        string? mealCategory;
        bool isVerifiedCategory = false;
        while (!isVerifiedCategory)
        {
            mealCategory = ReadLine();
            if (mealCategory is not null && _validMealCategory.IsMatch(mealCategory))
            {
                isVerifiedCategory = true;
                ShowMealByCategory(mealCategory);
            }
            else
            {
                WriteLine("Please enter a valid meal category (breakfast, lunch, dinner).");
            }
        }
    }

    private static void ShowMealByCategory(string mealCategory)
    {
        List<Meal> meals = GetMealsByCategory(mealCategory);
        if (meals is not null)
        {
            WriteLine($"Category: {mealCategory}");
            foreach (Meal meal in meals)
            {
                WriteLine($"Name: {meal.MealName}");
                WriteLine("Ingredients:");
                foreach (string ingredient in meal.MealIngredients.Split(","))
                {
                    WriteLine($"- {ingredient}");
                }
                WriteLine();
            }
        }
    }

    private static void PlanMealDialog()
    {
        List<string> weekdays = new() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        List<string> meals;
        string category;
        Plan plan;
        List<Plan> plans = new();
        foreach (string weekday in weekdays)
        {
            WriteLine(weekday);
            category = "breakfast";
            meals = GetMealsByCategory(category).Select(m => m.MealName).ToList();
            string breakfast = GetMealFromList(meals, category, weekday);
            category = "lunch";
            meals = GetMealsByCategory(category).Select(m => m.MealName).ToList();
            string lunch = GetMealFromList(meals, category, weekday);
            category = "dinner";
            meals = GetMealsByCategory(category).Select(m => m.MealName).ToList();
            string dinner = GetMealFromList(meals, category, weekday);
            plan = new Plan(weekday, breakfast, lunch, dinner);
            plans.Add(plan);
            WriteLine($"Yeah! We planned the meals for {weekday}.");
        }
        // overwrite existing plans before adding new ones (only planning for one week)
        ClearPlans();

        int affected = AddPlans(plans);
        if (affected == -1)
        {
            WriteLine("Failed to add plans to the database.");
        }
        else
        {
            WriteLine($"Added {affected} plans to the database.");
        }
        PrintPlans(plans);
    }

    private static string GetMealFromList(List<string> meals, string category, string weekday)
    {
        bool isVerifiedOption = false;
        string option = "";
        foreach (string meal in meals)
        {
            WriteLine($"- {meal}");
        }
        WriteLine($"Choose the {category} for {weekday} from the list above:");
        while (!isVerifiedOption)
        {
            option = ReadLine()!;
            if (meals.Contains(option))
            {
                isVerifiedOption = true;
            }
            else
            {
                WriteLine("This meal doesn't exist. Choose a meal from the list above.");
            }
        }
        return option;
    }

    private static void PrintPlans(List<Plan> plans)
    {
        foreach (Plan plan in plans)
        {
            WriteLine($"{plan.Day}:");
            WriteLine($"Breakfast: {plan.Breakfast}");
            WriteLine($"Lunch: {plan.Lunch}");
            WriteLine($"Dinner: {plan.Dinner}");
            WriteLine();
        }
    }

    private static void SavePlanDialog()
    {
        if (!PlanExists())
        {
            WriteLine("Unable to save. Please plan your meals first.");
            return;
        }
        List<Ingredient> ingredients = GetIngredientsForPlan();
        List<string> uniqueIngredients = RemoveDuplicatesFromIngredients(ingredients);
        WriteLine("Input a filename:");
        string? filename = ReadLine();
        if (string.IsNullOrEmpty(filename))
        {
            filename = "plan.txt";
        }
        SavePlanToFile(filename, uniqueIngredients);
        WriteLine("Saved!");
    }

    private static bool PlanExists()
    {
        return GetAllPlans().Count > 0;
    }

    private static List<string> RemoveDuplicatesFromIngredients(List<Ingredient> ingredients)
    {
        Dictionary<string, int> frequency = new();
        foreach (Ingredient ingredient in ingredients)
        {
            if (frequency.ContainsKey(ingredient.IngredientName))
            {
                frequency[ingredient.IngredientName]++;
            }
            else
            {
                frequency[ingredient.IngredientName] = 1;
            }
        }

        List<string> uniqueIngredients = new();
        foreach (var kvp in frequency)
        {
            if (kvp.Value > 1)
            {
                uniqueIngredients.Add($"{kvp.Key} x{kvp.Value}");
            }
            else
            {
                uniqueIngredients.Add(kvp.Key);
            }
        }

        return uniqueIngredients;
    }

    private static void SavePlanToFile(string filename, List<string> uniqueIngredients)
    {
        using StreamWriter file = new(filename);
        foreach (string ingredient in uniqueIngredients)
        {
            file.WriteLine(ingredient);
        }
    }
}