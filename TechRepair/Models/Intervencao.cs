namespace TechRepair.Models;

public class Intervencao
{
    public int Id { get; set; }
    public int ReparacaoId { get; set; }
    public DateTime DataIntervencao { get; set; } = DateTime.Now;
    public string DescricaoTrabalho { get; set; } = string.Empty;
    public string? PecasUtilizadas { get; set; }
    public decimal TempoTrabalho { get; set; }
    public decimal CustoMaoObra { get; set; }
    public decimal CustoPecas { get; set; }
    public string? Observacoes { get; set; }
    public virtual Reparacao Reparacao { get; set; } = null!;
}