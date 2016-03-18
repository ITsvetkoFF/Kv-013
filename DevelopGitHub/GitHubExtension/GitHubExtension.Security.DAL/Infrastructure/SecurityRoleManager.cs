﻿using GitHubExtension.Security.DAL.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;


namespace GitHubExtension.Security.DAL.Infrastructure
{
    public class SecurityRoleManager : RoleManager<IdentityRole>
    {
        // Check why we need IRoleStore<SecurityRole, string>
        public SecurityRoleManager(IRoleStore<IdentityRole, string> store)
            : base(store)
        {
        }

        public static SecurityRoleManager Create(
            IdentityFactoryOptions<SecurityRoleManager> options,
        IOwinContext context)
        {
            return new SecurityRoleManager(new
            RoleStore<IdentityRole>(context.Get<SecurityContext>()));
        }
    }
}
