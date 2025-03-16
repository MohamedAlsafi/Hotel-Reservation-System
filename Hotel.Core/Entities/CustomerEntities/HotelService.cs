namespace Hotel.Core.Entities.CustomerEntities
{
    public class HotelService
    {
        public int Id { get; set; } // Primary Key
        public string Description { get; set; }
        public decimal Amount { get; set; }

        // Foreign Key
        public int HotelInvoiceId { get; set; }
        public HotelInvoice HotelInvoice { get; set; }
    }
}