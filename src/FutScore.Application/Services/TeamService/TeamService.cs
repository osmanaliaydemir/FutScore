using AutoMapper;
using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Player;
using FutScore.Application.DTOs.Team;
using FutScore.Domain;
using Microsoft.EntityFrameworkCore;

namespace FutScore.Application.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TeamService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeamDetailDto> GetTeamDetailAsync(Guid id)
        {
            var team = await _context.Teams
                .Include(t => t.League)
                .Include(t => t.Players)
                .Include(t => t.HomeMatches)
                .Include(t => t.AwayMatches)
                .FirstOrDefaultAsync(t => t.Id == id);

            return _mapper.Map<TeamDetailDto>(team);
        }

        public async Task<List<TeamListDto>> GetTeamListAsync(TeamFilterDto filter)
        {
            var query = _context.Teams
                .Include(t => t.League)
                .AsQueryable();

            // Apply filters
            if (filter.LeagueId.HasValue)
                query = query.Where(t => t.LeagueId == filter.LeagueId);

            if (!string.IsNullOrEmpty(filter.Country))
                query = query.Where(t => t.League.Country == filter.Country);

            var teams = await query.ToListAsync();
            return _mapper.Map<List<TeamListDto>>(teams);
        }

        public async Task<List<TeamDto>> GetTeamsByLeagueAsync(Guid leagueId)
        {
            var teams = await _context.Teams
                .Where(t => t.LeagueId == leagueId)
                .OrderBy(t => t.Name)
                .ToListAsync();

            return _mapper.Map<List<TeamDto>>(teams);
        }

        public async Task<List<PlayerDto>> GetTeamPlayersAsync(Guid teamId)
        {
            var players = await _context.Players
                .Where(p => p.TeamId == teamId)
                .OrderBy(p => p.Name)
                .ToListAsync();

            return _mapper.Map<List<PlayerDto>>(players);
        }

        public async Task<List<MatchDto>> GetTeamMatchesAsync(Guid teamId)
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

        public async Task<TeamStatsDto> GetTeamStatsAsync(Guid teamId)
        {
            var team = await _context.Teams
                .Include(t => t.HomeMatches)
                .Include(t => t.AwayMatches)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            if (team == null)
                return null;

            var stats = new TeamStatsDto
            {
                TotalMatches = team.HomeMatches.Count + team.AwayMatches.Count,
                Wins = team.HomeMatches.Count(m => m.IsCompleted && m.HomeTeamScore > m.AwayTeamScore) +
                      team.AwayMatches.Count(m => m.IsCompleted && m.AwayTeamScore > m.HomeTeamScore),
                Draws = team.HomeMatches.Count(m => m.IsCompleted && m.HomeTeamScore == m.AwayTeamScore) +
                       team.AwayMatches.Count(m => m.IsCompleted && m.HomeTeamScore == m.AwayTeamScore),
                Losses = team.HomeMatches.Count(m => m.IsCompleted && m.HomeTeamScore < m.AwayTeamScore) +
                        team.AwayMatches.Count(m => m.IsCompleted && m.AwayTeamScore < m.HomeTeamScore),
                GoalsScored = team.HomeMatches.Where(m => m.IsCompleted).Sum(m => m.HomeTeamScore ?? 0) +
                             team.AwayMatches.Where(m => m.IsCompleted).Sum(m => m.AwayTeamScore ?? 0),
                GoalsConceded = team.HomeMatches.Where(m => m.IsCompleted).Sum(m => m.AwayTeamScore ?? 0) +
                               team.AwayMatches.Where(m => m.IsCompleted).Sum(m => m.HomeTeamScore ?? 0)
            };

            return stats;
        }

        public async Task<bool> AddPlayerToTeamAsync(Guid teamId, Guid playerId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            var player = await _context.Players.FindAsync(playerId);

            if (team == null || player == null) return false;

            player.TeamId = teamId;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemovePlayerFromTeamAsync(Guid teamId, Guid playerId)
        {
            var player = await _context.Players
                .FirstOrDefaultAsync(p => p.Id == playerId && p.TeamId == teamId);

            if (player == null) return false;

            player.TeamId = teamId;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateTeamStatsAsync(Guid teamId)
        {
            var team = await _context.Teams
                .Include(t => t.HomeMatches)
                .Include(t => t.AwayMatches)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            if (team == null) return false;

            // Update team statistics based on matches
            // This is a placeholder for actual implementation
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<TeamDto>> GetTopTeamsAsync(int count)
        {
            var teams = await _context.Teams
                .Include(t => t.HomeMatches)
                .Include(t => t.AwayMatches)
                .OrderByDescending(t => t.HomeMatches.Count(m => m.IsCompleted && m.HomeTeamScore > m.AwayTeamScore) +
                                      t.AwayMatches.Count(m => m.IsCompleted && m.AwayTeamScore > m.HomeTeamScore))
                .Take(count)
                .ToListAsync();

            return _mapper.Map<List<TeamDto>>(teams);
        }

        public async Task<List<TeamSeasonDto>> GetTeamSeasonsAsync(Guid teamId)
        {
            var seasons = await _context.TeamSeasons
                .Where(ts => ts.TeamId == teamId)
                .ToListAsync();

            return _mapper.Map<List<TeamSeasonDto>>(seasons);
        }

        public Task<List<TeamDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProcessResult> AddAsync(TeamDto entitiy)
        {
            throw new NotImplementedException();
        }

        public Task<ProcessResult> UpdateAsync(TeamDto entitiy)
        {
            throw new NotImplementedException();
        }

        public Task<TeamDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ProcessResult> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
} 