using localshop.Core.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace localshop.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Set default value in constructor
        /// </summary>
        public ApplicationUser()
        {
            LockoutEnabled = false;
            CreatedDate = DateTime.Now;
            Image = ImageLinks.LogoIcon;
        }

        #region Properties
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Image { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public DateTime CreatedDate { get; set; }
        #endregion

        public string FullName
        {
            get
            {
                return $"{LastName} {FirstName}";
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
