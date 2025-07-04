namespace TransportManagementSystem.entity
{
    public class Vehicle
    {
        private int vehicleID;
        private string model;
        private decimal capacity;
        private string type;
        private string status;

        public Vehicle() { }

        public Vehicle(int vehicleID, string model, decimal capacity, string type, string status)
        {
            this.vehicleID = vehicleID;
            this.model = model;
            this.capacity = capacity;
            this.type = type;
            this.status = status;
        }

        public int VehicleID { get => vehicleID; set => vehicleID = value; }
        public string Model { get => model; set => model = value; }
        public decimal Capacity { get => capacity; set => capacity = value; }
        public string Type { get => type; set => type = value; }
        public string Status { get => status; set => status = value; }
    }
}
