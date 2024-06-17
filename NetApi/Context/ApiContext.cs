using NetApi.Models;
using System.Data.Entity;

namespace NetApi.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext() : base("Conn")
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            CommonModelCreating(modelBuilder);


            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<User>().HasMany(e => e.ProductsCreated).WithRequired(e => e.UserCreator).HasForeignKey(e => e.CreatedBy);
            modelBuilder.Entity<User>().HasMany(e => e.ProductsUpdated).WithOptional(e => e.UserUpdater).HasForeignKey(e => e.UpdatedBy);

            modelBuilder.Entity<Product>().HasKey(e => e.Id);          

            base.OnModelCreating(modelBuilder);
        }

        private void CommonModelCreating(DbModelBuilder modelBuilder)
        {
            
        }        
    }
}