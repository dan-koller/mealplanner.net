using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mealplanner.Net;

public partial class Ingredient
{
    [Key]
    public int IngredientId { get; set; }

    [Required]
    [StringLength(50)]
    public string IngredientName { get; set; }

    public Ingredient(string IngredientName)
    {
        this.IngredientName = IngredientName;
    }

    // Need to override Equals() and GetHashCode() to make sure that
    // the list of ingredients does not contain duplicates.
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Ingredient otherIngredient = (Ingredient)obj;
        return string.Equals(IngredientName, otherIngredient.IngredientName);
    }

    public override int GetHashCode()
    {
        return IngredientName.GetHashCode();
    }
}