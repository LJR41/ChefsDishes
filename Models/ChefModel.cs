#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using AgeCalculator;
using AgeCalculator.Extensions;
namespace ChefsDishes.Models;

public class Chef

{
    [Key]
    public int ChefId {get;set;}
    [Required(ErrorMessage ="First name is required")]
    [MinLength(2,ErrorMessage ="First name must be at least 2 characters.")]
    public string FirstName {get;set;}
    [Required(ErrorMessage ="Last name is required")]
    [MinLength(2,ErrorMessage ="Last name must be at least 2 characters.")]
    public string LastName {get;set;}
    [Required(ErrorMessage ="Birthday is required")]
    [DataType(DataType.Date)]
    public DateTime Birthday {get;set;}

    //Nav Prop
    public List<Dish> Dishes {get;set;} = new();

    public DateTime CreatedAt {get;set;} = DateTime.Now;        
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    
    public int GetAge()
	{
		var birthday = Birthday;
	    var age = new Age(birthday, DateTime.Today);
    	return age.Years;
	}

}

