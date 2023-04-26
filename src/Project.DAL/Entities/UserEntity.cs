namespace Project.DAL.Entities;

public record UserEntity : IEntity
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? PhotoUrl { get; set; }
    public ICollection<ActivityEntity> Activities { get; init; } = new List<ActivityEntity>();
    public ICollection<UserProjectEntity> Projects { get; init; } = new List<UserProjectEntity>();
    public required Guid Id { get; set; }
}
