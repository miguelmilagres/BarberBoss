namespace BarberBoss.Application.Services.Reports.Excel;
public interface IGenerateServicesReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
