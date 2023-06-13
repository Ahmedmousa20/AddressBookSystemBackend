using AddressBook.DAL.Entities;
using AddressBook.PL.DTOs;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.PL.Helpers
{
    public class PictureUrlResolver : IValueResolver<AddressesBook, AddressBookDto, string>
    {
        public IConfiguration Configuration { get; }

        public PictureUrlResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public string Resolve(AddressesBook source, AddressBookDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PhotoUrl))
                return $"{Configuration["ApiUrl"]}{source.PhotoUrl}";
            return null;
        }
    }
}
