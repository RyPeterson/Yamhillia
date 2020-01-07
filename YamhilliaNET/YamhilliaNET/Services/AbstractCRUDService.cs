using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YamhilliaNET.Data;
using YamhilliaNET.Models;

namespace YamhilliaNET.Services
{
    public abstract class AbstractCRUDService<T>: CRUDService<T> where T: AbstractYamhilliaModel
    {
        protected readonly DbSet<T> _table;
        protected readonly ApplicationDbContext _db;

        protected AbstractCRUDService(ApplicationDbContext dbContext, DbSet<T> table) 
        {
            _db = dbContext;
            _table = table;
        }

        protected async Task Commit()
        {
            await _db.SaveChangesAsync();
        }

        protected void Nope(string param = "parameter")
        {
            throw new ArgumentException(param);
        }

        public async Task Delete(T model) 
        {
            _table.Remove(model);
            await Commit();
        }

        public virtual async Task<T> Create(T model)
        {
            if(model == null)
            {
                Nope("model");
            }
            /* 
                Docs suggest Npgsql doesn't actually set these via the computed.
                This is just to report history of the object, so its not 100% important that these be exact
            */
            var now = DateTime.UtcNow;
            model.CreatedAt = now;
            model.UpdatedAt = now;

            var trackedModel = _table.Add(model);
            await Commit();
            return trackedModel.Entity;
        }


        public virtual async Task<T> Get(long id)
        {
            return await _table.FindAsync(id);
        }

        public virtual async Task Delete(long id)
        {
            await Delete(await Get(id));
        }

        public virtual async Task<IEnumerable<T>> Get(GetOptions options)
        {
            IQueryable<T> query = _table;
            query = _Get(options, query);
            if(options.Limit.HasValue)
            {
                query = query.Take(options.Limit.Value);
            }
            return await query.ToListAsync();
        }

        protected abstract IQueryable<T> _Get(GetOptions options, IQueryable<T> query);
        
        public virtual async Task<T> Update(T model)
        {
            // See the Create method re: Npgsql
            model.UpdatedAt = DateTime.UtcNow;
            var updated = _table.Update(model);
            await Commit();
            return updated.Entity;
        }
    }
}