using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public partial class Plan
{
    [Key]
    public int PlanId { get; set; }

    [Required]
    [StringLength(50)]
    public string Day { get; set; }

    [Required]
    [StringLength(50)]
    public string Breakfast { get; set; }

    [Required]
    [StringLength(50)]
    public string Lunch { get; set; }

    [Required]
    [StringLength(50)]
    public string Dinner { get; set; }

    public Plan(string day, string breakfast, string lunch, string dinner)
    {
        this.Day = day;
        this.Breakfast = breakfast;
        this.Lunch = lunch;
        this.Dinner = dinner;
    }
}