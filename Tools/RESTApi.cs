using Newtonsoft.Json;
using RestSharp;


namespace Tools
{
    public class RESTApi
    {
        private Enums.RequestSource _rs = Enums.RequestSource.Portal;

        public T GetResponse<T>(string tokenId, string sirketId, string controllerName, string actionName, string values)
        {
            var val = GetResponse(tokenId, sirketId, controllerName, actionName, values);

            if (string.IsNullOrEmpty(val)) return Activator.CreateInstance<T>();

            return JsonConvert.DeserializeObject<T>(val, JSONConvertOptions.DeserializeSettings);
        }

        public T GetResponse<T>(string tokenId, string sirketId, string controllerName, Enums.CRUD actionName, string values) => GetResponse<T>(tokenId, sirketId, controllerName, actionName.ToString(), values);

        public string GetResponse(string tokenId, string sirketId, string controllerName, Enums.CRUD actionName, string values) => GetResponse(tokenId, sirketId, controllerName, actionName.ToString(), values);

        public string GetResponse(string tokenId, string sirketId, string controllerName, string actionName, string values) => GetResponse(controllerName, actionName, new Dictionary<string, string> { { "tokenId", tokenId }, { "sirketId", sirketId } }, values);

        public T GetResponse<T>(string controllerName, string actionName, Dictionary<string, string> parameters, string values)
        {
            var val = GetResponse(controllerName, actionName, parameters, values);

            if (string.IsNullOrEmpty(val)) return Activator.CreateInstance<T>();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(val, JSONConvertOptions.DeserializeSettings);
        }
        public string GetResponse(string controllerName, string actionName, Dictionary<string, string> parameters, string values)
        {
            RestClient client = new RestClient(GlobalValues.GlobalConstants.apiURL);

            var query = string.Format("/{0}/{1}", controllerName, actionName);

            var i = 0;

            foreach (var item in parameters.Keys)
            {
                if (i == 0)
                {
                    query += "?";
                }
                else { query += "&"; }
                query += string.Format("{0}={1}", item, parameters[item]);

                i++;
            }

            var request = new RestRequest(query.ToString(), Method.Post);

            request.AddHeader("accept", "application/json");
            request.AddHeader("rs", _rs.ToString());

            request.AddJsonBody(JsonConvert.SerializeObject(values));

            RestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return response.Content;
            }
            else
            {
                var val = response.Content.GetValue("Message");
                throw new Exception(val == null ? response.Content : val.ToString());
            }
        }
    }
}
