using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using NUnit.Framework;
using ThreeDCartAccess;
using ThreeDCartAccess.Models.Configuration;

namespace ThreeDCartAccessTests.Orders
{
	public class OrderTests
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
				this.Config = new ThreeDCartConfig( testConfig.StoreUrl, testConfig.UserKey );
			}
		}

		[ Test ]
		public void GetOrders()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = service.GetOrders();

			result.Should().NotBeNull();
			//result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersAsync()
		{
			var service = this.ThreeDCartFactory.CreateOrdersService( this.Config );
			var result = await service.GetOrdersAsync();

			result.Should().NotBeNull();
			//result.Count().Should().BeGreaterThan( 0 );
		}
	}
}