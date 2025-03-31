using AutoMapper;
using FutScore.Application.DTOs.Role;
using FutScore.Domain;
using FutScore.Domain.Entities;
using FutScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FutScore.Application.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RoleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<RoleDto>> GetAllAsync()
        {
            return await _context.Roles
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<RoleDto> GetByIdAsync(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return null;

            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt
            };
        }

        public async Task<RoleDto> GetByNameAsync(string name)
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == name);

            if (role == null)
                return null;

            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt
            };
        }

        public async Task<RoleDto> CreateAsync(RoleCreateDto roleDto)
        {
            var role = new Role
            {
                Name = roleDto.Name,
                Description = roleDto.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt
            };
        }

        public async Task<RoleDto> UpdateAsync(RoleUpdateDto roleDto)
        {
            var role = await _context.Roles.FindAsync(roleDto.Id);
            if (role == null)
                throw new KeyNotFoundException($"Role with ID {roleDto.Id} not found.");

            role.Name = roleDto.Name;
            role.Description = roleDto.Description;
            role.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                throw new KeyNotFoundException($"Role with ID {id} not found.");

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.Roles.AnyAsync(r => r.Name == name);
        }

        public async Task<List<RoleDto>> GetRolesByUserIdAsync(Guid userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => new RoleDto
                {
                    Id = ur.Role.Id,
                    Name = ur.Role.Name,
                    Description = ur.Role.Description,
                    CreatedAt = ur.Role.CreatedAt,
                    UpdatedAt = ur.Role.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task AssignRolesToUserAsync(Guid userId, List<string> roleNames)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            var roles = await _context.Roles
                .Where(r => roleNames.Contains(r.Name))
                .ToListAsync();

            foreach (var role in roles)
            {
                if (!user.UserRoles.Any(ur => ur.RoleId == role.Id))
                {
                    user.UserRoles.Add(new UserRole
                    {
                        UserId = userId,
                        RoleId = role.Id,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveRolesFromUserAsync(Guid userId, List<string> roleNames)
        {
            var userRoles = await _context.UserRoles
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == userId && roleNames.Contains(ur.Role.Name))
                .ToListAsync();

            _context.UserRoles.RemoveRange(userRoles);
            await _context.SaveChangesAsync();
        }
    }
} 