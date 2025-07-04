namespace TransportManagementSystem.entity
{
    public class Passenger
    {
        private int passengerID;
        private string firstName;
        private string gender;
        private int age;
        private string email;
        private string phoneNumber;

        public Passenger() { }

        public Passenger(int passengerID, string firstName, string gender, int age, string email, string phoneNumber)
        {
            this.passengerID = passengerID;
            this.firstName = firstName;
            this.gender = gender;
            this.age = age;
            this.email = email;
            this.phoneNumber = phoneNumber;
        }

        public int PassengerID { get => passengerID; set => passengerID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string Gender { get => gender; set => gender = value; }
        public int Age { get => age; set => age = value; }
        public string Email { get => email; set => email = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
    }
}
