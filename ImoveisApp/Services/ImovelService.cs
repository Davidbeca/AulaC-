using ImoveisApp.Models;

namespace ImoveisApp.Services;

public class ImovelService
{
    private readonly List<Imovel> _imoveis = new();
    private int _proximoId = 1;

    public void RegistrarImovel(Imovel imovel)
    {
        imovel.Id = _proximoId++;
        _imoveis.Add(imovel);
    }

    public List<Imovel> ListarTodos() => _imoveis;

    public List<Imovel> ListarDisponiveis() =>
        _imoveis.Where(i => i.Estado == EstadoDisponibilidade.Disponivel).ToList();

    public List<Imovel> ListarParaVenda() =>
        _imoveis.Where(i =>
            i.Estado == EstadoDisponibilidade.Disponivel &&
            (i.TipoNegocio == TipoNegocio.Venda || i.TipoNegocio == TipoNegocio.Ambos) &&
            i.PrecoVenda.HasValue).ToList();

    public List<Imovel> ListarParaAluguer() =>
        _imoveis.Where(i =>
            i.Estado == EstadoDisponibilidade.Disponivel &&
            (i.TipoNegocio == TipoNegocio.Aluguer || i.TipoNegocio == TipoNegocio.Ambos) &&
            i.RendaMensal.HasValue).ToList();

    public Imovel? BuscarPorId(int id) => _imoveis.FirstOrDefault(i => i.Id == id);

    public List<Imovel> Pesquisar(
        string? localizacao = null,
        TipoImovel? tipo = null,
        double? precoMaximo = null,
        int? quartosMinimos = null)
    {
        var resultado = _imoveis.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(localizacao))
            resultado = resultado.Where(i =>
                i.Localizacao.Contains(localizacao, StringComparison.OrdinalIgnoreCase));

        if (tipo.HasValue)
        {
            resultado = tipo.Value == TipoImovel.Casa
                ? resultado.OfType<Casa>()
                : resultado.OfType<Apartamento>();
        }

        if (quartosMinimos.HasValue)
            resultado = resultado.Where(i => i.Quartos >= quartosMinimos.Value);

        if (precoMaximo.HasValue)
        {
            resultado = resultado.Where(i =>
                (i.PrecoVenda.HasValue && i.PrecoVenda.Value <= precoMaximo.Value) ||
                (i.RendaMensal.HasValue && i.RendaMensal.Value <= precoMaximo.Value));
        }

        return resultado.ToList();
    }

    public bool AlterarEstado(int id, EstadoDisponibilidade novoEstado)
    {
        var imovel = BuscarPorId(id);
        if (imovel is null) return false;
        imovel.Estado = novoEstado;
        return true;
    }

    public bool RemoverImovel(int id)
    {
        var imovel = BuscarPorId(id);
        if (imovel is null) return false;
        _imoveis.Remove(imovel);
        return true;
    }

    public int TotalImoveis => _imoveis.Count;
}