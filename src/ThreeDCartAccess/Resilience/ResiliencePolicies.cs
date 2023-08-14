using System;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using SkuVault.Integrations.Core.Logging;

namespace ThreeDCartAccess.Resilience;

public static class ResiliencePolicies
{
	private const int RetryCount = 10;

	public static RetryPolicy Submit( IIntegrationLogger logger ) => Policy.Handle< Exception >().WaitAndRetry( RetryCount,
		( retryAttempt, _ ) => TimeSpan.FromSeconds( 10 + 20 * retryAttempt ),
		( _, retryDelay, _ ) =>
		{
			logger.Logger.LogTrace( "Retrying 3dCart API submit call after {RetryDelay} seconds", retryDelay.TotalSeconds );
		} );

	public static AsyncRetryPolicy SubmitAsync( IIntegrationLogger logger ) => Policy.Handle< Exception >().WaitAndRetryAsync( RetryCount,
		( retryAttempt, _ ) => TimeSpan.FromSeconds( 10 + 20 * retryAttempt ),
		( _, _, retryAttempt, _ ) =>
		{
			logger.Logger.LogTrace( "Retrying 3dCart API submit call for the {RetryAttempt} time", retryAttempt );
		} );

	public static RetryPolicy Get( IIntegrationLogger logger ) => Policy.Handle< Exception >().WaitAndRetry( RetryCount,
		( retryAttempt, _ ) => TimeSpan.FromSeconds( 10 + 20 * retryAttempt ),
		( _, retryDelay, _ ) =>
		{
			logger.Logger.LogTrace( "Retrying 3dCart API get call after {RetryDelay} seconds", retryDelay.TotalSeconds );
		} );

	public static AsyncRetryPolicy GetAsync( IIntegrationLogger logger ) => Policy.Handle< Exception >().WaitAndRetryAsync( RetryCount,
		( retryAttempt, _ ) => TimeSpan.FromSeconds( 10 + 20 * retryAttempt ),
		( _, _, retryAttempt, _ ) =>
		{
			logger.Logger.LogTrace( "Retrying 3dCart API get call for the {RetryAttempt} time", retryAttempt );
		} );
}