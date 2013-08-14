using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace RecipeRepository
{
    public class DbRepositoryEF<T> : IRepository<T> where T : class
    {
        protected DbContext Context { get; set; }
        protected IDbSet<T> DbSet { get; set; }

        public DbRepositoryEF(DbContext contex)
        {
            if (contex == null)
            {
                throw new ArgumentException(
                    "An instance of DbContext is required to use this repository.", 
                    "context");
            }

            this.Context = contex;
            this.DbSet = contex.Set<T>();
        }

        public IQueryable<T> All()
        {
            return this.DbSet;
        }

        public T Get(int id)
        {
            return this.DbSet.Find(id);
        }

        public T Add(T item)
        {
            this.DbSet.Add(item);
            this.Context.SaveChanges();
            return item;
        }

        public void Delete(int id)
        {
            var entity = this.Get(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public void Delete(T item)
        {
            DbEntityEntry entry = this.Context.Entry(item);

            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(item);
                this.DbSet.Remove(item);
            }

            this.Context.SaveChanges();
        }

        public T Update(int id, T item)
        {
            DbEntityEntry entry = this.Context.Entry(item);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(item);
            }

            entry.State = EntityState.Modified;
            this.Context.SaveChanges();

            return item;
        }
    }
}
