namespace DB.Entities
{
    public interface ISortableEntity : IEntity
    {
        int Sort { get; set; }
    }
}
