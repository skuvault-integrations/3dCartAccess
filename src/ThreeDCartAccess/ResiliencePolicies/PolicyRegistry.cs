using System;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace ThreeDCartAccess.ResiliencePolicies;

public static class PolicyRegistry
{
    private const int RetryCount = 10;

    public static RetryPolicy CreateSubmitPolicy() => Policy.Handle<Exception>().WaitAndRetry( RetryCount,
        (retryAttempt, _) => TimeSpan.FromSeconds( 10 + 20 * retryAttempt ),
        ( _, retryAttempt, context ) => {
        if( context.TryGetValue( PolicyContext.LoggerKey, out var loggerObject ) && loggerObject is ILogger logger )
        {
            logger.LogTrace( "Retrying 3dCart API submit call for the {RetryAttempt} time", retryAttempt );
        }
    } );

    public static AsyncRetryPolicy CreateSubmitAsyncPolicy() => Policy.Handle< Exception >().WaitAndRetryAsync( RetryCount,
        (retryAttempt, _) => TimeSpan.FromSeconds( 10 + 20 * retryAttempt ),
        ( _, _, retryAttempt, context ) =>
    {
        if( context.TryGetValue( PolicyContext.LoggerKey, out var loggerObject ) && loggerObject is ILogger logger )
        {
            logger.LogTrace( "Retrying 3dCart API submit call for the {RetryAttempt} time", retryAttempt );
        }
    } );

    public static RetryPolicy CreateGetPolicy() => Policy.Handle< Exception >().WaitAndRetry( RetryCount,
        ( retryAttempt, _ ) => TimeSpan.FromSeconds( 10 + 20 * retryAttempt ),
        ( _, retryAttempt, context ) =>
        {
            if( context.TryGetValue( PolicyContext.LoggerKey, out var loggerObject ) && loggerObject is ILogger logger )
            {
                logger.LogTrace( "Retrying 3dCart API get call for the {RetryAttempt} time", retryAttempt );
            }
        } );

    public static AsyncRetryPolicy CreateGetAsyncPolicy() => Policy.Handle< Exception >().WaitAndRetryAsync( RetryCount,
        ( retryAttempt, _ ) => TimeSpan.FromSeconds( 10 + 20 * retryAttempt ),
        ( _, _, retryAttempt, context ) =>
        {
            if( context.TryGetValue( PolicyContext.LoggerKey, out var loggerObject ) && loggerObject is ILogger logger )
            {
                logger.LogTrace( "Retrying 3dCart API get call for the {RetryAttempt} time", retryAttempt );
            }
        } );
}