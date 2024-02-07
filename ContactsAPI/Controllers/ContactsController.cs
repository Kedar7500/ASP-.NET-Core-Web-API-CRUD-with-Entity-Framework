using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAPI.Controllers
{
    [ApiController]// to tell it its not mvc contorller
    [Route("api/contacts")]
    public class ContactsController : Controller
    {
        // feild to talk to inmemory database
        private readonly ContactsAPIDBContext dbContext;

        // to talk to inmemory database
        public ContactsController(ContactsAPIDBContext dbContext) 
        { 
            this.dbContext=dbContext;
        }

        [HttpGet]
        public IActionResult GetContacts()
        {
            return Ok(dbContext.Contacts.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest) 
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FullName = addContactRequest.FullName,
                Email = addContactRequest.Email,
                Phone = addContactRequest.Phone,
                Address = addContactRequest.Address,

            };

            // to make our method async
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);

        }
    }
}
