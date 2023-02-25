namespace Project.DAL.Entities;

public class ActivityUserListEntity : IEntity
{
    public Guid ActivityId { get; set; }
    public ActivityEntity ActivityEntity { get; set; }
    public Guid UserId { get; set; }
    public UserEntity UserEntity { get; set; }
    public Guid Id { get; set; }
}