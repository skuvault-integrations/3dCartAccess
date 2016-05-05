using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using ThreeDCartAccess.Misc;
using ThreeDCartAccess.V1.Models;
using ThreeDCartAccess.V1.Models.Configuration;

namespace ThreeDCartAccess.V1.Misc
{
	internal class WebRequestServices
	{
		public TResponse Execute< TResponse >( string methodName, ThreeDCartConfig config, Func< XElement > func )
		{
			if( methodName == null )
				methodName = func.Method.Name;

			this.LogRequest( methodName, config );
			var funkResult = func();
			var funkResultStr = funkResult.ToString();
			this.LogResponse( methodName, config, funkResultStr );
			var result = this.ParseResult< TResponse >( funkResult, funkResultStr );

			return result;
		}

		public async Task< TResponse > ExecuteAsync< TResponse >( string methodName, ThreeDCartConfig config, Func< Task< XElement > > func )
		{
			if( methodName == null )
				methodName = func.Method.Name;

			this.LogRequest( methodName, config );
			var funkResult = await func();
			var funkResultStr = funkResult.ToString();
			this.LogResponse( methodName, config, funkResultStr );
			var result = this.ParseResult< TResponse >( funkResult, funkResultStr );

			return result;
		}

		public T ParseResult< T >( XElement xElement, string xElementStr )
		{
			if( xElement.Name.LocalName == "Error" )
			{
				var error = xElementStr.Deserialize< ThreeDCartError >();
				return this.ProcessError< T >( error );
			}

			var result = xElementStr.Deserialize< T >();

			if( xElement.Name.LocalName == "runQueryResponse" )
			{
				var queryResult = result as RunQueryResponse;
				if( queryResult != null && queryResult.Error != null )
					return this.ProcessError< T >( queryResult.Error );
			}

			return result;
		}

		private T ProcessError< T >( ThreeDCartError error )
		{
			if( error.Id >= 46 && error.Id <= 49 )
				return default(T);
			throw new Exception( error.Message ?? error.Description );
		}

		private void LogRequest( string methodName, ThreeDCartConfig config )
		{
			var logstr = string.Format( "Request for {0}\tStoreUrl:{1}", methodName, config.StoreUrl );
			ThreeDCartLogger.Log.Trace( logstr );
		}

		private void LogResponse( string methodName, ThreeDCartConfig config, string response )
		{
			var logstr = string.Format( "Response for {0}\tStoreUrl:{1}\tData:\n {2}", methodName, config.StoreUrl, response );
			ThreeDCartLogger.Log.Trace( logstr );
		}
	}
}