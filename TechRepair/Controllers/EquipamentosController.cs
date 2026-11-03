using Microsoft.AspNetCore.Mvc;
using TechRepair.Services;
using TechRepair.ViewModels;

namespace TechRepair.Controllers;

public class EquipamentosController : Controller
{
    private readonly IEquipamentoService _equipamentoService;
    private readonly IClienteService _clienteService;

    public EquipamentosController(IEquipamentoService equipamentoService, IClienteService clienteService)
    {
        _equipamentoService = equipamentoService;
        _clienteService = clienteService;
    }

    // GET: Equipamentos
    public async Task<IActionResult> Index()
    {
        var equipamentos = await _equipamentoService.GetAllAsync();
        return View(equipamentos);
    }

    // GET: Equipamentos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var equipamento = await _equipamentoService.GetByIdAsync(id.Value);
        if (equipamento == null) return NotFound();
        return View(equipamento);
    }

    // GET: Equipamentos/Create
    public async Task<IActionResult> Create()
    {
        var clientes = await _clienteService.GetAllAsync();
        ViewBag.Clientes = clientes;
        return View();
    }

    // POST: Equipamentos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EquipamentoCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var clientes = await _clienteService.GetAllAsync();
            ViewBag.Clientes = clientes;
            return View(model);
        }

        if (!await _clienteService.ExistsAsync(model.ClienteId))
            ModelState.AddModelError("ClienteId", "Cliente não encontrado.");

        if (await _equipamentoService.NumeroSerieExistsAsync(model.NumeroSerie))
            ModelState.AddModelError("NumeroSerie", "Este número de série já está registado.");

        if (!ModelState.IsValid)
        {
            var clientes2 = await _clienteService.GetAllAsync();
            ViewBag.Clientes = clientes2;
            return View(model);
        }

        await _equipamentoService.CreateAsync(model);
        TempData["Success"] = "Equipamento registado com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Equipamentos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var equipamento = await _equipamentoService.GetByIdAsync(id.Value);
        if (equipamento == null) return NotFound();

        var clientes = await _clienteService.GetAllAsync();
        ViewBag.Clientes = clientes;

        return View(new EquipamentoEditViewModel
        {
            Id = equipamento.Id,
            ClienteId = equipamento.ClienteId,
            TipoEquipamento = equipamento.TipoEquipamento,
            Marca = equipamento.Marca,
            Modelo = equipamento.Modelo,
            NumeroSerie = equipamento.NumeroSerie,
            Descricao = equipamento.Descricao
        });
    }

    // POST: Equipamentos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EquipamentoEditViewModel model)
    {
        if (id != model.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            var clientes = await _clienteService.GetAllAsync();
            ViewBag.Clientes = clientes;
            return View(model);
        }

        if (await _equipamentoService.NumeroSerieExistsAsync(model.NumeroSerie, model.Id))
            ModelState.AddModelError("NumeroSerie", "Este número de série já está registado.");

        if (!ModelState.IsValid)
        {
            var clientes2 = await _clienteService.GetAllAsync();
            ViewBag.Clientes = clientes2;
            return View(model);
        }

        var success = await _equipamentoService.UpdateAsync(model);
        if (!success) return NotFound();

        TempData["Success"] = "Equipamento atualizado com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Equipamentos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var equipamento = await _equipamentoService.GetByIdAsync(id.Value);
        if (equipamento == null) return NotFound();
        return View(equipamento);
    }

    // POST: Equipamentos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var hasReparacoes = await _equipamentoService.HasReparacoesAsync(id);
        if (hasReparacoes)
        {
            TempData["Error"] = "Não é possível eliminar um equipamento que possui reparações.";
            return RedirectToAction(nameof(Index));
        }

        var success = await _equipamentoService.DeleteAsync(id);
        if (!success) return NotFound();

        TempData["Success"] = "Equipamento eliminado com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Equipamentos/ByCliente/5
    public async Task<IActionResult> ByCliente(int clienteId)
    {
        var equipamentos = await _equipamentoService.GetByClienteAsync(clienteId);
        var cliente = await _clienteService.GetByIdAsync(clienteId);
        ViewBag.ClienteNome = cliente?.Nome;
        ViewBag.ClienteId = clienteId;
        return View(equipamentos);
    }
}