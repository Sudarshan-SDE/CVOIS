using CVOIS.Interfaces.Admin;
using CVOIS.DataAccessLayer.Admin;
using CVOIS.Interfaces.IViewer;
using CVOIS.DataAccessLayer.Viewer;
using CVOIS.Interfaces.ISuperAdmin;
using CVOIS.DataAccessLayer.SuperAdmin_DAL;
using CVOIS.DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using CVOIS.Interfaces;
using CVOIS.DataAccessLayer.HomeDAL;

namespace CVOIS.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDALServices(this IServiceCollection services)
        {
            services.AddScoped<IReport_Admin, Report_Admin_DAL>();
            services.AddScoped<IReport_Viewer, Report_Viewer_DAL>();
            services.AddScoped<IMinistry, Ministry_DAL>();
            services.AddScoped<IDepartment, Department_DAL>();
            services.AddScoped<IOrganization, Organization_DAL>();
            services.AddScoped<IDatabaseDAL, DatabaseDAL>();
            services.AddScoped<ILevel, Level_DAL>();
            services.AddScoped<IAppointingAuthority, AppointingAuthority_DAL>();
            services.AddScoped<IMasterCvoServices, MasterCvoServices_DAL>();
            services.AddScoped<IState, State_DAL>();
            services.AddScoped<IUser, User_DAL>();
        }
    }
}
