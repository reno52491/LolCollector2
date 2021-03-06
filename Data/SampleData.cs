using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using SkinApp.Models;
using System.Collections.Generic;

namespace SkinApp.Data
{
    public class SampleData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            // Ensure db
            context.Database.EnsureCreated();

            // Ensure Stephen (IsAdmin)
            var stephen = await userManager.FindByNameAsync("Stephen.Walther@CoderCamps.com");
            if (stephen == null)
            {
                // create user
                stephen = new ApplicationUser
                {
                    UserName = "Stephen.Walther@CoderCamps.com",
                    Email = "Stephen.Walther@CoderCamps.com",
                    Skins = new List<Skin>
                    {
                        new Skin { UrlString = "Thresh_1" },
                        new Skin { UrlString = "Fizz_4" },
                        new Skin { UrlString = "KogMaw_5" },
                        new Skin { UrlString = "Chogath_4" }
                    }
                };
                await userManager.CreateAsync(stephen, "Secret123!");

                // add claims
                await userManager.AddClaimAsync(stephen, new Claim("IsAdmin", "true")); //do this same thing inside of service 
            }

            // Ensure Mike (not IsAdmin)
            var mike = await userManager.FindByNameAsync("Mike@CoderCamps.com");
            if (mike == null)
            {
                // create user
                mike = new ApplicationUser
                {
                    UserName = "Mike@CoderCamps.com",
                    Email = "Mike@CoderCamps.com",
                    Skins = new List<Skin>
                    {
                        new Skin { UrlString = "Olaf_1" },
                        new Skin { UrlString = "RekSai_1" },
                        new Skin { UrlString = "Elise_2" },
                        new Skin { UrlString = "Maokai_3" }
                    }
                };
                await userManager.CreateAsync(mike, "Secret123!");
            }


        }

    }
}
