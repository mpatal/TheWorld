using System.Collections.Generic;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
        void AddTrip(Trip newTrip);
        bool SaveAll();
        Trip GetTripByName(string tripName, string name);
        void AddStop(string tripName, string username, Stop stop);
        IEnumerable<Trip> GetUserTripsWithStops(string name);
    }
}