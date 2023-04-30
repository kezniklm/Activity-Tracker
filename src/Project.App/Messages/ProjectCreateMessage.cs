namespace Project.App.Messages;

public record ProjectCreateMessage
{
    public required Guid ProjectId { get; init; }
}
