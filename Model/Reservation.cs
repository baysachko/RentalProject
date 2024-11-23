namespace WinFormsApp1.Model
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public int ResidentID { get; set; }
        public int RoomID { get; set; }
    }
}