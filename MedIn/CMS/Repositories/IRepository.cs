using System.Data.Entity;
using System.Linq;
using System.Reflection;
using DB.Entities;

namespace CMS.Repositories
{
	public interface IRepository<TEntity, out TDbContext>
		where TDbContext : DbContext, new()
		where TEntity : class, IEntity
	{
		IQueryable<TEntity> All();
		IQueryable<TEntity> All(string lang);
		TEntity GetByPrimaryKey(int id);
		TDbContext DataContext
		{
			get;
		}
		PropertyInfo GetPrimaryKeyInfo();
		void AddObject(TEntity entity);
		void Save();
		void Delete(TEntity entity);
		//IQueryable<TEntity> AllByForeignKey(string foreignKeyName, object foreignKeyValue);
		//TEntity GetByAlias(string alias);
	}
}
