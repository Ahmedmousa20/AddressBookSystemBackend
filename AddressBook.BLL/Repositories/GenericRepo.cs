using AddressBook.BLL.Interfaces;
using AddressBook.BLL.Specifications;
using AddressBook.DAL.Data;
using AddressBook.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.BLL.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly DemoContext context;

        public GenericRepo(DemoContext context)
        {
            this.context = context;
        }
        public async Task<int> Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Update(T entity)
        {
            context.Set<T>().Update(entity);
            return await context.SaveChangesAsync();

        }

        public async Task<int> Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            return await context.SaveChangesAsync();

        }

        public async Task<IReadOnlyList<T>> GeAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>(), spec);
        }

        public async Task<IReadOnlyList<T>> GeAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }
        
      
    }
}
