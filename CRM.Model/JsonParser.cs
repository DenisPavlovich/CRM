using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CRM.Data.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
        public MethodType Method;
        public Organization Obj;
        public JsonParser(Organization obj, MethodType method)
        {
            Obj = (Organization) obj;
            Method = method;
        }
        public static string Serialize(JsonParser jp)
        {
            return JsonConvert.SerializeObject(jp);
        }
        public static JsonParser Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<JsonParser>(json);
        }

        public static string BasicSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T BasicDeserialize<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
