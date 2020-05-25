using System;
namespace apisam.entities
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class NotificationMessage
    {
        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("notification")]
        public NotificationModel Notification { get; set; }
    }

    public partial class NotificationModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("sound")]
        public string Sound { get; set; }

        [JsonProperty("click_action")]
        public string ClickAction { get; set; }
    }

    public partial class NotificationMessage
    {
        public static NotificationMessage FromJson(string json) => JsonConvert.DeserializeObject<NotificationMessage>(json, entities.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this NotificationMessage self) => JsonConvert.SerializeObject(self, entities.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
