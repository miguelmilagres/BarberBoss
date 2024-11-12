using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Requests
{
    public class RequestRegisterServiceJson
    {
        public string Title { get; set; } = string.Empty;
        public string? Comment { get; set; }
        public PaymentType PaymentType { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
