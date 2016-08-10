namespace PortoAlegrePlus.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PortoAlegrePlus.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PortoAlegrePlus.Models.ApplicationDbContext";
        }

        protected override void Seed(PortoAlegrePlus.Models.ApplicationDbContext context)
        {
            var hasher = new PasswordHasher();
            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    NomeUsuario = "Robson",
                    SobrenomeUsuario = "Abreu",
                    UserName = "admin@portoalegreplus.com.br",
                    PasswordHash = hasher.HashPassword("PortoAlegre+"),
                    SecurityStamp = Guid.NewGuid().ToString()
                });

            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    NomeUsuario = "Amanda",
                    SobrenomeUsuario = "Freitas",
                    UserName = "smov@portoalegreplus.com.br",
                    PasswordHash = hasher.HashPassword("PortoAlegre+"),
                    SecurityStamp = Guid.NewGuid().ToString()
                });

            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    NomeUsuario = "Thiago",
                    SobrenomeUsuario = "Vargas",
                    UserName = "eptc@portoalegreplus.com.br",
                    PasswordHash = hasher.HashPassword("PortoAlegre+"),
                    SecurityStamp = Guid.NewGuid().ToString()
                });

            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    NomeUsuario = "Júnior",
                    SobrenomeUsuario = "Ferreira",
                    UserName = "junior@portoalegreplus.com.br",
                    PasswordHash = hasher.HashPassword("PortoAlegre+"),
                    SecurityStamp = Guid.NewGuid().ToString()
                });

            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    NomeUsuario = "Fernanda",
                    SobrenomeUsuario = "Rosa",
                    UserName = "fernanda@portoalegreplus.com.br",
                    PasswordHash = hasher.HashPassword("PortoAlegre+"),
                    SecurityStamp = Guid.NewGuid().ToString()
                });

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Contas Oficiais"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Contas Oficiais" };
                manager.Create(role);

            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ApplicationUser user = userManager.FindByName("admin@portoalegreplus.com.br");
            if (!(user == null))
            {
                var result = userManager.AddToRole(user.Id, "Admin");
            }

            ApplicationUser user2 = userManager.FindByName("smov@portoalegreplus.com.br");
            if (!(user2 == null))
            {
                var result = userManager.AddToRole(user2.Id, "Contas Oficiais");
            }

            ApplicationUser user3 = userManager.FindByName("eptc@portoalegreplus.com.br");
            if (!(user3 == null))
            {
                var result = userManager.AddToRole(user3.Id, "Contas Oficiais");
            }
        }
    }
}
