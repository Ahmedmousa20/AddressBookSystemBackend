using AddressBook.BLL.Specifications;
using AddressBook.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.BLL.Interfaces
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GeAllAsync();
        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GeAllWithSpecAsync(ISpecification<T> spec);
        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
    }
}
