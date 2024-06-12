using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealApp.Models
{ 
    public class MealsModel
    {
        public List<Meal> meals { get; set; }
    }
    public class Meal
    {
        public string strMeal { get; set; }
        public string strMealThumb { get; set; }
        public string idMeal { get; set; }
    } 
}
