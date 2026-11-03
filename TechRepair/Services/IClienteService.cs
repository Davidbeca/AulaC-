using TechRepair.Models;
using TechRepair.ViewModels;

namespace TechRepair.Services;

public interface IClienteService
{
    Task<List<ClienteViewModel>> GetAllAsync();
    Task<ClienteViewModel?> GetByIdAsync(int id);
    Task<int> CreateAsync(ClienteCreateViewModel model);
    Task<bool> UpdateAsync(ClienteEditViewModel model);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> NifExistsAsync(string nif, int? excludeId = null);
    Task<bool> EmailExistsAsync(string email, int? excludeId = null);
    Task<bool> HasEquipamentosAsync(int id);
}