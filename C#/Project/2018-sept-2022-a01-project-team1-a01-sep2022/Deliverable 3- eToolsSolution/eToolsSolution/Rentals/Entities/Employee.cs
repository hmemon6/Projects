﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rentals.Entities
{
    internal partial class Employee
    {
        public Employee()
        {
            Rentals = new HashSet<Rental>();
        }

        [Key]
        public int EmployeeID { get; set; }
        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(25)]
        public string LastName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateHired { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateReleased { get; set; }
        public int PositionID { get; set; }
        [StringLength(30)]
        public string LoginID { get; set; }
        [Required]
        [StringLength(75)]
        public string Address { get; set; }
        [Required]
        [StringLength(30)]
        public string City { get; set; }
        [Required]
        [StringLength(12)]
        public string ContactPhone { get; set; }
        [Required]
        [StringLength(6)]
        public string PostalCode { get; set; }

        [InverseProperty("Employee")]
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}