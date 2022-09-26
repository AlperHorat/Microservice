using ContactService.Api.Domain.Entities;
using ContactService.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Api.Services
{
    public interface IContactService
    {
        bool AddPerson(Person entity);
        bool DeletePerson(Guid id);
        bool AddPersonContactInfo(PersonContactInfo entity);
        bool DeletePersonContactInfo(Guid id);
        List<Person> GetPersons();
        Person GetPersonById(Guid id);
        PersonContacts GetPersonContacts(Guid personid);

    }
}
