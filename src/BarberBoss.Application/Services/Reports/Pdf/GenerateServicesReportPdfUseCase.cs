using BarberBoss.Application.Services.Reports.Pdf.Fonts;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;

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

        return [];
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
}
