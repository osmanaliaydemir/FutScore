using FutScore.Application.DTOs.Stadium;
using FutScore.Domain;
using FutScore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutScore.Application.Interfaces
{
    public interface IStadiumService
    {
        Task<ProcessResult> AddStadiumAsync(StadiumDto stadiumDto);
        Task<ProcessResult> UpdateStadiumAsync(StadiumDto stadiumDto);
        Task<ProcessResult> DeleteStadiumAsync(int id);
        Task<IEnumerable<StadiumDto>> GetAllStadiumsAsync();
        Task<StadiumDto> GetStadiumByIdAsync(int id);
    }
}
