using System;

namespace TransportManagementSystem.entity
{
  
    public class Booking
    {
        private int bookingID;
        private int tripID;
        private int passengerID;
        private DateTime bookingDate;
        private string status;

        public Booking() { }

        public Booking(int bookingID, int tripID, int passengerID, DateTime bookingDate, string status)
        {
            this.bookingID = bookingID;
            this.tripID = tripID;
            this.passengerID = passengerID;
            this.bookingDate = bookingDate;
            this.status = status;
        }

        public int BookingID { get => bookingID; set => bookingID = value; }
        public int TripID { get => tripID; set => tripID = value; }
        public int PassengerID { get => passengerID; set => passengerID = value; }
        public DateTime BookingDate { get => bookingDate; set => bookingDate = value; }
        public string Status { get => status; set => status = value; }
    }
}
