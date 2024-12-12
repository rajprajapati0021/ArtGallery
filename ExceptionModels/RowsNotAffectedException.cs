namespace ArtGallery.ExceptionModels;
public class RowsNotAffectedException : BadRequestException
{
    public RowsNotAffectedException(string action, long id = 0) : base(id == 0
        ? $"No Record {action}"
        : $"No Record {action} with id {id}")
    {
    }
}