using Microsoft.AspNetCore.Mvc;

namespace OurRecipes.Utilities
{
    public static class RedirectHelper
    {
        public static IActionResult RedirectByRoleName(string action, string roleName)
        {
            switch (roleName.ToLower())
            {
                case "admin":
                    return new RedirectToActionResult(action, "AdminDash", null);
                case "user":
                    return new RedirectToActionResult(action, "UserDash", null);
                case "chief":
                    return new RedirectToActionResult(action, "ChiefDash", null);
                default:
                    return new RedirectToActionResult("Login", "Auth", null);
            }
        }

        public static IActionResult RedirectByRoleId(string action, decimal roleId)
        {
            switch (roleId)
            {
                case 1:
                    return new RedirectToActionResult(action, "AdminDash", null);
                case 2:
                    return new RedirectToActionResult(action, "UserDash", null);
                case 3:
                    return new RedirectToActionResult(action, "ChiefDash", null);
                default:
                    return new RedirectToActionResult("Login", "Auth", null);
            }
        }
    }
    }
