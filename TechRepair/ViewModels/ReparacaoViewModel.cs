namespace TechRepair.ViewModels;

public enum EstadoReparacaoViewModel
{
    AguardarDiagnostico = 0,
    EmReparacao = 1,
    AguardarPecas = 2,
    Concluida = 3,
    Entregue = 4
}

public class ReparacaoViewModel
{
    public int Id { get; set; }
    public int EquipamentoId { get; set; }
    public string EquipamentoDescricao { get; set; } = string.Empty;
    public string ClienteNome { get; set; } = string.Empty;
    public int? TecnicoId { get; set; }
    public string TecnicoNome { get; set; } = string.Empty;
    public DateTime DataEntrada { get; set; }
    public string DescricaoAvaria { get; set; } = string.Empty;
    public string? Diagnostico { get; set; }
    public EstadoReparacaoViewModel Estado { get; set; }
    public DateTime? DataConclusao { get; set; }
    public DateTime? DataEntrega { get; set; }
    public string? Observacoes { get; set; }
    public decimal CustoTotal { get; set; }
    public int TotalIntervencoes { get; set; }
}

public class ReparacaoCreateViewModel
{
    public int EquipamentoId { get; set; }
    public int? TecnicoId { get; set; }
    public string DescricaoAvaria { get; set; } = string.Empty;
    public string? Diagnostico { get; set; }
    public EstadoReparacaoViewModel Estado { get; set; } = EstadoReparacaoViewModel.AguardarDiagnostico;
    public string? Observacoes { get; set; }
}

public class ReparacaoEditViewModel
{
    public int Id { get; set; }
    public int EquipamentoId { get; set; }
    public int? TecnicoId { get; set; }
    public string DescricaoAvaria { get; set; } = string.Empty;
    public string? Diagnostico { get; set; }
    public EstadoReparacaoViewModel Estado { get; set; }
    public DateTime? DataConclusao { get; set; }
    public DateTime? DataEntrega { get; set; }
    public string? Observacoes { get; set; }
}