using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using buddy.EntityFrameworkCore;
using buddy.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace buddy.Web.Tests
{
    [DependsOn(
        typeof(buddyWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class buddyWebTestModule : AbpModule
    {
        public buddyWebTestModule(buddyEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(buddyWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(buddyWebMvcModule).Assembly);
        }
    }
}