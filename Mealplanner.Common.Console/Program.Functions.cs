using System.Text.RegularExpressions; // Regex
using Mealplanner.Net;

partial class Program
{
    private static readonly Regex validMealCategory = new("breakfast|lunch|dinner");
    private static readonly Regex validMealName = new("[a-zA-Z ]+");
    private static readonly Regex validIngredients = new("([a-zA-Z]+,? ?)+(?<!,)(?<! )");


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
            if (mealCategory is not null && validMealCategory.IsMatch(mealCategory))
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
            if (mealName is not null && validMealName.IsMatch(mealName))
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
            if (ingredients is not null && validIngredients.IsMatch(IngredientArrayToString(ingredients!)))
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
}