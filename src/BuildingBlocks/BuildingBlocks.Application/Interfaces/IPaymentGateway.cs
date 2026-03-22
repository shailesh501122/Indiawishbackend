using Payments.Domain;
namespace BuildingBlocks.Application.Interfaces;
public interface IPaymentGateway
{
    Task<PaymentGatewayResult> ChargeAsync(Payment payment, CancellationToken cancellationToken);
}
public sealed record PaymentGatewayResult(bool IsSuccessful, string ExternalReference, string Message);
