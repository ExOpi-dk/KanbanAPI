namespace Kanban.Models
{
    public class Board
    {
        public required int BoardId { get; set; }
        public required string Name { get; set; }
        public required User Owner { get; set; }
        public required int OwnerId { get; set; }
        public List<Story> Stories { get; set; } = [];
    }
}
