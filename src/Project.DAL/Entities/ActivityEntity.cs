namespace Project.DAL.Entities;

public record ActivityEntity : IEntity
{
    public required string ActivityType { get; set; }
    public string? Description { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }

    public required Guid UserId { get; set; }
    public UserEntity? User { get; init; }
    public Guid? ProjectId { get; set; }
    public ProjectEntity? Project { get; init; }
    public required Guid Id { get; set; }
}
