// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This class is a container for extensions methods to the <see cref="IServiceCollection"/> interface.
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extension method for cloning an instace of <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection Clone(this IServiceCollection serviceCollection)
        {
            IServiceCollection clone = new ServiceCollection();
            
            foreach (var service in serviceCollection)
                clone.Add(service);
            
            return clone;
        }
    }
}