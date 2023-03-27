namespace Project.BL.Models;
public record UserProjectListModel : ModelBase
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }

    public static UserProjectListModel Empty => new()
    {
        Id = Guid.Empty,
        ProjectId = Guid.Empty,
        UserId = Guid.Empty,
    };
}
