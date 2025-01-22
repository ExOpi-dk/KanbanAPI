using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kanban.Models
{
    public class Story : Dto
    {
        public string? Description { get; set; }
        public int OwnerId { get; set;  }
        [JsonIgnore]
        public User? Owner { get; set; }
        public int BoardId { get; set;  }
        [JsonIgnore]
        public Board? Board { get; set; }
        [NotMapped]
        public List<int> AssigneeIds { get { return Assignees.Select(a => a.Id).ToList(); } }
        [JsonIgnore]
        public List<User> Assignees { get; set; } = [];
        public int? StatusId { get; set; }
        [JsonIgnore]
        public Status? Status { get; set; }
    }
}
