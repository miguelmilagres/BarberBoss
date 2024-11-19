using BarberBoss.Application.Services.Reports.Pdf.Fonts;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace BarberBoss.Application.Services.Reports.Pdf;
public class GenerateServicesReportPdfUseCase : IGenerateServicesReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
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
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
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
}
