using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using ThreeDCartAccess.Models;
using ThreeDCartAccess.Models.Configuration;

namespace ThreeDCartAccess.Misc
{
	internal class WebRequestServices
	{
		private readonly ActionPolicies _actionPolicies;

		public WebRequestServices( ActionPolicies actionPolicies )
		{
			this._actionPolicies = actionPolicies;
		}

		public TResponse Get< TResponse >( ThreeDCartConfig config, Func< XElement > func )
		{
			this.LogRequest( func.Method.Name, config );
			var result = this._actionPolicies.Get.Get( () =>
			{
				var funkResult = func();
				return this.ParseResult< TResponse >( func.Method.Name, config, funkResult );
			} );

			return result;
		}

		public async Task< TResponse > GetAsync< TResponse >( ThreeDCartConfig config, Func< Task< XElement > > func )
		{
			this.LogRequest( func.Method.Name, config );
			var result = await this._actionPolicies.GetAsync.Get( async () =>
			{
				var funkResult = await func();
				return this.ParseResult< TResponse >( func.Method.Name, config, funkResult );
			} );

			return result;
		}

		public TResponse Submit< TResponse >( ThreeDCartConfig config, Func< XElement > func )
		{
			this.LogRequest( func.Method.Name, config );
			var result = this._actionPolicies.Submit.Get( () =>
			{
				var funkResult = func();
				return this.ParseResult< TResponse >( func.Method.Name, config, funkResult );
			} );

			return result;
		}

		public async Task< TResponse > SubmitAsync< TResponse >( ThreeDCartConfig config, Func< Task< XElement > > func )
		{
			this.LogRequest( func.Method.Name, config );
			var result = await this._actionPolicies.SubmitAsync.Get( async () =>
			{
				var funkResult = await func();
				return this.ParseResult< TResponse >( func.Method.Name, config, funkResult );
			} );

			return result;
		}

		private T ParseResult< T >( string methodName, ThreeDCartConfig config, XElement xElement )
		{
			var xElementStr = xElement.ToString();
			var logStr = string.Format( " response for {0}\tStoreUrl:{1}\tData:\n {2}", methodName, config.StoreUrl, xElementStr );

			if( xElement.Name.LocalName == "Error" )
			{
				var error = xElementStr.Deserialize< ThreeDCartError >();
				if( error.Id == 46 || error.Id == 47 || error.Id == 48 || error.Id == 49 )
				{
					ThreeDCartLogger.Log.Trace( "Success" + logStr );
					return default( T );
				}
				ThreeDCartLogger.Log.Error( "Failed" + logStr );
				throw new Exception( error.Message ?? error.Description );
			}

			ThreeDCartLogger.Log.Trace( "Success" + logStr );
			var result = xElementStr.Deserialize< T >();
			return result;
		}

		private void LogRequest( string methodName, ThreeDCartConfig config )
		{
			var logstr = string.Format( "Request for {0}\tStoreUrl:{1}", methodName, config.StoreUrl );
			ThreeDCartLogger.Log.Trace( logstr );
		}
	}
}