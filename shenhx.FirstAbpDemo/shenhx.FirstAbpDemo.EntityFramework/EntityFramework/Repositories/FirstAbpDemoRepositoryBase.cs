using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace shenhx.FirstAbpDemo.EntityFramework.Repositories
{
    public abstract class FirstAbpDemoRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<FirstAbpDemoDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected FirstAbpDemoRepositoryBase(IDbContextProvider<FirstAbpDemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class FirstAbpDemoRepositoryBase<TEntity> : FirstAbpDemoRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected FirstAbpDemoRepositoryBase(IDbContextProvider<FirstAbpDemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
