using Microsoft.JSInterop;
using SexyWebSite.Data;

namespace SexyWebSite.Shared
{
    public class ProductDisplay : IAsyncDisposable
    {
        DotNetStreamReference _ref;
        ImageCreator ImgCreate;


        private ProductDisplay(Product product, ImageCreator creator,int index)
        {
            if (product is null) throw new ArgumentNullException();
            Product = product;
            Stream = new MemoryStream(product.Images[index].ImageData);
            _ref = new DotNetStreamReference(Stream);
            ImgCreate = creator;
        }

        private async ValueTask Init()
        {
            ImageUrl = await ImgCreate.CreateUrlFromStream(_ref);
        }

        public async ValueTask DisposeAsync()
        {
            _ref.Dispose();
            await ImgCreate.DisposeUrl(ImageUrl);
        }

        public Stream Stream { get; }

        public string ImageUrl { get; private set; } = "";

        public Product Product { get; }

        /// <summary>
        /// Create The Product Display
        /// </summary>
        /// <param name="product"></param>
        /// <param name="creator"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async ValueTask<ProductDisplay> CreateDisplayAsync(Product product, ImageCreator creator)
        {
            var disp = new ProductDisplay(product, creator, product.MainImageIndex);
            await disp.Init();
            return disp;
        }


        /// <summary>
        /// Create The Product Display
        /// </summary>
        /// <param name="product"></param>
        /// <param name="creator"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async ValueTask<ProductDisplay> CreateDisplayAsync(Product product, ImageCreator creator,int index)
        {
            var disp = new ProductDisplay(product, creator, index);
            await disp.Init();
            return disp;
        }
    }
}
