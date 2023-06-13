using AddressBook.DAL.Entities;
using AddressBook.PL.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.PL.Helpers
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<AddressesBook, AddressBookDto>()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department.Name))
                .ForMember(d => d.JobName, o => o.MapFrom(s => s.Job.Name))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom<PictureUrlResolver>());
            CreateMap<AddressBookDto, AddressesBook>();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Job, JobDto>().ReverseMap();
        }
    }
}
