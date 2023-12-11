using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ProjetEcommerceApplication.Models
{
    public class CommerceContext : IdentityDbContext
    {
        public CommerceContext(DbContextOptions<CommerceContext> options) : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Order> Order { get; set; }
    }
}
