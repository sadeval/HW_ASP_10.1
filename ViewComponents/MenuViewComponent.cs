using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HW_ASP_10._1.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Пример имитации пользователя-админа:
            var adminIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "Admin") }, "TestAuth");
            var adminUser = new ClaimsPrincipal(adminIdentity);

            // var userIdentity = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, "User") }, "TestAuth");
            // var normalUser = new ClaimsPrincipal(userIdentity);

            // Сейчас для теста используем adminUser:
            var user = adminUser;

            var menuItems = new List<(string Text, string Controller, string Action)>();

            if (user.IsInRole("Admin"))
            {
                menuItems.Add(("Manage Users", "Admin", "ManageUsers"));
                menuItems.Add(("Permissions", "Admin", "Permissions"));
            }

            if (user.Identity.IsAuthenticated)
            {
                menuItems.Add(("Profile", "Account", "Profile"));
                menuItems.Add(("My Orders", "Orders", "Index"));
            }
            else
            {
                menuItems.Add(("Login", "Account", "Login"));
                menuItems.Add(("Register", "Account", "Register"));
            }

            return View("Default", menuItems);
        }
    }
}