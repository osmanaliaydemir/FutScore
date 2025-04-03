using FutScore.Application.DTOs.League;
using FutScore.Application.Interfaces;
using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using AutoMapper;
using System.Linq.Expressions;

namespace FutScore.Application.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly IMapper _mapper;

        public LeagueService(ILeagueRepository leagueRepository, IMapper mapper)
        {
            _leagueRepository = leagueRepository;
            _mapper = mapper;
        }

        public async Task<League> GetByIdAsync(int id)
        {
            return await _leagueRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<League>> GetAllAsync()
        {
            return await _leagueRepository.GetAllAsync();
        }

        public async Task<IEnumerable<League>> FindAsync(Expression<Func<League, bool>> predicate)
        {

            var result = await _leagueRepository.FindAsync(predicate);
            return result != null ? new List<League> { result } : Enumerable.Empty<League>();
        }

        public async Task<League> AddAsync(League entity)
        {
            await _leagueRepository.AddAsync(entity);
            await _leagueRepository.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(League entity)
        {
            _leagueRepository.Update(entity);
            await _leagueRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var league = await _leagueRepository.GetByIdAsync(id);
            if (league == null)
                throw new KeyNotFoundException($"League with ID {id} not found.");

            _leagueRepository.Delete(league);
            await _leagueRepository.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _leagueRepository.ExistsAsync(id);
        }

        public async Task<League> GetLeagueWithSeasonsAsync(int leagueId)
        {
            return await _leagueRepository.GetLeagueWithSeasonsAsync(leagueId);
        }

        public async Task<IEnumerable<League>> GetLeaguesByCountryAsync(string country)
        {
            return await _leagueRepository.GetLeaguesByCountryAsync(country);
        }

        public async Task<bool> IsLeagueNameUniqueAsync(string name, int? excludeId = null)
        {
            return await _leagueRepository.IsLeagueNameUniqueAsync(name, excludeId);
        }
    }
} 