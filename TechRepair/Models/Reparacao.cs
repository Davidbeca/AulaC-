namespace TechRepair.Models;

public enum EstadoReparacao
{
    AguardarDiagnostico,
    EmReparacao,
    AguardarPecas,
    Concluida,
    Entregue
}

public class Reparacao
{
    public int Id { get; set; }
    public int EquipamentoId { get; set; }
    public int? TecnicoId { get; set; }
    public DateTime DataEntrada { get; set; } = DateTime.Now;
    public string DescricaoAvaria { get; set; } = string.Empty;
    public string? Diagnostico { get; set; }
    public EstadoReparacao Estado { get; set; } = EstadoReparacao.AguardarDiagnostico;
    public DateTime? DataConclusao { get; set; }
    public DateTime? DataEntrega { get; set; }
    public string? Observacoes { get; set; }
    public virtual Equipamento Equipamento { get; set; } = null!;
    public virtual Tecnico? Tecnico { get; set; }
    public virtual ICollection<Intervencao> Intervencoes { get; set; } = new List<Intervencao>();
}