﻿using Hotel.Core.Entities.FeedbackModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities.customer
{
    public class Customer : IdentityUser
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; } 
        public Entities.Customer.CustomerAddress Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
