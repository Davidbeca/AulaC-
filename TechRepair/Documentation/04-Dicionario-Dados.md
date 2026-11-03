# 04 - Dicionário de Dados

## Tabela: Clientes

| Nome do Campo | Tipo de Dados | Obrigatório | Chave | Descrição |
|---------------|---------------|-------------|-------|-----------|
| Id | INTEGER | Sim | PK | Identificador único, auto-incrementável |
| Nome | TEXT(200) | Sim | | Nome completo do cliente |
| NIF | TEXT(20) | Sim | UK | Número de Identificação Fiscal (único) |
| Email | TEXT(200) | Sim | UK | Endereço de email (único) |
| Telefone | TEXT(50) | Sim | | Contacto telefónico |
| Morada | TEXT(300) | Sim | | Morada completa |
| CodigoPostal | TEXT(20) | Sim | | Código postal |
| Localidade | TEXT(100) | Sim | | Cidade/localidade |
| DataRegisto | DATETIME | Sim | | Data e hora do registo do cliente |

## Tabela: Tecnicos

| Nome do Campo | Tipo de Dados | Obrigatório | Chave | Descrição |
|---------------|---------------|-------------|-------|-----------|
| Id | INTEGER | Sim | PK | Identificador único, auto-incrementável |
| Nome | TEXT(200) | Sim | | Nome completo do técnico |
| Email | TEXT(200) | Sim | UK | Endereço de email (único) |
| Telefone | TEXT(50) | Sim | | Contacto telefónico |
| Especialidade | TEXT(100) | Sim | | Área de especialização técnica |
| Ativo | BOOLEAN | Sim | | Indica se o técnico está ativo (1) ou inativo (0) |

## Tabela: Equipamentos

| Nome do Campo | Tipo de Dados | Obrigatório | Chave | Descrição |
|---------------|---------------|-------------|-------|-----------|
| Id | INTEGER | Sim | PK | Identificador único, auto-incrementável |
| ClienteId | INTEGER | Sim | FK | Chave estrangeira para Clientes.Id |
| TipoEquipamento | TEXT(100) | Sim | | Tipo de equipamento (ex: Portátil, Desktop, Impressora) |
| Marca | TEXT(100) | Sim | | Marca do fabricante |
| Modelo | TEXT(100) | Sim | | Modelo do equipamento |
| NumeroSerie | TEXT(100) | Sim | UK | Número de série (único) |
| Descricao | TEXT(500) | Não | | Descrição detalhada do estado do equipamento |
| DataRegisto | DATETIME | Sim | | Data de entrada do equipamento na assistência |

## Tabela: Reparacoes

| Nome do Campo | Tipo de Dados | Obrigatório | Chave | Descrição |
|---------------|---------------|-------------|-------|-----------|
| Id | INTEGER | Sim | PK | Identificador único, auto-incrementável |
| EquipamentoId | INTEGER | Sim | FK | Chave estrangeira para Equipamentos.Id |
| TecnicoId | INTEGER | Não | FK | Chave estrangeira para Tecnicos.Id (opcional) |
| DataEntrada | DATETIME | Sim | | Data de entrada do equipamento para reparação |
| DescricaoAvaria | TEXT(1000) | Sim | | Descrição da avaria reportada pelo cliente |
| Diagnostico | TEXT(1000) | Não | | Diagnóstico técnico após avaliação |
| Estado | INTEGER | Sim | | Estado da reparação (enum: 0=AguardarDiagnostico, 1=EmReparacao, 2=AguardarPecas, 3=Concluida, 4=Entregue) |
| DataConclusao | DATETIME | Não | | Data em que a reparação foi concluída |
| DataEntrega | DATETIME | Não | | Data de entrega do equipamento ao cliente |
| Observacoes | TEXT(1000) | Não | | Observações adicionais |

## Tabela: Intervencoes

| Nome do Campo | Tipo de Dados | Obrigatório | Chave | Descrição |
|---------------|---------------|-------------|-------|-----------|
| Id | INTEGER | Sim | PK | Identificador único, auto-incrementável |
| ReparacaoId | INTEGER | Sim | FK | Chave estrangeira para Reparacoes.Id |
| DataIntervencao | DATETIME | Sim | | Data em que a intervenção foi realizada |
| DescricaoTrabalho | TEXT(1000) | Sim | | Descrição detalhada do trabalho executado |
| PecasUtilizadas | TEXT(1000) | Não | | Lista de peças utilizadas |
| TempoTrabalho | DECIMAL(18,2) | Sim | | Tempo gasto na intervenção (horas) |
| CustoMaoObra | DECIMAL(18,2) | Sim | | Custo monetário da mão de obra |
| CustoPecas | DECIMAL(18,2) | Sim | | Custo monetário das peças |
| Observacoes | TEXT(1000) | Não | | Observações adicionais |

## Estados de Reparação (Enum)

| Valor | Nome | Descrição |
|-------|------|-----------|
| 0 | AguardarDiagnostico | Aguardando diagnóstico inicial |
| 1 | EmReparacao | Equipamento em reparação |
| 2 | AguardarPecas | Aguardando chegada de peças |
| 3 | Concluida | Reparação concluída |
| 4 | Entregue | Equipamento entregue ao cliente |

## Considerações sobre Valores Monetários

- Os campos `TempoTrabalho`, `CustoMaoObra` e `CustoPecas` utilizam o tipo `DECIMAL(18,2)` para garantir precisão em valores financeiros.
- No SQLite, estes são armazenados como TEXT, mas o Entity Framework Core trata-os como `decimal` em C#.
- Não utilizamos `float` ou `double` para evitar problemas de precisão em cálculos financeiros.