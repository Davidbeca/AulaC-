namespace ImoveisApp.Models;

public abstract class Imovel
{
    public int Id { get; set; }
    public string Localizacao { get; set; } = string.Empty;
    public int Quartos { get; set; }
    public double Area { get; set; }
    public TipoNegocio TipoNegocio { get; set; }
    public double? PrecoVenda { get; set; }
    public double? RendaMensal { get; set; }
    public EstadoDisponibilidade Estado { get; set; } = EstadoDisponibilidade.Disponivel;

    public abstract string DescricaoDetalhada { get; }

    public virtual string Resumo =>
        $"ID: {Id} | {Localizacao} | {GetType().Name} | {Quartos} quartos | {Area} m² | Estado: {Estado}";

    public override string ToString() => Resumo;
}