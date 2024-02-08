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
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetContacts()
        {
            return Ok(dbContext.Contacts.ToList());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task <IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = dbContext.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound(nameof(contact));
            }
            return Ok(contact);

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
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute]Guid id,UpdateContactRequest updatecontactrequest)
        {
            var contact = dbContext.Contacts.Find(id);
            if(contact != null) 
            {
                contact.FullName = updatecontactrequest.FullName;
                contact.Email = updatecontactrequest.Email;
                contact.Phone = updatecontactrequest.Phone;
                contact.Address = updatecontactrequest.Address;

                await dbContext.SaveChangesAsync();
                return Ok(contact);

            }
            return NotFound();

        }
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var conatct=await dbContext.Contacts.FindAsync(id);
            if (conatct != null)
            {
                dbContext.Remove(conatct);
                await dbContext.SaveChangesAsync();
                return Ok("Conatct is deleted");
            }
            return NotFound();

        }
    }
}
