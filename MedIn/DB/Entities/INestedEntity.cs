using System.Data.Entity.Core.Objects.DataClasses;

namespace DB.Entities
{
    public interface INestedEntity<TEntity> : IPlainTreeItem
        where TEntity : class, IEntity
    {
        EntityCollection<TEntity> Children { get; set; }
    }
}
