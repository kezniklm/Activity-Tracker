namespace Project.App.Messages;

public record ActivityEditMessage
{
    public required Guid ActivityId { get; init; }
}
