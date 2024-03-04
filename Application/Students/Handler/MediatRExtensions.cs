using Application.Students.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Students.Handler
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddConfigureHandler(this IServiceCollection services)
        {

            return services;
        }
    }
}
