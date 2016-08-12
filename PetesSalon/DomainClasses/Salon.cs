using System.Data.Entity;

namespace PetesSalon.DomainClasses
{
    public class Salon : DbContext
    {
        public Salon() : base("Salon")
        {

        }

        public  DbSet<ProductAndService> Products { get; set; }
        public  DbSet<User> User { get; set; }
    }
}