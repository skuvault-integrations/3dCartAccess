using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using ThreeDCartAccess.Models;

namespace ThreeDCartAccess.Misc
{
	internal class WebRequestServices
	{
		public TResponse Get< TResponse, TFunkResponse >( Func< TFunkResponse > func, bool skipErrors = false )
			where TFunkResponse : XElement
		{
			//			this.LogRequest( func.Method.Name, credentials, request );
			var result = ActionPolicies.Get.Get( () =>
			{
				var funkResult = func();
				return this.ParseResult< TResponse >( funkResult );
			} );
			//			this.LogResponse( func.Method.Name, credentials, result, skipErrors );

			return result;
		}

		public async Task< TResponse > GetAsync< TResponse, TFunkResponse >( Func< Task< TFunkResponse > > func, bool skipErrors = false )
			where TFunkResponse : XElement
		{
			//			this.LogRequest( func.Method.Name, credentials, request );
			var result = await ActionPolicies.GetAsync.Get( async () =>
			{
				var funkResult = await func();
				return this.ParseResult< TResponse >( funkResult );
			} );
			//			this.LogResponse( func.Method.Name, credentials, result, skipErrors );

			return result;
		}

		private T ParseResult< T >( XElement xElement )
		{
			if( xElement.Name.LocalName == "Error" )
			{
				var error = xElement.ToString().Deserialize< ThreeDCartError >();
				throw new Exception( error.Message );
			}

			var result = xElement.ToString().Deserialize< T >();
			return result;
		}

		//private void LogRequest< T >( string methodName, SecurityCredentialType credentials, T obj )
		//{
		//	var json = this.Serializer.Serialize( obj );
		//	var logstr = string.Format( "Request for {0}\tApplication:{1}\tUserToken:{2}\nData: {3}", methodName, credentials.Application, credentials.UserToken, json );
		//	NetworkSolutionsLogger.Log.Trace( logstr );
		//}

		//private void LogResponse< T >( string methodName, SecurityCredentialType credentials, T obj, bool skipErrors = false )
		//{
		//	var json = this.Serializer.Serialize( obj );
		//	var logStr = string.Format( " response for {0}\tApplication:{1}\tUserToken:{2}\nData: {3}", methodName, credentials.Application, credentials.UserToken, json );

		//	if( json.Contains( "\"Status\":1" ) )
		//		NetworkSolutionsLogger.Log.Trace( "Success" + logStr );
		//	else
		//	{
		//		if( skipErrors )
		//			NetworkSolutionsLogger.Log.Trace( "Skipped failed" + logStr );
		//		else
		//		{
		//			logStr = "Failed" + logStr;
		//			NetworkSolutionsLogger.Log.Error( logStr );
		//			throw new Exception( "Was received an error from Network Solutions. See logs or inner exception for details", new Exception( logStr ) );
		//		}
		//	}
		//}
	}
}