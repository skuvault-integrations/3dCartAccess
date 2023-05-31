using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using Netco.Logging;
using NUnit.Framework;
using ThreeDCartAccess;
using ThreeDCartAccess.RestApi.Models.Order;

namespace ThreeDCartAccessTests.Orders
{
	public class RestApiOrderTests : BaseRestApiTests
	{
		private IThreeDCartFactory ThreeDCartFactory;

		[ SetUp ]
		public void Init()
		{
			NetcoLogger.LoggerFactory = new ConsoleLoggerFactory();
			const string credentialsFilePath = @"..\..\Files\RestApiThreeDCartCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< RestApiTestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true, IgnoreUnknownColumns = true } ).FirstOrDefault();

			if( testConfig != null )
			{
				this.ThreeDCartFactory = new ThreeDCartFactory( testConfig.PrivateKey );
				this.StoreUrl = testConfig.StoreUrl;
				this.Token = testConfig.Token;
				this.TimeZone = testConfig.TimeZone;
			}
		}

		[ Explicit ]
		[ TestCase( ThreeDCartConfigVersion.V1 ) ]
		[ TestCase( ThreeDCartConfigVersion.V2 ) ]
		public void IsGetNewOrders( ThreeDCartConfigVersion configVersion )
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig( configVersion ) );
			var result = service.IsGetNewOrders();

			result.Should().BeTrue();
		}

		#region Get New Orders
		[ Explicit ]
		[ TestCase( ThreeDCartConfigVersion.V1 ) ]
		[ TestCase( ThreeDCartConfigVersion.V2 ) ]
		public void GetNewOrders( ThreeDCartConfigVersion configVersion )
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig( configVersion ) );
			var result = service.GetNewOrders( new DateTime( 2021, 11, 2 ), new DateTime( 2022, 11, 4 ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Explicit ]
		[ TestCase( ThreeDCartConfigVersion.V1 ) ]
		[ TestCase( ThreeDCartConfigVersion.V2 ) ]
		public void GetNewOrders2( ThreeDCartConfigVersion configVersion )
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig( configVersion ) );
			var result = new List< ThreeDCartOrder >();
			service.GetNewOrders( DateTime.UtcNow.AddDays( -3 ), DateTime.UtcNow, x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Explicit ]
		[ TestCase( ThreeDCartConfigVersion.V1 ) ]
		[ TestCase( ThreeDCartConfigVersion.V2 ) ]
		public async Task GetNewOrdersAsync( ThreeDCartConfigVersion configVersion )
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig( configVersion ) );
			var result = await service.GetNewOrdersAsync( DateTime.UtcNow.AddDays( -1 ), DateTime.UtcNow );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Explicit ]
		[ TestCase( ThreeDCartConfigVersion.V1 ) ]
		[ TestCase( ThreeDCartConfigVersion.V2 ) ]
		public async Task GetNewOrdersAsync2( ThreeDCartConfigVersion configVersion )
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig( configVersion ) );
			var result = new List< ThreeDCartOrder >();
			await service.GetNewOrdersAsync( DateTime.UtcNow.AddHours( -3 ), DateTime.UtcNow, x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}
		#endregion

		#region Get Updated Orders
		[ Explicit ]
		[ TestCase( ThreeDCartConfigVersion.V1 ) ]
		[ TestCase( ThreeDCartConfigVersion.V2 ) ]
		public async Task GetUpdatedOrdersAsync( ThreeDCartConfigVersion configVersion )
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig( configVersion ) );
			var result = await service.GetUpdatedOrdersAsync( DateTime.UtcNow.AddDays( -1 ), DateTime.UtcNow );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}
		#endregion

		#region Get Orders By Number
		[ Explicit ]
		[ TestCase( ThreeDCartConfigVersion.V1 ) ]
		[ TestCase( ThreeDCartConfigVersion.V2 ) ]
		public void GetOrdersByNumber( ThreeDCartConfigVersion configVersion )
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig( configVersion ) );
			var numbers = new List< string > { "AB-1043" };
			var result = service.GetOrdersByNumber( numbers, new DateTime( 2014, 12, 2 ), new DateTime( 2014, 12, 4 ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Explicit ]
		[ TestCase( ThreeDCartConfigVersion.V1 ) ]
		[ TestCase( ThreeDCartConfigVersion.V2 ) ]
		public void GetOrdersByNumber2( ThreeDCartConfigVersion configVersion )
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig( configVersion ) );
			var numbers = new List< string > { "AB-1043", "AB-1042" };
			var result = new List< ThreeDCartOrder >();
			service.GetOrdersByNumber( numbers, DateTime.UtcNow.AddDays( -3 ), DateTime.UtcNow, x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Explicit ]
		[ TestCase( ThreeDCartConfigVersion.V1 ) ]
		[ TestCase( ThreeDCartConfigVersion.V2 ) ]
		public async Task GetOrdersByNumberAsync( ThreeDCartConfigVersion configVersion )
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig( configVersion ) );
			var numbers = new List< string > { "AB-1043", "AB-1042" };
			var result = await service.GetOrdersByNumberAsync( numbers, DateTime.UtcNow.AddDays( -100 ), DateTime.UtcNow );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}

		[ Explicit ]
		[ TestCase( ThreeDCartConfigVersion.V1 ) ]
		[ TestCase( ThreeDCartConfigVersion.V2 ) ]
		public async Task GetOrdersByNumberAsync2( ThreeDCartConfigVersion configVersion )
		{
			var service = this.ThreeDCartFactory.CreateRestOrdersService( this.GetConfig( configVersion ) );
			var numbers = new List< string > { "AB-1043" };
			var result = new List< ThreeDCartOrder >();
			await service.GetOrdersByNumberAsync( numbers, DateTime.UtcNow.AddHours( -9 ), DateTime.UtcNow, x => result.Add( x ) );

			result.Should().NotBeNull();
			result.Count.Should().BeGreaterThan( 0 );
		}
		#endregion
	}
}