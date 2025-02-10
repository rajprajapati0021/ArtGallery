using ArtGallery.RequestModels;
using ArtGallery.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;


[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

    [Authorize]
    [HttpPost("add-product")]
    public async Task<IActionResult> AddProduct([FromForm] AddUpdateProductRequestModel productRequestModel)
    {
        await productService.AddUpdateProductAsync(productRequestModel);
        return Ok();
    }

    [Authorize]
    [HttpGet("get-product")]
    public async Task<IActionResult> GetProducts(long? productId)
    {
        var products = await productService.GetProductsAsync(productId);
        return Ok(products);
    }

    [Authorize]
    [HttpGet("get-all-product")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await productService.GetAllProductsAsync();
        return Ok(products);
    }

    //[Authorize]
    [HttpPost("delete-product")]
    public async Task<IActionResult> DeleteProduct([FromBody] long productId)
    {
        await productService.DeleteProductAsync(productId);
        return Ok();
    }

    [Authorize]
    [HttpPost("like-unlike-product")]
    public async Task<IActionResult> LikeUnlikeProduct(LikeRequestModel likeRequestModel)
    {
        bool likeFlag = await productService.LikeUnlikeProductAsync(likeRequestModel);
        return Ok(likeFlag);
    }

    [Authorize]
    [HttpPost("add-comment")]
    public async Task<IActionResult> AddComment(CommentRequestModel commentRequestModel)
    {
        var comment = await productService.AddUpdateCommentAsync(commentRequestModel);
        return Ok(comment);
    }

    [Authorize]
    [HttpGet("get-comments")]
    public async Task<IActionResult> GetComments(long productId)
    {
        var comments = await productService.GetAllCommentsAsync(productId);
        return Ok(comments);
    }

    [Authorize]
    [HttpPost("delete-comment")]
    public async Task<IActionResult> DeleteComment(long commentId)
    {
        await productService.DeleteComment(commentId);
        return Ok();
    }

    [Authorize]
    [HttpPost("add-to-cart")]
    public async Task<IActionResult> AddToCart([FromBody]long productId)
    {
        await productService.AddToCartAsync(productId);
        return Ok();
    }

    [Authorize]
    [HttpPost("remove-from-cart")]
    public async Task<IActionResult> RemoveFromCart([FromBody]long cartItemId)
    {
        await productService.RemoveFromCartAsync(cartItemId);
        return Ok();
    }

    [Authorize]
    [HttpGet("get-cart-items")]
    public async Task<IActionResult> GetCartItems()
    {   
        var cartItems = await productService.GetCartItemsAsync();
        return Ok(cartItems);
    }

    [Authorize]
    [HttpPost("order")]
    public async Task<IActionResult> AddOrder(List<long> orderProductIds)
    {
        await productService.AddOrderAsync(orderProductIds);
        return Ok();
        }

    [Authorize]
    [HttpGet("get-orders")]
    public async Task<IActionResult> GetOrders()
        {
        var orders = await productService.GetOrdersAsync();
        return Ok(orders);
    }

    [HttpPost("/pdfToImage")]
    public async Task<IActionResult> PdfToImage(IFormFile pdf)
    {
        if (pdf == null || pdf.Length == 0)
        {
            return BadRequest("Invalid PDF file.");
        }

        try
        {
            // Save the uploaded file to a temporary location
            var tempPdfPath = Path.GetTempFileName();
            await using (var stream = new FileStream(tempPdfPath, FileMode.Create))
            {
                await pdf.CopyToAsync(stream);
            }

            // Extract images from the PDF
            var images = await ExtractImagesFromPdf(tempPdfPath);

            var combinedImage = CombineImages(images);

            var outputStream = new MemoryStream(); // Do not dispose this stream yet
            using (var image = SKImage.FromBitmap(combinedImage))
            using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
            {
                data.SaveTo(outputStream);
            }

            outputStream.Seek(0, SeekOrigin.Begin);

            var base64String = Convert.ToBase64String(outputStream.ToArray());

            System.IO.File.Delete(tempPdfPath);

            // Return the combined image as a PNG file
            return Ok(base64String);
        }
        catch (Exception e) {
            return Ok(e.Message);
        }

    }

    private async Task<List<SKBitmap>> ExtractImagesFromPdf(string pdfPath)
    {
        var images = new List<SKBitmap>();

        using (var document = UglyToad.PdfPig.PdfDocument.Open(pdfPath))
        {
            foreach (var page in document.GetPages())
            {
                // Extract images from the page
                var pageImages = page.GetImages();
                foreach (var image in pageImages)
                {
                    // Convert the extracted PdfPig image to a SKBitmap
                    var skBitmap = ConvertToSKBitmap(image);
                    images.Add(skBitmap);
                }
            }
        }

        return images;
    }

    private SKBitmap ConvertToSKBitmap(UglyToad.PdfPig.Content.IPdfImage image)
    {
        byte[] imageData = image.RawBytes.ToArray();
        using (var stream = new MemoryStream(imageData))
        {
            return SKBitmap.Decode(stream);
        }
    }

    private SKBitmap CombineImages(List<SKBitmap> images)
{
    int totalHeight = 0;
    int maxWidth = images.Max(image => image.Width);

    // Resize all images to have the same width
    var resizedImages = images.Select(image =>
    {
        if (image.Width != maxWidth)
        {
            int newHeight = (int)((double)image.Height / image.Width * maxWidth);
            var resizedBitmap = new SKBitmap(maxWidth, newHeight);
            using (var canvas = new SKCanvas(resizedBitmap))
            {
                canvas.Clear(SKColors.Transparent);
                canvas.DrawBitmap(image, new SKRect(0, 0, maxWidth, newHeight));
            }
            return resizedBitmap;
        }
        return image;
    }).ToList();

    // Calculate the total height after resizing
    totalHeight = resizedImages.Sum(image => image.Height);

    // Create a combined bitmap with uniform width
    var combinedBitmap = new SKBitmap(maxWidth, totalHeight);
    using (var canvas = new SKCanvas(combinedBitmap))
    {
        canvas.Clear(SKColors.White);

        int currentY = 0;
        foreach (var image in resizedImages)
        {
            canvas.DrawBitmap(image, new SKPoint(0, currentY));
            currentY += image.Height;
        }
    }

    return combinedBitmap;
}


}
