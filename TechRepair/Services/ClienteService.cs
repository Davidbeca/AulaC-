using Microsoft.EntityFrameworkCore;
using TechRepair.Data;
using TechRepair.Models;
using TechRepair.ViewModels;

namespace TechRepair.Services;

public class ClienteService : IClienteService
{
    private readonly ApplicationDbContext _context;

    public ClienteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ClienteViewModel>> GetAllAsync()
    {
        return await _context.Clientes
            .Include(c => c.Equipamentos)
            .Select(c => new ClienteViewModel
            {
                Id = c.Id,
                Nome = c.Nome,
                NIF = c.NIF,
                Email = c.Email,
                Telefone = c.Telefone,
                Morada = c.Morada,
                CodigoPostal = c.CodigoPostal,
                Localidade = c.Localidade,
                DataRegisto = c.DataRegisto,
                TotalEquipamentos = c.Equipamentos.Count
            })
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<ClienteViewModel?> GetByIdAsync(int id)
    {
        return await _context.Clientes
            .Include(c => c.Equipamentos)
            .Where(c => c.Id == id)
            .Select(c => new ClienteViewModel
            {
                Id = c.Id,
                Nome = c.Nome,
                NIF = c.NIF,
                Email = c.Email,
                Telefone = c.Telefone,
                Morada = c.Morada,
                CodigoPostal = c.CodigoPostal,
                Localidade = c.Localidade,
                DataRegisto = c.DataRegisto,
                TotalEquipamentos = c.Equipamentos.Count
            })
            .FirstOrDefaultAsync();
    }

    public async Task<int> CreateAsync(ClienteCreateViewModel model)
    {
        var cliente = new Cliente
        {
            Nome = model.Nome,
            NIF = model.NIF,
            Email = model.Email,
            Telefone = model.Telefone,
            Morada = model.Morada,
            CodigoPostal = model.CodigoPostal,
            Localidade = model.Localidade,
            DataRegisto = DateTime.Now
        };
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return cliente.Id;
    }

    public async Task<bool> UpdateAsync(ClienteEditViewModel model)
    {
        var cliente = await _context.Clientes.FindAsync(model.Id);
        if (cliente == null) return false;

        cliente.Nome = model.Nome;
        cliente.NIF = model.NIF;
        cliente.Email = model.Email;
        cliente.Telefone = model.Telefone;
        cliente.Morada = model.Morada;
        cliente.CodigoPostal = model.CodigoPostal;
        cliente.Localidade = model.Localidade;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cliente = await _context.Clientes
            .Include(c => c.Equipamentos)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (cliente == null) return false;
        if (cliente.Equipamentos.Any()) return false;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Clientes.AnyAsync(c => c.Id == id);
    }

    public async Task<bool> NifExistsAsync(string nif, int? excludeId = null)
    {
        var query = _context.Clientes.Where(c => c.NIF == nif);
        if (excludeId.HasValue)
            query = query.Where(c => c.Id != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
    {
        var query = _context.Clientes.Where(c => c.Email == email);
        if (excludeId.HasValue)
            query = query.Where(c => c.Id != excludeId.Value);
        return await query.AnyAsync();
    }

    public async Task<bool> HasEquipamentosAsync(int id)
    {
        return await _context.Equipamentos.AnyAsync(e => e.ClienteId == id);
    }
}