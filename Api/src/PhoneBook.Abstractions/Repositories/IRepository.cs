using PhoneBook.Abstractions.Enums;
using PhoneBook.Abstractions.Model;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace PhoneBook.Abstractions.Repositories
{
    public interface IRepository<T, TId> where T : IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="filterType"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAggregatesAsync(List<SearchParameter> searchParameters, FilterType filterType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> LoadAggregateAsync(TId id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        Task<TId> SaveAggregateAsync(T aggregate);
    }
}
