using AddressBook.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.PL.DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //public virtual ICollection<AddressesBook> MyProperty { set; get; } = new HashSet<AddressesBook>();

    }
}
