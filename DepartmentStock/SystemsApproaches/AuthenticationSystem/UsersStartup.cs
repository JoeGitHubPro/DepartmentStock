using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DepartmentStock.SystemsApproaches.AuthenticationSystem;
using DepartmentStock.Models;

namespace DepartmentStock.SystemsApproaches.AuthenticationSystem
{
    public static class UsersStartup
    {
        public static void createRolesandUsers()
        {

            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            // In Startup iam creating first Admin Role and creating a default Admin User
            if (!roleManager.RoleExists(RoleNameModels.Admin))
            {
             

                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = RoleNameModels.Admin;
                roleManager.Create(role);
                //Here we create a Admin super user who will maintain the website 
                var user = new ApplicationUser();
                user.UserName = "Admin";
                user.Email = "Admin@mail.com";
                user.Name = "Admin";

                string userPWD = "7nJ4oq7@f*Dg";

                var chkUser = UserManager.Create(user, userPWD);
                //add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var resultl = UserManager.AddToRole(user.Id, RoleNameModels.Admin);
                }
            }


            // creating Creating Trainer role

            if (!roleManager.RoleExists(RoleNameModels.User))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = RoleNameModels.User;
                roleManager.Create(role);
            }
            // creating Creating Manager role 
            if (!roleManager.RoleExists(RoleNameModels.Manger))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = RoleNameModels.Manger;
                roleManager.Create(role);

            }
        }
    }
}