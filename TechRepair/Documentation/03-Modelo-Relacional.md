# 03 - Modelo Relacional

## Tabelas

### 1. Clientes
| Campo | Tipo | Chave | Descrição |
|-------|------|-------|-----------|
| Id | INTEGER | PK | Identificador único do cliente |
| Nome | TEXT(200) | | Nome completo do cliente |
| NIF | TEXT(20) | UK | Número de Identificação Fiscal |
| Email | TEXT(200) | UK | Endereço de email |
| Telefone | TEXT(50) | | Número de telefone |
| Morada | TEXT(300) | | Morada completa |
| CodigoPostal | TEXT(20) | | Código postal |
| Localidade | TEXT(100) | | Localidade |
| DataRegisto | TEXT | | Data de registo do cliente |

### 2. Tecnicos
| Campo | Tipo | Chave | Descrição |
|-------|------|-------|-----------|
| Id | INTEGER | PK | Identificador único do técnico |
| Nome | TEXT(200) | | Nome completo do técnico |
| Email | TEXT(200) | UK | Endereço de email |
| Telefone | TEXT(50) | | Número de telefone |
| Especialidade | TEXT(100) | | Área de especialização |
| Ativo | INTEGER (boolean) | | Estado ativo/inativo do técnico |

### 3. Equipamentos
| Campo | Tipo | Chave | Descrição |
|-------|------|-------|-----------|
| Id | INTEGER | PK | Identificador único do equipamento |
| ClienteId | INTEGER | FK | Referência ao cliente proprietário |
| TipoEquipamento | TEXT(100) | | Tipo de equipamento (ex: Portátil, Impressora) |
| Marca | TEXT(100) | | Marca do equipamento |
| Modelo | TEXT(100) | | Modelo do equipamento |
| NumeroSerie | TEXT(100) | UK | Número de série |
| Descricao | TEXT(500) | | Descrição detalhada |
| DataRegisto | TEXT | | Data de registo do equipamento |

### 4. Reparacoes
| Campo | Tipo | Chave | Descrição |
|-------|------|-------|-----------|
| Id | INTEGER | PK | Identificador único da reparação |
| EquipamentoId | INTEGER | FK | Referência ao equipamento |
| TecnicoId | INTEGER | FK | Referência ao técnico responsável (opcional) |
| DataEntrada | TEXT | | Data de entrada do equipamento |
| DescricaoAvaria | TEXT(1000) | | Descrição da avaria reportada |
| Diagnostico | TEXT(1000) | | Diagnóstico técnico |
| Estado | INTEGER | | Estado da reparação (enum) |
| DataConclusao | TEXT | | Data de conclusão da reparação |
| DataEntrega | TEXT | | Data de entrega ao cliente |
| Observacoes | TEXT(1000) | | Observações adicionais |

### 5. Intervencoes
| Campo | Tipo | Chave | Descrição |
|-------|------|-------|-----------|
| Id | INTEGER | PK | Identificador único da intervenção |
| ReparacaoId | INTEGER | FK | Referência à reparação |
| DataIntervencao | TEXT | | Data da intervenção |
| DescricaoTrabalho | TEXT(1000) | | Descrição do trabalho realizado |
| PecasUtilizadas | TEXT(1000) | | Peças utilizadas na intervenção |
| TempoTrabalho | DECIMAL(18,2) | | Tempo de trabalho em horas |
| CustoMaoObra | DECIMAL(18,2) | | Custo da mão de obra |
| CustoPecas | DECIMAL(18,2) | | Custo das peças |
| Observacoes | TEXT(1000) | | Observações adicionais |

## Chaves Primárias

| Tabela | Coluna PK | Tipo |
|--------|-----------|------|
| Clientes | Id | INTEGER (AUTOINCREMENT) |
| Tecnicos | Id | INTEGER (AUTOINCREMENT) |
| Equipamentos | Id | INTEGER (AUTOINCREMENT) |
| Reparacoes | Id | INTEGER (AUTOINCREMENT) |
| Intervencoes | Id | INTEGER (AUTOINCREMENT) |

## Chaves Estrangeiras

| Tabela Origem | Coluna FK | Tabela Destino | Coluna PK | Ação DELETE |
|---------------|-----------|----------------|-----------|-------------|
| Equipamentos | ClienteId | Clientes | Id | RESTRICT |
| Reparacoes | EquipamentoId | Equipamentos | Id | RESTRICT |
| Reparacoes | TecnicoId | Tecnicos | Id | SET NULL |
| Intervencoes | ReparacaoId | Reparacoes | Id | RESTRICT |

## Relações

1. **Cliente 1:N Equipamento** - Um cliente possui vários equipamentos. Um equipamento pertence a um cliente.
2. **Equipamento 1:N Reparação** - Um equipamento gera várias reparações ao longo do tempo. Uma reparação diz respeito a um equipamento.
3. **Tecnico 1:N Reparação** - Um técnico executa várias reparações. Uma reparação é atribuída a (no máximo) um técnico.
4. **Reparação 1:N Intervenção** - Uma reparação contém várias intervenções técnicas. Uma intervenção pertence a uma reparação.

## Índices Únicos

| Tabela | Coluna(s) |
|--------|-----------|
| Clientes | NIF (único) |
| Clientes | Email (único) |
| Tecnicos | Email (único) |
| Equipamentos | NumeroSerie (único) |