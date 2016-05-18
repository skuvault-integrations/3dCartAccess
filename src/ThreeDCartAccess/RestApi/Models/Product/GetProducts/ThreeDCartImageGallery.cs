namespace ThreeDCartAccess.RestApi.Models.Product.GetProducts
{
	public class ThreeDCartImageGallery
	{
		public int? ImageGalleryID{ get; set; }
		public string ImageGalleryFile{ get; set; }
		public string ImageGalleryCaption{ get; set; }
		public int ImageGallerySorting{ get; set; }
	}
}