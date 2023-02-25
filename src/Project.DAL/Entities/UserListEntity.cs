namespace Project.DAL.Entities;

public class UserListEntity : IEntity
{
    public Guid ProjectId { get; set; }
    public ProjectEntity ProjectEntity { get; set; }
    public Guid UserId { get; set; }
    public UserEntity UserEntity { get; set; }
    public Guid Id { get; set; }
}