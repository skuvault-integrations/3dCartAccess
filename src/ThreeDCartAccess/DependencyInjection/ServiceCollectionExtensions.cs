using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ThreeDCartAccess.Configuration;
using ThreeDCartAccess.SoapApi.Misc;

namespace ThreeDCartAccess.DependencyInjection;

/// <summary>
/// Provides a way to register all the required services for the 3DCart Access library use.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all the required services for the ThreeDCart Access library with the provided <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> the services are being registered with</param>
    /// <param name="apiPrivateKey">The private key used to access the 3DCart API</param>
    /// <param name="configureLogging">The <see cref="ILoggingBuilder"/> configuration delegate</param>
    /// <returns>The <see cref="IServiceCollection"/> the method was called on</returns>
    /// <exception cref="ArgumentNullException">If the <see cref="IServiceCollection"/> provided is null</exception>
    public static IServiceCollection Add3DCartServices( this IServiceCollection services, string apiPrivateKey, Action<ILoggingBuilder> configureLogging ) {
        if( services is null )
        {
            throw new ArgumentNullException(nameof(services));
        }

        if( configureLogging is null )
        {
            throw new ArgumentException(nameof(configureLogging));
        }

        var internalServicesCollection = new ServiceCollection();
        internalServicesCollection.RegisterInternalServices( apiPrivateKey, configureLogging );
        var internalServiceProvider = internalServicesCollection.BuildServiceProvider();

        services.AddSingleton< IThreeDCartFactory, ThreeDCartFactory>(_ => ( ThreeDCartFactory )internalServiceProvider.GetRequiredService<IThreeDCartFactory>());

        return services;
    }

    private static IServiceCollection RegisterInternalServices(this IServiceCollection internalServices, string restApiPrivateKey, Action<ILoggingBuilder> configureLogging) {
        internalServices.AddLogging(configureLogging);

        internalServices.Configure<ThreeDCartSettings>( settings => settings.PrivateApiKey = restApiPrivateKey );
        internalServices.AddTransient<ISoapWebRequestServices, SoapWebRequestServices>();
        internalServices.AddSingleton<IThreeDCartFactory, ThreeDCartFactory>( );

        return internalServices;
    }
}