using System;
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
using ThreeDCartAccess.RestApi.Models.Order;
using ThreeDCartAccess.SoapApi.Misc;

namespace ThreeDCartAccessTests.Integration.Orders
{
	public class RestApiOrderTests : BaseRestApiTests
	{
		private IThreeDCartFactory ThreeDCartFactory;
		private ILogger _logger;

		[ SetUp ]
		public void Init()
		{
			this._logger = TestHelper.CreateLogger();
			const string credentialsFilePath = @"..\..\Files\RestApiThreeDCartCredentials.csv";
			var cc = new CsvContext();
			var rawTestConfig = cc.Read< RestApiTestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true, IgnoreUnknownColumns = true } ).FirstOrDefault();
			var testConfig = Options.Create( new ThreeDCartSettings() {
				PrivateApiKey = rawTestConfig?.PrivateKey
			});

			if( rawTestConfig != null )
			{
				this.ThreeDCartFactory = new ThreeDCartFactory( new SoapWebRequestServices( this._logger ), testConfig, this._logger );
				this.StoreUrl = rawTestConfig.StoreUrl;
				this.Token = rawTestConfig.Token;
				this.TimeZone = rawTestConfig.TimeZone;
			}
		}

		[ Test ]
		[ Explicit ]
		public void IsGetNewOrders()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig() );
			var result = service.IsGetNewOrders();

			result.Should().BeTrue();
		}

		#region Get New Orders
		[ Test ]
		[ Explicit ]
		public void GetNewOrders()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig() );
			var result = service.GetNewOrders( new DateTime( 2021, 11, 2 ), new DateTime( 2022, 11, 4 ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public void GetNewOrders2()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig() );
			var result = new List< ThreeDCartOrder >();
			service.GetNewOrders( DateTime.UtcNow.AddDays( -3 ), DateTime.UtcNow, x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public async Task GetNewOrdersAsync()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig() );
			var result = await service.GetNewOrdersAsync( DateTime.UtcNow.AddDays( -1 ), DateTime.UtcNow );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public async Task GetNewOrdersAsync2()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig() );
			var result = new List< ThreeDCartOrder >();
			await service.GetNewOrdersAsync( DateTime.UtcNow.AddHours( -3 ), DateTime.UtcNow, x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}
		#endregion

		#region Get Updated Orders
		[ Test ]
		[ Explicit ]
		public async Task GetUpdatedOrdersAsync()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig() );
			var result = await service.GetUpdatedOrdersAsync( DateTime.UtcNow.AddDays( -1 ), DateTime.UtcNow );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}
		#endregion

		#region Get Orders By Number
		[ Test ]
		[ Explicit ]
		public void GetOrdersByNumber()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig() );
			var numbers = new List< string > { "AB-1043" };
			var result = service.GetOrdersByNumber( numbers, new DateTime( 2014, 12, 2 ), new DateTime( 2014, 12, 4 ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public void GetOrdersByNumber2()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig() );
			var numbers = new List< string > { "AB-1043", "AB-1042" };
			var result = new List< ThreeDCartOrder >();
			service.GetOrdersByNumber( numbers, DateTime.UtcNow.AddDays( -3 ), DateTime.UtcNow, x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public async Task GetOrdersByNumberAsync()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig() );
			var numbers = new List< string > { "AB-1043", "AB-1042" };
			var result = await service.GetOrdersByNumberAsync( numbers, DateTime.UtcNow.AddDays( -100 ), DateTime.UtcNow );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		[ Explicit ]
		public async Task GetOrdersByNumberAsync2()
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig() );
			var numbers = new List< string > { "AB-1043" };
			var result = new List< ThreeDCartOrder >();
			await service.GetOrdersByNumberAsync( numbers, DateTime.UtcNow.AddHours( -9 ), DateTime.UtcNow, x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}
		#endregion
	}
}