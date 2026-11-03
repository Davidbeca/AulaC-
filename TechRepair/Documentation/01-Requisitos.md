# 01 - Requisitos do Sistema TechRepair

## Requisitos Funcionais

### Gestão de Clientes
- RF01: Registar um novo cliente com nome, NIF, email, telefone, morada, código postal e localidade.
- RF02: Consultar a lista de clientes registados.
- RF03: Editar informações de um cliente existente.
- RF04: Eliminar um cliente (apenas se não tiver equipamentos associados).
- RF05: Pesquisar clientes por nome, NIF ou localidade.

### Gestão de Equipamentos
- RF06: Registar um novo equipamento associado a um cliente (tipo, marca, modelo, número de série, descrição).
- RF07: Consultar a lista de equipamentos.
- RF08: Editar informações de um equipamento.
- RF09: Eliminar um equipamento (apenas se não tiver reparações associadas).
- RF10: Listar equipamentos de um cliente específico.

### Gestão de Técnicos
- RF11: Registar um novo técnico (nome, email, telefone, especialidade, estado ativo/inativo).
- RF12: Consultar a lista de técnicos.
- RF13: Editar informações de um técnico.
- RF14: Desativar/ativar um técnico.
- RF15: Listar reparações atribuídas a um técnico.

### Gestão de Reparações
- RF16: Criar uma nova reparação associando um equipamento e (opcionalmente) um técnico.
- RF17: Consultar a lista de reparações com seus estados.
- RF18: Atualizar o estado de uma reparação (A aguardar diagnóstico, Em reparação, A aguardar peças, Concluída, Entregue ao cliente).
- RF19: Registar diagnóstico e data de conclusão.
- RF20: Registar data de entrega ao cliente.

### Gestão de Intervenções Técnicas
- RF21: Registar uma intervenção técnica numa reparação (data, descrição do trabalho, peças utilizadas, tempo de trabalho, custo mão de obra, custo peças).
- RF22: Consultar todas as intervenções de uma reparação.
- RF23: Calcular o custo total de uma reparação (mão de obra + peças).

### Relatórios e Consultas
- RF24: Listar reparações por estado.
- RF25: Listar reparações por técnico.
- RF26: Listar equipamentos por cliente.

## Requisitos Técnicos

- RT01: Aplicação desenvolvida em ASP.NET Core MVC com C#.
- RT02: Base de dados SQLite obrigatória (Entity Framework Core).
- RT03: Interface com Tabler UI e Bootstrap 5.
- RT04: Ambiente de desenvolvimento Visual Studio Code.
- RT05: .NET SDK estável (versão 10.x).
- RT06: Arquitetura organizada em pastas: Controllers, Models, ViewModels, Data, Services, Views, wwwroot, Documentation.
- RT07: Sem utilização de SQL Server ou outras bases de dados.

## Regras de Negócio

- RN01: O NIF de cada cliente deve ser único no sistema.
- RN02: O email de cada cliente deve ser único.
- RN03: O email de cada técnico deve ser único.
- RN04: O número de série de cada equipamento deve ser único.
- RN05: Um cliente só pode ser eliminado se não tiver equipamentos associados (proteção de histórico).
- RN06: Um equipamento só pode ser eliminado se não tiver reparações associadas.
- RN07: Um técnico só pode ser eliminado se não tiver reparações atribuídas (ou ser desativado em vez de eliminado).
- RN08: Uma reparação só pode ser eliminada se não tiver intervenções associadas.
- RN09: O estado inicial de uma reparação é "A aguardar diagnóstico".
- RN10: Os estados de reparação seguem um fluxo lógico: aguardar diagnóstico → em reparação → aguardar peças → concluída → entregue.
- RN11: Valores monetários (custo mão de obra, custo peças) são armazenados com precisão decimal.
- RN12: Não são permitidas eliminações em cascata que destruam históricos de dados.