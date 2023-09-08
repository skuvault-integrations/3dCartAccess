using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeDCartAccess.Extensions;

//TODO In TD-270 the below methods will be moved to Integrations.Core
//TODO TD-271 Call the instances in Integrations.Core and remove these methods and related code (tests)
internal static class ParallelProcessExtensions
{
	/// <summary>
	/// Performs an asynchronous action on each element of enumerable in <paramref name="batchSize"/> of concurrent tasks.
	/// </summary>
	/// <typeparam name="TInput">The type of the input.</typeparam>
	/// <param name="inputEnumerable">The input enumerable.</param>
	/// <param name="batchSize">Size of the batch.</param>
	/// <param name="processor">The processor delegate method.</param>
	/// <returns>Task indicating when all action have been performed.</returns>
	public static async Task DoInBatchesAsync< TInput >( this IEnumerable< TInput > inputEnumerable, int batchSize, Func< TInput, Task > processor )
	{
		var batchDetails = new BatchDetails< TInput >( batchSize, processor );

		await DoInBatchesAsync( inputEnumerable, batchDetails ).ConfigureAwait( false );
	}

	private static async Task DoInBatchesAsync< TInput >( this IEnumerable< TInput > inputEnumerable, BatchDetails< TInput > batchDetails )
	{
		if( inputEnumerable == null )
			return;

		var processingTasks = new List< Task >( batchDetails.BatchSize );

		foreach( var input in inputEnumerable )
		{
			processingTasks.Add( batchDetails.Processor( input ) );

			if( processingTasks.Count == batchDetails.BatchSize ) // batch size reached, wait for completion and process
			{
				await Task.WhenAll( processingTasks ).ConfigureAwait( false );
				processingTasks.Clear();
			}
		}

		await Task.WhenAll( processingTasks ).ConfigureAwait( false );
	}

	/// <summary>
	/// Calls an asynchronous function on each element of enumerable in <paramref name="batchSize"/> of concurrent tasks.
	/// </summary>
	/// <typeparam name="TInput">The type of the input.</typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <param name="inputEnumerable">The input enumerable.</param>
	/// <param name="batchSize">Size of the batch.</param>
	/// <param name="processor">The processor delegate method.</param>
	/// <param name="ignoreNull">if set to <c>true</c> and <paramref name="processor"/> returns <c>null</c> the result is ignored.</param>
	/// <returns>Result of processing.</returns>
	public static async Task< IEnumerable< TResult > > DoInBatchesAsync< TInput, TResult >( this IEnumerable< TInput > inputEnumerable, int batchSize, Func< TInput, Task< TResult > > processor, bool ignoreNull = true )
	{
		var batchDetails = new BatchDetails< TInput, TResult >( batchSize, processor );

		return await DoInBatchesAsync( inputEnumerable, batchDetails, ignoreNull ).ConfigureAwait( false );
	}

	private static async Task< IEnumerable< TResult > > DoInBatchesAsync< TInput, TResult >( IEnumerable< TInput > inputEnumerable, BatchDetails< TInput, TResult > batchDetails, bool ignoreNull )
	{
		if( inputEnumerable == null )
			return Enumerable.Empty< TResult >();

		var result = new List< TResult >( inputEnumerable.Count() );
		var processingTasks = new List< Task< TResult > >( batchDetails.BatchSize );

		foreach( var input in inputEnumerable )
		{
			processingTasks.Add( batchDetails.Processor( input ) );

			if( processingTasks.Count == batchDetails.BatchSize ) // batch size reached, wait for completion and process
			{
				AddResultToList( await Task.WhenAll( processingTasks ).ConfigureAwait( false ), result, ignoreNull );
				processingTasks.Clear();
			}
		}

		AddResultToList( await Task.WhenAll( processingTasks ).ConfigureAwait( false ), result, ignoreNull );
		return result;
	}

	private static void AddResultToList< TResult >( IEnumerable< TResult > intermediateResult, List< TResult > endResult, bool ignoreNull )
	{
		foreach( var value in intermediateResult )
		{
			if( ignoreNull && Equals( value, default(TResult) ) )
				continue;

			endResult.Add( value );
		}
	}
}