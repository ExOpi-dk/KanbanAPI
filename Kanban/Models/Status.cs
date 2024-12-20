namespace Kanban.Models
{
    public class Status
    {
        public required int StatusId { get; set; }
        public required string Name { get; set; }
        public List<Story> Stories { get; set; } = [];
    }
}
