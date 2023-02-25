namespace Project.DAL.Entities;

public class ProjectEntity : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<ActivityListEntity> Activities { get; set; }
    public ICollection<UserListEntity> Users { get; set; }
}