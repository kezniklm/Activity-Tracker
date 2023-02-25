namespace Project.DAL.Entities;

public class UserEntity : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhotoUri { get; set; }
    public ICollection<ActivityUserListEntity> ActivitiesUsers { get; set; }
    public ICollection<UserListEntity> Projects { get; set; }
}