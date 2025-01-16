using System.Text.Json.Serialization;

namespace Kanban.Models
{
    public class Board
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int OwnerId { get; set; }
        [JsonIgnore]
        public User? Owner { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
