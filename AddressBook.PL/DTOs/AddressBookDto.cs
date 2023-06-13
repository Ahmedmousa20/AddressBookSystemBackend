using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.PL.DTOs
{
    public class AddressBookDto
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public int JobId { get; set; }
        public string JobName { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        [Required]
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "Must be egyptian phone number")]
        public string MobileNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoUrl { get; set; }
        public int Age { get; set; }
    }
}
