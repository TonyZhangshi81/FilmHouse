using FilmHouse.Core.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace FilmHouse.Core.DependencyInjection
{
    internal class ServiceActivator
    {
        private static IServiceProvider ServiceProvider = null;

        /// <summary>
        /// Configure ServiceActivator with full serviceProvider
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void Configure(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Create a scope where use this ServiceActivator
        /// </summary>
        /// <returns></returns>
        public static IServiceScope GetScope()
        {
            var provider = Guard.GetNotNull(ServiceProvider, nameof(IServiceProvider));
            return provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        }

        public static T GetRequiredService<T>() where T : notnull
        {
            var provider = Guard.GetNotNull(ServiceProvider, nameof(IServiceProvider));
            return provider.GetRequiredService<T>();
        }
    }
}
