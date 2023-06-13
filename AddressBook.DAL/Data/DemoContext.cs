using AddressBook.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.DAL.Data
{
    public class DemoContext: DbContext
    {

        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {

        }


        public DbSet<AddressesBook> Addressbook { set; get; }
        public DbSet<Department> Departments { set; get; }
        public DbSet<Job> Jobs { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
