using System.Text.Json.Serialization;

namespace Kanban.Models
{
    public class Board
    {
        public int Id { get; }
        public string? Name { get; set; }
        public int OwnerId { get; set;  }
        [JsonIgnore]
        public User? Owner { get; set; }
        public DateTime Created { get; }
        public DateTime? LastUpdated { get; }
    }
}
