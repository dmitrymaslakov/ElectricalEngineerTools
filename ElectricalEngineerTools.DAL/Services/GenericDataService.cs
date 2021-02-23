using ElectricalEngineerTools.DAL.ContextDb;
using ElectricalEngineerTools.DAL.Entities;
using ElectricalEngineerTools.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectricalEngineerTools.DAL.Services
{
    public class GenericDataService<T> : IDataService<T> where T : class, IEntity
    {
        private readonly IDesignTimeDbContextFactory<ElectricsContext> _contextFactory;
        //private readonly NonQueryDataService<T> _nonQueryDataService;

        public GenericDataService(IDesignTimeDbContextFactory<ElectricsContext> contextFactory)
        {
            _contextFactory = contextFactory;
            //_nonQueryDataService = new NonQueryDataService<T>(contextFactory);
        }

        public async Task<T> Create(T entity)
        {
            using (var context = _contextFactory.CreateDbContext(null))
            {
                var createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }

            //return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(Guid id)
        {
            using (var context = _contextFactory.CreateDbContext(null))
            {
                var entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id.CompareTo(id) == 0);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }

            //return await _nonQueryDataService.Delete(id);
        }

        public async Task<T> Get(Guid id)
        {
            using(var context = _contextFactory.CreateDbContext(null))
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id.CompareTo(id) == 0);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using(var context = _contextFactory.CreateDbContext(null))
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

        public async Task<T> Update(Guid id, T entity)
        {
            using (var context = _contextFactory.CreateDbContext(null))
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }

            //return await _nonQueryDataService.Update(id, entity);
        }
    }
}
