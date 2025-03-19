using Hotel.Core.Dtos;
using Hotel.Core.Entities.CustomerEntities;
using Hotel_Reservation_System.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<HotelInvoiceViewModel>> GenerateInvoice([FromBody] HotelInvoiceDto invoice)
        {
            if (invoice is null) return BadRequest("Invoice data is required.");

            await Task.Run(() =>
            {
                invoice.SubTotal = invoice.NumberOfNights * invoice.NightlyRate + invoice.AdditionalServices.Sum(service => service.Amount);
                invoice.Tax = invoice.SubTotal * 0.1m;        // Assuming 10% tax rate  
                invoice.TotalAmount = invoice.SubTotal + invoice.Tax;
            });

            return Ok(new
            {
                Message = "Hotel invoice generated successfully!",
                Invoice = invoice
            });

        }
    }
}
