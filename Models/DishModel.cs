#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsDishes.Models;

public class Dish
{
    [Key]
    public int DishId {get; set;}
    [Required]
    [MinLength(2,ErrorMessage ="Dish Name must be 2 characters or more")]
    [MaxLength(30,ErrorMessage ="Dish Name must be 30 characters or less")]
    public string Name { get; set;}

    [Required(ErrorMessage ="Tastiness is required!")]
    [Range(1,5)]
    public int Tastiness { get; set;}
    [Required(ErrorMessage ="Calories is required!")]
    [Range(1, int.MaxValue)]
    public int Calories { get; set;}
    [Required(ErrorMessage ="Please pick a chef!")]
    //Set Foreign Key
    public int ChefId {get;set;}
    //Nav Prop
    public Chef? Chef {get;set;}
    public DateTime CreatedAt { get; set;} = DateTime.Now;
    public DateTime UpdatedAt { get; set;} = DateTime.Now; 
}