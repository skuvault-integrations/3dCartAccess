using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ThreeDCartAccess.Extensions;

namespace ThreeDCartAccessTests.Extensions
{
	public class ParallelProcessExtensionsTests
	{
		[ Test ]
		public async Task DoInBatchesAsyncOverloadWithReturnValue_ShouldReturnCorrectValues()
		{
			var items = new List< int > { 1, 2, 3, 4, 5 };
			const int batchSize = 2;
			const int multiplier = 2;
			Func< int, Task< int > > processor = i => Task.FromResult( i * multiplier );

			var result = await items.DoInBatchesAsync( batchSize, processor ).ConfigureAwait( false );

			Assert.That( result, Is.EqualTo( items.Select( x => x * multiplier ) ) );
		}

		[ Test ]
		public async Task DoInBatchesAsyncOverloadWithoutReturnValue_ShouldCallProcessorWithCorrectValues()
		{
			var inputItems = new List< int > { 1, 2, 3, 4, 5 };
			var observedCalls = new List< int >();
			const int batchSize = 2;
			Func< int, Task > processor = i => 
			{
				observedCalls.Add( i );
				return Task.CompletedTask;
			};

			await inputItems.DoInBatchesAsync( batchSize, processor ).ConfigureAwait( false );

			Assert.That( observedCalls, Is.EqualTo( inputItems ) );
		}
	}
}