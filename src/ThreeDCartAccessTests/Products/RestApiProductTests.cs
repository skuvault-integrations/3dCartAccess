using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using Netco.Logging;
using NUnit.Framework;
using ThreeDCartAccess;
using ThreeDCartAccess.RestApi.Models.Configuration;
using ThreeDCartAccess.RestApi.Models.Product;

namespace ThreeDCartAccessTests.Products
{
	public class RestApiProductTests
	{
		private IThreeDCartFactory ThreeDCartFactory;
		private ThreeDCartConfig Config;

		[ SetUp ]
		public void Init()
		{
			NetcoLogger.LoggerFactory = new ConsoleLoggerFactory();
			const string credentialsFilePath = @"..\..\Files\RestApiThreeDCartCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< RestApiTestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true, IgnoreUnknownColumns = true } ).FirstOrDefault();

			if( testConfig != null )
			{
				this.ThreeDCartFactory = new ThreeDCartFactory();
				this.Config = new ThreeDCartConfig( testConfig.StoreUrl, testConfig.PrivateKey, testConfig.Token, testConfig.TimeZone );
			}
		}

		[ Test ]
		public void IsGetProducts()
		{
			var service = this.ThreeDCartFactory.CreateRestProductsService( this.Config );
			var result = service.IsGetProducts();

			result.Should().BeTrue();
		}

		[ Test ]
		public void GetAllProducts()
		{
			var service = this.ThreeDCartFactory.CreateRestProductsService( this.Config );
			var result = service.GetAllProducts();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetAllProducts2()
		{
			var service = this.ThreeDCartFactory.CreateRestProductsService( this.Config );
			var result = new List< ThreeDCartProduct >();
			service.GetAllProducts( x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetAllProductsAsync()
		{
			var service = this.ThreeDCartFactory.CreateRestProductsService( this.Config );
			var result = await service.GetAllProductsAsync();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetAllProductsAsync2()
		{
			var service = this.ThreeDCartFactory.CreateRestProductsService( this.Config );
			var result = new List< ThreeDCartProduct >();
			await service.GetAllProductsAsync( x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}
	}
}