namespace Application.DTOs.Pilot
{
    public class PilotShipmentDTO
    {
        public int ShipmentId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public string District { get; set; }
        public string Status { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}