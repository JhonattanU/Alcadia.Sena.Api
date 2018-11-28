using LightInject;

namespace Alcadia.Sena.Repository
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry service)
        {
            service.Register<IConnectionFactory, ConnectionFactory>();
            service.Register(typeof(IRepository<>), typeof(Repository<>));
            service.RegisterAssembly(GetType().Assembly, (serviceType, implementingType) => serviceType.Name.EndsWith("Repository"));
        }
    }
}
