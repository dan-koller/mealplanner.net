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
    public string day { get; set; }

    [Required]
    [StringLength(50)]
    public string breakfast { get; set; }

    [Required]
    [StringLength(50)]
    public string lunch { get; set; }

    [Required]
    [StringLength(50)]
    public string dinner { get; set; }

    public Plan(string day, string breakfast, string lunch, string dinner)
    {
        this.day = day;
        this.breakfast = breakfast;
        this.lunch = lunch;
        this.dinner = dinner;
    }
}