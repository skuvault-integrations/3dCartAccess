namespace ThreeDCartAccess.SoapApi.Misc
{
	internal static class ScriptsBuilder
	{
		public static string GetInventory()
		{
			const string script = @"SELECT p.catalogid, p.id, p.name, p.stock, p.show_out_stock, o.AO_Code, o.AO_Sufix, o.AO_Name, o.AO_Cost, o.AO_Stock
								 FROM products as p
								 LEFT JOIN options_Advanced AS o on p.catalogid = o.ProductID";

			return script;
		}

		public static string GetInventory( int batchSize, int startNum )
		{
			var endNumber = startNum + batchSize;

			const string script = @"SELECT *
							FROM ( SELECT ROW_NUMBER() OVER(ORDER BY p.catalogid) AS RowNum, 
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

		public static string UpdateProductOptionInventory( int newQuantity, string optionCode )
		{
			return string.Format( "UPDATE options_Advanced SET AO_Stock = {0} WHERE AO_Sufix = '{1}'", newQuantity, optionCode );
		}

		public static string GetOrderStatuses()
		{
			return "select id, StatusID, StatusDefinition, StatusText, Visible from order_Status";
		}
	}
}