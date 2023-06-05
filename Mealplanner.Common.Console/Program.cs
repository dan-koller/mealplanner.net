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
        case "plan":
            PlanMealDialog();
            break;
        case "save":
            SavePlanDialog();
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

/*
The functionality to delete a meal by its id is not implemented in the program, but it is possible to do so.
The following code snippet shows how to delete a meal by its id:
*/
// int latestId = GetLatestMealId();
// WriteLine($"Latest meal id is {latestId}.");
// int affected = DeleteMealById(latestId);
