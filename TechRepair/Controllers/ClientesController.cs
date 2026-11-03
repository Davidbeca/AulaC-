using Microsoft.AspNetCore.Mvc;
using TechRepair.Services;
using TechRepair.ViewModels;

namespace TechRepair.Controllers;

public class ClientesController : Controller
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    // GET: Clientes
    public async Task<IActionResult> Index()
    {
        var clientes = await _clienteService.GetAllAsync();
        return View(clientes);
    }

    // GET: Clientes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var cliente = await _clienteService.GetByIdAsync(id.Value);
        if (cliente == null) return NotFound();
        return View(cliente);
    }

    // GET: Clientes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Clientes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClienteCreateViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        if (await _clienteService.NifExistsAsync(model.NIF))
            ModelState.AddModelError("NIF", "Este NIF já está registado.");

        if (await _clienteService.EmailExistsAsync(model.Email))
            ModelState.AddModelError("Email", "Este email já está registado.");

        if (!ModelState.IsValid) return View(model);

        await _clienteService.CreateAsync(model);
        TempData["Success"] = "Cliente registado com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Clientes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var cliente = await _clienteService.GetByIdAsync(id.Value);
        if (cliente == null) return NotFound();

        return View(new ClienteEditViewModel
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            NIF = cliente.NIF,
            Email = cliente.Email,
            Telefone = cliente.Telefone,
            Morada = cliente.Morada,
            CodigoPostal = cliente.CodigoPostal,
            Localidade = cliente.Localidade
        });
    }

    // POST: Clientes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ClienteEditViewModel model)
    {
        if (id != model.Id) return NotFound();
        if (!ModelState.IsValid) return View(model);

        if (await _clienteService.NifExistsAsync(model.NIF, model.Id))
            ModelState.AddModelError("NIF", "Este NIF já está registado.");

        if (await _clienteService.EmailExistsAsync(model.Email, model.Id))
            ModelState.AddModelError("Email", "Este email já está registado.");

        if (!ModelState.IsValid) return View(model);

        var success = await _clienteService.UpdateAsync(model);
        if (!success) return NotFound();

        TempData["Success"] = "Cliente atualizado com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Clientes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var cliente = await _clienteService.GetByIdAsync(id.Value);
        if (cliente == null) return NotFound();
        return View(cliente);
    }

    // POST: Clientes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var hasEquipamentos = await _clienteService.HasEquipamentosAsync(id);
        if (hasEquipamentos)
        {
            TempData["Error"] = "Não é possível eliminar um cliente que possui equipamentos.";
            return RedirectToAction(nameof(Index));
        }

        var success = await _clienteService.DeleteAsync(id);
        if (!success) return NotFound();

        TempData["Success"] = "Cliente eliminado com sucesso!";
        return RedirectToAction(nameof(Index));
    }
}