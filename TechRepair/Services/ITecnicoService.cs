using TechRepair.Models;
using TechRepair.ViewModels;

namespace TechRepair.Services;

public interface ITecnicoService
{
    Task<List<TecnicoViewModel>> GetAllAsync();
    Task<TecnicoViewModel?> GetByIdAsync(int id);
    Task<int> CreateAsync(TecnicoCreateViewModel model);
    Task<bool> UpdateAsync(TecnicoEditViewModel model);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> EmailExistsAsync(string email, int? excludeId = null);
    Task<bool> HasReparacoesAsync(int id);
}