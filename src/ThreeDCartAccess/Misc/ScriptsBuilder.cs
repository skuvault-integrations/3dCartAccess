namespace ThreeDCartAccess.Misc
{
	internal static class ScriptsBuilder
	{
		public static string GetInventory( int batchSize, int startNum )
		{
			var endNumber = startNum + batchSize;

			const string script = @"SELECT *
							FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY catalogid ) AS RowNum, 
								 p.catalogid, p.id, p.name, p.stock, p.show_out_stock, o.AO_Code, o.AO_Sufix, o.AO_Name, o.AO_Cost, o.AO_Stock
								 FROM products as p
								 LEFT JOIN options_Advanced AS o on p.catalogid = o.ProductID
								) AS RowConstrainedResult
							WHERE RowNum >= {0}
							    AND RowNum < {1}
							ORDER BY RowNum";
			var result = string.Format( script, startNum, endNumber );
			return result;
		}
	}
}