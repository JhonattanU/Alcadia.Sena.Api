using AutoMapper;
using LightInject;
using System;

namespace Alcadia.Sena.Processing
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry service)
        {
            service.Register(typeof(IProcessor<>), typeof(Processor<>));
            service.RegisterAssembly(GetType().Assembly, (serviceType, implemeningType) => serviceType.Name.EndsWith("Processor"));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(GetType().Assembly);

                //Ignore null values
                cfg.ForAllMaps((map, expression) => expression.ForAllMembers(options => options.PreCondition((source) => source != null && !string.IsNullOrWhiteSpace(source?.ToString()))));
                cfg.ForAllMaps((map, expression) => expression.ForAllMembers(options => options.Condition((src, dest, srcMember) => srcMember != null && srcMember.ToString() != default(Guid).ToString() && srcMember.ToString() != DateTime.MinValue.ToString())));
            });

            var mapper = config.CreateMapper();
            service.Register(factory => mapper);
        }
    }
}
