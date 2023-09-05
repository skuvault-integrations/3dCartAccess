using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using SkuVault.Integrations.Core.Logging;
using ThreeDCartAccess.SoapApi.Models;
using ThreeDCartAccess.SoapApi.Models.Configuration;

namespace ThreeDCartAccess.SoapApi.Misc
{
	internal class WebRequestServices
	{
		private readonly IIntegrationLogger _logger;

		public WebRequestServices( IIntegrationLogger logger )
		{
			this._logger = logger;
		}

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

			if( funkResult.Name != null && funkResult.Name.LocalName != "Error" )
			{
				var funkResultStr = funkResult.ToString();
				this.LogResponse( methodName, config, funkResultStr );
				return this.ParseResult< TResponse >( funkResult, funkResultStr );
			}

			return default(TResponse);
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
			this._logger.Logger.LogTrace( "Request for {MethodName}\tStoreUrl:{StoreUrl}", methodName, config.StoreUrl );
		}

		private void LogResponse( string methodName, ThreeDCartConfig config, string response )
		{
			this._logger.Logger.LogTrace( "Response for {MethodName}\tStoreUrl:{StoreUrl}\tData:\n {Response}", methodName, config.StoreUrl, response );
		}
	}
}