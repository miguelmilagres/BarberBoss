using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories;
using ClosedXML.Excel;

namespace BarberBoss.Application.Services.Reports.Excel;
public class GenerateServicesReportExcelUseCase : IGenerateServicesReportExcelUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IServicesReadOnlyRepository _repository;

    public GenerateServicesReportExcelUseCase(IServicesReadOnlyRepository repository)
    {
        _repository = repository;
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var services = await _repository.FilterByMonth(month);

        if (services.Count == 0)
            return [];

        using var workbook = new XLWorkbook();

        workbook.Author = "Miguel M. de Macedo";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);

        int raw = 2;
        foreach (Service service in services)
        {
            worksheet.Cell($"A{raw}").Value = service.Title;
            worksheet.Cell($"B{raw}").Value = service.Date;
            worksheet.Cell($"C{raw}").Value = ConvertPaymentType(service.PaymentType);
            worksheet.Cell($"D{raw}").Value = service.Price;
            worksheet.Cell($"D{raw}").Style.NumberFormat.Format = $"{CURRENCY_SYMBOL} #,##0.00";
            worksheet.Cell($"E{raw}").Value = service.Comment;

            raw++;
        }

        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.PRICE;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.COMMENT;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;

        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#205858");
        worksheet.Cells("A1:E1").Style.Font.FontColor = XLColor.White;

        worksheet.Cells("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cells("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cells("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cells("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cells("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
    }

    private string ConvertPaymentType(PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => ResourceReportGenerationMessages.CASH,
            PaymentType.CreditCard => ResourceReportGenerationMessages.CREDIT_CARD,
            PaymentType.DebitCard => ResourceReportGenerationMessages.DEBIT_CARD,
            PaymentType.EletronicTransfer => ResourceReportGenerationMessages.ELETRONIC_TRANSFER,
            _ => string.Empty
        };
    }
}
