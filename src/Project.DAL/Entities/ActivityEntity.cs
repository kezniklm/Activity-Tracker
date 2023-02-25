namespace Project.DAL.Entities
{
    public class ActivityEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}