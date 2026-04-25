using System.Text.Json.Serialization;

namespace TripService.Domain.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status {  RESERVED , FINISHED , PLANNED , CANCELLED}
}
