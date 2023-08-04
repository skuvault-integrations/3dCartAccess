using System.Linq;
using LINQtoCSV;
using Microsoft.Extensions.Options;
using ThreeDCartAccess;
using ThreeDCartAccess.RestApi.Models.Configuration;
using ThreeDCartAccess.SoapApi.Models.Configuration;

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
				var testDevSettings = Options.Create( new SkuVaultDeveloperSettings() );
				this.ThreeDCartFactory = new ThreeDCartFactory( testDevSettings, TestHelper.CreateLogger() );
				this.Config = new ThreeDCartConfig( testConfig.StoreUrl, testConfig.UserKey, testConfig.TimeZone );
			}
		}
	}
}