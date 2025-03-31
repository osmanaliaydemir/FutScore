using AutoMapper;
using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.MatchEvent;
using FutScore.Domain;
using FutScore.Domain.Entities;
using FutScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FutScore.Application.Services.MatchService
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MatchService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MatchDetailDto> GetMatchDetailAsync(Guid id)
        {
            var match = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.League)
                .Include(m => m.MatchEvents)
                .FirstOrDefaultAsync(m => m.Id == id);

            return _mapper.Map<MatchDetailDto>(match);
        }

        public async Task<List<MatchListDto>> GetMatchListAsync(MatchFilterDto filter)
        {
            var query = _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.League)
                .AsQueryable();

            // Apply filters
            //if (filter.LeagueId.HasValue)
            //    query = query.Where(m => m.LeagueId == filter.LeagueId);

            //if (filter.TeamId.HasValue)
            //    query = query.Where(m => m.HomeTeamId == filter.TeamId || m.AwayTeamId == filter.TeamId);

            //if (filter.StartDate.HasValue)
            //    query = query.Where(m => m.MatchDate >= filter.StartDate);

            //if (filter.EndDate.HasValue)
            //    query = query.Where(m => m.MatchDate <= filter.EndDate);

            //if (!string.IsNullOrEmpty(filter.Status))
            //    query = query.Where(m => m.Status == filter.Status);

            var matches = await query.ToListAsync();
            return _mapper.Map<List<MatchListDto>>(matches);
        }

        public async Task<List<MatchDto>> GetUpcomingMatchesAsync()
        {
            var matches = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.League)
                .Where(m => m.MatchDate > DateTime.UtcNow && m.MatchStatus == "Scheduled")
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            return _mapper.Map<List<MatchDto>>(matches);
        }

        public async Task<List<MatchDto>> GetLiveMatchesAsync()
        {
            var matches = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.League)
                .Where(m => m.MatchStatus == "Live")
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            return _mapper.Map<List<MatchDto>>(matches);
        }

        public async Task<List<MatchDto>> GetFinishedMatchesAsync()
        {
            var matches = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.League)
                .Where(m => m.MatchStatus == "Finished")
                .OrderByDescending(m => m.MatchDate)
                .ToListAsync();

            return _mapper.Map<List<MatchDto>>(matches);
        }

        public async Task<List<MatchDto>> GetMatchesByLeagueAsync(Guid leagueId)
        {
            var matches = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => m.LeagueId == leagueId)
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            return _mapper.Map<List<MatchDto>>(matches);
        }

        public async Task<List<MatchDto>> GetMatchesByTeamAsync(Guid teamId)
        {
            var matches = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.League)
                .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            return _mapper.Map<List<MatchDto>>(matches);
        }

        public async Task<bool> UpdateMatchScoreAsync(Guid matchId, int? homeTeamScore, int? awayTeamScore)
        {
            var match = await _context.Matches.FindAsync(matchId);
            if (match == null) return false;

            match.HomeTeamScore = homeTeamScore;
            match.AwayTeamScore = awayTeamScore;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateMatchStatusAsync(Guid matchId, string status)
        {
            var match = await _context.Matches.FindAsync(matchId);
            if (match == null) return false;

            match.MatchStatus = status;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<MatchEventDto>> GetMatchEventsAsync(Guid matchId)
        {
            var events = await _context.MatchEvents
                .Where(e => e.MatchId == matchId)
                .OrderBy(e => e.Minute)
                .ToListAsync();

            return _mapper.Map<List<MatchEventDto>>(events);
        }

        public async Task<bool> AddMatchEventAsync(Guid matchId, MatchEventDto matchEvent)
        {
            var match = await _context.Matches.FindAsync(matchId);
            if (match == null) return false;

            var newEvent = _mapper.Map<MatchEvent>(matchEvent);
            newEvent.MatchId = matchId;

            _context.MatchEvents.Add(newEvent);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateMatchEventAsync(Guid matchId, Guid eventId, MatchEventDto matchEvent)
        {
            var existingEvent = await _context.MatchEvents
                .FirstOrDefaultAsync(e => e.Id == eventId && e.MatchId == matchId);

            if (existingEvent == null) return false;

            _mapper.Map(matchEvent, existingEvent);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteMatchEventAsync(Guid matchId, Guid eventId)
        {
            var matchEvent = await _context.MatchEvents
                .FirstOrDefaultAsync(e => e.Id == eventId && e.MatchId == matchId);

            if (matchEvent == null) return false;

            _context.MatchEvents.Remove(matchEvent);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<List<MatchDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProcessResult> AddAsync(MatchDto entitiy)
        {
            throw new NotImplementedException();
        }

        public Task<ProcessResult> UpdateAsync(MatchDto entitiy)
        {
            throw new NotImplementedException();
        }

        public Task<MatchDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ProcessResult> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
} 