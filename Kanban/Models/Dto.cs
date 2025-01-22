using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Kanban.Models
{
    public class Dto : ITrackable
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [ReadOnly(true)]
        public DateTime Created { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [ReadOnly(true)]
        public DateTime LastUpdated { get; set; }
    }
}
