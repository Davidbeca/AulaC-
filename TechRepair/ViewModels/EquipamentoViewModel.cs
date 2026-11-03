namespace TechRepair.ViewModels;

public class EquipamentoViewModel
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string ClienteNome { get; set; } = string.Empty;
    public string TipoEquipamento { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string NumeroSerie { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataRegisto { get; set; }
    public int TotalReparacoes { get; set; }
}

public class EquipamentoCreateViewModel
{
    public int ClienteId { get; set; }
    public string TipoEquipamento { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string NumeroSerie { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
}

public class EquipamentoEditViewModel
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string TipoEquipamento { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string NumeroSerie { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
}