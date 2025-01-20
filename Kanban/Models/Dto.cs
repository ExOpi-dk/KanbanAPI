namespace Kanban.Models
{
    public class Dto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Created { get; }
        public DateTime LastUpdated { get; }
    }
}
