using Microsoft.AspNetCore.Mvc;
using TechRepair.Services;
using TechRepair.ViewModels;

namespace TechRepair.Controllers;

public class TecnicosController : Controller
{
    private readonly ITecnicoService _tecnicoService;

    public TecnicosController(ITecnicoService tecnicoService)
    {
        _tecnicoService = tecnicoService;
    }

    // GET: Tecnicos
    public async Task<IActionResult> Index()
    {
        var tecnicos = await _tecnicoService.GetAllAsync();
        return View(tecnicos);
    }

    // GET: Tecnicos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var tecnico = await _tecnicoService.GetByIdAsync(id.Value);
        if (tecnico == null) return NotFound();
        return View(tecnico);
    }

    // GET: Tecnicos/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Tecnicos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TecnicoCreateViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        if (await _tecnicoService.EmailExistsAsync(model.Email))
            ModelState.AddModelError("Email", "Este email já está registado.");

        if (!ModelState.IsValid) return View(model);

        await _tecnicoService.CreateAsync(model);
        TempData["Success"] = "Técnico registado com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Tecnicos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var tecnico = await _tecnicoService.GetByIdAsync(id.Value);
        if (tecnico == null) return NotFound();

        return View(new TecnicoEditViewModel
        {
            Id = tecnico.Id,
            Nome = tecnico.Nome,
            Email = tecnico.Email,
            Telefone = tecnico.Telefone,
            Especialidade = tecnico.Especialidade,
            Ativo = tecnico.Ativo
        });
    }

    // POST: Tecnicos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TecnicoEditViewModel model)
    {
        if (id != model.Id) return NotFound();
        if (!ModelState.IsValid) return View(model);

        if (await _tecnicoService.EmailExistsAsync(model.Email, model.Id))
            ModelState.AddModelError("Email", "Este email já está registado.");

        if (!ModelState.IsValid) return View(model);

        var success = await _tecnicoService.UpdateAsync(model);
        if (!success) return NotFound();

        TempData["Success"] = "Técnico atualizado com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Tecnicos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var tecnico = await _tecnicoService.GetByIdAsync(id.Value);
        if (tecnico == null) return NotFound();
        return View(tecnico);
    }

    // POST: Tecnicos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var hasReparacoes = await _tecnicoService.HasReparacoesAsync(id);
        if (hasReparacoes)
        {
            TempData["Error"] = "Não é possível eliminar um técnico que possui reparações atribuídas.";
            return RedirectToAction(nameof(Index));
        }

        var success = await _tecnicoService.DeleteAsync(id);
        if (!success) return NotFound();

        TempData["Success"] = "Técnico eliminado com sucesso!";
        return RedirectToAction(nameof(Index));
    }
}