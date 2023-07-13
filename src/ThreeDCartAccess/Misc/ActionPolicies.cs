using System;
using System.Threading.Tasks;
using Netco.ActionPolicyServices;
using Netco.Utils;

namespace ThreeDCartAccess.Misc
{
	public static class ActionPolicies
	{
		private const int RetryCount = 10;

		public static readonly ActionPolicy Submit = ActionPolicy.Handle< Exception >().Retry( RetryCount, ( ex, i ) =>
		{
			ThreeDCartLogger.Log.Trace( ex, "Retrying 3dCart API submit call for the {0} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 10 + 20 * i ) );
		} );

		public static readonly ActionPolicyAsync SubmitAsync = ActionPolicyAsync.Handle< Exception >().RetryAsync( RetryCount, async ( ex, i ) =>
		{
			ThreeDCartLogger.Log.Trace( ex, "Retrying 3dCart API submit call for the {0} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 10 + 20 * i ) );
		} );

		public static readonly ActionPolicy Get = ActionPolicy.Handle< Exception >().Retry( RetryCount, ( ex, i ) =>
		{
			ThreeDCartLogger.Log.Trace( ex, "Retrying 3dCart API get call for the {0} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 10 + 20 * i ) );
		} );

		public static readonly ActionPolicyAsync GetAsync = ActionPolicyAsync.Handle< Exception >().RetryAsync( RetryCount, async ( ex, i ) =>
		{
			ThreeDCartLogger.Log.Trace( ex, "Retrying 3dCart API get call for the {0} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 10 + 20 * i ) );
		} );
	}
}