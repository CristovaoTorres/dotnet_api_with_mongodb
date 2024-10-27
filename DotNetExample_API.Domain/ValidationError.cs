namespace DotNetExample_API.Domain;

public class ValidationError
{
    public int? Step { get; set; }

    public string Field { get; set; }

    public string ErrorMessage { get; set; }

}
