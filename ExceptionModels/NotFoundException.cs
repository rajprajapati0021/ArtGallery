namespace ArtGallery.ExceptionModels;
public class NotFoundException : BadRequestException
{
    public NotFoundException(string action, long id) : base($"{action} with id {id} not found")
    {
    }

    public NotFoundException(string action, string name) : base($"{action} with name {name} not found")
    {
    }
}