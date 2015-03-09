namespace DB.Infrastructure
{
	public interface IUnitOfWork
	{
		void Commit();
	}
}
