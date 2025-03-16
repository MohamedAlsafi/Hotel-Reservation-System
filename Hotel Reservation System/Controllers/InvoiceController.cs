using Hotel.Core.Entities.CustomerEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GenerateInvoice([FromBody] HotelInvoice invoice)
        {
            if (invoice == null)
            {
                return BadRequest("Invoice data is required.");
            }

            invoice.SubTotal = invoice.NumberOfNights * invoice.NightlyRate +
                               invoice.AdditionalServices.Sum(service => service.Amount);
            invoice.Tax = invoice.SubTotal * 0.1m; // Assuming 10% tax rate
            invoice.TotalAmount = invoice.SubTotal + invoice.Tax;

            return Ok(new
            {
                Message = "Hotel invoice generated successfully!",
                Invoice = invoice
            });
        }
    }
}
