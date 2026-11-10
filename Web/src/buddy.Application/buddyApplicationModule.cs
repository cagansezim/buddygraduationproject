using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using buddy.Authorization;

namespace buddy
{
    [DependsOn(
        typeof(buddyCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class buddyApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<buddyAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(buddyApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
