using Microsoft.AspNetCore.Identity;
using MVC_CarsForSale.Models;
using System.Net;

namespace MVC_CarsForSale.Data
{
    public class seed
    {
//        public static void SeedData(IApplicationBuilder applicationBuilder)
//        {
//            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
//            {
//                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

//                context.Database.EnsureCreated();

//                if (!context.Bikes.Any())
//                {
//                    context.Bikes.AddRange(new List<Bikes>()
//                                            {
//                                                new Bikes()
//                                                {
//                                                    Price = 5000,
//                                                    Milage = 100000,
//                                                    Description = "Example",
//                                                    Image = "https://pngimg.com/uploads/motorcycle/motorcycle_PNG5347.png",
//                                                    AddressId = 5,
//                                                    Address = new Address()
//                                                    {
//                                                        Province = "Quebec",
//                                                        City = "St-Donat",
//                                                        Country = "Canada"
//                                                    }
//                                                 },
//                                                new Bikes()
//                                                {
//                                                    Price = 5000,
//                                                    Milage = 100000,
//                                                    Description = "Example",
//                                                    Image = "https://photos.motogp.com/2021/03/06/suzuki-gsx-rr-2021-03_0.big.jpg",
//                                                    AddressId = 5,
//                                                    Address = new Address()
//                                                    {
//                                                        Province = "Quebec",
//                                                        City = "St-Donat",
//                                                        Country = "Canada"
//                                                    }

//                                                },
//                                                new Bikes()
//                                                {
//                                                    Price = 5000,
//                                                    Milage = 100000,
//                                                    Description = "Example",
//                                                    Image = " https://i.pinimg.com/736x/bf/f7/cb/bff7cb6bbdebc671bd2d53f5fe1eacb9--honda-motors-honda-cbr-rr.jpg",
//                                                    AddressId = 5,
//                                                    Address = new Address()
//                                                    {
//                                                        Province = "Quebec",
//                                                        City = "St-Donat",
//                                                        Country = "Canada"
//                                                    }
//                                                },
//                                                new Bikes()
//                                                {
//                                                    Price = 5000,
//                                                    Milage = 100000,
//                                                    Description = "Example",
//                                                    Image = "  https://www.motoplanete.com/voxan/voxan-1000-scrambler-2003.jpg",
//                                                    AddressId = 5,
//                                                    Address = new Address()
//                                                    {
//                                                        Province = "Quebec",
//                                                        City = "St-Donat",
//                                                        Country = "Canada"
//                                                    }
//                                                }
//                                            });
//                    context.SaveChanges();
//                }
//                //Houses
//                if (!context.Cars.Any())
//                {
//                    context.Cars.AddRange(new List<Cars>()
//                                            {
//                                                new Cars()
//                                                {
//                                                    Price = 5000,
//                                                    Milage = 100000,
//                                                    Description = "Example",
//                                                    Image = "https://www.liveenhanced.com/wp-content/uploads/2018/03/Bugatti-Chiron.jpg",
//                                                    AddressId = 5,
//                                                    Address = new Address()
//                                                    {
//                                                        Province = "Quebec",
//                                                        City = "St-Donat",
//                                                        Country = "Canada"
//                                                    }
//                                                },
//                                                new Cars()
//                                                {
//                                                  Price = 5000,
//                                                  Milage = 100000,
//                                                  Description = "Example",
//                                                  Image = "https://printcarposter.com/images/1274327-big.jpg",
//                                                  AddressId = 5,
//                                                  Address = new Address()
//                                                  {
//                                                      Province = "Quebec",
//                                                      City = "St-Donat",
//                                                      Country = "Canada"
//                                                  }
//                                                }
//                                            });
//                    context.SaveChanges();
//                }

//                //Vans
//                //Houses
//                if (!context.Vans.Any())
//                {
//                    context.Vans.AddRange(new List<Vans>()
//                                            {
//                                                new Vans()
//                                                {
//                                                  Price = 5000,
//                                                  Milage = 100000,
//                                                  Description = "Example",
//                                                  Image = "https://s3-us-west-2.amazonaws.com/hfc-ad-prod/plan_assets/57117/original/57117HA_e_1479202587.jpg?1506330239",
//                                                  AddressId = 5,
//                                                  Address = new Address()
//                                                  {
//                                                      Province = "Quebec",
//                                                      City = "St-Donat",
//                                                      Country = "Canada"
//                                                  }
//                                                },
//                                                new Vans()
//                                                {
//                                                  Price = 5000,
//                                                  Milage = 100000,
//                                                  Description = "Example",
//                                                  Image = "http://1.bp.blogspot.com/_4WhySEVuGnU/TUa78DpaOeI/AAAAAAAAADc/o8SZYWGtOMg/s1600/AmericanDreamHouse3.jpg",
//                                                  AddressId = 5,
//                                                  Address = new Address()
//                                                  {
//                                                      Province = "Quebec",
//                                                      City = "St-Donat",
//                                                      Country = "Canada"
//                                                  }
//                                                }
//                                            });
//                    context.SaveChanges();
//                }
//            }
//        }
//    }
//}
public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
{
    using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
    {
        //Roles
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        if (!await roleManager.RoleExistsAsync(UserRoles.User))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        //Users
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        string adminUserEmail = "admin@gmail.com";

        var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
        if (adminUser == null)
        {
            var newAdminUser = new AppUser()
            {
                FirstName = "Marc",
                LastName = "Bazinet",
                UserName = "admin",
                Email = adminUserEmail,
                EmailConfirmed = true,
                Address = new Address()
                {
                    Province = "Quebec",
                    City = "Montreal",
                    Country = "Canada"
                }
            };
            await userManager.CreateAsync(newAdminUser, "Admin@12345#");
            await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
        }

        string appUserEmail = "user@gmail.com";

        var appUser = await userManager.FindByEmailAsync(appUserEmail);
        if (appUser == null)
        {
            var newAppUser = new AppUser()
            {
                FirstName = "Jacob",
                LastName = "Rioux",
                UserName = "user",
                Email = appUserEmail,
                EmailConfirmed = true,
                Address = new Address()
                {
                    Province = "Quebec",
                    City = "Quebec",
                    Country = "Canada"
                }
            };
            await userManager.CreateAsync(newAppUser, "User@12345#");
            await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
        }
    }
}
    }
}


// Tools-> Nuggets Package Manager -> Package Manager Console ->

// dotnet run dataseed

// add-migration migrationName

// update-database
