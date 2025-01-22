using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Kanban.Models
{
    public abstract class Dto : ITrackable
    {
        [ReadOnly(true)]
        public int Id { get; }

        public string? Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [ReadOnly(true)]
        public DateTime Created { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [ReadOnly(true)]
        public DateTime LastUpdated { get; set; }
    }
}
