using AddressBook.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.DAL.Data.Config
{
    public class AddressesBookConfiguration : IEntityTypeConfiguration<AddressesBook>
    {
        public void Configure(EntityTypeBuilder<AddressesBook> builder)
        {
            builder.Property(P => P.FullName).IsRequired();
            builder.Property(P => P.DepartmentId).IsRequired();
            builder.Property(P => P.JobId).IsRequired();
            builder.Property(P => P.Age).IsRequired();
        }
    }
}
