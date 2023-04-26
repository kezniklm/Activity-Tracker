namespace Project.DAL.Entities;

public record UserProjectEntity : IEntity
{
    public required Guid ProjectId { get; set; }
    public ProjectEntity? Project { get; set; }
    public required Guid UserId { get; set; }
    public UserEntity? User { get; set; }
    public required Guid Id { get; set; }
}
