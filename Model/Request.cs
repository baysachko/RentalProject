public class Request
{
    public int RequestID { get; set; }
    public DateTime RequestDate { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public int ResidentID { get; set; }
    public int ServiceID { get; set; }
    public string ResidentName { get; set; }
    public string ServiceName { get; set; }
}