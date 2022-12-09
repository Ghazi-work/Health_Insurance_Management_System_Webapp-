using Health_Insuarance_Management_System_webapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Health_Insuarance_Management_System_webapp.ViewModels;

namespace Health_Insuarance_Management_System_webapp.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<PolicyModel> Policies { get; set; }
        public DbSet<InsuranceCompanyModel>  Insurance_Companies{ get; set; }
        public DbSet<DepartmentModel> Departments{ get; set; }
        public DbSet<Health_Insuarance_Management_System_webapp.ViewModels.DeleteRoleViewModel> DeleteRoleViewModel { get; set; }
        public DbSet<Health_Insuarance_Management_System_webapp.ViewModels.ChangePasswordViewModel> ChangePasswordViewModel { get; set; }


    }
}
