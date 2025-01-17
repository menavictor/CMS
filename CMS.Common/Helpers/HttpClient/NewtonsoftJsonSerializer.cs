﻿using System.Diagnostics.CodeAnalysis;
using System.IO;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;

namespace CMS.Common.Helpers.HttpClient
{
    [ExcludeFromCodeCoverage]
    public class NewtonsoftJsonSerializer : ISerializer//, IDeserializer
    {
        private readonly JsonSerializer _serializer;

        public NewtonsoftJsonSerializer(JsonSerializer serializer)
        {
            this._serializer = serializer;
        }

        public ContentType ContentType
        {
            get => "application/json";
// Probably used for Serialization?
            set { }
        }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    _serializer.Serialize(jsonTextWriter, obj);

                    return stringWriter.ToString();
                }
            }
        }

        //public T Deserialize<T>(IRestResponse response)
        //{
        //    var content = response.Content;

        //    using (var stringReader = new StringReader(content))
        //    {
        //        using (var jsonTextReader = new JsonTextReader(stringReader))
        //        {
        //            return serializer.Deserialize<T>(jsonTextReader);
        //        }
        //    }
        //}

        public static NewtonsoftJsonSerializer Default =>
            new NewtonsoftJsonSerializer(new JsonSerializer()
            {
                NullValueHandling = NullValueHandling.Ignore,
            });
    }
}
