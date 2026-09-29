namespace EventService.Api.Requests;

public sealed class DebitRequest
{
    public Guid RequestId { get; set; }

    public long AmountMinorUnits { get; set; }

    public Dictionary<string, string> Validate()
    {
        var errors = new Dictionary<string, string>();

        if (RequestId == Guid.Empty)
            errors.Add("request_id", "request_id is required");

        if (AmountMinorUnits < 0)
            errors.Add("amount_minor_units", "amount_minor_units is required");

        return errors;
    }
}