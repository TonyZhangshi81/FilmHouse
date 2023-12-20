using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmHouse.Core.DependencyInjection
{
    public static class IServiceProviderExtension
    {
        public static void SetActivator(this IServiceProvider serviceProvider)
        {
            ServiceActivator.Configure(serviceProvider);
        }
    }
}
