namespace Kanban.Models
{
    public class Story()
    {
        public required int StoryId { get; set; }
        public required User Owner { get; set; }
        public required int OwnerId { get; set; }
        public required Board Board { get; set; }
        public required int BoardId { get; set; }
        public List<User> Assignees { get; set; } = [];
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Status? Status { get; set; }
        public int? StatusId { get; set; }
    }
}
