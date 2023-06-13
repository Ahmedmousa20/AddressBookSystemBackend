using AddressBook.BLL.Interfaces;
using AddressBook.BLL.Repositories;
using AddressBook.BLL.Services;
using AddressBook.PL.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.PL.Extentions
{
    public static class ApplicationServicesSExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddAutoMapper(typeof(MapperProfile));

            return services;

        }
    }
}
