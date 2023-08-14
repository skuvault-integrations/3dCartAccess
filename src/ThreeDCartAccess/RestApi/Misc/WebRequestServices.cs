using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ServiceStack;
using SkuVault.Integrations.Core.Helpers;
using SkuVault.Integrations.Core.Logging;
using ThreeDCartAccess.RestApi.Models;
using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccess.RestApi.Misc
{
	internal class WebRequestServices
	{
		private readonly RestThreeDCartConfig _config;
		private readonly string RestApiPrivateKey;
		private readonly IIntegrationLogger _logger;

		public WebRequestServices( RestThreeDCartConfig config, string restApiPrivateKey, IIntegrationLogger logger )
		{
			this._config = config;
			this.RestApiPrivateKey = restApiPrivateKey;
			this._logger = logger;

			ValidationHelper.ThrowOnValidationErrors< RestThreeDCartConfig >( GetValidationErrors() );
		}

		//TODO GUARD-3057 Add tests
		private IEnumerable< string > GetValidationErrors()
		{
			var validationErrors = new List<string>();
			if ( string.IsNullOrWhiteSpace( this.RestApiPrivateKey ) )
			{
				validationErrors.Add( $"{nameof( this.RestApiPrivateKey )} is null or white space" );
			}
			
			return validationErrors;
		}

		public T GetResponse< T >( string endpoint, string marker )
		{
			var url = this.GetUrl( endpoint );
			this.LogGetInfo( url, marker );

			try
			{
				var request = this.CreateGetServiceGetRequest( url );
				var result = this.TryHandleException( marker, () =>
				{
					using( var response = request.GetResponse() )
						return this.ParseGetResponse< T >( response, marker );
				} );

				return result;
			}
			catch( Exception ex )
			{
				throw this.ExceptionForGetInfo( url, ex, marker );
			}
		}

		public async Task< T > GetResponseAsync< T >( string endpoint, string marker )
		{
			var url = this.GetUrl( endpoint );
			this.LogGetInfo( url, marker );

			try
			{
				var request = this.CreateGetServiceGetRequest( url );
				var result = await this.TryHandleExceptionAsync( marker, async () =>
				{
					using( var response = await request.GetResponseAsync() )
						return this.ParseGetResponse< T >( response, marker );
				} );

				return result;
			}
			catch( Exception ex )
			{
				throw this.ExceptionForGetInfo( url, ex, marker );
			}
		}

		public void PutData( string endpoint, string jsonContent, string marker )
		{
			var url = this.GetUrl( endpoint );
			this.LogPutInfo( url, jsonContent, marker );

			try
			{
				var request = this.CreateServicePutRequest( url, jsonContent );
				using( var response = ( HttpWebResponse )request.GetResponse() )
					this.ParsePutResponse( response, marker );
			}
			catch( Exception ex )
			{
				throw this.ExceptionForPutInfo( url, ex, marker );
			}
		}

		public async Task PutDataAsync( string endpoint, string jsonContent, string marker )
		{
			var url = this.GetUrl( endpoint );
			this.LogPutInfo( url, jsonContent, marker );

			try
			{
				var request = this.CreateServicePutRequest( url, jsonContent );
				using( var response = await request.GetResponseAsync() )
					this.ParsePutResponse( response, marker );
			}
			catch( Exception ex )
			{
				throw this.ExceptionForPutInfo( url, ex, marker );
			}
		}

		#region Misc
		private string GetUrl( string endpoint )
		{
			return string.Concat( this._config.BaseUrl, endpoint );
		}

		private HttpWebRequest CreateGetServiceGetRequest( string url )
		{
			var uri = new Uri( url );
			var request = ( HttpWebRequest )WebRequest.Create( uri );

			request.Method = WebRequestMethods.Http.Get;
			request.Headers.Add( "privatekey", this.RestApiPrivateKey );
			request.Headers.Add( "token", this._config.Token );
			request.Headers.Add( "secureUrl", this._config.StoreUrl );

			return request;
		}

		private HttpWebRequest CreateServicePutRequest( string url, string content )
		{
			var uri = new Uri( url );
			var request = ( HttpWebRequest )WebRequest.Create( uri );

			request.Method = WebRequestMethods.Http.Put;
			request.ContentType = "application/json";
			request.Headers.Add( "privatekey", this.RestApiPrivateKey );
			request.Headers.Add( "token", this._config.Token );
			request.Headers.Add( "secureUrl", this._config.StoreUrl );

			using( var writer = new StreamWriter( request.GetRequestStream() ) )
				writer.Write( content );

			return request;
		}

		private T ParseGetResponse< T >( WebResponse response, string marker )
		{
			var result = default(T);

			using( var stream = response.GetResponseStream() )
			using( var reader = new StreamReader( stream ) )
			{
				var jsonResponse = reader.ReadToEnd();

				this.LogGetInfoResult( response.ResponseUri.OriginalString, ( ( HttpWebResponse )response ).StatusCode, jsonResponse, marker );

				if( !string.IsNullOrEmpty( jsonResponse ) )
					result = jsonResponse.FromJson< T >();
			}

			return result;
		}

		private void ParsePutResponse( WebResponse response, string marker )
		{
			using( var stream = response.GetResponseStream() )
			using( var reader = new StreamReader( stream ) )
			{
				var jsonResponse = reader.ReadToEnd();

				this.LogPutInfoResult( response.ResponseUri.OriginalString, ( ( HttpWebResponse )response ).StatusCode, jsonResponse, marker );
			}
		}

		private T TryHandleException< T >( string marker, Func< T > body )
		{
			try
			{
				return body();
			}
			catch( WebException ex )
			{
				return this.TryHandleException< T >( ex, marker );
			}
		}

		private async Task< T > TryHandleExceptionAsync< T >( string marker, Func< Task< T > > body )
		{
			try
			{
				return await body();
			}
			catch( WebException ex )
			{
				return this.TryHandleException< T >( ex, marker );
			}
		}

		private T TryHandleException< T >( WebException ex, string marker )
		{
			if( ex.Response == null || ex.Status != WebExceptionStatus.ProtocolError ||
			    ex.Response.ContentType == null || ex.Response.ContentType.Contains( "text/html" ) )
				throw ex;

			var jsonError = ex.Response.ReadToEnd();

			var httpWebResponse = ex.Response as HttpWebResponse;
			if( httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.NotFound )
			{
				this._logger.Logger.LogTrace( "Marker: '{Mark}'. Skip not found exception.\n{Error}", marker, jsonError );
				return default(T);
			}

			var errors = jsonError.FromJson< List< ThreeDCartError > >();
			if( errors == null || errors.Count == 0 )
				throw ex;

			var error = errors.First();
			if( error.Message.Equals( "Offset amount exceeds the total number of records", StringComparison.InvariantCultureIgnoreCase ) )
			{
				this._logger.Logger.LogTrace( "Marker: '{Mark}'. Skip exception for paging.\n{Error}", marker, jsonError );
				return default(T);
			}

			throw new WebException( error.Message, ex );
		}

		private void LogGetInfo( string url, string marker )
		{
			this._logger.Logger.LogTrace( "Marker: '{Mark}'. GET call for url '{Url}'", marker, url );
		}

		private void LogGetInfoResult( string url, HttpStatusCode statusCode, string jsonContent, string marker )
		{
			this._logger.Logger.LogTrace( "Marker: '{Mark}'. GET call for url '{Url}' has been completed with code '{StatusCode}'.\n{JsonContent}", 
				marker, url, statusCode, jsonContent );
		}

		private Exception ExceptionForGetInfo( string url, Exception ex, string marker )
		{
			return new Exception( $"Marker: '{marker}'. GET call for url '{url}' failed", ex );
		}

		private void LogPutInfo( string url, string jsonContent, string marker )
		{
			this._logger.Logger.LogTrace( "Marker: '{Mark}'. PUT/POST data for url '{Url}':\n{JsonContent}", marker, url, jsonContent );
		}

		private void LogPutInfoResult( string url, HttpStatusCode statusCode, string jsonContent, string marker )
		{
			this._logger.Logger.LogTrace( "Marker: '{Mark}'. PUT/POST data for url '{Url}' has been completed with code '{StatusCode}'.\n{JsonContent}", marker, url, statusCode, jsonContent );
		}

		private Exception ExceptionForPutInfo( string url, Exception ex, string marker )
		{
			return new Exception( $"Marker: '{marker}'. PUT/POST data for url '{url}' failed", ex );
		}
		#endregion
	}
}