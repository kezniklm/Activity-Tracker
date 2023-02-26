namespace Project.DAL.Entities
{
    public record ActivityEntity : IEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ActivityType { get; set; }
        public required DateTime Start { get; set; }
        public required DateTime End { get; set; }
        public required UserEntity User { get; set; }
        public ProjectEntity? Project { get; set; }
    }
}