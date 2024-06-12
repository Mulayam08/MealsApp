using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealApp.ApiProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MealApp.Models;
using MealApp.Services; 
using Acr.UserDialogs;

namespace MealApp.BusinessCode
{
    internal class BuisnessCode : IBusinessCode
    {
        private readonly HttpClient _httpClient;
        IApiProvider _apiProvider;
        private readonly JsonSerializerSettings _serializerSettings;
        NetworkAccess accessType;

        public BuisnessCode(IApiProvider apiProvider)
        {
            try
            {
                //To initialize service providers...
                accessType = Connectivity.Current.NetworkAccess;
                _apiProvider = apiProvider;
                HttpClientHandler handler = new HttpClientHandler();
                _serializerSettings = new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    NullValueHandling = NullValueHandling.Ignore
                };
                _serializerSettings.Converters.Add(new StringEnumConverter());

                _httpClient = new HttpClient(handler);
                TimeSpan ts = TimeSpan.FromMilliseconds(100000);
                _httpClient.Timeout = ts;
            }
            catch (Exception ex)
            { }
        }

        #region  Apis Definations Methods

        #region Category Section
        /// <summary>
        /// To get list of all categories
        /// </summary>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<CategoryModel> GetAllCategrory(Action<object> success, Action<object> failed)
        {
            CategoryModel resmodel = new CategoryModel();
            try
            {
                var url = string.Format("{0}categories.php", WebServiceDetails.BaseUri);
                var result = _apiProvider.Get<CategoryModel>(url, null);
                CategoryModel objres = null;
                dynamic obj = "";
                if (result.IsSuccessful != false)
                {
                    objres = JsonConvert.DeserializeObject<CategoryModel>(result.RawResult);
                    success.Invoke(objres);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    failed.Invoke(obj);
                }
            }
            catch (Exception exception)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Something went wrong!  Please try again.");
            }
            return resmodel;
        }

        #endregion


        #region Meal Section 
        /// <summary>
        /// TODO : To Call Meals List api and pass category id to it.
        /// </summary>
        /// <param name="meal"></param>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<MealsModel> GetAllMeals(string meal, Action<object> success, Action<object> failed)
        {
            MealsModel resmodel = new MealsModel();
            try
            {
                var url = string.Format("{0}filter.php?c=" + meal, WebServiceDetails.BaseUri);
                var result = _apiProvider.Get<MealsModel>(url, null);
                MealsModel objres = null;
                dynamic obj = "";
                if (result.IsSuccessful != false)
                {
                    objres = JsonConvert.DeserializeObject<MealsModel>(result.RawResult);
                    success.Invoke(objres);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    failed.Invoke(obj);
                }
            }
            catch (Exception exception)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Something went wrong!  Please try again.");
            }
            return resmodel;
        }

        /// <summary>
        /// To get all meal intructions
        /// </summary>
        /// <param name="mealId"></param>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<MealsDetailModel> GetMealDetails(string mealId, Action<object> success, Action<object> failed)
        {
            MealsDetailModel resmodel = new MealsDetailModel();
            try
            {
                var url = string.Format("{0}lookup.php?i=" + mealId, WebServiceDetails.BaseUri);
                var result = _apiProvider.Get<MealsDetailModel>(url, null);
                MealsDetailModel objres = null;
                dynamic obj = "";
                if (result.IsSuccessful != false)
                {
                    objres = JsonConvert.DeserializeObject<MealsDetailModel>(result.RawResult);
                    success.Invoke(objres);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    failed.Invoke(obj);
                }
            }
            catch (Exception exception)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Something went wrong!  Please try again.");
            }
            return resmodel;
        }

        #endregion

        #endregion
    }
}
