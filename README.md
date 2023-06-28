#MvcCarsForSale Asp.Net C# Project

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
