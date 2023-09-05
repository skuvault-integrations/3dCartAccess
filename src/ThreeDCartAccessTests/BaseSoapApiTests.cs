using System.Linq;
using LINQtoCSV;
using Microsoft.Extensions.DependencyInjection;
using ThreeDCartAccess;
using ThreeDCartAccess.SoapApi.Models.Configuration;
using ThreeDCartAccessTests.Integration;

namespace ThreeDCartAccessTests
{
	public class BaseSoapApiTests
	{
		protected IThreeDCartFactory ThreeDCartFactory;
		protected ThreeDCartConfig Config;

		/// <summary>
		/// Get SOAP test credentials from .csv
		/// </summary>
		protected void GetCredentials()
		{
			const string credentialsFilePath = @"..\..\Files\ThreeDCartCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< SoapApiTestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true, IgnoreUnknownColumns = true } ).FirstOrDefault();

			if( testConfig != null )
			{
				this.SetupDependencyInjection();
				this.Config = new ThreeDCartConfig( testConfig.StoreUrl, testConfig.UserKey, testConfig.TimeZone );
			}
		}

		private void SetupDependencyInjection()
		{
			var serviceProvider = TestHelper.CreateServiceProvider( apiPrivateKey : "" );
			this.ThreeDCartFactory = serviceProvider.GetRequiredService< IThreeDCartFactory >();
		}
	}
}