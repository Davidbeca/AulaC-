namespace TechRepair.ViewModels;

public class ClienteViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string NIF { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Morada { get; set; } = string.Empty;
    public string CodigoPostal { get; set; } = string.Empty;
    public string Localidade { get; set; } = string.Empty;
    public DateTime DataRegisto { get; set; }
    public int TotalEquipamentos { get; set; }
}

public class ClienteCreateViewModel
{
    public string Nome { get; set; } = string.Empty;
    public string NIF { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Morada { get; set; } = string.Empty;
    public string CodigoPostal { get; set; } = string.Empty;
    public string Localidade { get; set; } = string.Empty;
}

public class ClienteEditViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string NIF { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Morada { get; set; } = string.Empty;
    public string CodigoPostal { get; set; } = string.Empty;
    public string Localidade { get; set; } = string.Empty;
}