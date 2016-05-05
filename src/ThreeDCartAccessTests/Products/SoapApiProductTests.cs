using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using Netco.Logging;
using NUnit.Framework;
using ThreeDCartAccess.SoapApi;
using ThreeDCartAccess.SoapApi.Models.Configuration;
using ThreeDCartAccess.SoapApi.Models.Product;

namespace ThreeDCartAccessTests.Products
{
	public class SoapApiProductTests
	{
		private IThreeDCartFactory ThreeDCartFactory;
		private ThreeDCartConfig Config;

		[ SetUp ]
		public void Init()
		{
			NetcoLogger.LoggerFactory = new ConsoleLoggerFactory();
			const string credentialsFilePath = @"..\..\Files\ThreeDCartCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< SoapApiTestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true, IgnoreUnknownColumns = true } ).FirstOrDefault();

			if( testConfig != null )
			{
				this.ThreeDCartFactory = new ThreeDCartFactory();
				this.Config = new ThreeDCartConfig( testConfig.StoreUrl, testConfig.UserKey, testConfig.TimeZone );
			}
		}

		[ Test ]
		public void IsGetProducts()
		{
			var service = this.ThreeDCartFactory.CreateProductsService( this.Config );
			var result = service.IsGetProducts();

			result.Should().Be( true );
		}

		[ Test ]
		public void GetProducts()
		{
			var service = this.ThreeDCartFactory.CreateProductsService( this.Config );
			var result = service.GetProducts().ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetProductsAsync()
		{
			var service = this.ThreeDCartFactory.CreateProductsService( this.Config );
			var result = ( await service.GetProductsAsync() ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void IsGetInventory()
		{
			var service = this.ThreeDCartFactory.CreateProductsService( this.Config );
			var result = service.IsGetInventory();

			result.Should().Be( true );
		}

		[ Test ]
		public void GetInventory()
		{
			var service = this.ThreeDCartFactory.CreateProductsService( this.Config );
			var result = service.GetInventory().ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetInventoryAsync()
		{
			var service = this.ThreeDCartFactory.CreateProductsService( this.Config );
			var result = ( await service.GetInventoryAsync() ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void UpdateInventory()
		{
			var service = this.ThreeDCartFactory.CreateProductsService( this.Config );
			var inventory = new List< ThreeDCartUpdateInventory >
			{
				new ThreeDCartUpdateInventory { ProductId = "testBundle2", OptionCode = "testBundle2-red", NewQuantity = 1 }
			};
			var result = service.UpdateInventory( inventory, true ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().Be( 2 );
		}

		[ Test ]
		public async Task UpdateInventoryAsync()
		{
			var service = this.ThreeDCartFactory.CreateProductsService( this.Config );
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