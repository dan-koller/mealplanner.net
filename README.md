# Meal Planner

This is a simple meal planner that I wrote to help me plan my meals for the week. It's a command line tool that
allows you to add meals to a database and then plan them for the week.

## Get started

### Requirements

Make sure you have the following installed on your machine:

-   .NET 7
-   SQLite 3

1. Clone the repo

```shell
git clone https://github.com/dan-koller/mealplanner.net.git
```

2. Restore the dependencies for each project

```shell
dotnet restore
```

3. Run the project

If you're using the dotnet CLI, make sure you are in the directory for the console project and run:

```shell
dotnet run
```

If you're using Visual Studio, make sure you have the console project set as the startup project and then run the
project.

## Usage

You can add new meals, show meals for a category, plan meals for the week, and create a shopping list.

### Add a new meal

```shell
What would you like to do (add, show, plan, save, exit)?
> add
Which meal do you want to add (breakfast, lunch, dinner)?
> lunch
Input the meal's name:
> salad
Input the ingredients:
> lettuce, tomato, onion, cheese, olives
The meal has been added!
```

### Show meals for a category

```shell
What would you like to do (add, show, plan, save, exit)?
> show
Which category do you want to print (breakfast, lunch, dinner)?
> breakfast
Category: breakfast
Name: oatmeal
Ingredients:
oats
milk
banana
peanut butter
```

### Plan meals for the week

```shell
What would you like to do (add, show, plan, save, exit)?
> plan
Monday
oatmeal
sandwich
scrambled eggs
yogurt
Choose the breakfast for Monday from the list above:
> yogurt
avocado egg salad
chicken salad
sushi
tomato salad
wraps
Choose the lunch for Monday from the list above:
> tomato salad
beef with broccoli
pesto chicken
pizza
ramen
tomato soup
Choose the dinner for Monday from the list above:
> spaghetti
This meal doesn’t exist. Choose a meal from the list above.
> ramen
Yeah! We planned the meals for Monday.

<... A bunch of other days ...>

Sunday
oatmeal
sandwich
scrambled eggs
yogurt
Choose the breakfast for Sunday from the list above:
> scrambled eggs
avocado egg salad
chicken salad
sushi
tomato salad
wraps
Choose the lunch for Sunday from the list above:
> tomato salad
beef with broccoli
pesto chicken
pizza
ramen
tomato soup
Choose the dinner for Sunday from the list above:
> beef with broccoli
Yeah! We planned the meals for Sunday.

Monday
Breakfast: yogurt
Lunch: tomato salad
Dinner: ramen

<... A bunch of other days ...>

Sunday
Breakfast: scrambled eggs
Lunch: tomato salad
Dinner: beef with broccoli
```

### Create a shopping list

```shell
What would you like to do (add, show, plan, save, exit)?
> save
Input a filename:
> shoppinglist.txt
Saved!
```

## Database setup

Make sure to have sqlite3 installed on your machine and available in your PATH. You can check if it's installed by
running:

```shell
sqlite3 --version
```

The database and tables will be created automatically when you run the application for the first time.

## Testing

The application is tested using xUnit. To run the tests, make sure you are in the directory for the test project and run:

```shell
dotnet test
```

Or if you're using Visual Studio, make sure you have the test project set as the startup project and then run the tests.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request if you have any ideas for improvements.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
