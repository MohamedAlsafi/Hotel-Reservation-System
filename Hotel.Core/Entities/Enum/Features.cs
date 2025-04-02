using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.Enum
{
    public enum Features
    {
        SignOut = 0,
        Login = 1,
        UpdateRoom = 2,
        DeleteRoom = 3,
        GetAllRooms = 4,
        GetRoomById = 5,
        UpdateRoomBasicInfo= 6,
        AddRoom =7,
        UpdateRoomImages =8,
        SearchForRoom = 9,
        GetAllOffers = 10,
        GetOfferById = 11,
        UpdateOffer = 12,
        DeleteOffer = 13,
        CreateOffer = 14,
        CreateReservation = 15,
        CancelReservation = 16,
        GetReservationById = 17,
        UpdateReservation = 18,
        AddFeedback = 19,
        GetAllFeedback = 20,
        GetFeedbackById = 21,
        RespondToFeedback = 22,
        GetFeedbackByCustomerId = 23,
        ProvideFeedbackFromSpecificCustomer = 24,
        AddFacility = 25,
        GetAllFacilities = 26,
        GetFacilityById = 27,
        UpdateFacility = 28,
        DeleteFacility = 29,
    }
}
