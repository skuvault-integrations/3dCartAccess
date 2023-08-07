using System;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace ThreeDCartAccess.Resilience;

public static class Policies
{
	private const int RetryCount = 10;

	public static RetryPolicy Submit( ILogger< string > logger ) => Policy.Handle< Exception >().WaitAndRetry( RetryCount,
		( retryAttempt, _ ) => TimeSpan.FromSeconds( 10 + 20 * retryAttempt ),
		( _, retryDelay, _ ) =>
		{
			logger.LogTrace( "Retrying 3dCart API submit call after {RetryDelay} seconds", retryDelay.TotalSeconds );
		} );

	public static AsyncRetryPolicy SubmitAsync( ILogger< string > logger ) => Policy.Handle< Exception >().WaitAndRetryAsync( RetryCount,
		( retryAttempt, _ ) => TimeSpan.FromSeconds( 10 + 20 * retryAttempt ),
		( _, _, retryAttempt, _ ) =>
		{
			logger.LogTrace( "Retrying 3dCart API submit call for the {RetryAttempt} time", retryAttempt );
		} );

	public static RetryPolicy Get( ILogger< string > logger ) => Policy.Handle< Exception >().WaitAndRetry( RetryCount,
		( retryAttempt, _ ) => TimeSpan.FromSeconds( 10 + 20 * retryAttempt ),
		( _, retryDelay, _ ) =>
		{
			logger.LogTrace( "Retrying 3dCart API get call after {RetryDelay} seconds", retryDelay.TotalSeconds );
		} );

	public static AsyncRetryPolicy GetAsync( ILogger< string > logger ) => Policy.Handle< Exception >().WaitAndRetryAsync( RetryCount,
		( retryAttempt, _ ) => TimeSpan.FromSeconds( 10 + 20 * retryAttempt ),
		( _, _, retryAttempt, _ ) =>
		{
			logger.LogTrace( "Retrying 3dCart API get call for the {RetryAttempt} time", retryAttempt );
		} );
}