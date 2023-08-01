using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using ThreeDCartAccess;
using ThreeDCartAccess.Configuration;
using ThreeDCartAccess.SoapApi.Misc;
using ThreeDCartAccess.SoapApi.Models.Configuration;
using ThreeDCartAccess.SoapApi.Models.Product;

namespace ThreeDCartAccessTests.Integration.Products
{
	public class SoapApiProductTests
	{
		private IThreeDCartFactory ThreeDCartFactory;
		private ThreeDCartConfig Config;
		private ILogger _logger;

		[ SetUp ]
		public void Init()
		{
			this._logger = TestHelper.CreateLogger();
			const string credentialsFilePath = @"..\..\Files\ThreeDCartCredentials.csv";

			var cc = new CsvContext();
			var rawTestConfig = cc.Read< SoapApiTestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true, IgnoreUnknownColumns = true } ).FirstOrDefault();
			var testConfig = Options.Create( new ThreeDCartSettings() );

			if( testConfig != null )
			{
				this.ThreeDCartFactory = new ThreeDCartFactory( new SoapWebRequestServices( this._logger ), testConfig, this._logger );
				this.Config = new ThreeDCartConfig( rawTestConfig.StoreUrl, rawTestConfig.UserKey, rawTestConfig.TimeZone );
			}
		}

		[ Test ]
		[ Explicit ]
		public void IsGetProducts()
		{
			var service = this.ThreeDCartFactory.CreateSoapProductsService( this.Config );
			var result = service.IsGetProducts();

			result.Should().Be( true );
		}

		[ Test ]
		[ Explicit ]
		public void GetProducts()
		{
			var service = this.ThreeDCartFactory.CreateSoapProductsService( this.Config );
			var result = service.GetProducts().ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public async Task GetProductsAsync()
		{
			var service = this.ThreeDCartFactory.CreateSoapProductsService( this.Config );
			var result = ( await service.GetProductsAsync() ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public void IsGetInventory()
		{
			var service = this.ThreeDCartFactory.CreateSoapProductsService( this.Config );
			var result = service.IsGetInventory();

			result.Should().Be( true );
		}

		[ Test ]
		[ Explicit ]
		public void GetInventory()
		{
			var service = this.ThreeDCartFactory.CreateSoapProductsService( this.Config );
			var result = service.GetInventory().ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public async Task GetInventoryAsync()
		{
			var service = this.ThreeDCartFactory.CreateSoapProductsService( this.Config );
			var result = ( await service.GetInventoryAsync() ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public void UpdateInventory()
		{
			var service = this.ThreeDCartFactory.CreateSoapProductsService( this.Config );
			var inventory = new List< ThreeDCartUpdateInventory >
			{
				new ThreeDCartUpdateInventory { ProductId = "testBundle2", OptionCode = "testBundle2-red", NewQuantity = 1 }
			};
			var result = service.UpdateInventory( inventory, true ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().Be( 2 );
		}

		[ Test ]
		[ Explicit ]
		public async Task UpdateInventoryAsync()
		{
			var service = this.ThreeDCartFactory.CreateSoapProductsService( this.Config );
			var inventory = new List< ThreeDCartUpdateInventory >
			{
				new ThreeDCartUpdateInventory { ProductId = "testBundle2", NewQuantity = 2 }
			};
			var result = ( await service.UpdateInventoryAsync( inventory, true ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().Be( 1 );
		}
	}
}