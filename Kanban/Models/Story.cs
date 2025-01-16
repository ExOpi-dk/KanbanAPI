using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kanban.Models
{
    public class Story()
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required int OwnerId { get; set; }
        [JsonIgnore]
        public User? Owner { get; set; }
        public required int BoardId { get; set; }
        [JsonIgnore]
        public Board? Board { get; set; }
        [NotMapped]
        public List<int> AssigneeIds { get { return Assignees.Select(a => a.Id).ToList(); } }
        [JsonIgnore]
        public List<User> Assignees { get; set; } = [];
        public int? StatusId { get; set; }
        [JsonIgnore]
        public Status? Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
