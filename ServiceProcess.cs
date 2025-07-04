using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TransportManagementSystem.entity;
using TransportManagementSystem.exception;
using TransportManagementSystem.util;

namespace TransportManagementSystem.dao
{
    public class TransportManagementServiceImpl : ITransportManagementService
    {
        public bool AddVehicle(Vehicle vehicle)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Vehicles (Model, Capacity, Type, Status) VALUES (@Model, @Capacity, @Type, @Status)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                cmd.Parameters.AddWithValue("@Capacity", vehicle.Capacity);
                cmd.Parameters.AddWithValue("@Type", vehicle.Type);
                cmd.Parameters.AddWithValue("@Status", vehicle.Status);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateVehicle(Vehicle vehicle)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Vehicles SET Model=@Model, Capacity=@Capacity, Type=@Type, Status=@Status WHERE VehicleID=@VehicleID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                cmd.Parameters.AddWithValue("@Capacity", vehicle.Capacity);
                cmd.Parameters.AddWithValue("@Type", vehicle.Type);
                cmd.Parameters.AddWithValue("@Status", vehicle.Status);
                cmd.Parameters.AddWithValue("@VehicleID", vehicle.VehicleID);
                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                    throw new VehicleNotFoundException("Vehicle ID not found.");
                return true;
            }
        }

        public bool DeleteVehicle(int vehicleId)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Vehicles WHERE VehicleID=@VehicleID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                    throw new VehicleNotFoundException("Vehicle ID not found.");
                return true;
            }
        }

        public bool ScheduleTrip(int vehicleId, int routeId, string departureDate, string arrivalDate)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Trips (VehicleID, RouteID, DepartureDate, ArrivalDate, Status, TripType, MaxPassengers)
                                 VALUES (@VehicleID, @RouteID, @DepartureDate, @ArrivalDate, 'Scheduled', 'Passenger', 20)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                cmd.Parameters.AddWithValue("@RouteID", routeId);
                cmd.Parameters.AddWithValue("@DepartureDate", DateTime.Parse(departureDate));
                cmd.Parameters.AddWithValue("@ArrivalDate", DateTime.Parse(arrivalDate));
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool CancelTrip(int tripId)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Trips SET Status='Cancelled' WHERE TripID=@TripID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TripID", tripId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool BookTrip(int tripId, int passengerId, string bookingDate)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Bookings (TripID, PassengerID, BookingDate, Status)
                                 VALUES (@TripID, @PassengerID, @BookingDate, 'Confirmed')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TripID", tripId);
                cmd.Parameters.AddWithValue("@PassengerID", passengerId);
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(bookingDate));
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool CancelBooking(int bookingId)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Bookings SET Status='Cancelled' WHERE BookingID=@BookingID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookingID", bookingId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool AllocateDriver(int tripId, int driverId)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO DriverTrip (TripID, DriverID) VALUES (@TripID, @DriverID)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TripID", tripId);
                cmd.Parameters.AddWithValue("@DriverID", driverId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeallocateDriver(int tripId)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM DriverTrip WHERE TripID=@TripID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TripID", tripId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Booking> GetBookingsByPassenger(int passengerId)
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Bookings WHERE PassengerID=@PassengerID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PassengerID", passengerId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bookings.Add(new Booking
                    {
                        BookingID = Convert.ToInt32(reader["BookingID"]),
                        TripID = Convert.ToInt32(reader["TripID"]),
                        PassengerID = Convert.ToInt32(reader["PassengerID"]),
                        BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                        Status = reader["Status"].ToString()
                    });
                }
            }

            if (bookings.Count == 0)
                throw new BookingNotFoundException("No bookings found for the given passenger ID.");

            return bookings;
        }

        public List<Booking> GetBookingsByTrip(int tripId)
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Bookings WHERE TripID=@TripID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TripID", tripId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bookings.Add(new Booking
                    {
                        BookingID = Convert.ToInt32(reader["BookingID"]),
                        TripID = Convert.ToInt32(reader["TripID"]),
                        PassengerID = Convert.ToInt32(reader["PassengerID"]),
                        BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                        Status = reader["Status"].ToString()
                    });
                }
            }

            if (bookings.Count == 0)
                throw new BookingNotFoundException("No bookings found for the given trip ID.");

            return bookings;
        }

        public List<Driver> GetAvailableDrivers()
        {
            List<Driver> drivers = new List<Driver>();
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string query = @"
                SELECT * FROM Drivers 
                WHERE DriverID NOT IN (SELECT DriverID FROM DriverTrip)";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    drivers.Add(new Driver
                    {
                        DriverID = Convert.ToInt32(reader["DriverID"]),
                        FirstName = reader["FirstName"].ToString(),
                        LicenseNumber = reader["LicenseNumber"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString()
                    });
                }
            }
            return drivers;
        }
    }
}
