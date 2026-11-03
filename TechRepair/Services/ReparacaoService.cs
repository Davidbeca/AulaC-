using Microsoft.EntityFrameworkCore;
using TechRepair.Data;
using TechRepair.Models;
using TechRepair.ViewModels;

namespace TechRepair.Services;

public class ReparacaoService : IReparacaoService
{
    private readonly ApplicationDbContext _context;

    public ReparacaoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReparacaoViewModel>> GetAllAsync()
    {
        return await _context.Reparacoes
            .Include(r => r.Equipamento).ThenInclude(e => e.Cliente)
            .Include(r => r.Tecnico)
            .Include(r => r.Intervencoes)
            .Select(r => new ReparacaoViewModel
            {
                Id = r.Id,
                EquipamentoId = r.EquipamentoId,
                EquipamentoDescricao = $"{r.Equipamento.Marca} {r.Equipamento.Modelo}",
                ClienteNome = r.Equipamento.Cliente.Nome,
                TecnicoId = r.TecnicoId,
                TecnicoNome = r.Tecnico != null ? r.Tecnico.Nome : string.Empty,
                DataEntrada = r.DataEntrada,
                DescricaoAvaria = r.DescricaoAvaria,
                Diagnostico = r.Diagnostico,
                Estado = (EstadoReparacaoViewModel)r.Estado,
                DataConclusao = r.DataConclusao,
                DataEntrega = r.DataEntrega,
                Observacoes = r.Observacoes,
                CustoTotal = r.Intervencoes.Sum(i => i.CustoMaoObra + i.CustoPecas),
                TotalIntervencoes = r.Intervencoes.Count
            })
            .OrderByDescending(r => r.DataEntrada)
            .ToListAsync();
    }

    public async Task<List<ReparacaoViewModel>> GetByEstadoAsync(int estado)
    {
        return await _context.Reparacoes
            .Include(r => r.Equipamento).ThenInclude(e => e.Cliente)
            .Include(r => r.Tecnico)
            .Include(r => r.Intervencoes)
        .Where(r => r.Estado == (EstadoReparacao)estado)
            .Select(r => new ReparacaoViewModel
            {
                Id = r.Id,
                EquipamentoId = r.EquipamentoId,
                EquipamentoDescricao = $"{r.Equipamento.Marca} {r.Equipamento.Modelo}",
                ClienteNome = r.Equipamento.Cliente.Nome,
                TecnicoId = r.TecnicoId,
                TecnicoNome = r.Tecnico != null ? r.Tecnico.Nome : string.Empty,
                DataEntrada = r.DataEntrada,
                DescricaoAvaria = r.DescricaoAvaria,
                Diagnostico = r.Diagnostico,
                Estado = (EstadoReparacaoViewModel)r.Estado,
                DataConclusao = r.DataConclusao,
                DataEntrega = r.DataEntrega,
                Observacoes = r.Observacoes,
                CustoTotal = r.Intervencoes.Sum(i => i.CustoMaoObra + i.CustoPecas),
                TotalIntervencoes = r.Intervencoes.Count
            })
            .OrderByDescending(r => r.DataEntrada)
            .ToListAsync();
    }

    public async Task<List<ReparacaoViewModel>> GetByTecnicoAsync(int tecnicoId)
    {
        return await _context.Reparacoes
            .Include(r => r.Equipamento).ThenInclude(e => e.Cliente)
            .Include(r => r.Tecnico)
            .Include(r => r.Intervencoes)
            .Where(r => r.TecnicoId == tecnicoId)
            .Select(r => new ReparacaoViewModel
            {
                Id = r.Id,
                EquipamentoId = r.EquipamentoId,
                EquipamentoDescricao = $"{r.Equipamento.Marca} {r.Equipamento.Modelo}",
                ClienteNome = r.Equipamento.Cliente.Nome,
                TecnicoId = r.TecnicoId,
                TecnicoNome = r.Tecnico != null ? r.Tecnico.Nome : string.Empty,
                DataEntrada = r.DataEntrada,
                DescricaoAvaria = r.DescricaoAvaria,
                Diagnostico = r.Diagnostico,
                Estado = (EstadoReparacaoViewModel)r.Estado,
                DataConclusao = r.DataConclusao,
                DataEntrega = r.DataEntrega,
                Observacoes = r.Observacoes,
                CustoTotal = r.Intervencoes.Sum(i => i.CustoMaoObra + i.CustoPecas),
                TotalIntervencoes = r.Intervencoes.Count
            })
            .OrderByDescending(r => r.DataEntrada)
            .ToListAsync();
    }

    public async Task<ReparacaoViewModel?> GetByIdAsync(int id)
    {
        return await _context.Reparacoes
            .Include(r => r.Equipamento).ThenInclude(e => e.Cliente)
            .Include(r => r.Tecnico)
            .Include(r => r.Intervencoes)
            .Where(r => r.Id == id)
            .Select(r => new ReparacaoViewModel
            {
                Id = r.Id,
                EquipamentoId = r.EquipamentoId,
                EquipamentoDescricao = $"{r.Equipamento.Marca} {r.Equipamento.Modelo}",
                ClienteNome = r.Equipamento.Cliente.Nome,
                TecnicoId = r.TecnicoId,
                TecnicoNome = r.Tecnico != null ? r.Tecnico.Nome : string.Empty,
                DataEntrada = r.DataEntrada,
                DescricaoAvaria = r.DescricaoAvaria,
                Diagnostico = r.Diagnostico,
                Estado = (EstadoReparacaoViewModel)r.Estado,
                DataConclusao = r.DataConclusao,
                DataEntrega = r.DataEntrega,
                Observacoes = r.Observacoes,
                CustoTotal = r.Intervencoes.Sum(i => i.CustoMaoObra + i.CustoPecas),
                TotalIntervencoes = r.Intervencoes.Count
            })
            .FirstOrDefaultAsync();
    }

    public async Task<int> CreateAsync(ReparacaoCreateViewModel model)
    {
        var reparacao = new Reparacao
        {
            EquipamentoId = model.EquipamentoId,
            TecnicoId = model.TecnicoId,
            DataEntrada = DateTime.Now,
            DescricaoAvaria = model.DescricaoAvaria,
            Diagnostico = model.Diagnostico,
            Estado = (EstadoReparacao)model.Estado,
            Observacoes = model.Observacoes
        };
        _context.Reparacoes.Add(reparacao);
        await _context.SaveChangesAsync();
        return reparacao.Id;
    }

    public async Task<bool> UpdateAsync(ReparacaoEditViewModel model)
    {
        var reparacao = await _context.Reparacoes.FindAsync(model.Id);
        if (reparacao == null) return false;

        reparacao.EquipamentoId = model.EquipamentoId;
        reparacao.TecnicoId = model.TecnicoId;
        reparacao.DescricaoAvaria = model.DescricaoAvaria;
        reparacao.Diagnostico = model.Diagnostico;
        reparacao.Estado = (EstadoReparacao)model.Estado;
        reparacao.DataConclusao = model.DataConclusao;
        reparacao.DataEntrega = model.DataEntrega;
        reparacao.Observacoes = model.Observacoes;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var reparacao = await _context.Reparacoes
            .Include(r => r.Intervencoes)
            .FirstOrDefaultAsync(r => r.Id == id);
        if (reparacao == null) return false;
        if (reparacao.Intervencoes.Any()) return false;

        _context.Reparacoes.Remove(reparacao);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Reparacoes.AnyAsync(r => r.Id == id);
    }

    public async Task<bool> EquipamentoExistsAsync(int equipamentoId)
    {
        return await _context.Equipamentos.AnyAsync(e => e.Id == equipamentoId);
    }

    public async Task<bool> TecnicoExistsAsync(int? tecnicoId)
    {
        if (!tecnicoId.HasValue) return true;
        return await _context.Tecnicos.AnyAsync(t => t.Id == tecnicoId.Value);
    }

    public async Task<bool> HasIntervencoesAsync(int id)
    {
        return await _context.Intervencoes.AnyAsync(i => i.ReparacaoId == id);
    }
}
