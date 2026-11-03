namespace TechRepair.Models;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string NIF { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Morada { get; set; } = string.Empty;
    public string CodigoPostal { get; set; } = string.Empty;
    public string Localidade { get; set; } = string.Empty;
    public DateTime DataRegisto { get; set; } = DateTime.Now;
    public virtual ICollection<Equipamento> Equipamentos { get; set; } = new List<Equipamento>();
}