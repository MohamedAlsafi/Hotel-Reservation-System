using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.Enum
{
    public enum ErrorCode
    {
        None = 0,
        // General errors (1-99)
        GenericError = 1,
        ValidationError = 2,
        NotFound = 3,
        Unauthorized = 4,
        Forbidden = 5,
        BadRequest = 6,
        Conflict = 7,
        ServerError = 8,
        ServiceUnavailable = 9,
        // Authentication errors (100-199)
        InvalidCredentials = 100,
        AccountLocked = 101,
        AccountDisabled = 102,
        SessionExpired = 103,
        InvalidToken = 104,
        TwoFactorRequired = 105,
        PasswordExpired = 106,

        // User management errors (200-299)
        UserNotFound = 200,
        DuplicateUsername = 201,
        DuplicateEmail = 202,
        InvalidUserData = 203,
        PasswordTooWeak = 204,
        EmailVerificationRequired = 205,


        // Reservation errors (300-399)
        ReservationNotFound = 300,
        ReservationConflict = 301,
        NoRoomsAvailable = 302,
        InvalidDateRange = 303,
        MinimumStayNotMet = 304,
        MaximumStayExceeded = 305,
        ReservationCancellationFailed = 306,
        AdvanceBookingRequired = 307,
        ReservationModificationDeadlinePassed = 308,
        GuestLimitExceeded = 309,


        // Room errors (400-499)
        RoomNotFound = 400,
        RoomTypeUnavailable = 401,
        RoomAlreadyOccupied = 402,
        RoomUnderMaintenance = 403,
        RoomNotClean = 404,
        RoomFeatureUnavailable = 405,


        // Payment errors (500-599)
        PaymentFailed = 500,
        PaymentDeclined = 501,
        InvalidPaymentMethod = 502,
        InsufficientFunds = 503,
        RefundFailed = 504,
        PaymentAlreadyProcessed = 505,
        DepositRequired = 506,


        // Guest errors (600-699)
        GuestNotFound = 600,
        GuestCheckInFailed = 601,
        GuestCheckOutFailed = 602,
        GuestBlacklisted = 603,
        InvalidGuestInformation = 604,
        MinimumAgeRequirement = 605,


        // Inventory and services errors (700-799)
        InventoryItemNotAvailable = 700,
        AmenityNotAvailable = 702,
        ExceedsServiceCapacity = 703,
        OutsideServiceHours = 704,
        SpecialRequestCannotBeFulfilled = 705,


        // Rate and pricing errors (800-899)
        InvalidRateCode = 800,
        PromotionExpired = 801,
        PromotionInvalid = 802,
        RateUnavailable = 803,
        MinimumRateNotMet = 804,
        DiscountNotApplicable = 805,
        

        // Feedback and Review errors (1000-1099)
        FeedbackNotFound = 1000,
        DuplicateFeedback = 1001,
        FeedbackSubmissionFailed = 1002,
        InvalidRating = 1003,
        ReviewNotAllowed = 1004,
        ReviewPeriodExpired = 1005,
        ResponseTooLong = 1006,
        InvalidReviewContent = 1007,
        ReviewModificationForbidden = 1008,
        ReviewDeletionForbidden = 1009,
        NoStayHistoryForReview = 1010,
        ReviewAlreadyResponded = 1011,
        ResponseEditForbidden = 1012,
        ReviewRejected = 1013,
        ReviewPendingModeration = 1014,
        ReviewContainsProhibitedContent = 1015,
        ImageUploadFailed = 1016,
        TooManyImages = 1017,
        InvalidImageFormat = 1018
    }
}
