using System;
using System.Threading.Tasks;
using Netco.ActionPolicyServices;
using Netco.Utils;

namespace ThreeDCartAccess.Misc
{
	public static class ActionPolicies
	{
#if DEBUG
		private const int _retryCount = 1;
#else
		private const int _retryCount = 10;
#endif

		public static ActionPolicy Submit
		{
			get { return _sumbitPolicy; }
		}

		private static readonly ActionPolicy _sumbitPolicy = ActionPolicy.Handle< Exception >().Retry( _retryCount, ( ex, i ) =>
		{
			ThreeDCartLogger.Log.Trace( ex, "Retrying 3dCart API submit call for the {0} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicyAsync SubmitAsync
		{
			get { return _sumbitAsyncPolicy; }
		}

		private static readonly ActionPolicyAsync _sumbitAsyncPolicy = ActionPolicyAsync.Handle< Exception >().RetryAsync( _retryCount, async ( ex, i ) =>
		{
			ThreeDCartLogger.Log.Trace( ex, "Retrying 3dCart API submit call for the {0} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicy Get
		{
			get { return _getPolicy; }
		}

		private static readonly ActionPolicy _getPolicy = ActionPolicy.Handle< Exception >().Retry( _retryCount, ( ex, i ) =>
		{
			ThreeDCartLogger.Log.Trace( ex, "Retrying 3dCart API get call for the {0} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicyAsync GetAsync
		{
			get { return _getAsyncPolicy; }
		}

		private static readonly ActionPolicyAsync _getAsyncPolicy = ActionPolicyAsync.Handle< Exception >().RetryAsync( _retryCount, async ( ex, i ) =>
		{
			ThreeDCartLogger.Log.Trace( ex, "Retrying 3dCart API get call for the {0} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 0.5 + i ) );
		} );
	}
}