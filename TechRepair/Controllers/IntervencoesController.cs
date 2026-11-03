using Microsoft.AspNetCore.Mvc;
using TechRepair.Services;
using TechRepair.ViewModels;

namespace TechRepair.Controllers;

public class IntervencoesController : Controller
{
    private readonly IIntervencaoService _intervencaoService;
    private readonly IReparacaoService _reparacaoService;

    public IntervencoesController(
        IIntervencaoService intervencaoService,
        IReparacaoService reparacaoService)
    {
        _intervencaoService = intervencaoService;
        _reparacaoService = reparacaoService;
    }

    // GET: Intervencoes/ByReparacao/5
    public async Task<IActionResult> ByReparacao(int reparacaoId)
    {
        var intervencoes = await _intervencaoService.GetByReparacaoAsync(reparacaoId);
        var reparacao = await _reparacaoService.GetByIdAsync(reparacaoId);
        if (reparacao == null) return NotFound();

        ViewBag.ReparacaoId = reparacaoId;
        ViewBag.ReparacaoDescricao = $"Reparação #{reparacaoId} - {reparacao.EquipamentoDescricao}";
        ViewBag.ClienteNome = reparacao.ClienteNome;
        return View(intervencoes);
    }

    // GET: Intervencoes/Create/5
    public async Task<IActionResult> Create(int reparacaoId)
    {
        var reparacao = await _reparacaoService.GetByIdAsync(reparacaoId);
        if (reparacao == null) return NotFound();

        ViewBag.ReparacaoId = reparacaoId;
        ViewBag.ReparacaoDescricao = $"Reparação #{reparacaoId} - {reparacao.EquipamentoDescricao}";
        return View(new IntervencaoCreateViewModel { ReparacaoId = reparacaoId });
    }

    // POST: Intervencoes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(IntervencaoCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ReparacaoId = model.ReparacaoId;
            var reparacao = await _reparacaoService.GetByIdAsync(model.ReparacaoId);
            ViewBag.ReparacaoDescricao = reparacao != null ? $"Reparação #{model.ReparacaoId} - {reparacao.EquipamentoDescricao}" : "";
            return View(model);
        }

        if (!await _intervencaoService.ReparacaoExistsAsync(model.ReparacaoId))
            ModelState.AddModelError("ReparacaoId", "Reparação não encontrada.");

        if (!ModelState.IsValid)
        {
            ViewBag.ReparacaoId = model.ReparacaoId;
            var reparacao2 = await _reparacaoService.GetByIdAsync(model.ReparacaoId);
            ViewBag.ReparacaoDescricao = reparacao2 != null ? $"Reparação #{model.ReparacaoId} - {reparacao2.EquipamentoDescricao}" : "";
            return View(model);
        }

        await _intervencaoService.CreateAsync(model);
        TempData["Success"] = "Intervenção registada com sucesso!";
        return RedirectToAction(nameof(ByReparacao), new { reparacaoId = model.ReparacaoId });
    }

    // GET: Intervencoes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var intervencao = await _intervencaoService.GetByIdAsync(id.Value);
        if (intervencao == null) return NotFound();

        ViewBag.ReparacaoId = intervencao.ReparacaoId;
        return View(new IntervencaoEditViewModel
        {
            Id = intervencao.Id,
            ReparacaoId = intervencao.ReparacaoId,
            DataIntervencao = intervencao.DataIntervencao,
            DescricaoTrabalho = intervencao.DescricaoTrabalho,
            PecasUtilizadas = intervencao.PecasUtilizadas,
            TempoTrabalho = intervencao.TempoTrabalho,
            CustoMaoObra = intervencao.CustoMaoObra,
            CustoPecas = intervencao.CustoPecas,
            Observacoes = intervencao.Observacoes
        });
    }

    // POST: Intervencoes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, IntervencaoEditViewModel model)
    {
        if (id != model.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            ViewBag.ReparacaoId = model.ReparacaoId;
            return View(model);
        }

        var success = await _intervencaoService.UpdateAsync(model);
        if (!success) return NotFound();

        TempData["Success"] = "Intervenção atualizada com sucesso!";
        return RedirectToAction(nameof(ByReparacao), new { reparacaoId = model.ReparacaoId });
    }

    // GET: Intervencoes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var intervencao = await _intervencaoService.GetByIdAsync(id.Value);
        if (intervencao == null) return NotFound();
        return View(intervencao);
    }

    // POST: Intervencoes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var intervencao = await _intervencaoService.GetByIdAsync(id);
        if (intervencao == null) return NotFound();

        var reparacaoId = intervencao.ReparacaoId;
        var success = await _intervencaoService.DeleteAsync(id);
        if (!success) return NotFound();

        TempData["Success"] = "Intervenção eliminada com sucesso!";
        return RedirectToAction(nameof(ByReparacao), new { reparacaoId });
    }
}
