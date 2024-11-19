namespace BarberBoss.Application.Services.Reports.Pdf;
public interface IGenerateServicesReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
