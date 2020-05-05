using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoaVenda.Areas.Identity.Data;
using Entity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoaVenda.Models
{
    public class BoaVendaContext : IdentityDbContext<BoaVendaUser>
    {

        public DbSet<Usuario> Usuarios { get; set; }


        public BoaVendaContext(DbContextOptions<BoaVendaContext> options): base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Usuario>().HasKey(u => u.UsuarioId);
            builder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();

        }




    }
}
