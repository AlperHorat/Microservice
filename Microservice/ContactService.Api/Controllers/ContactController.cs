
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
        public ActionResult AddPerson(string name, string surname, string firm)
        {
            Person entity = new Person();
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = _currentuser.Id;
            entity.CreationDate = DateTime.Now;
            entity.Name = name;
            entity.Surname = surname;
            entity.Firm = firm;
            entity.IsActive = true;

            var result = _contactService.AddPerson(entity);
            if (result)
                return Ok("Kişi ekleme işlemi başarılı. Eklenen Id= " + entity.Id);
            else
                return BadRequest("Kişi eklenirken bir hata oluştu.");
        }
        [HttpGet("getpersonlist")]
        public List<Person> GetPersons()
        {
            return _contactService.GetPersons();
        }
        [HttpPost("deleteperson")]
        public ActionResult DeletePerson(Guid id)
        {
            var result = _contactService.DeletePerson(id);
            if (result)
                return Ok("Kişi silme işlemi başarılı");
            else
                return BadRequest("Kişi silinirken bir hata oluştu.");
        }

        [HttpGet("getpersonbyid")]
        public Person GetPersonById(Guid id)
        {
            return _contactService.GetPersonById(id);
        }
        [HttpPost("AddPersonContactInfo")]
        public ActionResult AddPersonContactInfo(string info, Guid personid, ContactType type)
        {
            PersonContactInfo entity = new PersonContactInfo();
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = _currentuser.Id;
            entity.CreationDate = DateTime.Now;
            entity.Contacttype = type;
            entity.Info = info;
            entity.PersonId = personid;
            entity.IsActive = true;

            var result = _contactService.AddPersonContactInfo(entity);
            if (result)
                return Ok("Kişi iletişim bilgisi ekleme işlemi başarılı. Id= " + entity.Id);
            else
                return BadRequest("Kişi iletişim bilgisi eklenirken bir hata oluştu.");
        }
        [HttpPost("deletepersoncontactinfo")]
        public ActionResult DeletePersonContactInfo(Guid id)
        {
            var result = _contactService.DeletePersonContactInfo(id);
            if (result)
                return Ok("Kişi iletişim bilgisi silme işlemi başarılı");
            else
                return BadRequest("Kişi iletişim bilgisi silinirken bir hata oluştu.");
        }
        [HttpGet("GetPersonContactsByPersonId")]
        public PersonContacts GetPersonContacts(Guid personid)
        {
            return _contactService.GetPersonContacts(personid);
        }
    }
}
