using System;
using System.Reflection.Emit;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TheWorld.Models
{
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }

    }
}