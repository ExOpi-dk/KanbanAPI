using System.Text.Json.Serialization;

namespace Kanban.Models
{
    public class Board : Dto
    {
        public int OwnerId { get; set;  }
        [JsonIgnore]
        public User? Owner { get; set; }
    }
}
