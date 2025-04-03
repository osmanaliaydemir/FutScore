using FutScore.Application.Interfaces;
using FutScore.Domain.Entities;
using FutScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FutScore.Application.Services
{
    public class StandingService : BaseService<Standing>, IStandingService
    {
        public StandingService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Standing>> GetStandingsBySeasonAsync(int seasonId)
        {
            return await _dbSet
                .Where(s => s.SeasonId == seasonId)
                .Include(s => s.Team)
                .OrderBy(s => s.Points)
                .ThenBy(s => s.GoalDifference)
                .ThenBy(s => s.GoalsFor)
                .ToListAsync();
        }

        public async Task<Standing> GetTeamStandingAsync(int seasonId, int teamId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(s => s.SeasonId == seasonId && s.TeamId == teamId);
        }

        public async Task UpdateStandingsAfterMatchAsync(int matchId)
        {
            var match = await _context.Matches
                .Include(m => m.Season)
                .FirstOrDefaultAsync(m => m.Id == matchId);

            //if (match == null || !match.IsFinished || !match.HomeScore.HasValue || !match.AwayScore.HasValue)
            //    return;

            //var homeStanding = await GetTeamStandingAsync(match.SeasonId, match.HomeTeamId);
            //var awayStanding = await GetTeamStandingAsync(match.SeasonId, match.AwayTeamId);

            //if (homeStanding == null || awayStanding == null)
            //    return;

            //// Update home team standing
            //homeStanding.GoalsFor += match.HomeScore.Value;
            //homeStanding.GoalsAgainst += match.AwayScore.Value;
            
            //// Update away team standing
            //awayStanding.GoalsFor += match.AwayScore.Value;
            //awayStanding.GoalsAgainst += match.HomeScore.Value;

            //if (match.HomeScore > match.AwayScore)
            //{
            //    homeStanding.Points += 3;
            //    homeStanding.Wins += 1;
            //    awayStanding.Losses += 1;
            //}
            //else if (match.HomeScore < match.AwayScore)
            //{
            //    awayStanding.Points += 3;
            //    awayStanding.Wins += 1;
            //    homeStanding.Losses += 1;
            //}
            //else
            //{
            //    homeStanding.Points += 1;
            //    awayStanding.Points += 1;
            //    homeStanding.Draws += 1;
            //    awayStanding.Draws += 1;
            //}

            await _context.SaveChangesAsync();
        }

        public async Task InitializeSeasonStandingsAsync(int seasonId)
        {
            var teams = await _context.SeasonTeams
                .Where(st => st.SeasonId == seasonId)
                .Select(st => st.TeamId)
                .ToListAsync();

            foreach (var teamId in teams)
            {
                var standing = new Standing
                {
                    SeasonId = seasonId,
                    TeamId = teamId,
                    Points = 0,
                    Wins = 0,
                    Draws = 0,
                    Losses = 0,
                    GoalsFor = 0,
                    GoalsAgainst = 0
                };

                await _dbSet.AddAsync(standing);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RecalculateSeasonStandingsAsync(int seasonId)
        {
            // Reset all standings
            var standings = await _dbSet
                .Where(s => s.SeasonId == seasonId)
                .ToListAsync();

            foreach (var standing in standings)
            {
                standing.Points = 0;
                standing.Wins = 0;
                standing.Draws = 0;
                standing.Losses = 0;
                standing.GoalsFor = 0;
                standing.GoalsAgainst = 0;
            }

            // Get all completed matches
            //var matches = await _context.Matches
            //    .Where(m => m.SeasonId == seasonId && m.IsFinished && m.HomeScore.HasValue && m.AwayScore.HasValue)
            //    .OrderBy(m => m.MatchDate)
            //    .ToListAsync();

            //// Recalculate standings based on matches
            //foreach (var match in matches)
            //{
            //    await UpdateStandingsAfterMatchAsync(match.Id);
            //}
        }
    }
} 