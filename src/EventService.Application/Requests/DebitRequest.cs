namespace EventService.Application.Requests;

public sealed class DebitRequest
{
    public long AmountMinorUnits { get; set; }

    public Dictionary<string, string> Validate()
    {
        var errors = new Dictionary<string, string>();

        switch (AmountMinorUnits)
        {
            case 0:
                errors.Add("amount_minor_units", "amount_minor_units is required");
                break;
            case < 0:
                errors.Add("amount_minor_units", "amount_minor_units cannot be negative");
                break;
        }

        return errors;
    }
}