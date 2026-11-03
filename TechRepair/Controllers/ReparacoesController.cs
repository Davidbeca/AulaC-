using Microsoft.AspNetCore.Mvc;
using TechRepair.Services;
using TechRepair.ViewModels;

namespace TechRepair.Controllers;

public class ReparacoesController : Controller
{
    private readonly IReparacaoService _reparacaoService;
    private readonly IEquipamentoService _equipamentoService;
    private readonly ITecnicoService _tecnicoService;

    public ReparacoesController(
        IReparacaoService reparacaoService,
        IEquipamentoService equipamentoService,
        ITecnicoService tecnicoService)
    {
        _reparacaoService = reparacaoService;
        _equipamentoService = equipamentoService;
        _tecnicoService = tecnicoService;
    }

    // GET: Reparacoes
    public async Task<IActionResult> Index()
    {
        var reparacoes = await _reparacaoService.GetAllAsync();
        return View(reparacoes);
    }

    // GET: Reparacoes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var reparacao = await _reparacaoService.GetByIdAsync(id.Value);
        if (reparacao == null) return NotFound();
        return View(reparacao);
    }

    // GET: Reparacoes/Create
    public async Task<IActionResult> Create()
    {
        var equipamentos = await _equipamentoService.GetAllAsync();
        var tecnicos = await _tecnicoService.GetAllAsync();
        ViewBag.Equipamentos = equipamentos;
        ViewBag.Tecnicos = tecnicos.Where(t => t.Ativo).ToList();
        return View();
    }

    // POST: Reparacoes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ReparacaoCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var equipamentos = await _equipamentoService.GetAllAsync();
            var tecnicos = await _tecnicoService.GetAllAsync();
            ViewBag.Equipamentos = equipamentos;
            ViewBag.Tecnicos = tecnicos.Where(t => t.Ativo).ToList();
            return View(model);
        }

        if (!await _equipamentoService.ExistsAsync(model.EquipamentoId))
            ModelState.AddModelError("EquipamentoId", "Equipamento não encontrado.");

        if (!await _tecnicoService.ExistsAsync(model.TecnicoId ?? 0) && model.TecnicoId.HasValue)
            ModelState.AddModelError("TecnicoId", "Técnico não encontrado.");

        if (!ModelState.IsValid)
        {
            var equipamentos2 = await _equipamentoService.GetAllAsync();
            var tecnicos2 = await _tecnicoService.GetAllAsync();
            ViewBag.Equipamentos = equipamentos2;
            ViewBag.Tecnicos = tecnicos2.Where(t => t.Ativo).ToList();
            return View(model);
        }

        await _reparacaoService.CreateAsync(model);
        TempData["Success"] = "Reparação registada com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Reparacoes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var reparacao = await _reparacaoService.GetByIdAsync(id.Value);
        if (reparacao == null) return NotFound();

        var equipamentos = await _equipamentoService.GetAllAsync();
        var tecnicos = await _tecnicoService.GetAllAsync();
        ViewBag.Equipamentos = equipamentos;
        ViewBag.Tecnicos = tecnicos;

        return View(new ReparacaoEditViewModel
        {
            Id = reparacao.Id,
            EquipamentoId = reparacao.EquipamentoId,
            TecnicoId = reparacao.TecnicoId,
            DescricaoAvaria = reparacao.DescricaoAvaria,
            Diagnostico = reparacao.Diagnostico,
            Estado = reparacao.Estado,
            DataConclusao = reparacao.DataConclusao,
            DataEntrega = reparacao.DataEntrega,
            Observacoes = reparacao.Observacoes
        });
    }

    // POST: Reparacoes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ReparacaoEditViewModel model)
    {
        if (id != model.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            var equipamentos = await _equipamentoService.GetAllAsync();
            var tecnicos = await _tecnicoService.GetAllAsync();
            ViewBag.Equipamentos = equipamentos;
            ViewBag.Tecnicos = tecnicos;
            return View(model);
        }

        var success = await _reparacaoService.UpdateAsync(model);
        if (!success) return NotFound();

        TempData["Success"] = "Reparação atualizada com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Reparacoes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var reparacao = await _reparacaoService.GetByIdAsync(id.Value);
        if (reparacao == null) return NotFound();
        return View(reparacao);
    }

    // POST: Reparacoes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var hasIntervencoes = await _reparacaoService.HasIntervencoesAsync(id);
        if (hasIntervencoes)
        {
            TempData["Error"] = "Não é possível eliminar uma reparação que possui intervenções.";
            return RedirectToAction(nameof(Index));
        }

        var success = await _reparacaoService.DeleteAsync(id);
        if (!success) return NotFound();

        TempData["Success"] = "Reparação eliminada com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Reparacoes/Estado/0
    public async Task<IActionResult> Estado(int estado)
    {
        var reparacoes = await _reparacaoService.GetByEstadoAsync(estado);
        ViewBag.EstadoSelecionado = estado;
        return View("Index", reparacoes);
    }

    // GET: Reparacoes/ByTecnico/5
    public async Task<IActionResult> ByTecnico(int tecnicoId)
    {
        var reparacoes = await _reparacaoService.GetByTecnicoAsync(tecnicoId);
        var tecnico = await _tecnicoService.GetByIdAsync(tecnicoId);
        ViewBag.TecnicoNome = tecnico?.Nome;
        ViewBag.TecnicoId = tecnicoId;
        return View(reparacoes);
    }
}
