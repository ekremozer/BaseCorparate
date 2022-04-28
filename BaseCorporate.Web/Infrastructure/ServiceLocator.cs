using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseCorporate.Web.Infrastructure
{
    public static class ServiceLocator
    {
        private static IServiceProviderProxy _diProxy;

        public static IServiceProviderProxy ServiceProvider => _diProxy ?? throw new Exception("You should Initialize the ServiceProvider before using it.");

        public static void Initialize(IServiceProviderProxy proxy)
        {
            _diProxy = proxy;
        }
    }
}
