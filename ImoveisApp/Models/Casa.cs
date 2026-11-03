namespace ImoveisApp.Models;

public class Casa : Imovel
{
    public bool TemJardim { get; set; }
    public int NumeroAndares { get; set; }

    public override string DescricaoDetalhada =>
        $"Casa em {Localizacao} - {Quartos} quarto(s), {Area} m², {NumeroAndares} andar(es), Jardim: {(TemJardim ? "Sim" : "Não")} | " +
        $"{(TipoNegocio == TipoNegocio.Venda || TipoNegocio == TipoNegocio.Ambos ? $"Venda: {PrecoVenda:C}" : "")} " +
        $"{(TipoNegocio == TipoNegocio.Aluguer || TipoNegocio == TipoNegocio.Ambos ? $"Renda: {RendaMensal:C}/mês" : "")} | Estado: {Estado}";
}