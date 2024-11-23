namespace WinFormsApp1.Model
{
    public class Rent
    {
        public int RentID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal RentAmount { get; set; }
        public int ResidentID { get; set; }
        public int RoomID { get; set; }
        public string ResidentName { get; set; }
        public string RoomNumber { get; set; }
    }
}