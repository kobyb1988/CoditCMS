using System.Data.Entity;
using System.Diagnostics;

namespace DB.Infrastructure.Implementation
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IDatabaseFactory _databaseFactory;
		private DbContext _dataContext;

		[DebuggerStepThrough]
		public UnitOfWork(IDatabaseFactory databaseFactory)
		{
			_databaseFactory = databaseFactory;
		}

        protected DbContext DataContext
		{
			get { return _dataContext ?? (_dataContext = _databaseFactory.Get()); }
		}

		public void Commit()
		{
			DataContext.SaveChanges();
		}
	}
}
