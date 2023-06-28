#MvcCarsForSale Asp.Net C# Project

Microsoft Visual Studio 2022

NuGet Packages :

Bootstrap v5.2.3
CloudinaryDotNet ( Cloud storage of pictures) v1.21.0
Microsoft.AspNetCore.Identity.EntityFrameworkCore v6.0.9
Microsoft.EntityFrameworkCore v7.0.5
Microsoft.EntityFrameWorkCore.Design v7.0.5
Microsoft.EntityFrameworkCore.SqlServer v7.0.5
Microsoft.EntityFrameworkCore.Tools v7.0.5



Problem: Non-Registered Member can access Cars, Vans, Bikes Page.
Solution: Authorize (UserInRole(“Admin”) +” ”+ UserInRole(“User”)

Problem: Non-Registered Member cannot access Create, Edit, Delete, DashBoard.
Solution: if(!User.IsInRole(“Admin”) || User.IsInRole(“User”) {Return RedirectToAction(nameof(Index));}

Problem: Need to be Register to create, edit, delete Cars, Vans, Bikes.
Solution: Implement the CRUD inside DashBoardController using Authorize.

Problem: Registered Member can only edit their own Cars, Vans, Bikes using the DashBoard.
Solution:  if(editVm.Id != user.Id) {Return RedirectToAction(nameof(Index));}

Problem: Registered Member can only delete and edit their own profile.
Solution: if(deleteVM.Id != user.Id) { Return RedirectToAction(nameof(Index));}

Problem: ONLY 1 Admin user that have all the right.
Solution: Create Admin outside of the registration form , seeddata + role.

Problem: Only logged in Admin and Registered member can access DashBoard.
Solution: Authorize (UserInRole(“Admin”) +” ”+ UserInRole(“User”)
