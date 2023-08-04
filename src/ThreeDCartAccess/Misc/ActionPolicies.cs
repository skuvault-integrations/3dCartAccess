using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Netco.ActionPolicyServices;
using Netco.Utils;

namespace ThreeDCartAccess.Misc
{
	//TODO TD-257 Replace with Polly ActionPolicy and remove any references to Netco
	public static class ActionPolicies
	{
		private const int RetryCount = 10;

		public static ActionPolicy Submit( ILogger logger ) => ActionPolicy.Handle< Exception >().Retry( RetryCount, ( ex, i ) =>
		{
			logger.LogTrace( ex, "Retrying 3dCart API submit call for the {0} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 10 + 20 * i ) );
		} );

		public static ActionPolicyAsync SubmitAsync( ILogger logger ) => ActionPolicyAsync.Handle< Exception >().RetryAsync( RetryCount, async ( ex, i ) =>
		{
			logger.LogTrace( ex, "Retrying 3dCart API submit call for the {0} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 10 + 20 * i ) );
		} );

		public static ActionPolicy Get( ILogger logger ) => ActionPolicy.Handle< Exception >().Retry( RetryCount, ( ex, i ) =>
		{
			logger.LogTrace( ex, "Retrying 3dCart API get call for the {AttemptCount} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 10 + 20 * i ) );
		} );

		public static ActionPolicyAsync GetAsync( ILogger logger ) => ActionPolicyAsync.Handle< Exception >().RetryAsync( RetryCount, async ( ex, i ) =>
		{
			logger.LogTrace( ex, "Retrying 3dCart API get call for the {AttemptCount} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 10 + 20 * i ) );
		} );
	}
}