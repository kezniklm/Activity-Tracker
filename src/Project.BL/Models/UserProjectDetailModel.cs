namespace Project.BL.Models;

public record UserProjectDetailModel : ModelBase
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }

    public static UserProjectDetailModel Empty =>
        new() { Id = Guid.Empty, ProjectId = Guid.Empty, UserId = Guid.Empty };
}
