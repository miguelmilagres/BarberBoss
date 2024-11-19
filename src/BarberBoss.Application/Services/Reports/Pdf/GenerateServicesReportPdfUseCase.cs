
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Application.Services.Reports.Pdf;
public class GenerateServicesReportPdfUseCase : IGenerateServicesReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IServicesReadOnlyRepository _repository;

    public GenerateServicesReportPdfUseCase(IServicesReadOnlyRepository repository)
    {
        _repository = repository;
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var services = await _repository.FilterByMonth(month);
        if (services.Count == 0)
            return [];

        return [];
    }
}
