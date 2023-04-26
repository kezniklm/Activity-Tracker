namespace Project.DAL.Entities;

public record ProjectEntity : IEntity
{
    public required string Name { get; set; }
    public ICollection<ActivityEntity> Activities { get; init; } = new List<ActivityEntity>();
    public ICollection<UserProjectEntity> Users { get; init; } = new List<UserProjectEntity>();
    public required Guid Id { get; set; }
}
