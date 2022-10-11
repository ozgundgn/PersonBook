using AutoMapper;
using ContactService.Application.Persons.Commands;
using ContactService.Application.Persons.Queries;
using ContactService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Application.Common.Mappings
{
    public class PersonMappingProfile:Profile
    {
        public PersonMappingProfile()
        {
            CreateMap<Person, CreatePersonCommand>().ReverseMap();
            CreateMap<Person, UpdatePersonCommand>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();

        }
    }
}
