using System.Linq;
using LINQtoCSV;
using Microsoft.Extensions.DependencyInjection;
using ThreeDCartAccess;
using ThreeDCartAccess.DependencyInjection;
using ThreeDCartAccess.RestApi.Models.Configuration;

namespace ThreeDCartAccessTests
{
	public abstract class BaseRestApiTests
	{
		protected IThreeDCartFactory ThreeDCartFactory;
		
		protected string StoreUrl { get; set; }
		protected string Token { get; set; }
		protected int TimeZone { get; set; }

		protected RestThreeDCartConfig GetConfig() => new RestThreeDCartConfig( this.StoreUrl, this.Token, this.TimeZone );

		/// <summary>
		/// Get REST test credentials from .csv
		/// </summary>
		protected void GetCredentials()
		{
			const string credentialsFilePath = @"..\..\Files\RestApiThreeDCartCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< RestApiTestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true, IgnoreUnknownColumns = true } ).FirstOrDefault();

			if( testConfig != null )
			{
				var serviceProvider = TestHelper.CreateServiceProvider( testConfig.PrivateKey );
				this.ThreeDCartFactory = serviceProvider.GetRequiredService< IThreeDCartFactory >();
				this.StoreUrl = testConfig.StoreUrl;
				this.Token = testConfig.Token;
				this.TimeZone = testConfig.TimeZone;
			}
		}
	}
}