namespace Hotel_Reservation_System.ViewModels.Room
{
    public class UpdateRoomImagesViewModel
    {
        public int RoomId { get; set; }
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
        public List<string> ExistingImageUrls { get; set; } = new List<string>();
    }
}
