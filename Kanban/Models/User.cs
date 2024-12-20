namespace Kanban.Models
{
    public class User
    {
        public required int UserId { get; set; }
        public required string Name { get; set; }
        public List<Board> OwnedBoards { get; set; } = [];
        public List<Story> OwnedStories { get; set; } = [];
        public List<Story> AssignedStories { get; set; } = [];
    }
}
