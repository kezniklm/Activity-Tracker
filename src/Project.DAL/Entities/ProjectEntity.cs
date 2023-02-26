namespace Project.DAL.Entities;

public record ProjectEntity : IEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
    public ICollection<UserProjectEntity> Users { get; set; } = new List<UserProjectEntity>();
}