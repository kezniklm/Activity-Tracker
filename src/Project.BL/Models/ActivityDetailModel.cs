using Project.DAL.Entities;

namespace Project.BL.Models;

public record ActivityDetailModel : ModelBase
{
    public required string ActivityType { get; set; }
    public string? Description { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required UserEntity User { get; set; }
    public ProjectEntity? Project { get; set; }

    public static ActivityDetailModel Empty => new()
    {
        Id = Guid.Empty,
        ActivityType = string.Empty,
        Description = string.Empty,
        Start = DateTime.MinValue,
        End = DateTime.MinValue,
        User = new UserEntity(){Name = string.Empty, Surname = string.Empty}
    };
}
