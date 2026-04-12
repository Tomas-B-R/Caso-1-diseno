using DuaBusiness.Application.Interfaces;
using DuaBusiness.Domain.Interfaces;
using DuaBusiness.Infrastructure.Configuration;
using DuaBusiness.Infrastructure.Health;
using DuaBusiness.Infrastructure.Notifications;
using DuaBusiness.Infrastructure.Observability;
using DuaBusiness.Infrastructure.Persistence;
using DuaBusiness.Infrastructure.Persistence.Repositories;
using DuaBusiness.Infrastructure.Processing;
using DuaBusiness.Infrastructure.Security;
using DuaBusiness.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DuaBusiness.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiSecurityOptions>(configuration.GetSection(ApiSecurityOptions.SectionName));
        services.Configure<BlobStorageOptions>(configuration.GetSection(BlobStorageOptions.SectionName));
        services.Configure<NotificationHubOptions>(configuration.GetSection(NotificationHubOptions.SectionName));
        services.Configure<ProcessingOptions>(configuration.GetSection(ProcessingOptions.SectionName));
        services.Configure<AiInterpretationOptions>(configuration.GetSection(AiInterpretationOptions.SectionName));
        services.Configure<ObservabilityOptions>(configuration.GetSection(ObservabilityOptions.SectionName));
        services.Configure<RetentionOptions>(configuration.GetSection(RetentionOptions.SectionName));
        services.Configure<KeyVaultOptions>(configuration.GetSection(KeyVaultOptions.SectionName));
        services.Configure<SqlDatabaseOptions>(configuration.GetSection(SqlDatabaseOptions.SectionName));

        services.AddHttpContextAccessor();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetSection(SqlDatabaseOptions.SectionName)["ConnectionString"]));

        services.AddScoped<IProcessingJobRepository, ProcessingJobRepository>();
        services.AddScoped<ITemplateRepository, TemplateRepository>();
        services.AddScoped<INotificationSubscriptionRepository, NotificationSubscriptionRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IUnitOfWork, SqlUnitOfWork>();

        services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
        services.AddScoped<IDocumentIngestionOrchestrator, DocumentIngestionOrchestrator>();
        services.AddScoped<IFileStorageService, BlobFileStorageService>();
        services.AddScoped<IDuaDocumentGenerator, DuaDocumentGenerator>();
        services.AddScoped<ISemanticInterpretationService, SemanticInterpretationService>();
        services.AddScoped<IConfidenceFlagService, ConfidenceFlagService>();
        services.AddScoped<INotificationPublisher, NotificationHubPublisher>();
        services.AddScoped<IAuditTrailWriter, ApplicationTelemetry>();

        services.AddScoped<IExtractionStrategy, PdfExtractionStrategy>();
        services.AddScoped<IExtractionStrategy, WordExtractionStrategy>();
        services.AddScoped<IExtractionStrategy, ExcelExtractionStrategy>();
        services.AddScoped<IExtractionStrategy, ImageOcrExtractionStrategy>();

        services.AddHealthChecks()
            .AddCheck<BlobStorageHealthCheck>("blob-storage")
            .AddCheck<SqlDatabaseHealthCheck>("sql-database")
            .AddCheck<NotificationHubHealthCheck>("notification-hub");

        return services;
    }
}
