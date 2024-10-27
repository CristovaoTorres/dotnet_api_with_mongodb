using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DotNetExample_API.Domain;

public class BaseResponse<T>
{
    private HttpStatusCode _statusCode;

    private T? _data;

    private readonly List<ValidationError> _errors = new List<ValidationError>();

    public BaseResponse()
    {
    }
    public BaseResponse(T data)
    {
        AddData(data);
    }

    //
    // Summary:
    //     Gets the status code of the response.
    //
    // Value:
    //     A number that represents the outcome of the operation.
    [JsonIgnore]
    public int StatusCode => (int)_statusCode;

    //
    // Summary:
    //     Gets the data of the response.
    //
    // Value:
    //     An object of type T that contains the result of the operation.
    [JsonPropertyName("data")]
    public T? Data => _data;

    //
    // Summary:
    //     Gets the error messages of the response.
    //
    // Value:
    //     A collection of ErrorResponse objects that provide details about the errors that
    //     occurred.
    [JsonPropertyName("errors")]
    public IEnumerable<ValidationError> Errors => _errors;

    //
    // Summary:
    //     Gets a boolean value indicating if any error was added to the response
    [JsonIgnore]
    public bool HasError => _errors.Count > 0;

    //
    // Summary:
    //     Adds data to the response. If not set status code, Post = 201, Put Or Delete
    //     = 204, Head = 200
    //
    // Parameters:
    //   data:
    //     The data object to add.
    public void AddData(T data)
    {
        _data = data;
    }

    //
    // Summary:
    //     Adds data to the response and sets a status code.
    //
    // Parameters:
    //   data:
    //     The data object to add.
    //
    //   statusCode:
    public void AddData(T data, HttpStatusCode statusCode)
    {
        AddData(data);
        SetStatusCode(statusCode);
    }

    //
    // Summary:
    //     Adds an error message to the response.
    //
    // Parameters:
    //   error:
    public void AddError(ValidationError error)
    {
        _errors.Add(error);
        SetStatusCode(HttpStatusCode.BadRequest);
    }

    //
    // Summary:
    //     Adds an error message to the response and sets a status code.
    //
    // Parameters:
    //   error:
    //
    //   statusCode:
    public void AddError(ValidationError error, HttpStatusCode statusCode)
    {
        AddError(error);
        SetStatusCode(statusCode);
    }

    //
    // Summary:
    //     Adds error messages to the response.
    //
    // Parameters:
    //   errors:
    public void AddErrors(IEnumerable<ValidationError> errors)
    {
        _errors.AddRange(errors);
    }

    //
    // Summary:
    //     Adds error messages to the response and sets a status code.
    //
    // Parameters:
    //   errors:
    //
    //   statusCode:
    public void AddErrors(IEnumerable<ValidationError> errors, HttpStatusCode statusCode)
    {
        AddErrors(errors);
        SetStatusCode(statusCode);
    }

    //
    // Summary:
    //     Adds error messages from serialized json to the response.
    //
    // Parameters:
    //   errors:
    public void AddErrors(string errors)
    {
        _errors.AddRange(JsonSerializer.Deserialize<List<ValidationError>>(errors));
    }

    //
    // Summary:
    //     Adds error messages to the response and sets a status code.
    //
    // Parameters:
    //   errors:
    //
    //   statusCode:
    public void AddErrors(string errors, HttpStatusCode statusCode)
    {
        AddErrors(errors);
        SetStatusCode(statusCode);
    }

    //
    // Summary:
    //     Set status code for the response.
    //
    // Parameters:
    //   statusCode:
    public void SetStatusCode(HttpStatusCode statusCode)
    {
        _statusCode = statusCode;
    }
}
public class BaseResponse : BaseResponse<object>
{
    public BaseResponse()
    {

    }
    public BaseResponse(ValidationError errorResponse)
    {
        AddError(errorResponse);
    }
}
