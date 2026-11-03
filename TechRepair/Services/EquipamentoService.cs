using Microsoft.EntityFrameworkCore;
using TechRepair.Data;
using TechRepair.Models;
using TechRepair.ViewModels;

namespace TechRepair.Services;

public class EquipamentoService : IEquipamentoService
{
    private readonly ApplicationDbContext _context;

    public EquipamentoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<EquipamentoViewModel>> GetAllAsync()
    {
        return await _context.Equipamentos
            .Include(e => e.Cliente)
            .Include(e => e.Reparacoes)
            .Select(e => new EquipamentoViewModel
            {
                Id = e.Id,
                ClienteId = e.ClienteId,
                ClienteNome = e.Cliente.Nome,
                TipoEquipamento = e.TipoEquipamento,
                Marca = e.Marca,
                Modelo = e.Modelo,
                NumeroSerie = e.NumeroSerie,
                Descricao = e.Descricao,
                DataRegisto = e.DataRegisto,
                TotalReparacoes = e.Reparacoes.Count
            })
            .OrderBy(e => e.Marca)
            .ThenBy(e => e.Modelo)
            .ToListAsync();
    }

    public async Task<List<EquipamentoViewModel>> GetByClienteAsync(int clienteId)
    {
        return await _context.Equipamentos
            .Include(e => e.Cliente)
            .Include(e => e.Reparacoes)
            .Where(e => e.ClienteId == clienteId)
            .Select(e => new EquipamentoViewModel
            {
                Id = e.Id,
                ClienteId = e.ClienteId,
                ClienteNome = e.Cliente.Nome,
                TipoEquipamento = e.TipoEquipamento,
                Marca = e.Marca,
                Modelo = e.Modelo,
                NumeroSerie = e.NumeroSerie,
                Descricao = e.Descricao,
                DataRegisto = e.DataRegisto,
                TotalReparacoes = e.Reparacoes.Count
            })
            .OrderBy(e => e.Marca)
            .ThenBy(e => e.Modelo)
            .ToListAsync();
    }

    public async Task<EquipamentoViewModel?> GetByIdAsync(int id)
    {
        return await _context.Equipamentos
            .Include(e => e.Cliente)
            .Include(e => e.Reparacoes)
            .Where(e => e.Id == id)
            .Select(e => new EquipamentoViewModel
            {
                Id = e.Id,
                ClienteId = e.ClienteId,
                ClienteNome = e.Cliente.Nome,
                TipoEquipamento = e.TipoEquipamento,
                Marca = e.Marca,
                Modelo = e.Modelo,
                NumeroSerie = e.NumeroSerie,
                Descricao = e.Descricao,
                DataRegisto = e.DataRegisto,
                TotalReparacoes = e.Reparacoes.Count
            })
            .FirstOrDefaultAsync();
    }

    public async Task<int> CreateAsync(EquipamentoCreateViewModel model)
    {
        var equipamento = new Equipamento
        {
            ClienteId = model.ClienteId,
            TipoEquipamento = model.TipoEquipamento,
            Marca = model.Marca,
            Modelo = model.Modelo,
            NumeroSerie = model.NumeroSerie,
            Descricao = model.Descricao,
            DataRegisto = DateTime.Now
        };
        _context.Equipamentos.Add(equipamento);
        await _context.SaveChangesAsync();
        return equipamento.Id;
    }

    public async Task<bool> UpdateAsync(EquipamentoEditViewModel model)
    {
        var equipamento = await _context.Equipamentos.FindAsync(model.Id);
        if (equipamento == null) return false;

        equipamento.ClienteId = model.ClienteId;
        equipamento.TipoEquipamento = model.TipoEquipamento;
        equipamento.Marca = model.Marca;
        equipamento.Modelo = model.Modelo;
        equipamento.NumeroSerie = model.NumeroSerie;
        equipamento.Descricao = model.Descricao;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var equipamento = await _context.Equipamentos
            .Include(e => e.Reparacoes)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (equipamento == null) return false;
        if (equipamento.Reparacoes.Any()) return false;

        _context.Equipamentos.Remove(equipamento);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Equipamentos.AnyAsync(e => e.Id == id);
    }

    public async Task<bool> NumeroSerieExistsAsync(string numeroSerie, int? excludeId = null)
    {
        var query = _context.Equipamentos.Where(e => e.NumeroSerie == numeroSerie);
        if (excludeId.HasValue)
            query = query.Where(e => e.Id != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<bool> ClienteExistsAsync(int clienteId)
    {
        return await _context.Clientes.AnyAsync(c => c.Id == clienteId);
    }

    public async Task<bool> HasReparacoesAsync(int id)
    {
        return await _context.Reparacoes.AnyAsync(r => r.EquipamentoId == id);
    }
}