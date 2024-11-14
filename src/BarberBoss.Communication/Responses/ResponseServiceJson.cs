using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Responses
{
    public class ResponseServiceJson
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Comment { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
