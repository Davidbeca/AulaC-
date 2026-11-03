namespace TechRepair.ViewModels;

public class IntervencaoViewModel
{
    public int Id { get; set; }
    public int ReparacaoId { get; set; }
    public string ReparacaoDescricao { get; set; } = string.Empty;
    public DateTime DataIntervencao { get; set; }
    public string DescricaoTrabalho { get; set; } = string.Empty;
    public string? PecasUtilizadas { get; set; }
    public decimal TempoTrabalho { get; set; }
    public decimal CustoMaoObra { get; set; }
    public decimal CustoPecas { get; set; }
    public decimal CustoTotal { get; set; }
    public string? Observacoes { get; set; }
}

public class IntervencaoCreateViewModel
{
    public int ReparacaoId { get; set; }
    public DateTime DataIntervencao { get; set; } = DateTime.Now;
    public string DescricaoTrabalho { get; set; } = string.Empty;
    public string? PecasUtilizadas { get; set; }
    public decimal TempoTrabalho { get; set; }
    public decimal CustoMaoObra { get; set; }
    public decimal CustoPecas { get; set; }
    public string? Observacoes { get; set; }
}

public class IntervencaoEditViewModel
{
    public int Id { get; set; }
    public int ReparacaoId { get; set; }
    public DateTime DataIntervencao { get; set; }
    public string DescricaoTrabalho { get; set; } = string.Empty;
    public string? PecasUtilizadas { get; set; }
    public decimal TempoTrabalho { get; set; }
    public decimal CustoMaoObra { get; set; }
    public decimal CustoPecas { get; set; }
    public string? Observacoes { get; set; }
}