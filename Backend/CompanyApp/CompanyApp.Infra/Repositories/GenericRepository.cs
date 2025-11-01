using CompanyApp.Domain.Repositories;
using CompanyApp.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Infra.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly PostgreSQLContext _context;
        private readonly DbSet<T> _DBSet;

        public GenericRepository(PostgreSQLContext context)
        {
            _context = context;
            _DBSet = _context.Set<T>();
        }

        public async Task<List<T>> FindAll(int take, int skip)
        {
            return await _DBSet
                .Take(take)
                .Skip(skip)
                .ToListAsync();
        }

        public async Task<T> Create(T entity)
        {
            _DBSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _DBSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T entity)
        {
            _DBSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
