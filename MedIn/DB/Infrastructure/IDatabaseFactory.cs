using System.Data.Entity;

namespace DB.Infrastructure
{
	public interface IDatabaseFactory
	{
        DbContext Get();
	}
}
