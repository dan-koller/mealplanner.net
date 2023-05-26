partial class Program
{
    // This function converts a string of ingredients to a string array.
    static string IngredientArrayToString(string[] ingredients)
    {
        return string.Join(",", ingredients);
    }

    static string[] RemoveWhitespace(string[] ingredients)
    {
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredients[i] = ingredients[i].Trim();
        }
        return ingredients;
    }
}