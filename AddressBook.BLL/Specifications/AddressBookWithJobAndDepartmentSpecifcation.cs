using AddressBook.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.BLL.Specifications
{
    public class AddressBookWithJobAndDepartmentSpecifcation : BaseSpecification<AddressesBook>
    {
        public AddressBookWithJobAndDepartmentSpecifcation()
        {
            AddInclude(P => P.Department);
            AddInclude(P => P.Job);
        }

        public AddressBookWithJobAndDepartmentSpecifcation(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.Department);
            AddInclude(P => P.Job);
        }
    }
}
