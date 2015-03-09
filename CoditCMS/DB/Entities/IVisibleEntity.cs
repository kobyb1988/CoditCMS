namespace DB.Entities
{
    public interface IVisibleEntity : IEntity
    {
        bool Visibility { get; set; }
    }
}
