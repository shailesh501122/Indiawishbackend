using BuildingBlocks.Application.Interfaces;
using Payments.Domain;

namespace BuildingBlocks.Infrastructure.Services;

public sealed class MockPaymentGateway : IPaymentGateway
{
    public Task<PaymentGatewayResult> ChargeAsync(Payment payment, CancellationToken cancellationToken)
        => Task.FromResult(new PaymentGatewayResult(true, $"mock-{payment.Id:N}", "Mock payment captured."));
}
