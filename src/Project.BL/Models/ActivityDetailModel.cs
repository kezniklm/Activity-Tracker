using Project.DAL.Entities;

namespace Project.BL.Models;

public record ActivityDetailModel : ModelBase
{
    public required string ActivityType { get; set; }
    public string? Description { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required string UserName { get; set; }
    public required string UserSurname { get; set; }
    public required Guid UserId { get; set; }
    public Guid? ProjectId { get; set; }

    public static ActivityDetailModel Empty => new()
    {
        Id = Guid.Empty,
        ActivityType = string.Empty,
        Description = string.Empty,
        Start = DateTime.MinValue,
        End = DateTime.MinValue,
        UserName = string.Empty,
        UserSurname = string.Empty,
        UserId = Guid.Empty,
        ProjectId = Guid.Empty
    };
}
