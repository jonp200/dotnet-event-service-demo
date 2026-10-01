using EventService.Application.Debits;
using EventService.Application.Requests;

namespace EventService.Api.Endpoints;

public static class AccountEndpoints
{
    public static void MapDebitEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/accounts/{accountId:guid}/debits");

        group.MapPost("/", async (Guid accountId, DebitRequest request, ProcessDebit useCase, CancellationToken ct) =>
        {
            var errors = request.Validate();
            if (errors.Count != 0)
                return Results.UnprocessableEntity(errors);

            var outcome = await useCase.ExecuteAsync(
                new ProcessDebitCommand(
                    accountId,
                    request.AmountMinorUnits),
                ct);

            return Results.Ok(outcome);
        });
    }
}