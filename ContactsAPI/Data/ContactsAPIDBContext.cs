using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Data
{
    // craete a DBContext class which inherits from DbContext
    public class ContactsAPIDBContext : DbContext
    {
        public ContactsAPIDBContext(DbContextOptions options) : base(options)
        {
            // create a constructor 
        }

        // craete a property which act as tables for entity framework core

        public DbSet<Contact> Contacts { get; set; }
    }
}
