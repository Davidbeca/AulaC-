using TechRepair.Models;
using TechRepair.ViewModels;

namespace TechRepair.Services;

public interface IReparacaoService
{
    Task<List<ReparacaoViewModel>> GetAllAsync();
    Task<List<ReparacaoViewModel>> GetByEstadoAsync(int estado);
    Task<List<ReparacaoViewModel>> GetByTecnicoAsync(int tecnicoId);
    Task<ReparacaoViewModel?> GetByIdAsync(int id);
    Task<int> CreateAsync(ReparacaoCreateViewModel model);
    Task<bool> UpdateAsync(ReparacaoEditViewModel model);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> EquipamentoExistsAsync(int equipamentoId);
    Task<bool> TecnicoExistsAsync(int? tecnicoId);
    Task<bool> HasIntervencoesAsync(int id);
}