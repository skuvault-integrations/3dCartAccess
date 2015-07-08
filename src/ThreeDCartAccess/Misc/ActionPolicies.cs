using System;
using System.Threading.Tasks;
using Netco.ActionPolicyServices;
using Netco.Utils;

namespace ThreeDCartAccess.Misc
{
	public class ActionPolicies
	{
		public ActionPolicies( int retryCount = 10 )
		{
			this._retryCount = retryCount;
		}

		private readonly int _retryCount;

		public ActionPolicy Submit
		{
			get
			{
				return ActionPolicy.Handle< Exception >().Retry( this._retryCount, ( ex, i ) =>
				{
					ThreeDCartLogger.Log.Trace( ex, "Retrying 3dCart API submit call for the {0} time", i );
					SystemUtil.Sleep( TimeSpan.FromSeconds( 10 + 20 * i ) );
				} );
			}
		}

		public ActionPolicyAsync SubmitAsync
		{
			get
			{
				return ActionPolicyAsync.Handle< Exception >().RetryAsync( this._retryCount, async ( ex, i ) =>
				{
					ThreeDCartLogger.Log.Trace( ex, "Retrying 3dCart API submit call for the {0} time", i );
					await Task.Delay( TimeSpan.FromSeconds( 10 + 20 * i ) );
				} );
			}
		}

		public ActionPolicy Get
		{
			get
			{
				return ActionPolicy.Handle< Exception >().Retry( this._retryCount, ( ex, i ) =>
				{
					ThreeDCartLogger.Log.Trace( ex, "Retrying 3dCart API get call for the {0} time", i );
					SystemUtil.Sleep( TimeSpan.FromSeconds( 10 + 20 * i ) );
				} );
			}
		}

		public ActionPolicyAsync GetAsync
		{
			get
			{
				return ActionPolicyAsync.Handle< Exception >().RetryAsync( this._retryCount, async ( ex, i ) =>
				{
					ThreeDCartLogger.Log.Trace( ex, "Retrying 3dCart API get call for the {0} time", i );
					await Task.Delay( TimeSpan.FromSeconds( 10 + 20 * i ) );
				} );
			}
		}
	}
}