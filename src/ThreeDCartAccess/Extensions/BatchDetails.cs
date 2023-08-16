using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkuVault.Integrations.Core.Helpers;

namespace ThreeDCartAccess.Extensions;

public class BatchDetails< TInput, TResult >
{
	public readonly int BatchSize;
	public readonly Func< TInput, Task< TResult > > Processor;

	public BatchDetails( int batchSize, Func< TInput, Task< TResult > > processor )
	{
		this.BatchSize = batchSize;
		this.Processor = processor;

		ValidationHelper.ThrowOnValidationErrors< BatchDetails< TInput, TResult > >( GetValidationErrors() );
	}

	private IEnumerable< string > GetValidationErrors()
	{
		var validationErrors = new List< string >();
		if( this.BatchSize < 1 )
		{
			validationErrors.Add( $"{nameof(this.BatchSize)} is less than 1" );
		}

		if( this.Processor == null )
		{
			validationErrors.Add( $"{nameof(this.Processor)} is null" );
		}

		return validationErrors;
	}
}

public class BatchDetails< TInput >
{
	internal readonly int BatchSize;
	internal readonly Func< TInput, Task > Processor;

	public BatchDetails( int batchSize, Func< TInput, Task > processor )
	{
		this.BatchSize = batchSize;
		this.Processor = processor;

		ValidationHelper.ThrowOnValidationErrors< BatchDetails< TInput, Task > >( GetValidationErrors() );
	}

	private IEnumerable< string > GetValidationErrors()
	{
		var validationErrors = new List< string >();
		if( this.BatchSize < 1 )
		{
			validationErrors.Add( $"{nameof(this.BatchSize)} is less than 1" );
		}

		if( this.Processor == null )
		{
			validationErrors.Add( $"{nameof(this.Processor)} is null" );
		}

		return validationErrors;
	}
}