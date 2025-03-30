using FutScore.Application.DTOs.Role;

namespace FutScore.Application.Services.RoleService
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAllAsync();
        Task<RoleDto> GetByIdAsync(Guid id);
        Task<RoleDto> GetByNameAsync(string name);
        Task<RoleDto> CreateAsync(RoleCreateDto role);
        Task<RoleDto> UpdateAsync(RoleUpdateDto role);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        Task<List<RoleDto>> GetRolesByUserIdAsync(Guid userId);
        Task AssignRolesToUserAsync(Guid userId, List<string> roleNames);
        Task RemoveRolesFromUserAsync(Guid userId, List<string> roleNames);
    }
} 