using ContactService.Api.Context;
using ContactService.Api.Domain.Entities;
using ContactService.Api.Domain.Models;
using ContactService.Api.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Api.Managers
{
    public class ContactManager : IContactService
    {
        private readonly PostgreSqlDbContext _context;

        public ContactManager(PostgreSqlDbContext context)
        {
            _context = context;
        }

        public bool AddPerson(Person entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool AddPersonContactInfo(PersonContactInfo entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool DeletePerson(Guid id)
        {
            try
            {
                var entity = _context.Person.Where(a => a.Id == id).FirstOrDefault();
                entity.IsActive = false;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeletePersonContactInfo(Guid id)
        {
            try
            {
                var entity = _context.PersonContactInfo.Where(a => a.Id == id).FirstOrDefault();
                entity.IsActive = false;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Person GetPersonById(Guid id)
        {
            return _context.Person.Where(a => a.Id == id && a.IsActive == true).FirstOrDefault();
        }

        public List<Person> GetPersons()
        {
            return _context.Person.Where(a=> a.IsActive == true).ToList();
        }

        public PersonContacts GetPersonContacts(Guid personid)
        {
            var person = _context.Person.Where(a => a.Id == personid).FirstOrDefault();
            if (person != null)
            {
                PersonContacts entity = new PersonContacts();
                entity.PersonContactList = new List<PersonContactInfoModel>();
                entity.Name = person.Name;
                entity.Surname = person.Surname;
                entity.Firm = person.Firm;
                var personcontactinfo = _context.PersonContactInfo.Where(a => a.PersonId == personid && a.IsActive == true).ToList();
                foreach (var item in personcontactinfo)
                {
                    PersonContactInfoModel entitycontactinfo = new PersonContactInfoModel();
                    entitycontactinfo.Contacttype = item.Contacttype;
                    entitycontactinfo.Info = item.Info;
                    entitycontactinfo.Contacttypedesc = ContactTypeData.ContactTypeList.Where(a => a.Id == item.Contacttype).FirstOrDefault().Description;
                    entity.PersonContactList.Add(entitycontactinfo);
                }
                return entity;
            }
            else
                return null;
        }
    }
}
