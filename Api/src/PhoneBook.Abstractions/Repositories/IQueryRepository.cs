using PhoneBook.Abstractions.Model;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace PhoneBook.Abstractions.Repositories
{
      public interface IQueryRepository<T, TId> where T : IQueryModel
    {
        Task<T> LoadModelAsync(TId modelId);
        Task<IEnumerable<T>> FindModelsAsync(List<SearchParameter> searchParameters);
        Task<TId> SaveModelAsync(T model);
    }
}

