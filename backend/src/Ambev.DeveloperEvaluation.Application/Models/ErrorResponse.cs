public class ErrorResponse
{
    public string Type { get; set; }
    public string Error { get; set; }
    public string Detail { get; set; }

    public ErrorResponse(string type, string error, string detail)
    {
        Type = type;
        Error = error;
        Detail = detail;
    }
}