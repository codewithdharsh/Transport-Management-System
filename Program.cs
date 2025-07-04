using System;
using TransportManagementSystem.dao;
using TransportManagementSystem.entity;
using TransportManagementSystem.exception;
using System.Collections.Generic;

namespace TransportManagementSystem.main
{
    public class MainModule
    {
        static ITransportManagementService transportService = new TransportManagementServiceImpl();

        public static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n--- Transport Management System Menu ---");
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. Update Vehicle");
                Console.WriteLine("3. Delete Vehicle");
                Console.WriteLine("4. Schedule Trip");
                Console.WriteLine("5. Cancel Trip");
                Console.WriteLine("6. Book Trip");
                Console.WriteLine("7. Cancel Booking");
                Console.WriteLine("8. View Bookings by Passenger");
                Console.WriteLine("9. View Bookings by Trip");
                Console.WriteLine("10. Allocate Driver");
                Console.WriteLine("11. Deallocate Driver");
                Console.WriteLine("12. View Available Drivers");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": AddVehicle(); break;
                        case "2": UpdateVehicle(); break;
                        case "3": DeleteVehicle(); break;
                        case "4": ScheduleTrip(); break;
                        case "5": CancelTrip(); break;
                        case "6": BookTrip(); break;
                        case "7": CancelBooking(); break;
                        case "8": ViewBookingsByPassenger(); break;
                        case "9": ViewBookingsByTrip(); break;
                        case "10": AllocateDriver(); break;
                        case "11": DeallocateDriver(); break;
                        case "12": ViewAvailableDrivers(); break;
                        case "0":
                            exit = true;
                            Console.WriteLine("Exiting application. Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice! Please enter a valid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        static void AddVehicle()
        {
            Console.WriteLine("\n--- Add Vehicle ---");
            Vehicle v = new Vehicle();

            Console.Write("Model: ");
            v.Model = Console.ReadLine();

            Console.Write("Capacity (decimal): ");
            v.Capacity = decimal.Parse(Console.ReadLine());

            Console.Write("Type (Truck/Van/Bus): ");
            v.Type = Console.ReadLine();

            Console.Write("Status (Available/On Trip/Maintenance): ");
            v.Status = Console.ReadLine();

            transportService.AddVehicle(v);
            Console.WriteLine("Vehicle added successfully.");
        }

        static void UpdateVehicle()
        {
            Console.WriteLine("\n--- Update Vehicle ---");
            Vehicle v = new Vehicle();

            Console.Write("VehicleID to update: ");
            v.VehicleID = int.Parse(Console.ReadLine());

            Console.Write("New Model: ");
            v.Model = Console.ReadLine();

            Console.Write("New Capacity (decimal): ");
            v.Capacity = decimal.Parse(Console.ReadLine());

            Console.Write("New Type (Truck/Van/Bus): ");
            v.Type = Console.ReadLine();

            Console.Write("New Status (Available/On Trip/Maintenance): ");
            v.Status = Console.ReadLine();

            transportService.UpdateVehicle(v);
            Console.WriteLine("Vehicle updated successfully.");
        }

        static void DeleteVehicle()
        {
            Console.WriteLine("\n--- Delete Vehicle ---");
            Console.Write("VehicleID to delete: ");
            int id = int.Parse(Console.ReadLine());

            transportService.DeleteVehicle(id);
            Console.WriteLine("Vehicle deleted successfully.");
        }

        static void ScheduleTrip()
        {
            Console.WriteLine("\n--- Schedule Trip ---");

            Console.Write("VehicleID: ");
            int vehicleId = int.Parse(Console.ReadLine());

            Console.Write("RouteID: ");
            int routeId = int.Parse(Console.ReadLine());

            Console.Write("Departure Date (yyyy-MM-dd HH:mm): ");
            string departureDate = DateTime.Parse(Console.ReadLine()).ToString("yyyy-MM-dd HH:mm");

            Console.Write("Arrival Date (yyyy-MM-dd HH:mm): ");
            string arrivalDate = DateTime.Parse(Console.ReadLine()).ToString("yyyy-MM-dd HH:mm");

            bool success = transportService.ScheduleTrip(vehicleId, routeId, departureDate, arrivalDate);

            Console.WriteLine(success ? "Trip scheduled successfully." : "Failed to schedule trip.");
        }

        static void CancelTrip()
        {
            Console.WriteLine("\n--- Cancel Trip ---");
            Console.Write("TripID to cancel: ");
            int id = int.Parse(Console.ReadLine());

            bool success = transportService.CancelTrip(id);
            Console.WriteLine(success ? "Trip cancelled successfully." : "Failed to cancel trip.");
        }

        static void BookTrip()
        {
            Console.WriteLine("\n--- Book Trip ---");
            Console.Write("TripID: ");
            int tripId = int.Parse(Console.ReadLine());

            Console.Write("PassengerID: ");
            int passengerId = int.Parse(Console.ReadLine());

            string bookingDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            bool success = transportService.BookTrip(tripId, passengerId, bookingDate);

            Console.WriteLine(success ? "Booking successful." : "Booking failed.");
        }

        static void CancelBooking()
        {
            Console.WriteLine("\n--- Cancel Booking ---");
            Console.Write("BookingID to cancel: ");
            int id = int.Parse(Console.ReadLine());

            bool success = transportService.CancelBooking(id);
            Console.WriteLine(success ? "Booking cancelled successfully." : "Failed to cancel booking.");
        }

        static void ViewBookingsByPassenger()
        {
            Console.WriteLine("\n--- View Bookings by Passenger ---");
            Console.Write("PassengerID: ");
            int id = int.Parse(Console.ReadLine());

            List<Booking> bookings = transportService.GetBookingsByPassenger(id);

            if (bookings.Count == 0)
            {
                Console.WriteLine("No bookings found.");
                return;
            }

            foreach (var b in bookings)
            {
                Console.WriteLine($"BookingID: {b.BookingID}, TripID: {b.TripID}, Date: {b.BookingDate}, Status: {b.Status}");
            }
        }

        static void ViewBookingsByTrip()
        {
            Console.WriteLine("\n--- View Bookings by Trip ---");
            Console.Write("TripID: ");
            int id = int.Parse(Console.ReadLine());

            List<Booking> bookings = transportService.GetBookingsByTrip(id);

            if (bookings.Count == 0)
            {
                Console.WriteLine("No bookings found.");
                return;
            }

            foreach (var b in bookings)
            {
                Console.WriteLine($"BookingID: {b.BookingID}, PassengerID: {b.PassengerID}, Date: {b.BookingDate}, Status: {b.Status}");
            }
        }

        static void AllocateDriver()
        {
            Console.WriteLine("\n--- Allocate Driver to Trip ---");
            Console.Write("TripID: ");
            int tripId = int.Parse(Console.ReadLine());
            Console.Write("DriverID: ");
            int driverId = int.Parse(Console.ReadLine());

            bool result = transportService.AllocateDriver(tripId, driverId);
            Console.WriteLine(result ? "Driver allocated successfully." : "Allocation failed.");
        }

        static void DeallocateDriver()
        {
            Console.WriteLine("\n--- Deallocate Driver from Trip ---");
            Console.Write("TripID: ");
            int tripId = int.Parse(Console.ReadLine());

            bool result = transportService.DeallocateDriver(tripId);
            Console.WriteLine(result ? "Driver deallocated successfully." : "Deallocation failed.");
        }

        static void ViewAvailableDrivers()
        {
            Console.WriteLine("\n--- Available Drivers ---");
            var drivers = transportService.GetAvailableDrivers();

            if (drivers.Count == 0)
            {
                Console.WriteLine("No available drivers.");
                return;
            }

            foreach (var d in drivers)
            {
                Console.WriteLine($"DriverID: {d.DriverID}, Name: {d.Name}, Phone: {d.PhoneNumber}, Status: {d.Status}");
            }
        }
    }
}
