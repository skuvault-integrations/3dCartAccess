using System;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ThreeDCartAccess.Extensions;

namespace ThreeDCartAccessTests.Extensions
{
	public class BatchDetailsTInputTests
	{
		private static readonly Randomizer _randomizer = new Randomizer();
		private readonly int validBatchSize = ( int )_randomizer.NextUInt( 1, int.MaxValue );
		private Func< int, Task > validProcessor => i => Task.CompletedTask;

		[ TestCase( -1 ) ]
		[ TestCase( 0 ) ]
		[ TestCase( null ) ]
		public void Constructor_ShouldThrow_WhenBatchSizeIsBelowOne( int batchSize )
		{
			Assert.Throws< ArgumentException >( () => new BatchDetails< int >( batchSize, validProcessor ) );
		}

		[ Test ]
		public void Constructor_ShouldThrow_WhenProcessorIsNull()
		{
			Assert.Throws< ArgumentException >( () => new BatchDetails< int >( validBatchSize, processor : null ) );
		}

		[ Test ]
		public void Constructor_ShouldNotThrow_WhenAllParamsAreValid()
		{
			Assert.DoesNotThrow( () => new BatchDetails< int >( validBatchSize, validProcessor ) );
		}

		[ Test ]
		public void Constructor_ShouldNotThrow_WhenProcessorIsValid_andBatchSizeIsMinimumAllowed()
		{
			Assert.DoesNotThrow( () => new BatchDetails< int >( 1, validProcessor ) );
		}
	}
}