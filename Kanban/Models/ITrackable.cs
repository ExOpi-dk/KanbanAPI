namespace Kanban.Models
{
    public interface ITrackable
    {
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
