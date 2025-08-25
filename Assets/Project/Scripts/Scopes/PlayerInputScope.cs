using VContainer;
using VContainer.Unity;

namespace Project.Scripts.Scopes
{
    public class PlayerInputScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerInput>(Lifetime.Singleton);
        }
    }
}