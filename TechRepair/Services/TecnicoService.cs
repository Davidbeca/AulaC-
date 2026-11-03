using Microsoft.EntityFrameworkCore;
using TechRepair.Data;
using TechRepair.Models;
using TechRepair.ViewModels;

namespace TechRepair.Services;

public class TecnicoService : ITecnicoService
{
    private readonly ApplicationDbContext _context;

    public TecnicoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TecnicoViewModel>> GetAllAsync()
    {
        return await _context.Tecnicos
            .Include(t => t.Reparacoes)
            .Select(t => new TecnicoViewModel
            {
                Id = t.Id,
                Nome = t.Nome,
                Email = t.Email,
                Telefone = t.Telefone,
                Especialidade = t.Especialidade,
                Ativo = t.Ativo,
                TotalReparacoes = t.Reparacoes.Count
            })
            .OrderBy(t => t.Nome)
            .ToListAsync();
    }

    public async Task<TecnicoViewModel?> GetByIdAsync(int id)
    {
        return await _context.Tecnicos
            .Include(t => t.Reparacoes)
            .Where(t => t.Id == id)
            .Select(t => new TecnicoViewModel
            {
                Id = t.Id,
                Nome = t.Nome,
                Email = t.Email,
                Telefone = t.Telefone,
                Especialidade = t.Especialidade,
                Ativo = t.Ativo,
                TotalReparacoes = t.Reparacoes.Count
            })
            .FirstOrDefaultAsync();
    }

    public async Task<int> CreateAsync(TecnicoCreateViewModel model)
    {
        var tecnico = new Tecnico
        {
            Nome = model.Nome,
            Email = model.Email,
            Telefone = model.Telefone,
            Especialidade = model.Especialidade,
            Ativo = model.Ativo
        };
        _context.Tecnicos.Add(tecnico);
        await _context.SaveChangesAsync();
        return tecnico.Id;
    }

    public async Task<bool> UpdateAsync(TecnicoEditViewModel model)
    {
        var tecnico = await _context.Tecnicos.FindAsync(model.Id);
        if (tecnico == null) return false;

        tecnico.Nome = model.Nome;
        tecnico.Email = model.Email;
        tecnico.Telefone = model.Telefone;
        tecnico.Especialidade = model.Especialidade;
        tecnico.Ativo = model.Ativo;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var tecnico = await _context.Tecnicos
            .Include(t => t.Reparacoes)
            .FirstOrDefaultAsync(t => t.Id == id);
        if (tecnico == null) return false;
        if (tecnico.Reparacoes.Any()) return false;

        _context.Tecnicos.Remove(tecnico);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Tecnicos.AnyAsync(t => t.Id == id);
    }

    public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
    {
        var query = _context.Tecnicos.Where(t => t.Email == email);
        if (excludeId.HasValue)
            query = query.Where(t => t.Id != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<bool> HasReparacoesAsync(int id)
    {
        return await _context.Reparacoes.AnyAsync(r => r.TecnicoId == id);
    }
}