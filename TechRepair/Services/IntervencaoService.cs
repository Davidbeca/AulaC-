using Microsoft.EntityFrameworkCore;
using TechRepair.Data;
using TechRepair.Models;
using TechRepair.ViewModels;

namespace TechRepair.Services;

public class IntervencaoService : IIntervencaoService
{
    private readonly ApplicationDbContext _context;

    public IntervencaoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<IntervencaoViewModel>> GetByReparacaoAsync(int reparacaoId)
    {
        return await _context.Intervencoes
            .Where(i => i.ReparacaoId == reparacaoId)
            .Select(i => new IntervencaoViewModel
            {
                Id = i.Id,
                ReparacaoId = i.ReparacaoId,
                ReparacaoDescricao = $"Reparação #{i.ReparacaoId}",
                DataIntervencao = i.DataIntervencao,
                DescricaoTrabalho = i.DescricaoTrabalho,
                PecasUtilizadas = i.PecasUtilizadas,
                TempoTrabalho = i.TempoTrabalho,
                CustoMaoObra = i.CustoMaoObra,
                CustoPecas = i.CustoPecas,
                CustoTotal = i.CustoMaoObra + i.CustoPecas,
                Observacoes = i.Observacoes
            })
            .OrderByDescending(i => i.DataIntervencao)
            .ToListAsync();
    }

    public async Task<IntervencaoViewModel?> GetByIdAsync(int id)
    {
        return await _context.Intervencoes
            .Where(i => i.Id == id)
            .Select(i => new IntervencaoViewModel
            {
                Id = i.Id,
                ReparacaoId = i.ReparacaoId,
                ReparacaoDescricao = $"Reparação #{i.ReparacaoId}",
                DataIntervencao = i.DataIntervencao,
                DescricaoTrabalho = i.DescricaoTrabalho,
                PecasUtilizadas = i.PecasUtilizadas,
                TempoTrabalho = i.TempoTrabalho,
                CustoMaoObra = i.CustoMaoObra,
                CustoPecas = i.CustoPecas,
                CustoTotal = i.CustoMaoObra + i.CustoPecas,
                Observacoes = i.Observacoes
            })
            .FirstOrDefaultAsync();
    }

    public async Task<int> CreateAsync(IntervencaoCreateViewModel model)
    {
        var intervencao = new Intervencao
        {
            ReparacaoId = model.ReparacaoId,
            DataIntervencao = model.DataIntervencao,
            DescricaoTrabalho = model.DescricaoTrabalho,
            PecasUtilizadas = model.PecasUtilizadas,
            TempoTrabalho = model.TempoTrabalho,
            CustoMaoObra = model.CustoMaoObra,
            CustoPecas = model.CustoPecas,
            Observacoes = model.Observacoes
        };
        _context.Intervencoes.Add(intervencao);
        await _context.SaveChangesAsync();
        return intervencao.Id;
    }

    public async Task<bool> UpdateAsync(IntervencaoEditViewModel model)
    {
        var intervencao = await _context.Intervencoes.FindAsync(model.Id);
        if (intervencao == null) return false;

        intervencao.ReparacaoId = model.ReparacaoId;
        intervencao.DataIntervencao = model.DataIntervencao;
        intervencao.DescricaoTrabalho = model.DescricaoTrabalho;
        intervencao.PecasUtilizadas = model.PecasUtilizadas;
        intervencao.TempoTrabalho = model.TempoTrabalho;
        intervencao.CustoMaoObra = model.CustoMaoObra;
        intervencao.CustoPecas = model.CustoPecas;
        intervencao.Observacoes = model.Observacoes;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var intervencao = await _context.Intervencoes.FindAsync(id);
        if (intervencao == null) return false;

        _context.Intervencoes.Remove(intervencao);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Intervencoes.AnyAsync(i => i.Id == id);
    }

    public async Task<bool> ReparacaoExistsAsync(int reparacaoId)
    {
        return await _context.Reparacoes.AnyAsync(r => r.Id == reparacaoId);
    }

    public async Task<decimal> GetCustoTotalByReparacaoAsync(int reparacaoId)
    {
        return await _context.Intervencoes
            .Where(i => i.ReparacaoId == reparacaoId)
            .SumAsync(i => i.CustoMaoObra + i.CustoPecas);
    }
}