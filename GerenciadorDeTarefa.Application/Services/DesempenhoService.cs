using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using GerenciadorDeTarefa.Application.Constants;
using GerenciadorDeTarefa.Application.Dtos.Relatorios;
using GerenciadorDeTarefa.Application.Services.Interfaces;
using GerenciadorDeTarefa.Domain.Configurations;
using GerenciadorDeTarefa.Domain.Entities.Projetos;
using GerenciadorDeTarefa.Domain.Entities.Tarefas;
using GerenciadorDeTarefa.Domain.Enums.Tarefas;
using GerenciadorDeTarefa.Domain.Enums.Usuarios;
using GerenciadorDeTarefa.Domain.Helpers;
using GerenciadorDeTarefa.Domain.Interfaces.Repositories;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GerenciadorDeTarefa.Application.Services
{
    public class DesempenhoService(IOptions<ReportConfiguration> reportConfiguration,
        IUsuarioRepository usuarioRepository,
        IProjetoRepository projetoRepository) : IDesempenhoService
    {
        public async Task<FileContentResult> GerarRelatorioDeTarefas(FiltroRelatorioTarefaDto filtroDto, CancellationToken token)
        {
            var usuarioGerente = await usuarioRepository.FindOneBy(x => x.Id == filtroDto.IdUsuarioGerente.ToObjectId(), token);
            if (usuarioGerente is null || usuarioGerente.Funcao != Funcao.Gerente)
                throw new Exception(ValidationError.UsuarioNaoLocalizadoOuSemPermissaoRelatorio);

            var filtro = PredicateBuilder.New<Tarefa>(true);
            if (filtroDto.DataFinal != DateTime.MinValue)
                filtro.And(x => x.DataInicio >= filtroDto.DataInicial);

            if (filtroDto.Status != null)
                filtro.And(x => x.Status == filtroDto.Status);

            if (filtroDto.DataFinal != DateTime.MinValue)
                filtro.And(x => x.DataFim <= filtroDto.DataFinal);

            if (!string.IsNullOrWhiteSpace(filtroDto.IdUsuario))
                filtro.And(x => x.Usuario.Id == filtroDto.IdUsuario.ToObjectId());

            var tarefas = await projetoRepository.FilterTarefasBy(filtro);
            if (tarefas.Count == 0) return default;

            using var workbook = new XLWorkbook();
            var tarefasPorUsuario = tarefas.GroupBy(t => new { t.Usuario.Id, t.Usuario.Acesso });
            foreach (var registroTarefas in tarefasPorUsuario)
            {
                var line = 0;
                var sheet = workbook.Worksheets.Add(registroTarefas.Key.Acesso);

                GenerateHeader(sheet, line += 1);

                foreach (var registroTarefa in registroTarefas)
                    GenerateRow(sheet, registroTarefa, line += 1);

                GenerateFooter(sheet, line += 1, registroTarefas);

                foreach (var item in sheet.ColumnsUsed())
                    item.AdjustToContents();
            }

            var fileName = reportConfiguration.Value.FullPath(
                DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"), reportConfiguration.Value.RelatorioTarefa);
            workbook.SaveAs(fileName);
            return new FileContentResult(File.ReadAllBytes(fileName), MimeTypeHelper.BuscarMimeTypePorExtencao(".xlsx")) { FileDownloadName = reportConfiguration.Value.RelatorioTarefa };
        }
        private void GenerateHeader(IXLWorksheet sheet, int line)
        {
            sheet.Cell(1.GetColumnPosition(line)).Value = nameof(Tarefa);
            sheet.Cell(2.GetColumnPosition(line)).Value = nameof(Tarefa.Status);
            sheet.Cell(3.GetColumnPosition(line)).Value = nameof(Tarefa.Usuario);
            sheet.Cell(4.GetColumnPosition(line)).Value = nameof(Tarefa.DataInicio);
            sheet.Cell(5.GetColumnPosition(line)).Value = nameof(Tarefa.DataFim);
            sheet.Cell(6.GetColumnPosition(line)).Value = nameof(Tarefa.Vencimento);
        }

        private void GenerateRow(IXLWorksheet sheet, Tarefa tarefa, int line)
        {
            sheet.Cell(1.GetColumnPosition(line)).Value = tarefa.Titulo;
            sheet.Cell(2.GetColumnPosition(line)).Value = tarefa.Status.ToString();
            sheet.Cell(3.GetColumnPosition(line)).Value = tarefa.Usuario.NomeCompleto;
            sheet.Cell(4.GetColumnPosition(line)).Value = tarefa.DataInicio.ToString("dd/MM/yyyy");
            sheet.Cell(5.GetColumnPosition(line)).Value = tarefa.DataFim.ToString("dd/MM/yyyy");
            sheet.Cell(6.GetColumnPosition(line)).Value = tarefa.Vencimento.ToString("dd/MM/yyyy");
        }

        private void GenerateFooter(IXLWorksheet sheet, int line, IEnumerable<Tarefa> tarefas)
        {
            line++;
            sheet.Cell(1.GetColumnPosition(line)).Value = $"Total Tarefas({nameof(Status.Pendente)})";
            sheet.Cell(2.GetColumnPosition(line)).Value = tarefas.Where(x => x.Status == Status.Pendente).Count();

            line++;
            sheet.Cell(1.GetColumnPosition(line)).Value = $"Total Tarefas({nameof(Status.EmAndamento)})";
            sheet.Cell(2.GetColumnPosition(line)).Value = tarefas.Where(x => x.Status == Status.EmAndamento).Count();

            line++;
            sheet.Cell(1.GetColumnPosition(line)).Value = $"Total Tarefas({nameof(Status.Concluida)})";
            sheet.Cell(2.GetColumnPosition(line)).Value = tarefas.Where(x => x.Status == Status.Concluida).Count();
        }
    }
}
