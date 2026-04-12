using DuaBusiness.Application.Interfaces;
using DuaBusiness.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DuaBusiness.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProcessingJobApplicationService, ProcessingJobApplicationService>();
        services.AddScoped<ITemplateApplicationService, TemplateApplicationService>();
        services.AddScoped<INotificationSubscriptionService, NotificationSubscriptionService>();
        services.AddScoped<INotificationCallbackService, NotificationCallbackService>();

        return services;
    }
}
