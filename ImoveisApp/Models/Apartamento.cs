namespace ImoveisApp.Models;

public class Apartamento : Imovel
{
    public int Piso { get; set; }
    public bool TemElevador { get; set; }
    public bool TemGaragem { get; set; }

    public override string DescricaoDetalhada =>
        $"Apartamento em {Localizacao} - {Quartos} quarto(s), {Area} m², Piso: {Piso}, Elevador: {(TemElevador ? "Sim" : "Não")}, Garagem: {(TemGaragem ? "Sim" : "Não")} | " +
        $"{(TipoNegocio == TipoNegocio.Venda || TipoNegocio == TipoNegocio.Ambos ? $"Venda: {PrecoVenda:C}" : "")} " +
        $"{(TipoNegocio == TipoNegocio.Aluguer || TipoNegocio == TipoNegocio.Ambos ? $"Renda: {RendaMensal:C}/mês" : "")} | Estado: {Estado}";
}