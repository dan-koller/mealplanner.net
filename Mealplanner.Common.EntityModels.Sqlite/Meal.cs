using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mealplanner.Net;

public partial class Meal
{
    [Key]
    public int MealId { get; set; }

    [Required]
    [StringLength(50)]
    public string MealName { get; set; }

    [Required]
    [StringLength(50)]
    public string MealCategory { get; set; }

    [Required]
    [StringLength(100)]
    public string MealIngredients { get; set; }

    // Note, that the ingredients are stored as a string, separated by commas.
    // Please make sure to convert the string[] to a string and back to a string[]
    // when you use the data.
    public Meal(string MealName, string MealCategory, string MealIngredients)
    {
        this.MealName = MealName;
        this.MealCategory = MealCategory;
        this.MealIngredients = MealIngredients;
    }
}
