# 02 - Diagrama Entidade-Relação (Mermaid)

```mermaid
erDiagram
    CLIENTE ||--o{ EQUIPAMENTO : "possui"
    EQUIPAMENTO ||--o{ REPARACAO : "gera"
    TECNICO ||--o{ REPARACAO : "executa"
    REPARACAO ||--|{ INTERVENCAO : "contém"

    CLIENTE {
        int Id PK
        string Nome UK
        string NIF UK
        string Email UK
        string Telefone
        string Morada
        string CodigoPostal
        string Localidade
        datetime DataRegisto
    }

    EQUIPAMENTO {
        int Id PK
        int ClienteId FK
        string TipoEquipamento
        string Marca
        string Modelo
        string NumeroSerie UK
        string Descricao
        datetime DataRegisto
    }

    TECNICO {
        int Id PK
        string Nome
        string Email UK
        string Telefone
        string Especialidade
        bool Ativo
    }

    REPARACAO {
        int Id PK
        int EquipamentoId FK
        int TecnicoId FK
        datetime DataEntrada
        string DescricaoAvaria
        string Diagnostico
        EstadoReparacao Estado
        datetime DataConclusao
        datetime DataEntrega
        string Observacoes
    }

    INTERVENCAO {
        int Id PK
        int ReparacaoId FK
        datetime DataIntervencao
        string DescricaoTrabalho
        string PecasUtilizadas
        decimal TempoTrabalho
        decimal CustoMaoObra
        decimal CustoPecas
        string Observacoes
    }
```

## Entidades e Cardinalidades

| Entidade | Cardinalidade | Descrição |
|----------|--------------|-----------|
| Cliente | 1:N com Equipamento | Um cliente pode ter vários equipamentos |
| Equipamento | N:1 com Cliente | Um equipamento pertence a um único cliente |
| Equipamento | 1:N com Reparação | Um equipamento pode ter várias reparações |
| Reparação | N:1 com Equipamento | Uma reparação diz respeito a um único equipamento |
| Tecnico | 1:N com Reparação | Um técnico pode executar várias reparações |
| Reparação | N:1 (opcional) com Tecnico | Uma reparação pode ter um técnico atribuído |
| Reparação | 1:N com Intervenção | Uma reparação pode ter várias intervenções |
| Intervenção | N:1 com Reparação | Uma intervenção pertence a uma única reparação |

## Regras de Integridade Referencial

- `ClienteId` em Equipamento → Chave estrangeira para Clientes (DELETE RESTRICT)
- `EquipamentoId` em Reparação → Chave estrangeira para Equipamentos (DELETE RESTRICT)
- `TecnicoId` em Reparação → Chave estrangeira para Tecnicos (DELETE SET NULL)
- `ReparacaoId` em Intervenção → Chave estrangeira para Reparacoes (DELETE RESTRICT)