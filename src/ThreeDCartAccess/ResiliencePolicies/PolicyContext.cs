using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace ThreeDCartAccess.ResiliencePolicies;

/// <summary>
/// Provides a standardized way to pass context data to a retry policy.
/// </summary>
/// <see href="https://github.com/App-vNext/Polly#policy-keys-and-context-data"/>
public class PolicyContext: Dictionary< string, object >
{
    public const string LoggerKey = "logger";

    /// <summary>
    /// Creates an instance of <see cref="PolicyContext"/>
    /// </summary>
    /// <param name="logger">An instance of <see cref="ILogger"/> used for logging</param>
    public PolicyContext( ILogger logger )
    {
        this.Add( LoggerKey, logger );
    }
}