
using ContactService.Api.Domain.Entities;
using ContactService.Api.Domain.Models;
using ContactService.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly UserData _currentuser;

        public ContactController(IContactService contactService)
        {
            this._contactService = contactService;
            this._currentuser = UserData.UserList.FirstOrDefault();// buradan kullanıcı set ederek createdbya standart bi veri atabilmek için
        }
        [HttpPost("addperson")]
        public bool AddPerson(string name, string surname, string firm)
        {
            Person entity = new Person();
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = Guid.NewGuid();
            entity.CreationDate = DateTime.Now;
            entity.Name = name;
            entity.Surname = surname;
            entity.Firm = firm;
            entity.IsActive = true;
            return _contactService.AddPerson(entity);
        }
        [HttpGet("getpersonlist")]
        public List<Person> GetPersonById()
        {
            return _contactService.GetPersons();
        }
        [HttpPost("deleteperson")]
        public bool DeletePerson(Guid id)
        {
            return _contactService.DeletePerson(id);
        }

        [HttpGet("getpersonbyid")]
        public Person GetPersonById(Guid id)
        {
            return _contactService.GetPersonById(id);
        }
    }
}
