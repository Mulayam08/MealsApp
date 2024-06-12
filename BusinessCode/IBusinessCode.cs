using MealApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealApp.BusinessCode
{
    public interface IBusinessCode
    {
        #region Apis Declarations  

        #region Meal Section
        Task<MealsDetailModel> GetMealDetails(string mealId, Action<object> success, Action<object> failed);
        Task<MealsModel> GetAllMeals(string meal, Action<object> success, Action<object> failed);
   
        #endregion

        #region Category Section
        Task<CategoryModel> GetAllCategrory(Action<object> success, Action<object> failed);
         
        #endregion 
        #endregion
    }
}
