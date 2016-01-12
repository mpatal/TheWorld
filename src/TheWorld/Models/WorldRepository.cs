using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            IEnumerable<Trip> trips;
            try
            {
                trips = _context.Trips.OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips from database", ex);
                throw;
            }
            
		  return trips;
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            IEnumerable<Trip> tripsWithStops;
            try
            {
                tripsWithStops = _context.Trips
                .Include(t => t.Stops)
                .OrderBy(t => t.Name)
                .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips with stops from database", ex);
                throw;  
            }

            return tripsWithStops;
        }

        public void AddTrip(Trip newTrip)
        {
            _context.Add(newTrip);
        }

        public bool SaveAll()
        {
            bool changesCommitted = _context.SaveChanges() > 0;
            return changesCommitted;
        }

        public Trip GetTripByName(string tripName)
        {
            var tripWithStops =_context.Trips
                .Include(t => t.Stops)
                .FirstOrDefault(t => t.Name == tripName);

            return tripWithStops;
        }

        public void AddStop(string tripName, Stop stop)
        {
            var trip = GetTripByName(tripName);
            stop.Order = trip.Stops.Max(s => s.Order) + 1;
            _context.Stops.Add(stop);
        }
    }

}