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
        [HttpPost("GenerateInvoice")]
        public async Task<ResponseViewModel<HotelInvoiceViewModel>> GenerateInvoice( HotelInvoiceDto invoice)
        {
            if (invoice is null)
            {
                return new ResponseViewModel<HotelInvoiceViewModel>(
                    success: false,
                    message: "Invoice data is required.",
                    data: null!,
                    errorCode: null
                );
            }

            await Task.Run(() =>
            {
                invoice.SubTotal = invoice.NumberOfNights * invoice.NightlyRate + invoice.AdditionalServices.Sum(service => service.Amount);
                invoice.Tax = invoice.SubTotal * 0.1m;        // Assuming 10% tax rate  
                invoice.TotalAmount = invoice.SubTotal + invoice.Tax;
            });

            return new ResponseViewModel<HotelInvoiceViewModel>(
                success: true,
                message: "Hotel invoice generated successfully!",
                data: new HotelInvoiceViewModel
                {
                    InvoiceNumber = invoice.InvoiceNumber,
                    SubTotal = invoice.SubTotal,
                    Tax = invoice.Tax,
                    TotalAmount = invoice.TotalAmount
                },
                errorCode: null
            );
        }
    }
}
