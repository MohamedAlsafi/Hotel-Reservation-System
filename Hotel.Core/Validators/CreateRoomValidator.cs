using FluentValidation;
using Hotel.Core.Dtos.Room.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Validators
{
    public class CreateRoomValidator : AbstractValidator<CreateRoomDTO>
    {
        public CreateRoomValidator()
        {
            RuleFor(r => r.RoomNumber)
                .NotEmpty().WithMessage("Room number is required.")
                .MaximumLength(10).WithMessage("Room number cannot exceed 10 characters.");

            RuleFor(r => r.Price)
                .GreaterThan(100).WithMessage("Room price must be greater than 100.");

            //RuleFor(r => r.FacilityIds)
            //.NotNull().WithMessage("Facilities are required.")
            //.Must(f => f.Count > 0).WithMessage("At least one facility must be selected.");
        }

    }
}
