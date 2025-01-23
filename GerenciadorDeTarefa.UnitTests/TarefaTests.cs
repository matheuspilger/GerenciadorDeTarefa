using GerenciadorDeTarefa.Domain.Constants;
using GerenciadorDeTarefa.Domain.Entities.Comentarios;
using GerenciadorDeTarefa.Domain.Entities.Tarefas;
using GerenciadorDeTarefa.Domain.Entities.Usuarios;
using GerenciadorDeTarefa.Domain.Enums.Tarefas;
using GerenciadorDeTarefa.Domain.Enums.Usuarios;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefa.UnitTests
{
    public class TarefaTests
    {
        [Fact]
        public void Tarefa_Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var id = ObjectId.GenerateNewId();
            var titulo = "Título Teste";
            var descricao = "Descrição Teste";
            var vencimento = DateTime.Now.AddDays(5);
            var status = Status.Pendente;
            var prioridade = Prioridade.Alta;
            var usuario = new Usuario(ObjectId.GenerateNewId(), true, "Usuário 1", "usuario1", "usuario1@teste.com", "1234", Funcao.Gerente);
            var comentarios = new List<Comentario>();

            // Act
            var tarefa = new Tarefa(id, titulo, descricao, vencimento, status, prioridade, usuario, comentarios);

            // Assert
            Assert.Equal(id, tarefa.Id);
            Assert.Equal(titulo, tarefa.Titulo);
            Assert.Equal(descricao, tarefa.Descricao);
            Assert.Equal(vencimento, tarefa.Vencimento);
            Assert.Equal(status, tarefa.Status);
            Assert.Equal(prioridade, tarefa.Prioridade);
            Assert.Equal(usuario, tarefa.Usuario);
            Assert.Equal(comentarios, tarefa.Comentarios);
        }

        [Fact]
        public void Update_WhenPrioridadeChanges_ShouldThrowArgumentException()
        {
            // Arrange
            var id = ObjectId.GenerateNewId();
            var tarefa = new Tarefa(
                id,
                "Título Original",
                "Descrição Original",
                DateTime.Now.AddDays(5),
                Status.Pendente,
                Prioridade.Media,
                new Usuario(ObjectId.GenerateNewId(), true, "Usuário 1", "usuario1", "usuario1@teste.com", "1234", Funcao.Gerente),
                []
            );

            var novaPrioridade = Prioridade.Alta;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                tarefa.Update(
                    id,
                    "Título Atualizado",
                    "Descrição Atualizada",
                    DateTime.Now.AddDays(10),
                    Status.EmAndamento,
                    novaPrioridade,
                    new Usuario(ObjectId.GenerateNewId(), true, "Usuário 2", "usuario2", "usuario2@teste.com", "1234", Funcao.Operador),
                    []
                )
            );

            Assert.Equal(ValidationError.AtualizarPrioridadeTarefa, exception.Message);
        }

        [Fact]
        public void Update_ValidData_ShouldUpdateProperties()
        {
            // Arrange
            var id = ObjectId.GenerateNewId();
            var tarefa = new Tarefa(
                id,
                "Título Original",
                "Descrição Original",
                DateTime.Now.AddDays(5),
                Status.Pendente,
                Prioridade.Media,
                new Usuario(ObjectId.GenerateNewId(), true, "Usuário 1", "usuario1", "usuario1@teste.com", "1234", Funcao.Gerente),
                []
            );

            var novoId = ObjectId.GenerateNewId();
            var novoTitulo = "Título Atualizado";
            var novaDescricao = "Descrição Atualizada";
            var novoVencimento = DateTime.Now.AddDays(10);
            var novoStatus = Status.Concluida;
            var novaPrioridade = Prioridade.Media; // Mantendo a prioridade original
            var novoUsuario = new Usuario(ObjectId.GenerateNewId(), true, "Usuário 2", "usuario2", "usuario2@teste.com", "1234", Funcao.Operador);
            var novosComentarios = new List<Comentario>();

            // Act
            tarefa.Update(novoId, novoTitulo, novaDescricao, novoVencimento, novoStatus, novaPrioridade, novoUsuario, novosComentarios);

            // Assert
            Assert.Equal(novoId, tarefa.Id);
            Assert.Equal(novoTitulo, tarefa.Titulo);
            Assert.Equal(novaDescricao, tarefa.Descricao);
            Assert.Equal(novoVencimento, tarefa.Vencimento);
            Assert.Equal(novoStatus, tarefa.Status);
            Assert.Equal(novaPrioridade, tarefa.Prioridade);
            Assert.Equal(novoUsuario, tarefa.Usuario);
            Assert.Equal(novosComentarios, tarefa.Comentarios);
        }
    }
}
