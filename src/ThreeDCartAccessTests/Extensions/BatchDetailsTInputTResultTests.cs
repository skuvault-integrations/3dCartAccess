using System;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ThreeDCartAccess.Extensions;

namespace ThreeDCartAccessTests.Extensions
{
	public class BatchDetailsTInputTResultTests
	{
		private static readonly Randomizer _randomizer = new Randomizer();
		private readonly int validBatchSize = ( int )_randomizer.NextUInt( 1, int.MaxValue );
		private Func< int, Task< int > > validProcessor => i => Task.FromResult( 0 );

		[ TestCase( -1 ) ]
		[ TestCase( 0 ) ]
		public void Constructor_ShouldThrow_WhenBatchSizeIsBelowOne( int batchSize )
		{
			Assert.Throws< ArgumentException >( () => new BatchDetails< int, int >( batchSize, validProcessor ) );
		}

		[ Test ]
		public void Constructor_ShouldThrow_WhenProcessorIsNull()
		{
			Assert.Throws< ArgumentException >( () => new BatchDetails< int, int >( validBatchSize, processor : null ) );
		}

		[ Test ]
		public void Constructor_ShouldNotThrow_WhenAllParamsAreValid()
		{
			Assert.DoesNotThrow( () => new BatchDetails< int, int >( validBatchSize, validProcessor ) );
		}

		[ Test ]
		public void Constructor_ShouldNotThrow_WhenProcessorIsValid_andBatchSizeIsMinimumAllowed()
		{
			Assert.DoesNotThrow( () => new BatchDetails< int, int >( 1, validProcessor ) );
		}
	}
}