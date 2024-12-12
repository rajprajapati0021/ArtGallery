namespace ArtGallery.ExceptionModels;

public class ValidationException : BadRequestException
{
    public ValidationException(params string[] errors) : base(string.Join(" | ", errors))
    {
    }
}