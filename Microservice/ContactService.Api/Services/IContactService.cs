using ContactService.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Api.Services
{
    interface IContactService
    {
        bool AddPerson();
        bool AddPersonContactInfo();
        Person GetPersonById();
        
    }
}
