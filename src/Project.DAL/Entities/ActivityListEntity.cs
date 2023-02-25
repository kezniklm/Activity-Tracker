namespace Project.DAL.Entities;

public class ActivityListEntity : IEntity
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public ProjectEntity ProjectEntity { get; set; }
    public Guid ActivityId { get; set; }
    public ActivityEntity ActivityEntity { get; set; }
}