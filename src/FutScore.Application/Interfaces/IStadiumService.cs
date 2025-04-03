using FutScore.Application.DTOs.Stadium;
using FutScore.Domain;
using FutScore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutScore.Application.Interfaces
{
    public interface IStadiumService
    {
        Task<ProcessResult> AddStadiumAsync(CreateStadiumDto stadiumDto);
        Task<ProcessResult> UpdateStadiumAsync(UpdateStadiumDto stadiumDto);
        Task<ProcessResult> DeleteStadiumAsync(int id);
        Task<IEnumerable<StadiumDto>> GetAllStadiumsAsync();
        Task<StadiumDto> GetStadiumByIdAsync(int id);
    }
}
