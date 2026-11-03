namespace TechRepair.Models;

public class Equipamento
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string TipoEquipamento { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string NumeroSerie { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataRegisto { get; set; } = DateTime.Now;
    public virtual Cliente Cliente { get; set; } = null!;
    public virtual ICollection<Reparacao> Reparacoes { get; set; } = new List<Reparacao>();
}