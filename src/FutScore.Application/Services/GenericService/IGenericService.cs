using FutScore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutScore.Application.Services.GenericService
{
    public interface IGenericService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<ProcessResult> AddAsync(T entitiy);
        Task<ProcessResult> UpdateAsync(T entitiy);
        Task<T> GetByIdAsync(Guid id);
        Task<ProcessResult> DeleteByIdAsync(Guid id);
    }

}
