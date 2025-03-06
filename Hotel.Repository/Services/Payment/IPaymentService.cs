using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Services.Payment
{
    public interface IPaymentService
    {
        Task<bool> MakePaymentAsync();
         
    }
}
