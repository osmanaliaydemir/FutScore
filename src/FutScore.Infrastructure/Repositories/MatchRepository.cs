using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using FutScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FutScore.Infrastructure.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly ApplicationDbContext _context;

        public MatchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Match>> GetAllAsync()
        {
            return await _context.Matches.ToListAsync();
        }

        public async Task<Match> GetByIdAsync(int id)
        {
            return await _context.Matches.FindAsync(id);
        }

        public async Task<Match> FindAsync(Expression<Func<Match, bool>> predicate)
        {
            return await _context.Matches.FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(Match entity)
        {
            await _context.Matches.AddAsync(entity);
        }

        public void Update(Match entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Match entity)
        {
            _context.Matches.Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Matches.AnyAsync(e => e.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Match> GetMatchWithTeamsAsync(int matchId)
        {
            return await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .FirstOrDefaultAsync(m => m.Id == matchId);
        }

 

        public async Task<IEnumerable<Match>> GetMatchesBySeasonAsync(int seasonId)
        {
            return await _context.Matches
                .Where(m => m.SeasonId == seasonId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Match>> GetMatchesByTeamAsync(int teamId)
        {
            return await _context.Matches
                .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Match>> GetUpcomingMatchesAsync(int? days = null)
        {
            var query = _context.Matches.Where(m => m.Status == MatchStatus.Scheduled);
            
            if (days.HasValue)
            {
                var date = DateTime.Now.AddDays(days.Value);
                query = query.Where(m => m.MatchDate <= date);
            }

            return await query.OrderBy(m => m.MatchDate).ToListAsync();
        }

        public async Task<IEnumerable<Match>> GetCompletedMatchesAsync(int? days = null)
        {
            var query = _context.Matches.Where(m => m.Status == MatchStatus.Completed);
            
            if (days.HasValue)
            {
                var date = DateTime.Now.AddDays(-days.Value);
                query = query.Where(m => m.MatchDate >= date);
            }

            return await query.OrderByDescending(m => m.MatchDate).ToListAsync();
        }

        public async Task<IEnumerable<Match>> GetLiveMatchesAsync()
        {
            return await _context.Matches
                .Where(m => m.Status == MatchStatus.Live)
                .ToListAsync();
        }

        public async Task UpdateMatchScoreAsync(int matchId, int homeScore, int awayScore)
        {
            var match = await GetByIdAsync(matchId);
            if (match != null)
            {
                match.HomeTeamScore = homeScore;
                match.AwayTeamScore = awayScore;
                Update(match);
                await SaveChangesAsync();
            }
        }

        public async Task FinishMatchAsync(int matchId)
        {
            var match = await GetByIdAsync(matchId);
            if (match != null)
            {
                match.Status = MatchStatus.Completed;
                match.EndTime = DateTime.Now;
                Update(match);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> IsMatchTimeValidAsync(int seasonId, DateTime matchDate)
        {
            var season = await _context.Seasons.FindAsync(seasonId);
            if (season == null) return false;

            return matchDate >= season.StartDate && matchDate <= season.EndDate;
        }

        public async Task<bool> AreTeamsAvailableAsync(int homeTeamId, int awayTeamId, int seasonId, DateTime matchDate)
        {
            return !await _context.Matches
                .AnyAsync(m => m.SeasonId == seasonId &&
                              m.MatchDate.Date == matchDate.Date &&
                              (m.HomeTeamId == homeTeamId || m.HomeTeamId == awayTeamId ||
                               m.AwayTeamId == homeTeamId || m.AwayTeamId == awayTeamId));
        }

        public async Task<bool> HasOverlappingMatchesAsync(int stadiumId, DateTime matchDate, int? excludeId = null)
        {
            var query = _context.Matches
                .Where(m => m.StadiumId == stadiumId &&
                           m.MatchDate.Date == matchDate.Date);

            if (excludeId.HasValue)
            {
                query = query.Where(m => m.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
} 