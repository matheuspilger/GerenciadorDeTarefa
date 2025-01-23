# GerenciadorDeTarefa

# Documentação da API

Esta API é parte do Gerenciador de Tarefa e fornece endpoints para gerenciar tarefas, projetos e gerar relatórios de desempenho.

## Endpoints

### **DesempenhoController**

Controlador responsável por relatórios de desempenho relacionados a tarefas.

- **GET /api/Desempenho/GerarRelatorioTarefas**
  - **Descrição:** Gera um relatório de tarefas com base nos parâmetros fornecidos.
  - **Parâmetros de Query:**
    - `GerarRelatorioTarefaCommand.Dto` (obrigatório): Comando contendo os filtros para o relatório.
      - **Propriedades:**
        - `Status? Status`: Status opcional da tarefa.
        - `DateTime DataInicial`: Data inicial do período a ser considerado.
        - `DateTime DataFinal`: Data final do período a ser considerado.
        - `string IdUsuario`: Identificador do usuário.
        - `string IdUsuarioGerente`: Identificador do gerente responsável.
  - **Resposta:**
    - Status 200 com o relatório gerado.
    - Status 400 para erros de validação.

### **ProjetoController**

Controlador para gerenciamento de projetos.

- **POST /api/Projeto/AddOrUpdateProjeto**
  - **Descrição:** Adiciona ou atualiza um projeto.
  - **Body:**
    - `AddOrUpdateProjetoCommand.Dto` (obrigatório): Comando contendo os dados do projeto a ser adicionado ou atualizado.
      - **Propriedades:**
        - `string Id`: Identificador do projeto.
        - `string Nome`: Nome do projeto.
        - `string Sobre`: Descrição sobre o projeto.
        - `string IdUsuario`: Identificador do usuário responsável.
  - **Resposta:**
    - Status 200 ao sucesso.
    - Status 400 para erros de validação.

- **DELETE /api/Projeto/DeleteProjeto**
  - **Descrição:** Exclui um projeto existente.
  - **Parâmetros de Query:**
    - `DeleteProjetoCommand.Dto` (obrigatório): Comando identificando o projeto a ser excluído.
      - **Propriedades:**
        - `string IdProjeto`: Identificador do projeto a ser excluído.
  - **Resposta:**
    - Status 200 ao sucesso.
    - Status 404 se o projeto não for encontrado.

- **GET /api/Projeto/GetProjetos**
  - **Descrição:** Recupera uma lista de projetos.
  - **Parâmetros de Query:**
    - `GetProjetoCommand.Dto` (obrigatório): Filtros para a busca de projetos.
      - **Propriedades:**
        - `string IdUsuario`: Identificador do usuário para filtrar os projetos.
  - **Resposta:**
    - Status 200 com a lista de projetos.
    - Status 400 para erros de validação.

### **TarefaController**

Controlador para gerenciamento de tarefas.

- **POST /api/Tarefa/AddOrUpdateTarefa**
  - **Descrição:** Adiciona ou atualiza uma tarefa.
  - **Body:**
    - `AddOrUpdateTarefaCommand.Dto` (obrigatório): Comando contendo os dados da tarefa a ser adicionada ou atualizada.
      - **Propriedades:**
        - `string Id`: Identificador da tarefa.
        - `string IdProjeto`: Identificador do projeto associado.
        - `string Titulo`: Título da tarefa.
        - `string Descricao`: Descrição detalhada da tarefa.
        - `DateTime Vencimento`: Data de vencimento da tarefa.
        - `Status Status`: Status atual da tarefa.
        - `Prioridade Prioridade`: Prioridade da tarefa.
        - `List<ComentarioDto> Comentarios`: Lista de comentários associados à tarefa.
        - `string IdUsuario`: Identificador do usuário criador.
        - `string IdUsuarioModificacao`: Identificador do usuário responsável pela última modificação.
  - **Resposta:**
    - Status 200 ao sucesso.
    - Status 400 para erros de validação.

- **DELETE /api/Tarefa/DeleteTarefa**
  - **Descrição:** Exclui uma tarefa existente.
  - **Parâmetros de Query:**
    - `DeleteTarefaCommand.Dto` (obrigatório): Comando identificando a tarefa a ser excluída.
      - **Propriedades:**
        - `string Id`: Identificador da tarefa.
        - `string IdProjeto`: Identificador do projeto associado à tarefa.
  - **Resposta:**
    - Status 200 ao sucesso.
    - Status 404 se a tarefa não for encontrada.

- **GET /api/Tarefa/GetTarefas**
  - **Descrição:** Recupera uma lista de tarefas.
  - **Parâmetros de Query:**
    - `GetTarefaCommand.Dto` (obrigatório): Filtros para a busca de tarefas.
      - **Propriedades:**
        - `string IdProjeto`: Identificador do projeto para filtrar as tarefas.
  - **Resposta:**
    - Status 200 com a lista de tarefas.
    - Status 400 para erros de validação.

## Tecnologias
- **RabbitMQ**:
  - Gerenciamento de mensagens assíncronas.
  - Comunicação entre microserviços.

- **MongoDB**:
  - Armazenamento de dados não estruturados.
  - Conexão com a aplicação utilizando MongoDB Driver.

- **Docker**:
  - Configuração de containers para ambientes de desenvolvimento e produção.
  - Facilita a execução isolada de serviços.

- **FluentAPI**:
  - Configuração de modelos de dados e relacionamentos.
  - Criação de migrações e mapeamento de objetos para banco de dados.

- **ASP.NET Core**:
  - Desenvolvimento de APIs RESTful e aplicações web.
  - Framework leve e modular para criação de soluções de alta performance.

- **MediatR**:
  - Implementação do padrão Mediator para facilitar a comunicação entre componentes sem dependências diretas.
  - Facilita a utilização de CQRS (Command Query Responsibility Segregation).

---

Para mais informações, consulte a documentação detalhada do código ou entre em contato.
