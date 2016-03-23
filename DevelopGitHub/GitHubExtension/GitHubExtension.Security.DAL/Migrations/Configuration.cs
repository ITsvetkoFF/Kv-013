using System.Collections.Generic;
using GitHubExtension.Models.CommunicationModels;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Helpers;
using GitHubExtension.Security.StorageModels.Identity;

namespace GitHubExtension.Security.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<GitHubExtension.Security.DAL.Context.SecurityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SecurityContext context)
        {

            context.SecurityRoles.AddOrUpdate(r => r.Name, new SecurityRole() { Name = "Admin" });
            context.SecurityRoles.AddOrUpdate(r => r.Name, new SecurityRole() { Name = "Developer" });
            context.SecurityRoles.AddOrUpdate(r => r.Name, new SecurityRole() { Name = "Reviewer" });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //context.Clients.AddRange(BuildClientsList());
            foreach (var item in BuildClientsList())
            {
                context.Clients.AddOrUpdate(item);
            }

            context.SaveChanges();
        }


        private static List<Client> BuildClientsList()
        {

            List<Client> ClientsList = new List<Client> 
            {
                new Client
                { Id = "ngAuthApp", 
                    Secret= Helper.GetHash("abc@123"), 
                    Name="AngularJS front-end Application", 
                    ApplicationType =  ApplicationTypes.JavaScript, 
                    Active = true, 
                    RefreshTokenLifeTime = 7200, 
                    AllowedOrigin = "http://ngauthenticationweb.azurewebsites.net"
                },
                new Client
                { Id = "consoleApp", 
                    Secret=Helper.GetHash("123@abc"), 
                    Name="Console Application", 
                    ApplicationType = ApplicationTypes.NativeConfidential, 
                    Active = true, 
                    RefreshTokenLifeTime = 14400, 
                    AllowedOrigin = "*"
                }
            };

            return ClientsList;
        }
    }
}