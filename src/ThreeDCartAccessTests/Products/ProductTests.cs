using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using NUnit.Framework;
using ThreeDCartAccess;
using ThreeDCartAccess.Models.Configuration;
using ThreeDCartAccess.Models.Product;

namespace ThreeDCartAccessTests.Products
{
	public class ProductTests
	{
		private IThreeDCartFactory ThreeDCartFactory;
		private ThreeDCartConfig Config;

		[ SetUp ]
		public void Init()
		{
			const string credentialsFilePath = @"..\..\Files\ThreeDCartCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< TestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ).FirstOrDefault();

			if( testConfig != null )
			{
				this.ThreeDCartFactory = new ThreeDCartFactory();
				this.Config = new ThreeDCartConfig( testConfig.StoreUrl, testConfig.UserKey, testConfig.TimeZone );
			}
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
		public void UpdateInventory()
		{
			var service = this.ThreeDCartFactory.CreateProductsService( this.Config );
			var inventory = new ThreeDCartUpdateInventory { ProductId = "testSku1", Quantity = 2, IsReplaceQty = true };
			var result = service.UpdateInventory( inventory );

			result.Should().NotBeNull();
			result.Quantity.Should().Be( 2 );
		}

		[ Test ]
		public async Task UpdateInventoryAsync()
		{
			var service = this.ThreeDCartFactory.CreateProductsService( this.Config );
			var inventory = new ThreeDCartUpdateInventory { ProductId = "testSku1", Quantity = 2, IsReplaceQty = true };
			var result = await service.UpdateInventoryAsync( inventory );

			result.Should().NotBeNull();
			result.Quantity.Should().Be( 2 );
		}

		[ Test ]
		public void UpdateInventoryList()
		{
			var service = this.ThreeDCartFactory.CreateProductsService( this.Config );
			var inventory = new List< ThreeDCartUpdateInventory >
			{
				new ThreeDCartUpdateInventory { ProductId = "testSku1", Quantity = 2, IsReplaceQty = true }
			};
			var result = service.UpdateInventory( inventory ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().Be( 1 );
		}

		[ Test ]
		public async Task UpdateInventoryListAsync()
		{
			var service = this.ThreeDCartFactory.CreateProductsService( this.Config );
			var inventory = new List< ThreeDCartUpdateInventory >
			{
				new ThreeDCartUpdateInventory { ProductId = "testSku1", Quantity = 2, IsReplaceQty = true }
			};
			var result = ( await service.UpdateInventoryAsync( inventory ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().Be( 1 );
		}
	}
}