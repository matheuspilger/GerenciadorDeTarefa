using GerenciadorDeTarefa.Domain.Constants;
using GerenciadorDeTarefa.Domain.Entities.Comentarios;
using GerenciadorDeTarefa.Domain.Entities.Projetos;
using GerenciadorDeTarefa.Domain.Entities.Tarefas;
using GerenciadorDeTarefa.Domain.Entities.Usuarios;
using GerenciadorDeTarefa.Domain.Enums.Tarefas;
using GerenciadorDeTarefa.Domain.Enums.Usuarios;
using GerenciadorDeTarefa.Domain.Helpers;
using MongoDB.Bson;

namespace GerenciadorDeTarefa.UnitTests
{
    public class ProjetoTests
    {
        [Fact]
        public void AddOrUpdateTarefa_AddNewTarefa_ShouldAddSuccessfully()
        {
            // Arrange
            var projeto = new Projeto(
                id: ObjectId.GenerateNewId(),
                nome: "Projeto Teste",
                sobre: "Descrição do projeto",
                usuario: new Usuario(ObjectId.GenerateNewId(), true, "Usuário 1", "usuario1", "usuario2@teste.com", "1234", Funcao.Gerente)
            );

            var titulo = "Tarefa Teste";
            var descricao = "Descrição da tarefa teste";
            var vencimento = DateTime.Now.AddDays(5);
            var status = Status.Pendente;
            var prioridade = Prioridade.Media;
            var usuario = new Usuario(ObjectId.GenerateNewId(), true, "Usuário 2", "usuario2", "usuario2@teste.com", "1234", Funcao.Operador);
            var comentarios = new List<Comentario>();

            // Act
            var result = projeto.AddOrUpdateTarefa(
                string.Empty,
                titulo,
                descricao,
                vencimento,
                status,
                prioridade,
                usuario,
                comentarios
            );

            // Assert
            Assert.NotNull(result.Id.ToString());
            Assert.Single(projeto.Tarefas);
        }

        [Fact]
        public void AddOrUpdateTarefa_UpdateExistingTarefa_ShouldUpdateSuccessfully()
        {
            // Arrange
            var projeto = new Projeto(
                id: ObjectId.GenerateNewId(),
                nome: "Projeto Teste",
                sobre: "Descrição do projeto",
                usuario: new Usuario(ObjectId.GenerateNewId(), true, "Usuário 1", "usuario1", "usuario2@teste.com", "1234", Funcao.Gerente)
            );

            var titulo = "Tarefa Teste";
            var descricao = "Descrição da tarefa teste";
            var vencimento = DateTime.Now.AddDays(5);
            var status = Status.Pendente;
            var prioridade = Prioridade.Media;
            var usuario = new Usuario(ObjectId.GenerateNewId(), true, "Usuário 2", "usuario2", "usuario2@teste.com", "1234", Funcao.Operador);
            var comentarios = new List<Comentario>();

            // Act
            var tarefa = projeto.AddOrUpdateTarefa(
                string.Empty,
                titulo,
                descricao,
                vencimento,
                status,
                prioridade,
                usuario,
                comentarios
            );

            // Act
            var result = projeto.AddOrUpdateTarefa(
                tarefa.Id.ToString(),
                "Tarefa Atualizada",
                "Descrição atualizada",
                DateTime.Now.AddDays(10),
                Status.Concluida,
                Prioridade.Media,
                new Usuario(ObjectId.GenerateNewId(), true, "Usuário 3", "usuario3", "usuario3@teste.com", "1234", Funcao.Operador),
                []
            );

            // Assert
            Assert.NotNull(result.Id.ToString());
            Assert.Equal(tarefa.Id, result.Id);
            Assert.Single(projeto.Tarefas);
            Assert.Equal("Tarefa Atualizada", projeto.Tarefas.First().Titulo);
        }

        [Fact]
        public void RemoverTarefa_TarefaExists_ShouldRemoveSuccessfully()
        {
            // Arrange
            var projeto = new Projeto(
                id: ObjectId.GenerateNewId(),
                nome: "Projeto Teste",
                sobre: "Descrição do projeto",
                usuario: new Usuario(ObjectId.GenerateNewId(), true, "Usuário 2", "usuario2", "usuario2@teste.com", "1234", Funcao.Operador)
            );

            var tarefa = projeto.AddOrUpdateTarefa(
                string.Empty,
                "Tarefa Teste",
                "Descrição da tarefa",
                DateTime.Now.AddDays(5),
                Status.Pendente,
                Prioridade.Media,
                new Usuario(ObjectId.GenerateNewId(), true, "Usuário 3", "usuario3", "usuario3@teste.com", "1234", Funcao.Operador),
                []
            );

            // Act
            projeto.RemoverTarefa(tarefa.Id.ToString());

            // Assert
            Assert.Empty(projeto.Tarefas);
        }

        [Fact]
        public void IsDeletable_ProjectHasPendingTarefas_ShouldThrowException()
        {
            // Arrange
            var projeto = new Projeto(
                id: ObjectId.GenerateNewId(),
                nome: "Projeto Teste",
                sobre: "Descrição do projeto",
                usuario: new Usuario(ObjectId.GenerateNewId(), true, "Usuário 1", "usuario1", "usuario1@teste.com", "1234", Funcao.Gerente)
            );

            projeto.AddOrUpdateTarefa(
                ObjectId.GenerateNewId().ToString(),
                "Tarefa Pendente",
                "Descrição da tarefa",
                DateTime.Now.AddDays(5),
                Status.Pendente,
                Prioridade.Media,
                new Usuario(ObjectId.GenerateNewId(), true, "Usuário 2", "usuario2", "usuario2@teste.com", "1234", Funcao.Operador),
                []
            );

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => projeto.IsDeletable());
            Assert.Equal(ValidationError.DeletarProjeto, exception.Message);
        }

        [Fact]
        public void IsDeletable_NoPendingTarefas_ShouldReturnTrue()
        {
            // Arrange
            var projeto = new Projeto(
                id: ObjectId.GenerateNewId(),
                nome: "Projeto Teste",
                sobre: "Descrição do projeto",
                new Usuario(ObjectId.GenerateNewId(), true, "Usuário 1", "usuario1", "usuario1@teste.com", "1234", Funcao.Gerente)
            );

            projeto.AddOrUpdateTarefa(
                ObjectId.GenerateNewId().ToString(),
                "Tarefa Concluida",
                "Descrição da tarefa",
                DateTime.Now.AddDays(5),
                Status.Concluida,
                Prioridade.Baixa,
                new Usuario(ObjectId.GenerateNewId(), true, "Usuário 2", "usuario2", "usuario2@teste.com", "1234", Funcao.Operador),
                []
            );

            // Act
            var result = projeto.IsDeletable();

            // Assert
            Assert.True(result);
        }


        [Fact]
        public void AddOrUpdateTarefa_WhenTarefasCountIs20_ShouldThrowArgumentException()
        {
            // Arrange
            var projeto = new Projeto(
                id: ObjectId.GenerateNewId(),
                nome: "Projeto Teste",
                sobre: "Descrição do projeto",
                usuario: new Usuario(ObjectId.GenerateNewId(), true, "Usuário 1", "usuario1", "usuario1@teste.com", "1234", Funcao.Gerente)
            );

            for (int i = 0; i < 20; i++)
            {
                projeto.AddOrUpdateTarefa(
                    string.Empty,
                    $"Tarefa {i + 1}",
                    "Descrição da tarefa",
                    DateTime.Now.AddDays(i + 1),
                    Status.Pendente,
                    Prioridade.Baixa,
                    new Usuario(ObjectId.GenerateNewId(), true, "Usuário 2", "usuario2", "usuario2@teste.com", "1234", Funcao.Operador),
                    []
                );
            }

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                projeto.AddOrUpdateTarefa(
                    string.Empty,
                    "Tarefa Extra",
                    "Descrição da tarefa extra",
                    DateTime.Now.AddDays(21),
                    Status.Pendente,
                    Prioridade.Media,
                    new Usuario(ObjectId.GenerateNewId(), true, "Usuário 3", "usuario3", "usuario3@teste.com", "1234", Funcao.Operador),
                    []
                )
            );

            Assert.Equal(ValidationError.NumeroMaximoTarefa, exception.Message);
        }
    }
}
