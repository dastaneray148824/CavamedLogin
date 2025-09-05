using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Formats.Asn1;
using System;


namespace Tools
{
    public sealed class TimeSpanConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Null)
            {
                return null;
            }
            if (token.Type == JTokenType.TimeSpan)
            {
                return token.ToObject<TimeSpan>();
            }
            if (token.Type == JTokenType.String)
            {
                var date = DateTime.Parse(token.ToObject<DateTime>().ToString());
                return new TimeSpan(date.Hour, date.Minute, date.Second);
            }
            if (token.Type == JTokenType.Date)
            {
                var date = DateTime.Parse(token.ToObject<DateTime>().ToString());
                return new TimeSpan(date.Hour, date.Minute, date.Second);
            }

            throw new JsonSerializationException("Unexpected token type: " +
            token.Type.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    //public sealed class EnumConverter : Newtonsoft.Json.Converters.StringEnumConverter
    //{
    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        if (existingValue == null || int.Parse(existingValue.ToString()) == 0)
    //            return existingValue;

    //        return base.ReadJson(reader, objectType, existingValue, serializer);
    //    }
    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        base.WriteJson(writer, value, serializer);
    //    }
    //}

    public sealed class EnumConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (existingValue == null && objectType == typeof(Enums.ParaBirimi)) return Enum.Parse(objectType, Enums.ParaBirimi.TRY.ToString());
            try
            {
                var val = JToken.Load(reader).Value<string>();

                if (string.IsNullOrEmpty(val) || val == "0")
                    return existingValue;

                return Enum.Parse(objectType, val);
            }
            catch (Exception ex)
            {
                if (objectType == typeof(Enums.ParaBirimi)) return Enum.Parse(objectType, Enums.ParaBirimi.TRY.ToString());
                // TODO: buraya error
               // Shell.Current.CurrentPage.DisplayAlert(Localization.Name.ConvName("Hata"), ex.Message, "OK");
                throw;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class JSONConvertOptions
    {
        public static JsonSerializerSettings DeserializeSettings => new() { Converters = new List<JsonConverter> { new Tools.TimeSpanConverter(), new Tools.EnumConverter() } };
    }

    public static class JsonExtention
    {
        public static object GetValue(this string value, string propName)
        {
            JObject o = JObject.Parse(value);

            var prop = o[propName];

            if (prop != null)
                return o[propName];

            return null;
        }

        public static string ToJson(this Dictionary<string, object> value)
        {
            var o = new JObject();

            foreach (var item in value.Keys)
            {
                o.Add(new JProperty(item, value[item]));
            }

            return o.ToString(Formatting.None);
        }

        public static string ToJson(this IDictionary<string, object> value)
        {
            var o = new JObject();

            foreach (var item in value.Keys)
            {
                o.Add(new JProperty(item, value[item]));
            }

            return o.ToString(Formatting.None);
        }
        public static Dictionary<string, object> JsonToDictionary(string json)
        {
            var dictionary = new Dictionary<string, object>();
            JObject jsonObject = JObject.Parse(json);

            foreach (var property in jsonObject.Properties())
            {
                dictionary[property.Name] = property.Value.ToObject<object>();
            }

            return dictionary;
        }
    }
}
