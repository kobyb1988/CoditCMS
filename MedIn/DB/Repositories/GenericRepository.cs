using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using DB.Entities;
using DB.Infrastructure;
using Libs.Services;

namespace DB.Repositories
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
    	private readonly IDatabaseFactory _db;
        private DbContext _context;

		public DbContext Context
        {
            get
            {
            	return _context ?? (_context = _db.Get());
            }
        }

		[DebuggerStepThrough]
    	protected GenericRepository(IDatabaseFactory db)
        {
        	_db = db;
        }

		private DbSet<TEntity> _sourceEntitySet;
		private IQueryable<TEntity> _entitySet;

	    protected string Lang()
	    {
			var lang = DependencyResolver.Current.GetService<ILocalizationProvider>();
		    return lang.GetLanguageName();
	    }

	    protected bool IsLocalizable()
	    {
		    return typeof (ILocalizableEntity).IsAssignableFrom(typeof (TEntity));
	    }

	    protected DbSet<TEntity> SourceEntitySet
	    {
		    get { return _sourceEntitySet ?? (_sourceEntitySet = Context.Set<TEntity>()); }
	    }

	    protected IQueryable<TEntity> EntitySet
        {
            get
            {
	            return _entitySet ?? (_entitySet = SourceEntitySet);//TODO IsLocalizable() ?
														            //TODO SourceEntitySet.Where("it.Lang=@lang", new ObjectParameter("lang", Lang())) :
            }
        }

        public IEnumerable<TEntity> All
        {
            get
            { 
                return EntitySet; 
            }
        }

	    protected TEntity SetLang(TEntity entity)
	    {
			if (IsLocalizable())
			{
				((ILocalizableEntity)entity).Lang = Lang();
			}
		    return entity;
	    }

	    public TEntity Create()
        {
			return SetLang(Activator.CreateInstance<TEntity>());
        }

        public TEntity GetById(int id)
        {
            return EntitySet.SingleOrDefault(x => x.Id == id);
        }

        public void Add(TEntity entity)
        {
			SourceEntitySet.Add(SetLang(entity));
        }

        public void Delete(TEntity entity)
        {
            SourceEntitySet.Remove(entity);
        }

    	public void DeleteById(int id)
    	{
    		Delete(GetById(id));
    	}
    }
}
