using System;

namespace TransportManagementSystem.exception
{
    public class VehicleException : Exception
    {
        public VehicleException(string msg) : base(msg) { }
    }

    public class VehicleNotFoundException : Exception
    {
        public VehicleNotFoundException(string msg) : base(msg) { }
    }

    public class TripException : Exception
    {
        public TripException(string msg) : base(msg) { }
    }

    public class TripNotFoundException : Exception
    {
        public TripNotFoundException(string msg) : base(msg) { }
    }

    public class BookingException : Exception
    {
        public BookingException(string msg) : base(msg) { }
    }
}
public class BookingNotFoundException : Exception
{
    public BookingNotFoundException(string msg) : base(msg) { }
}