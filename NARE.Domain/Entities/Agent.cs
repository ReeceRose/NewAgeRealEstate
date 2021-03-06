﻿using System;
using Microsoft.AspNetCore.Identity;

namespace NARE.Domain.Entities
{
    public class Agent : IdentityUser
    {
        // Default IdentityUser Properties
        /*
            Access Failed Count
            Concurrency Stamp
            Email
            Email Confirmed
            Id
            Lockout Enabled
            Lockout End
            Normalized Email
            Normalized User Name
            Password Hash
            Phone Number
            Phone Number Confirmed
            Security Stamp
            Two Factor Enabled
            Username
         */
        public string Name { get; set; }
        public string Description { get; set; }
        // Picture of agent
        public string ImageUrl { get; set; }
        public DateTime DateJoined { get; set; }
        // Different from Lockout Enabled as that's for password attempts
        public bool AccountEnabled { get; set; }
    }
}
