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
        public List<int> AssigneeIds { get; set; } = [];
        [JsonIgnore]
        public List<User> Assignees { get; set; } = [];
        public int? StatusId { get; set; }
        [JsonIgnore]
        public Status? Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
