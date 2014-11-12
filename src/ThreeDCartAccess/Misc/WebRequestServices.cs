using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using ThreeDCartAccess.Models;
using ThreeDCartAccess.Models.Configuration;

namespace ThreeDCartAccess.Misc
{
	internal class WebRequestServices
	{
		public TResponse Get< TResponse >( ThreeDCartConfig config, Func< XElement > func )
		{
			this.LogRequest( func.Method.Name, config );
			var result = ActionPolicies.Get.Get( () =>
			{
				var funkResult = func();
				return this.ParseResult< TResponse >( func.Method.Name, config, funkResult );
			} );

			return result;
		}

		public async Task< TResponse > GetAsync< TResponse >( ThreeDCartConfig config, Func< Task< XElement > > func )
		{
			this.LogRequest( func.Method.Name, config );
			var result = await ActionPolicies.GetAsync.Get( async () =>
			{
				var funkResult = await func();
				return this.ParseResult< TResponse >( func.Method.Name, config, funkResult );
			} );

			return result;
		}

		public TResponse Submit< TResponse >( ThreeDCartConfig config, Func< XElement > func )
		{
			this.LogRequest( func.Method.Name, config );
			var result = ActionPolicies.Submit.Get( () =>
			{
				var funkResult = func();
				return this.ParseResult< TResponse >( func.Method.Name, config, funkResult );
			} );

			return result;
		}

		public async Task< TResponse > SubmitAsync< TResponse >( ThreeDCartConfig config, Func< Task< XElement > > func )
		{
			this.LogRequest( func.Method.Name, config );
			var result = await ActionPolicies.SubmitAsync.Get( async () =>
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
				ThreeDCartLogger.Log.Error( "Failed" + logStr );
				var error = xElementStr.Deserialize< ThreeDCartError >();
				throw new Exception( error.Message );
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