namespace TransportManagementSystem.entity
{
    public class Driver
    {
        private int driverID;
        private string name;
        private string phoneNumber;
        private string status;
        private string firstName;        
        private string licenseNumber;   

        public Driver() { }

        public Driver(int driverID, string status, string name, string phoneNumber, string firstName, string licenseNumber)
        {
            this.driverID = driverID;
            this.status = status;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.firstName = firstName;
            this.licenseNumber = licenseNumber;
        }

        public int DriverID { get => driverID; set => driverID = value; }
        public string Name { get => name; set => name = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Status { get => status; set => status = value; }

        public string FirstName { get => firstName; set => firstName = value; }         // ✅ property
        public string LicenseNumber { get => licenseNumber; set => licenseNumber = value; } // ✅ property
    }
}
