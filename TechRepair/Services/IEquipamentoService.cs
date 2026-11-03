using TechRepair.Models;
using TechRepair.ViewModels;

namespace TechRepair.Services;

public interface IEquipamentoService
{
    Task<List<EquipamentoViewModel>> GetAllAsync();
    Task<List<EquipamentoViewModel>> GetByClienteAsync(int clienteId);
    Task<EquipamentoViewModel?> GetByIdAsync(int id);
    Task<int> CreateAsync(EquipamentoCreateViewModel model);
    Task<bool> UpdateAsync(EquipamentoEditViewModel model);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> NumeroSerieExistsAsync(string numeroSerie, int? excludeId = null);
    Task<bool> ClienteExistsAsync(int clienteId);
    Task<bool> HasReparacoesAsync(int id);
}