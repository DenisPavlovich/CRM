using System.Collections.Generic;
using CRM.Data.Dto;
using Newtonsoft.Json;

namespace CRM.Model
{
    public enum MethodType
    {
        AddOrganization,
        AddClient,
        EditClient,
        AppendPhoneNumberToClient,
        TakeManagerOwnerToClient
    }
    public class JsonParser
    {
        public MethodType method;
        public Dto obj;
        private JsonParser(Dto obj, MethodType method)
        {
            this.method = method;
            this.obj = obj;
        }
        public static string Serialize(Dto obj, MethodType method)
        {
            return JsonConvert.SerializeObject(new JsonParser(obj, method));
        }
        public static object[] Deserialize(string json)
        {
            JsonParser jp = JsonConvert.DeserializeObject<JsonParser>(json);
            object[] args = { jp.method, jp.obj };
            return args;
        }

        public static string BasicSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
