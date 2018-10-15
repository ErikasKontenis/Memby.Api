using Newtonsoft.Json;

namespace Memby.Models
{
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public string MessageCode { get; }

        public ValidationError(string field, string message)
        {
            var parsedMessage = message.Split("##;");
            Field = field != string.Empty ? field : null;
            Message = parsedMessage[1];
            MessageCode = parsedMessage[0];
        }
    }
}
