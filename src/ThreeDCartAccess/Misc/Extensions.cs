using System;

namespace ThreeDCartAccess.Misc
{
	public static class Extensions
	{
		public static T ToEnum< T >( this string value )
		{
			try
			{
				value = value.Replace( " ", "" );
				return ( T )Enum.Parse( typeof( T ), value, true );
			}
			catch( Exception ex )
			{
				ThreeDCartLogger.Log.Error( ex, "Can't parse enum value {0} for type {1}", value, typeof( T ) );
				return ( T )Enum.Parse( typeof( T ), "Undefined", true );
			}
		}

		public static T ToEnum< T >( this string value, T defaultValue )
		{
			if( string.IsNullOrWhiteSpace( value ) )
				return defaultValue;

			return value.ToEnum< T >();
		}
	}
}