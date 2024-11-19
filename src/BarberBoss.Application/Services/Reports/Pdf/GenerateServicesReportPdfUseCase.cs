using BarberBoss.Application.Services.Reports.Pdf.Colors;
using BarberBoss.Application.Services.Reports.Pdf.Fonts;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;
using BarberBoss.Domain.Extensions;
using PdfSharp.Drawing;

namespace BarberBoss.Application.Services.Reports.Pdf;
public class GenerateServicesReportPdfUseCase : IGenerateServicesReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private const int HEIGHT_ROW_SERVICE_TABLE = 25;
    private readonly IServicesReadOnlyRepository _repository;

    public GenerateServicesReportPdfUseCase(IServicesReadOnlyRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new ServicesReportFontResolver();
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var services = await _repository.FilterByMonth(month);
        if (services.Count == 0)
            return [];

        var document = CreateDocument(month);
        var page = CreatePage(document);

        CreateHeaderWithProfilePhotoAndName(page);

        var totalServices = services.Sum(service => service.Price);
        CreateTotalGainSection(page, month, totalServices);

        foreach (var service in services)
        {
            var table = CreateServiceTable(page);

            var row = table.AddRow();
            row.Height = HEIGHT_ROW_SERVICE_TABLE;
            AddServiceTitle(row.Cells[0], service.Title);
            AddHeaderForPrice(row.Cells[3]);

            row = table.AddRow();
            row.Height = HEIGHT_ROW_SERVICE_TABLE;
            row.Cells[0].AddParagraph(service.Date.ToString("D"));
            SetStyleBaseForServiceInformation(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 20;

            row.Cells[1].AddParagraph(service.Date.ToString("t"));
            SetStyleBaseForServiceInformation(row.Cells[1]);

            row.Cells[2].AddParagraph(service.PaymentType.PaymentTypeToString());
            SetStyleBaseForServiceInformation(row.Cells[2]);

            AddPriceForService(row.Cells[3], service.Price);

            if (string.IsNullOrWhiteSpace(service.Comment) == false)
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = HEIGHT_ROW_SERVICE_TABLE;
                descriptionRow.Cells[0].AddParagraph(service.Comment);
                descriptionRow.Cells[0].Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
                descriptionRow.Cells[0].Shading.Color = ColorsHelper.GRAY_LIGHT;
                descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                descriptionRow.Cells[0].MergeRight = 2;
                descriptionRow.Cells[0].Format.LeftIndent = 20;
                row.Cells[3].MergeDown = 1;
            }

            AddWhiteSpace(table);
        }

        return RenderDocument(document);
    }

    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();

        document.Info.Title = $"{ResourceReportGenerationMessages.SERVICES_FOR} {month:Y}";
        document.Info.Author = "Miguel de Macedo";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;

        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }


    private void CreateHeaderWithProfilePhotoAndName(Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        var pathFile = Path.Combine(directoryName!, "Logo", "profile-photo.png");

        row.Cells[0].AddImage(pathFile);

        row.Cells[1].AddParagraph("Barber Boss");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
    }

    private void CreateTotalGainSection(Section page, DateOnly month, decimal totalServices)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";

        var title = string.Format(ResourceReportGenerationMessages.TOTAL_GAIN_IN, month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });

        paragraph.AddLineBreak();
        
        paragraph.AddFormattedText($"{CURRENCY_SYMBOL} {totalServices}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }

    private Table CreateServiceTable(Section page)
    {
        var table = page.AddTable();
        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;
        return table;
    }

    private void AddServiceTitle(Cell cell, string serviceTitle)
    {
        cell.AddParagraph(serviceTitle);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.GREEN_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 20;
    }
    private void AddHeaderForPrice(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerationMessages.PRICE);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }
    private void SetStyleBaseForServiceInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.GRAY_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }
    private void AddPriceForService(Cell cell, decimal price)
    {
        cell.AddParagraph($"{CURRENCY_SYMBOL} {price}");
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }
    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }
}
