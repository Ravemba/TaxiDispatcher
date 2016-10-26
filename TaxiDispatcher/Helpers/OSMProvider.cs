using System.Collections.Generic;
using System.Threading.Tasks;
using MapControl;
using RestSharp;
using TaxiDispatcher.Properties;
using Newtonsoft.Json.Linq;
using TaxiDispatcher.Core;
using System;

namespace TaxiDispatcher.Helpers
{
    public class OSMProvider
    {
        #region Constructor and private fields
        private readonly RestClient clientOSM;
        private readonly RestClient clientMQuest;
        private static readonly OSMProvider instance = new OSMProvider();
        private static string COUNTRY = "Украина";

        public OSMProvider()
        {
            clientOSM = new RestClient(Resources.URL_OSM_API);
            clientMQuest = new RestClient(Resources.UPL_MAPQUEST_API);
        }
        #endregion

        #region Send helper
        //private Task<IRestResponse<T>> SendRequest<T>(string resource, Method method)
        //{
        //    return SendRequest<T>(resource, method, null, null);
        //}

        //private Task<IRestResponse<T>> SendRequest<T>(string resource, Method method, Dictionary<string, object> parameters)
        //{
        //    return SendRequest<T>(resource, method, parameters, null);
        //}

        //private async Task<IRestResponse<T>> SendRequestOSM<T>(string resource, Method method, Dictionary<string, object> parameters, Dictionary<string, string> headers)
        //{
        //    var request = new RestRequest(resource, method);
        //    if (parameters != null && parameters.Count > 0)
        //    {
        //        foreach (KeyValuePair<string, object> keyValuePair in parameters)
        //        {
        //            //request.AddBody(keyValuePair.Value);
        //            request.AddParameter(keyValuePair.Key, keyValuePair.Value);
        //        }
        //    }
        //    //            string jsonToSend = SimpleJson.SerializeObject(parameters);

        //    if (headers != null && headers.Count > 0)
        //    {
        //        foreach (KeyValuePair<string, string> keyValuePair in headers)
        //        {
        //            request.AddHeader(keyValuePair.Key, keyValuePair.Value);
        //        }
        //    }

        //    //request.AddHeader("Accept", "application/json");
        //    //request.Parameters.Clear();
        //    //request.AddParameter("application/json", parameters, ParameterType.RequestBody);

        //    return await clientOSM.ExecuteTaskAsync<T>(request);
        //}

        //private async Task<IRestResponse<T>> SendRequestObj<T>(string resource, Method method, object obj, Dictionary<string, string> headers)
        //{
        //    var request = new RestRequest(resource, method);


        //    if (headers != null && headers.Count > 0)
        //    {
        //        foreach (KeyValuePair<string, string> keyValuePair in headers)
        //        {
        //            request.AddHeader(keyValuePair.Key, keyValuePair.Value);
        //        }
        //    }


        //    string jsonToSend = SimpleJson.SerializeObject(obj);

        //    request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
        //    request.RequestFormat = DataFormat.Json;

        //    return await clientOSM.ExecuteTaskAsync<T>(request);
        //}

        //private Task<IRestResponse> SendRequest(string resource, Method method)
        //{
        //    return SendRequest(resource, method, null, null);
        //}

        //private Task<IRestResponse> SendRequest(string resource, Method method, Dictionary<string, object> parameters)
        //{
        //    return SendRequest(resource, method, parameters, null);
        //}

        //private async Task<IRestResponse> SendRequest(string resource, Method method, Dictionary<string, object> parameters, Dictionary<string, string> headers)
        //{
        //    var request = new RestRequest(resource, method);
        //    if (parameters != null && parameters.Count > 0)
        //    {
        //        foreach (KeyValuePair<string, object> keyValuePair in parameters)
        //        {
        //            request.AddParameter(keyValuePair.Key, keyValuePair.Value);
        //        }
        //    }
        //    if (headers != null && headers.Count > 0)
        //    {
        //        foreach (KeyValuePair<string, string> keyValuePair in headers)
        //        {
        //            request.AddHeader(keyValuePair.Key, keyValuePair.Value);
        //        }
        //    }
        //    return await clientOSM.ExecuteTaskAsync(request);
        //}

        private async Task<IRestResponse<T>> SendRequestMQuest<T>(string resource, Method method, Dictionary<string, string> parameters, Dictionary<string, string> headers)
        {
            var request = new RestRequest(resource, method);
            if (parameters != null && parameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> keyValuePair in parameters)
                {
                    request.AddQueryParameter(keyValuePair.Key, keyValuePair.Value);
                }
            }
            if (headers != null && headers.Count > 0)
            {
                foreach (KeyValuePair<string, string> keyValuePair in headers)
                {
                    request.AddHeader(keyValuePair.Key, keyValuePair.Value);
                }
            }
            return await clientMQuest.ExecuteTaskAsync<T>(request);
        }
        #endregion

        public static async Task<IRestResponse<JObject>> GetRoute(string from, string to)
        {
            var city = "Днепр";
            var locations = new JArray(getLocation(city, from), getLocation(city, to));
            //var locations = new JArray(from, to);
            var options = new MJson().Add("unit", "k").Add("locale", "ru_RU").ToString();

            var request = new Dictionary<string, string> {
                { "key", Resources.MAPQUEST_API_KEY },
                { "json", new MJson().Add("locations", locations ).ToString()},
                { "options", options },
            };
            //var response = instance.SendRequestMQuest(Resources.UPL_MAPQUEST_API, request, null);

            //var param = new Dictionary<string, string> {
            //    { "key", Resources.MAPQUEST_API_KEY },
            //    { "key", Resources.MAPQUEST_API_KEY },
            //};
            return await instance.SendRequestMQuest<JObject>(Resources.API_MQUEST_OPTIMROUT, Method.GET, request, null);
            //return JObject.Parse(res.Content);
        }

        public static async Task<List<Location>> GetTrafficPoints(string from, string to)
        {
            List<Location> rst = new List<Location>();

            var res = GetRoute(from, to);
            var re = await res;
            var ob = JObject.Parse(re.Content);
            var points = ob["route"]["legs"].First["maneuvers"];
            foreach (var item in points)
            {
                rst.Add(new Location(item["startPoint"]["lat"].ToObject<Double>(), item["startPoint"]["lng"].ToObject<Double>()));
            }
            return rst;
        }

        public static async Task<Double> GetDistance(string from, string to)
        {
            var res = GetRoute(from, to);
            var re = await res;
            var ob = JObject.Parse(re.Content);
            return ob["route"]["distance"].ToObject<Double>();
        }

        private static MJson getLocation(string city, string address)
        {
            return new MJson().Add("street", address).
                Add("city", city).Add("Country", COUNTRY);
        }
    }
}
