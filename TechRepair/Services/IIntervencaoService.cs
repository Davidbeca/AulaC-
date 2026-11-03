using TechRepair.Models;
using TechRepair.ViewModels;

namespace TechRepair.Services;

public interface IIntervencaoService
{
    Task<List<IntervencaoViewModel>> GetByReparacaoAsync(int reparacaoId);
    Task<IntervencaoViewModel?> GetByIdAsync(int id);
    Task<int> CreateAsync(IntervencaoCreateViewModel model);
    Task<bool> UpdateAsync(IntervencaoEditViewModel model);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> ReparacaoExistsAsync(int reparacaoId);
    Task<decimal> GetCustoTotalByReparacaoAsync(int reparacaoId);
}